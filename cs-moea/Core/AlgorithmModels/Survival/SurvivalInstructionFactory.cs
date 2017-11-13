using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MOEA.Core.ComponentModels;

namespace MOEA.Core.AlgorithmModels.Survival
{
    using System.Xml;

    public class SurvivalInstructionFactory<P, S>
        where P : IPop
        where S : ISolution
    {
        protected string mFilename;
        private SurvivalInstruction<P, S> mCurrentInstruction;

        public SurvivalInstructionFactory()
        {
            mCurrentInstruction = LoadDefaultInstruction();
        }

        public SurvivalInstructionFactory(string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                mCurrentInstruction = LoadDefaultInstruction();
            }
            else
            {
                mFilename = filename;
                XmlDocument doc = new XmlDocument();
                doc.Load(mFilename);
                XmlElement doc_root = doc.DocumentElement;
                string selected_strategy = doc_root.Attributes["strategy"].Value;
                foreach (XmlElement xml_level1 in doc_root.ChildNodes)
                {
                    if (xml_level1.Name == "strategy")
                    {
                        string attrname = xml_level1.Attributes["name"].Value;
                        if (attrname == selected_strategy)
                        {
                            mCurrentInstruction = LoadInstructionFromXml(selected_strategy, xml_level1);
                        }
                    }
                }
            }

        }

        protected virtual SurvivalInstruction<P, S> LoadDefaultInstruction()
        {
            return new SurvivalInstruction_Compete<P, S>();
        }

        protected virtual SurvivalInstruction<P, S> LoadInstructionFromXml(string selected_strategy, XmlElement xml_level1)
        {
            if (selected_strategy == "compete")
            {
                return new SurvivalInstruction_Compete<P, S>(xml_level1);
            }
            else if (selected_strategy == "probablistic")
            {
                return new SurvivalInstruction_Probablistic<P, S>(xml_level1);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public virtual SurvivalInstructionFactory<P, S> Clone()
        {
            SurvivalInstructionFactory<P, S> clone = new SurvivalInstructionFactory<P, S>(mFilename);
            return clone;
        }

        public virtual S Compete(P pop, S weak_program_in_current_pop, S child_program, Comparison<S> comparer)
        {
            if (mCurrentInstruction != null)
            {
                return mCurrentInstruction.Compete(pop, weak_program_in_current_pop, child_program, comparer);
            }
            else
            {
                throw new ArgumentNullException();
            }

        }


        public override string ToString()
        {
            if (mCurrentInstruction != null)
            {
                return mCurrentInstruction.ToString();
            }
            return "Survival Instruction Factory";
        }
    }
}
