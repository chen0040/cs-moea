using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MOEA.Core.ComponentModels
{
    public interface IGPSolution : ISolution
    {
        void SubtreeCrossover(IGPSolution rhs, int max_depth_for_crossover, string method, object tag = null);
        void Mutate(int max_depth_for_mutation, string method, object tag = null);
        void CreateWithDepth(int max_depth_for_creation, string method, object tag = null);
        int ProgramCount
        {
            get;
        }
        object FindProgramAt(int index);
    }
}
