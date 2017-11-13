using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MOEA.Core.ProblemModels
{
    public interface IGPFitnessCase
    {
        int GetInputCount();

        bool QueryInput(string input_name, out object input);

        void StoreOutput(object output, int program_index);
    }
}
