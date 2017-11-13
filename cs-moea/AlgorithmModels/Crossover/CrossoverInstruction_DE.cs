using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MOEA.ComponentModels.SolutionModels;
using MOEA.Core.ComponentModels;

namespace MOEA.AlgorithmModels.Crossover
{
    using Statistics;
    using ComponentModels;
    using System.Xml;
    using MOEA.Core.ProblemModels;
    using MOEA.Core.AlgorithmModels.Crossover;
    public class CrossoverInstruction_DE<P, S> : CrossoverInstruction<P, S>
        where S : ContinuousVector
        where P : IMOOPop
    {
        protected double mF=0.5;

        public double F
        {
            get { return mF; }
            set { mF = value; }
        }

        public CrossoverInstruction_DE()
        {

        }

        public CrossoverInstruction_DE(XmlElement xml_level1)
            : base(xml_level1)
        {

        }

        public override List<S> Crossover(P pop, params S[] parents)
        {
            List<S> results = new List<S>();

            int dimension_count = pop.Problem.GetDimensionCount();
            IMOOProblem problem = pop.Problem;
            double crossover_rate = pop.CrossoverRate;

            int ccount=parents.Length;
            S child1, parent1, parent2, parent3;
            for (int i = 0; i < ccount; i+=4)
            {
                int child1_index = i;
                int parent1_index = (i + 1) % ccount;
                int parent2_index = (i + 2) % ccount;
                int parent3_index = (i + 3) % ccount;

                child1 = parents[child1_index].Clone() as S;
                parent1 = parents[parent1_index];
                parent2 = parents[parent2_index];
                parent3 = parents[parent3_index];

                int jrand = DistributionModel.NextInt(dimension_count);

                for (int j = 0; j < dimension_count; j++)
                {
                    if ((DistributionModel.GetUniform() <= crossover_rate) || (j == jrand))
                    {
                        double v1 = parent1[j];
                        double v2 = parent2[j];
                        double v3 = parent3[j];

                        double y = v3 + mF * (v1 - v2);

                        if (y < problem.GetLowerBound(j))
                        {
                            y = problem.GetLowerBound(j);
                        }

                        if (y > problem.GetUpperBound(j))
                        {
                            y = problem.GetUpperBound(j);
                        }

                        child1[j]=y;
                    }
                }
                results.Add(child1);
            }

            return results;
        }

        public override CrossoverInstruction<P, S> Clone()
        {
            CrossoverInstruction_DE<P, S> clone = new CrossoverInstruction_DE<P, S>();
            return clone;
        }
    }
}
