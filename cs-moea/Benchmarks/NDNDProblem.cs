using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MOEA.Benchmarks
{
    using MOEA.Core.ProblemModels;
    using MOEA.Core.ComponentModels;
    using MOEA.ComponentModels.SolutionModels;

    public class NDNDProblem: IMOOProblem
    {
        public int GetObjectiveCount()
        {
            return 2;
        }

        public int GetDimensionCount()
        {
            return 2;
        }

        public bool IsFeasible(MOOSolution s)
        {
            return true;
        }

        public bool IsMaximizing()
        {
            return false;
        }

        public double CalcObjective(MOOSolution s, int objective_index)
        {
            ContinuousVector x = (ContinuousVector)s;

            double f1 = 1 - System.Math.Exp((-4) * x[0]) * System.Math.Pow(System.Math.Sin(5 * System.Math.PI * x[0]), 4);
            if (objective_index == 0)
            {
                return f1;
            }
            else
            {
                double f2, g, h;
                if (x[1] > 0 && x[1] < 0.4)
                    g = 4 - 3 * System.Math.Exp(-2500 * (x[1] - 0.2) * (x[1] - 0.2));
                else
                    g = 4 - 3 * System.Math.Exp(-25 * (x[1] - 0.7) * (x[1] - 0.7));
                double a = 4;
                if (f1 < g)
                    h = 1 - System.Math.Pow(f1 / g, a);
                else
                    h = 0;
                f2 = g * h;
                return f2;
            }
        }

        public double GetUpperBound(int dimension_index)
        {
            return 1;
        }

        public double GetLowerBound(int dimension_index)
        {
            return 0;
        }
    }
}
