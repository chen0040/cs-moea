using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MOEA.Core.ProblemModels;
using MOEA.AlgorithmModels;
using MOEA.Core.ComponentModels;

namespace MOEA.ComponentModels
{
    public class HAPMOEALayer<S> : MOOSolver
        where S : MOOSolution, new()
    {
        protected IMOOProblem mProblem;
        protected HAPMOEAConfig mConfig;
        protected Dictionary<string, ParetoNode<S>> mParetoNodes = new Dictionary<string, ParetoNode<S>>();
        protected Dictionary<ParetoNode<S>, List<ParetoNode<S>>> mTopDownNodes = new Dictionary<ParetoNode<S>, List<ParetoNode<S>>>();
        protected Dictionary<ParetoNode<S>, List<ParetoNode<S>>> mBottomUpNodes = new Dictionary<ParetoNode<S>, List<ParetoNode<S>>>();

        public void LinkTop2Bottom(ParetoNode<S> top_node, ParetoNode<S> bottom_node)
        {
            if (mTopDownNodes.ContainsKey(top_node))
            {
                mTopDownNodes[top_node].Add(bottom_node);
            }
            if (mBottomUpNodes.ContainsKey(bottom_node))
            {
                mBottomUpNodes[bottom_node].Add(top_node);
            }
        }

        public void Link2BottomLayer(HAPMOEALayer<S> bottom_layer)
        {
            foreach(string node_i in mParetoNodes.Keys)
            {
                ParetoNode<S> current_layer_node = mParetoNodes[node_i];
                foreach(string node_j in bottom_layer.mParetoNodes.Keys)
                {
                    ParetoNode<S> bottom_layer_node = bottom_layer.mParetoNodes[node_j];
                    if (!mTopDownNodes.ContainsKey(current_layer_node))
                    {
                        mTopDownNodes[current_layer_node] = new List<ParetoNode<S>>();
                    }
                    if (!bottom_layer.mBottomUpNodes.ContainsKey(bottom_layer_node))
                    {
                        bottom_layer.mBottomUpNodes[bottom_layer_node] = new List<ParetoNode<S>>();
                    }
                    mTopDownNodes[current_layer_node].Add(bottom_layer_node);
                    bottom_layer.mBottomUpNodes[bottom_layer_node].Add(current_layer_node);
                }
            }
        }

        public HAPMOEALayer(IMOOProblem problem, HAPMOEAConfig config)
        {
            mProblem = problem;
            mConfig = config;
        }

        public ParetoNode<S> CreateNode(string node_id)
        {
            ParetoNode<S> node = new ParetoNode<S>(mProblem);
            node.Population.Config.Copy(mConfig);
            node.NodeId = node_id;
            mParetoNodes[node_id] = node;
            return node;
        }

        public override void Initialize()
        {
            foreach (string node_id in mParetoNodes.Keys)
            {
                mParetoNodes[node_id].Initialize();
            }
        }

        public override bool IsTerminated
        {
            get { return mCurrentGeneration >= mConfig.MaxGenerations; }
        }

        public int NondominatedArchiveSize
        {
            get
            {
                return mParetoNodes.First().Value.NondominatedArchiveSize;
            }
        }

        public NondominatedPopulation<S> NondominatedArchive
        {
            get
            {
                return mParetoNodes.First().Value.NondominatedArchive;
            }
        }

        public override void Evolve()
        {
            foreach (ParetoNode<S> node in mParetoNodes.Values)
            {
                node.Evolve();
            }
        }

        public void RandomShuffle(List<S> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                S value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public void SendRandomIndividualsDown2Layer(int number_individuals_exchanged)
        {
            foreach (ParetoNode<S> node in mTopDownNodes.Keys)
            {
                List<ParetoNode<S>> bottom_nodes = mTopDownNodes[node];

                foreach (ParetoNode<S> bottom_node in bottom_nodes)
                {
                    node.SendRandomIndividuals2(bottom_node, number_individuals_exchanged);
                }
            }
        }

        public void SendBestIndividualsUp2Layer(int number_individuals_exchanged)
        {
            foreach (ParetoNode<S> node in mBottomUpNodes.Keys)
            {
                List<ParetoNode<S>> top_nodes = mBottomUpNodes[node];

                foreach (ParetoNode<S> top_node in top_nodes)
                {
                    node.SendBestIndividuals2(top_node, number_individuals_exchanged);
                }
            }
        }
    }
}
