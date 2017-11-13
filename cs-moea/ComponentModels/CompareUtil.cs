using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MOEA.Core.ComponentModels;

namespace MOEA.ComponentModels
{
    public class CompareUtil
    {
        private CompareUtil()
        {

        }

        public static int EpsilonObjectiveCompare<S>(S solution1, S solution2, double[] epsilons, out bool is_same_box)
            where S : MOOSolution
        {
            is_same_box=false;

            bool dominate1 = false;
            bool dominate2 = false;

            for (int i = 0; i < solution1.ObjectiveCount; i++)
            {
                double epsilon = epsilons[i < epsilons.Length-1 ? i : epsilons.Length-1];

                int index1 = (int)System.Math.Floor(solution1.FindObjectiveAt(i) / epsilon);
                int index2 = (int)System.Math.Floor(solution2.FindObjectiveAt(i) / epsilon);

                if (index1 < index2)
                {
                    dominate1 = true;

                    if (dominate2)
                    {
                        return 0;
                    }
                }
                else if (index1 > index2)
                {
                    dominate2 = true;

                    if (dominate1)
                    {
                        return 0;
                    }
                }
            }

            if (!dominate1 && !dominate2)
            {
                is_same_box=true;

                double dist1 = 0.0;
                double dist2 = 0.0;

                for (int i = 0; i < solution1.ObjectiveCount; i++)
                {
                    double epsilon = epsilons[i < epsilons.Length - 1 ? i : epsilons.Length - 1];

                    int index1 = (int)System.Math.Floor(solution1.FindObjectiveAt(i)
                            / epsilon);
                    int index2 = (int)System.Math.Floor(solution2.FindObjectiveAt(i)
                            / epsilon);

                    dist1 += System.Math.Pow(solution1.FindObjectiveAt(i) - index1 * epsilon,
                            2.0);
                    dist2 += System.Math.Pow(solution2.FindObjectiveAt(i) - index2 * epsilon,
                            2.0);
                }

                dist1 = System.Math.Sqrt(dist1);
                dist2 = System.Math.Sqrt(dist2);

                if (dist1 < dist2)
                {
                    return -1;
                }
                else
                {
                    return 1;
                }
            }
            else if (dominate1)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }

        public static int ConstraintCompare<S>(S solution1, S solution2)
            where S : MOOSolution
        {
            double constraints1 = 0;
            for (int i = 0; i < solution1.ConstraintCount; i++)
            {
                constraints1 += System.Math.Abs(solution1.FindConstraintAt(i));
            }

            double constraints2 = 0;
            for (int i = 0; i < solution2.ConstraintCount; i++)
            {
                constraints2 += System.Math.Abs(solution2.FindConstraintAt(i));
            }

            if ((constraints1 != 0.0) || (constraints2 != 0.0))
            {
                if (constraints1 == 0.0)
                {
                    return -1;
                }
                else if (constraints2 == 0.0)
                {
                    return 1;
                }
                else
                {
                    return constraints1.CompareTo(constraints2);
                }
            }
            else
            {
                return 0;
            }
        }

        public static int ParetoConstraintCompare<S>(S solution1, S solution2)
            where S : MOOSolution
        {
            bool dominate1 = false;
            bool dominate2 = false;

            for (int i = 0; i < solution1.ConstraintCount; i++)
            {
                if (System.Math.Abs(solution1.FindConstraintAt(i)) < System.Math.Abs(solution2.FindConstraintAt(i)))
                {
                    dominate1 = true;
                    if (dominate2)
                    {
                        return 0;
                    }
                }
                else if (System.Math.Abs(solution1.FindConstraintAt(i)) > System.Math.Abs(solution2.FindConstraintAt(i)))
                {
                    dominate2 = true;
                    if (dominate1)
                    {
                        return 0;
                    }
                }
            }

            if (dominate1 == dominate2)
            {
                return 0;
            }
            else if (dominate1)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }

        public static int ParetoObjectiveCompare<S>(S solution1, S solution2)
            where S : MOOSolution
        {
            bool dominate1 = false;
            bool dominate2 = false;

            for (int i = 0; i < solution1.ObjectiveCount; i++)
            {
                if (solution1.FindObjectiveAt(i) < solution2.FindObjectiveAt(i))
                {
                    dominate1 = true;

                    if (dominate2)
                    {
                        return 0;
                    }
                }
                else if (solution1.FindObjectiveAt(i) > solution2.FindObjectiveAt(i))
                {
                    dominate2 = true;

                    if (dominate1)
                    {
                        return 0;
                    }
                }
            }

            if (dominate1 == dominate2)
            {
                return 0;
            }
            else if (dominate1)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }



        public static int CrowdingDistanceCompare<S>(S solution1, S solution2)
            where S : MOOSolution
        {
            double crowding1 = solution1.CrowdingDistance;
            double crowding2 = solution2.CrowdingDistance;

            if (crowding1 > crowding2)
            {
                return -1;
            }
            else if (crowding1 < crowding2)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public static int RankCompare<S>(S solution1, S solution2)
            where S : MOOSolution
        {
            int rank1 = solution1.Rank;
            int rank2 = solution2.Rank;

            if (rank1 < rank2)
            {
                return -1;
            }
            else if (rank1 > rank2)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

    }
}
