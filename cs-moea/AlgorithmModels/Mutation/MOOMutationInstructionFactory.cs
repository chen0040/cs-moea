using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MOEA.Core.ComponentModels;

namespace MOEA.AlgorithmModels.Mutation
{
    using System.Xml;
    using MOEA.ComponentModels;
    using MOEA.Core.AlgorithmModels.Mutation;

    public class MOOMutationInstructionFactory<P, S> : MutationInstructionFactory<P, S>
        where S : MOOSolution
        where P : IMOOPop
    {
        public MOOMutationInstructionFactory(string filename)
            :base(filename)
        {
            
        }

        public MOOMutationInstructionFactory()
        {
            
        }

        protected override MutationInstruction<P, S> LoadDefaultInstruction()
        {
            return new MutationInstruction_UniformRandom<P, S>();
        }

        protected override MutationInstruction<P, S> LoadInstructionFromXml(string selected_strategy, XmlElement xml)
        {
            if (selected_strategy == "uniform_random")
            {
                return new MutationInstruction_UniformRandom<P, S>(xml);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public override MutationInstructionFactory<P, S> Clone()
        {
            MOOMutationInstructionFactory<P, S> clone = new MOOMutationInstructionFactory<P, S>(mFilename);
            return clone;
        }
    }
}
