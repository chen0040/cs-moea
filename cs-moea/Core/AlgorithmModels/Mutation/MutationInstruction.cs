using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MOEA.Core.AlgorithmModels.Mutation
{
    using System.Xml;

    public abstract class MutationInstruction<P, S>
    {
        public MutationInstruction()
        {

        }

        public MutationInstruction(XmlElement xml_level1)
        {

        }

        public abstract void Mutate(P lgpPop, S child);

        public abstract MutationInstruction<P, S> Clone();
    }
}
