using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MOEA.Core.ProblemModels;
using MOEA.Core.ComponentModels;
using MOEA.ComponentModels.SolutionModels;

namespace MOEA.Benchmarks
{
    public class TNKProblem : IMOOProblem
    {
        public const double M = 888888.0;

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
            ContinuousVector x=(ContinuousVector)s;
	        double h=0 , f=0;
	        switch(objective_index)
	        {
                case 0:
                    {
                        h = GetTNKConstraints(x[0], x[1]);
                        f = x[0] + M * h;
                        break;
                    }
                case 1:
                    {
                        h = GetTNKConstraints(x[0], x[1]);
                        f = x[1] + M * h;
                        break;
                    }
	        }
	        return f;
        }

        //  use the design parameters to compute the constraint equation to Get the value
        double GetTNKConstraints(double x1,double x2)
        {
	        double c1,c2,h;
	        c1 = -x1*x1-x2*x2+1+0.1*System.Math.Cos(16*System.Math.Atan(x1/x2));
	        c2 = (x1-0.5)*(x1-0.5)+(x2-0.5)*(x2-0.5)-0.5;
	        if(c1>c2)
		        h = (c1>0)?c1:0;
	        else
		        h = (c2>0)?c2:0;
	        return h;
        }

        public int GetObjectiveCount()
        {
            return 2;
        }

        public int GetDimensionCount()
        {
            return 2;
        }

        public double GetLowerBound(int index) 
        {
            return 0;
        }

        public double GetUpperBound(int index)
        {
            return System.Math.PI;
        }
    }
}
