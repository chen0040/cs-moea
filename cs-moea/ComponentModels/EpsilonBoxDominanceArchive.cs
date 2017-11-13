using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MOEA.Core.ComponentModels;

namespace MOEA.ComponentModels
{
    public class EpsilonBoxDominanceArchive<S> : NondominatedPopulation<S>
        where S : MOOSolution, new()
    {
        private double[] mEpsilons;
        private int mNumberOfImprovements = 0;
        private int mNumberOfDominatingImprovements = 0;

        public EpsilonBoxDominanceArchive(double epsilon)
        {
            mEpsilons = new double[] { epsilon };
        }

        public int NumberOfImprovements
        {
            get
            {
                return mNumberOfImprovements;
            }
        }

        public int NumberOfDominatingImprovements
        {
            get
            {
                return mNumberOfDominatingImprovements;
            }
        }

        private int Compare(S s1, S s2, out bool is_same_box)
        {
            is_same_box = false;
            int flag = CompareUtil.ConstraintCompare(s1, s2);
            if (flag == 0)
            {
                flag = CompareUtil.EpsilonObjectiveCompare(s1, s2, mEpsilons, out is_same_box);
            }
            return flag;
        }

        public override bool Add(S solution_to_add)
        {
            List<S> solutions_to_remove = new List<S>();

            bool same = false;
            bool dominates = false;

            bool should_add = true;
            foreach (S s in mIndividuals)
            {
                bool is_same_box;
                int flag = Compare(solution_to_add, s, out is_same_box);

                if (flag < 0)
                {
                    if (is_same_box)
                    {
                        same = true;
                    }
                    else
                    {
                        dominates = true;
                    }

                    solutions_to_remove.Add(s);
                }
                else if (flag > 0)
                {
                    should_add = false;
                    break;
                }
            }

            foreach (S s in solutions_to_remove)
            {
                mIndividuals.Remove(s);
            }

            if (!same)
            {
                mNumberOfImprovements++;

                if (dominates)
                {
                    mNumberOfDominatingImprovements++;
                }
            }

            if (should_add)
            {
                mIndividuals.Add(solution_to_add.Clone() as S);
            }

            return should_add;
        }
    }
}
