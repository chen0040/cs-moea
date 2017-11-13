using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MOEA.Core.ComponentModels;

namespace MOEA.ComponentModels
{
    public class SortUtil
    {
        private SortUtil()
        {

        }

        public static void FastNondominatedSort<S>(Population<S> population)
            where S : MOOSolution, new()
        {
            List<S> remaining = new List<S>();

            foreach (S solution in population.Solutions)
            {
                remaining.Add(solution);
            }

            int rank = 0;

            while (remaining.Count > 0)
            {
                NondominatedPopulation<S> front = new NondominatedPopulation<S>();

                foreach (S solution in remaining)
                {
                    front.Add(solution);
                }

                foreach (S solution in front.Solutions)
                {
                    remaining.Remove(solution);
                    solution.Rank = rank;
                }

                UpdateCrowdingDistance(front);

                rank++;
            }
        }

        public static void UpdateCrowdingDistance<S>(Population<S> front)
            where S : MOOSolution, new()
        {
            int n = front.Count;

            if (n < 3)
            {
                foreach (S solution in front.Solutions)
                {
                    solution.CrowdingDistance = Double.PositiveInfinity;
                }
            }
            else
            {
                int numberOfObjectives = front[0].ObjectiveCount;

                foreach (S solution in front.Solutions)
                {
                    solution.CrowdingDistance = 0.0;
                }

                for (int i = 0; i < numberOfObjectives; i++)
                {
                    // Xianshun:
                    // Sort the pareto front to have solution with minimum i-th objective at front[0]
                    // and solution with maximum i-th objective at front[n-1]
                    front.Sort(
                        delegate(S s1, S s2)
                        {
                            return s1.FindObjectiveAt(i).CompareTo(s2.FindObjectiveAt(i));
                        });

                    double minObjective = front[0].FindObjectiveAt(i);
                    double maxObjective = front[n - 1].FindObjectiveAt(i);

                    front[0].CrowdingDistance = Double.PositiveInfinity;
                    front[n - 1].CrowdingDistance = Double.PositiveInfinity;

                    for (int j = 1; j < n - 1; j++)
                    {
                        front[j].CrowdingDistance += (front[j + 1].FindObjectiveAt(i) - front[j - 1].FindObjectiveAt(i)) / (maxObjective - minObjective);
                    }
                }
            }
        }
    }
}
