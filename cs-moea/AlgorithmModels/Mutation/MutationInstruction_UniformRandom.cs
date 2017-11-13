using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MOEA.Core.ComponentModels;

namespace MOEA.AlgorithmModels.Mutation
{
    using System.Xml;
    using MOEA.ComponentModels;
    using Statistics;
    using MOEA.Core.AlgorithmModels.Mutation;

    public class MutationInstruction_UniformRandom<P, S> : MutationInstruction<P, S>
        where S : MOOSolution
        where P : IMOOPop
    {
        public MutationInstruction_UniformRandom()
        {
            
        }

        public MutationInstruction_UniformRandom(XmlElement xml_level1)
            : base(xml_level1)
        {
            
        }

        public override void Mutate(P pop, S child)
        {
            double mutation_rate=pop.MacroMutationRate;
            child.MutateUniformly(mutation_rate);
        }

        public override MutationInstruction<P, S> Clone()
        {
            MutationInstruction_UniformRandom<P, S> clone = new MutationInstruction_UniformRandom<P, S>();
            return clone;
        }
    }
}
