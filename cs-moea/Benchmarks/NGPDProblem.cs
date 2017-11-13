using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MOEA.Core.ProblemModels;
using MOEA.Core.ComponentModels;
using MOEA.ComponentModels.SolutionModels;

namespace MOEA.Benchmarks
{
    public class NGPDProblem : IMOOProblem
    {
        private const double M = 888888.0;

        public bool IsMaximizing()
        {
            return false;
        }

        public bool IsFeasible(MOOSolution s)
        {
            return true;
        }

        public double CalcObjective(MOOSolution s, int objective_index)
        {
            ContinuousVector x = (ContinuousVector)s;
            
            if(objective_index==0)
            {
                double h = GetNGPDConstraints(x[0], x[1], x[2], x[3]);
                double f = 1.10471 * x[0] * x[0] * x[2] + 0.04811 * x[3] * x[1] * (14.0 + x[2]) - 5 - M * h;
                return f;
            }
            else if(objective_index==1)
            {
                double h = GetNGPDConstraints(x[0], x[1], x[2], x[3]);
                double f = 2.1952 / (x[3] * x[3] * x[3] * x[1]) - 0.0001 - M * h;
                return f;
            }
            else
            {
                throw new ArgumentException("objective_index cannot be greater than 1");
            }
        }

        public double GetNGPDConstraints(double h, double b, double l, double t)
        {
            double c1, c2, c3, c4, r0, r1, r2, temp;
            r1 = 6000 / (System.Math.Sqrt(2.0) * h * l);
            r2 = (1500 * (14 + 0.5 * l) *System.Math.Sqrt(l * l + (h + t) * (h + t))) / (0.707 * h * l * ((l * l) / 12.0 + 0.25 * (h + t) * (h + t)));
            r0 =System.Math.Sqrt(r1 * r1 + r2 * r2 + (2 * l * r1 * r2) / (System.Math.Sqrt(l * l + (h + t) * (h + t))));
            c1 = 13600 - r0;
            c2 = 30000 - 504000 / (t * t * b);
            c3 = b - h;
            c4 = 64746.022 * (1 - 0.0282346 * t) * t * b * b * b - 6000;
            if (c1 < c2)
            {
                if (c3 < c4)
                    temp = (c3 < c1) ? c3 : c1;
                else
                    temp = (c4 < c1) ? c4 : c1;
            }
            else
            {
                if (c3 < c4)
                    temp = (c3 < c2) ? c3 : c2;
                else
                    temp = (c4 < c2) ? c4 : c2;
            }
            return (temp < 0) ? temp : 0;
        }

        public int GetObjectiveCount()
        {
            return 2;
        }

        public int GetDimensionCount()
        {
            return 4;
        }

        public double GetLowerBound(int index) 
        {
	        if(index<2)
		        return 0.125;
	        else
		        return 0.1;
        }

        public double GetUpperBound(int index)
        {
	        if(index<2)
		        return 5.0;
	        else
		        return 10.0;
        }
    }
}
