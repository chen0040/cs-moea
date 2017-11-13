using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MOEA.Core.AlgorithmModels.Survival
{
    using System.Xml;
    using MOEA.Core.ComponentModels;
    using MOEA.Core.AlgorithmModels.Survival;
    using Statistics;

    class SurvivalInstruction_Probablistic<P, S> : SurvivalInstruction<P, S>
        where S : ISolution
        where P : IPop
    {
        private double m_reproduction_probability = 1;

        public SurvivalInstruction_Probablistic()
        {

        }

        public SurvivalInstruction_Probablistic(XmlElement xml_level1)
            : base(xml_level1)
        {
            foreach (XmlElement xml_level2 in xml_level1.ChildNodes)
            {
                if (xml_level2.Name == "param")
                {
                    string attrname = xml_level2.Attributes["name"].Value;
                    string attrvalue = xml_level2.Attributes["value"].Value;
                    if (attrname == "reproduction_probability")
                    {
                        double value = 0;
                        double.TryParse(attrvalue, out value);
                        m_reproduction_probability = value;
                    }
                }
            }
        }

        public override S Compete(P pop, S weak_program_in_current_pop, S child_program, Comparison<S> comparer)
        {
            double r = DistributionModel.GetUniform();

            if (r < m_reproduction_probability)
            {
                pop.Replace(weak_program_in_current_pop, child_program);
                return weak_program_in_current_pop;
            }

            return child_program;
        }

        public override SurvivalInstruction<P, S> Clone()
        {
            SurvivalInstruction_Probablistic<P, S> clone = new SurvivalInstruction_Probablistic<P, S>();
            return clone;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(">> Name: SurvivalInstruction_Probablistic");
            sb.AppendFormat(">> Reproduction Probability: {0}", m_reproduction_probability);

            return sb.ToString();
        }
    }
}
