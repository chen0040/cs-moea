using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MOEA.Core.AlgorithmModels.Survival
{
    using System.Xml;

    public abstract class SurvivalInstruction<P, S>
    {
        public SurvivalInstruction()
        {

        }

        public SurvivalInstruction(XmlElement xml_level1)
        {

        }

        // Xianshun says:
        // this method return the pointer of the program that is to be deleted (loser in the competition for survival)
        public abstract S Compete(P pop, S weak_program_in_current_pop, S child_program, Comparison<S> comparer);
        public abstract SurvivalInstruction<P, S> Clone();
    }
}
