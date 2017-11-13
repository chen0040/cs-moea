using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MOEA.Core.AlgorithmModels.PopInit
{
    using System.Xml;

    public abstract class PopInitInstruction<P, S>
    {
        public static int CREATION_ATTEMPTS = 50;

        public PopInitInstruction()
        {

        }

        public PopInitInstruction(XmlElement xml_level1)
        {

        }

        public abstract void Initialize(P pop);
        public abstract PopInitInstruction<P, S> Clone();
    }
}
