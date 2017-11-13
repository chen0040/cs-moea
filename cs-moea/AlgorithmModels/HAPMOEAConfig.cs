using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MOEA.AlgorithmModels
{
    public class HAPMOEAConfig : MOOConfig
    {
        public HAPMOEAConfig()
        {
            PopulationSize = 50;
            MaxGenerations = 200;
        }
    }
}
