using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MOEA.ComponentModels.SolutionModels;
using MOEA.Core.ComponentModels;

namespace MOEA.AlgorithmModels.Crossover
{
    using System.Xml;
    using MOEA.ComponentModels;
    using MOEA.Core.AlgorithmModels.Crossover;

    public class MOOCrossoverInstructionFactory<P, S> : CrossoverInstructionFactory<P, S>
        where S : MOOSolution
        where P : IMOOPop
    {
        public MOOCrossoverInstructionFactory(string filename)
            : base(filename)
        {
            
        }

        public MOOCrossoverInstructionFactory()
        {
            
        }

        protected override CrossoverInstruction<P, S> CreateDefaultInstruction()
        {
            return new CrossoverInstruction_OnePoint<P, S>();
        }

        protected override CrossoverInstruction<P, S> CreateInstructionFromXml(string strategy_name, XmlElement xml)
        {
            if (strategy_name == "one_point")
            {
                return new CrossoverInstruction_OnePoint<P, S>(xml);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public override CrossoverInstructionFactory<P, S> Clone()
        {
            MOOCrossoverInstructionFactory<P, S> clone = new MOOCrossoverInstructionFactory<P, S>(mFilename);
            return clone;
        }


    }
}
