using MOEA.Core.ProblemModels;
using MOEA.Core.ComponentModels;
using MOEA.ComponentModels.SolutionModels;

namespace MOEA.Benchmarks
{
    public class OKA2Problem : IMOOProblem
    {
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
	        double f=0;
	        switch(objective_index)
	        {
                case 0:
                    {
                        f = x[0];
                        break;
                    }

                case 1:
                    {
                        f = 1 - System.Math.Pow((x[0] + System.Math.PI), 2) / (4 * System.Math.Pow(System.Math.PI, 2)) + System.Math.Pow(System.Math.Abs(x[1] - 5 * System.Math.Cos(x[0])), 1.0 / 3.0) + System.Math.Pow(System.Math.Abs(x[2] - 5 * System.Math.Sin(x[0])), 1.0 / 3.0);
                        break;
                    }
	        }
	        return f;
        }

        public int GetObjectiveCount()
        {
            return 2;
        }

        public int GetDimensionCount()
        {
            return 3;
        }

        public double GetLowerBound(int index) 
        {
	        if(index == 0)
                return -System.Math.PI;
	        else
		        return -5;
        }

        public double GetUpperBound(int index)
        {
	        if(index == 0)
		        return System.Math.PI;
	        else
		        return 5;
        }
    }
}
