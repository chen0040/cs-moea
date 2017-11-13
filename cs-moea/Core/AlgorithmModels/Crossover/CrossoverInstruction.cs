using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MOEA.Core.AlgorithmModels.Crossover
{
    using System.Xml;
    public abstract class CrossoverInstruction<P, S>
    {
        public CrossoverInstruction()
        {

        }

        public CrossoverInstruction(XmlElement xml_level1)
        {

        }

        public abstract List<S> Crossover(P pop, params S[] parents);
        public abstract CrossoverInstruction<P, S> Clone();
    }
}
