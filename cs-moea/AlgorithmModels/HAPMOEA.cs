using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MOEA.ComponentModels.SolutionModels;
using MOEA.Core.ComponentModels;

namespace MOEA.AlgorithmModels
{
    using ComponentModels;
    using MOEA.Core.ProblemModels;

    public class HAPMOEA<S> : MOOSolver
        where S : MOOSolution, new()
    {
        private HAPMOEAConfig mConfig = new HAPMOEAConfig();
        private IMOOProblem mProblem1;
        private IMOOProblem mProblem2;
        private IMOOProblem mProblem3;
        private HAPMOEALayer<S> mLayer1;
        private HAPMOEALayer<S> mLayer2;
        private HAPMOEALayer<S> mLayer3;

        public HAPMOEAConfig Config
        {
            get { return mConfig; }
        }

        private bool mIsInitialized = false;

        public HAPMOEA(IMOOProblem problem)
        {
            mProblem1 = problem;
            mProblem2 = problem;
            mProblem3 = problem;
        }

        public HAPMOEA(IMOOProblem problem1, IMOOProblem problem2, IMOOProblem problem3)
        {
            mProblem1 = problem1;
            mProblem2 = problem2;
            mProblem3 = problem3;
        }

        public override bool IsTerminated
        {
            get { return mCurrentGeneration >= mConfig.MaxGenerations; }
        }

        public int NondominatedArchiveSize
        {
            get { return mLayer1.NondominatedArchiveSize; }
        }

        public NondominatedPopulation<S> NondominatedArchive
        {
            get { return mLayer1.NondominatedArchive; }
        }

       

        public override void Initialize()
        {
            if(mIsInitialized) return;
            mIsInitialized=true;

            mLayer1 = new HAPMOEALayer<S>(mProblem1, mConfig);
            mLayer2 = new HAPMOEALayer<S>(mProblem2, mConfig);
            mLayer3 = new HAPMOEALayer<S>(mProblem3, mConfig);

	        // initialize layer 1
            ParetoNode<S> node0 = mLayer1.CreateNode("Node 0");

	        // initialize layer 2
            ParetoNode<S> node1 = mLayer2.CreateNode("Node 1");
            ParetoNode<S> node2 = mLayer2.CreateNode("Node 2");

	        // initialize layer 3
            ParetoNode<S> node3 = mLayer3.CreateNode("Node 3");
            ParetoNode<S> node4 = mLayer3.CreateNode("Node 4");
            ParetoNode<S> node5 = mLayer3.CreateNode("Node 5");
            ParetoNode<S> node6 = mLayer3.CreateNode("Node 6");

            mLayer1.Link2BottomLayer(mLayer2);
            mLayer2.Link2BottomLayer(mLayer3);

            mLayer1.Initialize();
            mLayer2.Initialize();
            mLayer3.Initialize();

            
        }

        public override void Evolve()
        {
            mLayer1.Evolve();
            mLayer2.Evolve();
            mLayer3.Evolve();

            int number_individuals_exchanged = (int)(mConfig.PopulationSize * 0.333);

            mLayer3.SendBestIndividualsUp2Layer(number_individuals_exchanged);
            mLayer2.SendBestIndividualsUp2Layer(number_individuals_exchanged);

            mLayer1.SendRandomIndividualsDown2Layer(number_individuals_exchanged);
            mLayer2.SendRandomIndividualsDown2Layer(number_individuals_exchanged);

            mCurrentGeneration++;
        }
    }
}
