using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MOEA.Core.ComponentModels
{
    using ProblemModels;
    public interface IGPPop : IPop
    {
        IGPEnvironment Environment
        {
            get;
        }



        object CreateProgram();


        int MaximumDepthForCrossover
        {
            get;
            set;
        }

        int MaximumDepthForMutation
        {
            get;
            set;
        }

        int MaximumDepthForCreation
        {
            get;
            set;
        }

        double EvaluateObjective(ISolution solution, int objective_index);

    }
}
