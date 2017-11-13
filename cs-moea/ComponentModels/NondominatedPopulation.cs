using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MOEA.Core.ComponentModels;

namespace MOEA.ComponentModels
{
    public class NondominatedPopulation<S> : Population<S>
        where S : MOOSolution, new()
    {
        public static double Epsilon = 1e-10;

        public NondominatedPopulation()
        {

        }

        protected int Compare(S s1, S s2)
        {
            int flag = 0;
            flag = CompareUtil.ConstraintCompare(s1, s2);

            if (flag == 0)
            {
                flag = CompareUtil.ParetoObjectiveCompare(s1, s2);
            }
            return flag;
        }



        public override bool Add(S solution_to_add)
        {
            List<S> solutions_to_remove = new List<S>();

            bool should_add = true;
            foreach (S solution in mIndividuals)
            {
                int flag = Compare(solution_to_add, solution);

                if (flag < 0)
                {
                    solutions_to_remove.Add(solution);
                }
                else if (flag > 0)
                {
                    should_add = false;
                    break;
                }
                else if (GetDistance(solution_to_add, solution) < Epsilon)
                {
                    should_add = false;
                    break;
                }
            }

            foreach (S solution in solutions_to_remove)
            {
                mIndividuals.Remove(solution);
            }

            if (should_add)
            {
                return base.Add(solution_to_add);
            }
            return should_add;
        }

        protected double GetDistance(S s1, S s2)
        {
            double distance = 0.0;

            for (int i = 0; i < s1.ObjectiveCount; i++)
            {
                distance += System.Math.Pow(s1.FindObjectiveAt(i) - s2.FindObjectiveAt(i), 2.0);
            }

            return System.Math.Sqrt(distance);
        }

        public void Truncate(int size)
        {
            Truncate(size, delegate(S s1, S s2)
            {
                return Compare(s1, s2);
            });
        }
    }
}
