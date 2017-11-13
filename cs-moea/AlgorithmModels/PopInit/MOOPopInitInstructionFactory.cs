using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MOEA.Core.ComponentModels;

namespace MOEA.AlgorithmModels.PopInit
{
    using System.Xml;
    using MOEA.ComponentModels;
    using MOEA.Core.AlgorithmModels.PopInit;

    public class MOOPopInitInstructionFactory<P, S> : PopInitInstructionFactory<P, S>
        where S : MOOSolution
        where P : IMOOPop
    {
        public MOOPopInitInstructionFactory(string filename)
            : base(filename)
        {
     
        }

        public MOOPopInitInstructionFactory()
        {

        }

        protected override PopInitInstruction<P, S> LoadDefaultInstruction()
        {
            return new PopInitInstruction_Random<P, S>();
        }

        protected override PopInitInstruction<P, S> LoadInstructionFromXml(string selected_strategy, XmlElement xml_level1)
        {
            if (selected_strategy == "random")
            {
                return new PopInitInstruction_Random<P, S>(xml_level1);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public override PopInitInstructionFactory<P, S> Clone()
        {
            MOOPopInitInstructionFactory<P, S> clone = new MOOPopInitInstructionFactory<P, S>(mFilename);
            return clone;
        }
    }
}
