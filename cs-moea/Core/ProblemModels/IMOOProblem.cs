using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MOEA.Core.ProblemModels
{
    using ComponentModels;
    public interface IMOOProblem
    {
        int GetObjectiveCount();
        double CalcObjective(MOOSolution s, int objective_index);
        double GetUpperBound(int dimension_index);
        double GetLowerBound(int dimension_index);
        int GetDimensionCount();
        bool IsFeasible(MOOSolution s);
        bool IsMaximizing();
    }
}
