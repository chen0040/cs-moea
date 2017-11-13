using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MOEA.Core.ComponentModels
{
    public interface IPop
    {
        double CrossoverRate
        {
            get;
            set;
        }

        /// <summary>
        /// The number of solution in the current generation
        /// </summary>
        int SolutionCount
        {
            get;
        }

        /// <summary>
        /// The population size specified in the configuration file or by user
        /// </summary>
        int PopulationSize
        {
            get;
            set;
        }

        double MacroMutationRate
        {
            get;
            set;
        }

        ISolution FindSolutionByIndex(int index);

        void Replace(ISolution old_solution, ISolution new_solution);



        List<ISolution> ToList();



        ISolution CreateSolution();

        void AddSolution(ISolution solution);






        bool IsMaximization
        {
            get;
        }
    }
}
