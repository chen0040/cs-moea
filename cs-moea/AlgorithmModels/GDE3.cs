using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MOEA.ComponentModels;
using MOEA.AlgorithmModels.Crossover;
using MOEA.Core.ProblemModels;
using Statistics;
using MOEA.Core.AlgorithmModels.Crossover;
using MOEA.Core.ComponentModels;
using MOEA.ComponentModels.SolutionModels;

namespace MOEA.AlgorithmModels
{
    public class GDE3<S> : GDE3<NondominatedSortingPopulation<S>, S>
         where S : ContinuousVector, new()
    {
        public GDE3(IMOOProblem problem)
            : base(problem)
        {

        }
    }

    public class GDE3<P, S> : NSGAII<P, S>
        where S : ContinuousVector, new()
        where P : NondominatedSortingPopulation<S>, new()
    {
        protected double mF = 0.5;
        public double F
        {
            get { return mF; }
            set { mF = value; }
        }

        public GDE3(IMOOProblem problem)
            : base(problem)
        {
            mPopulation.MacroMutationRate = 0.0001;
        }

        protected override CrossoverInstructionFactory<P, S> CreateCrossoverInstructionFactory()
        {
            CrossoverInstructionFactory<P, S> factory = base.CreateCrossoverInstructionFactory();
            CrossoverInstruction_DE<P, S> crossover = new CrossoverInstruction_DE<P, S>();
            crossover.F=F;
            factory.CurrentInstruction = crossover;
            return factory;
        }


        public override void Evolve()
        {
            int populationSize = mPopulation.PopulationSize;

            int index = 0;
            S[] parents = new S[4];
            Population<S> children = new Population<S>();
            for (int i = 0; i < populationSize; ++i )
            {
                
                parents[0] = mPopulation[i];

                HashSet<int> indices = new HashSet<int>();
                indices.Add(i);
                for (int j = 1; j < 4; ++j)
                {
                    do 
                    {
                        index=DistributionModel.NextInt(populationSize);
                    } while (indices.Contains(index));
                    indices.Add(index);
                    parents[j] = mPopulation[index];
                }

                List<S> result = mCrossoverInstructionFactory.Crossover(mPopulation, parents);

                if (result.Count > 1)
                {
                    throw new Exception();
                }

                foreach (S child in result)
                {
                    mMutationInstructionFactory.Mutate(mPopulation, child);
                    children.Add(child);
                }
            }

            Evaluate(children);

            Merge2(children);

            mCurrentGeneration++;
        }

        

    }
}
