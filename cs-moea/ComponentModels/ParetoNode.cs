using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MOEA.AlgorithmModels;
using MOEA.Core.ProblemModels;
using MOEA.Core.ComponentModels;

namespace MOEA.ComponentModels
{
    public class ParetoNode<S> : NSGAII<S>
        where S : MOOSolution, new()
    {
        protected string mNodeId;
        public string NodeId
        {
            get { return mNodeId; }
            set { mNodeId = value; }
        }

        public ParetoNode(IMOOProblem problem)
            : base(problem)
        {

        }

        public void EvaluateAndAdd(S solution)
        {
            solution.Population = mPopulation;
            solution.Problem = mProblem;
            solution.Evaluate();

            mPopulation.Add(solution);
            //mPopulation.Truncate(Config.PopulationSize);
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

        public void SendRandomIndividuals2(ParetoNode<S> bottom_node, int number_individuals_exchanged)
        {
            List<S> solutions = mPopulation.Solutions.ToList();
            RandomShuffle(solutions);

            for (int i = 0; i < number_individuals_exchanged; ++i)
            {
                S solution=solutions[i].Clone() as S;
                bottom_node.EvaluateAndAdd(solution);
            }
        }

        public void SendBestIndividuals2(ParetoNode<S> top_node, int number_individuals_exchanged)
        {
            
            for (int i = 0; i < number_individuals_exchanged; ++i)
            {
                S solution = Population[i].Clone() as S;
                top_node.EvaluateAndAdd(solution);
            }
        }
    }
}
