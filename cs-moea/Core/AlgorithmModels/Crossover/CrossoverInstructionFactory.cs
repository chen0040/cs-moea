using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MOEA.Core.AlgorithmModels.Crossover
{
    using System.Xml;
    public abstract class CrossoverInstructionFactory<P, S>
    {
        protected CrossoverInstruction<P, S> mCurrentCrossover;
        protected string mFilename;

        public CrossoverInstructionFactory()
        {
            mCurrentCrossover = CreateDefaultInstruction();
        }

        public virtual string InstructionType
        {
            set
            {

            }
        }

        public CrossoverInstruction<P, S> CurrentInstruction
        {
            get
            {
                return mCurrentCrossover;
            }
            set
            {
                mCurrentCrossover = value;
            }
        }

        public CrossoverInstructionFactory(string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                mCurrentCrossover = CreateDefaultInstruction();
            }
            else
            {
                mFilename = filename;
                XmlDocument doc = new XmlDocument();
                doc.Load(filename);
                XmlElement doc_root = doc.DocumentElement;
                string selected_strategy = doc_root.Attributes["strategy"].Value;
                foreach (XmlElement xml_level1 in doc_root.ChildNodes)
                {
                    if (xml_level1.Name == "strategy")
                    {
                        string attrname = xml_level1.Attributes["name"].Value;
                        if (attrname == selected_strategy)
                        {
                            mCurrentCrossover = CreateInstructionFromXml(attrname, xml_level1);
                        }
                    }
                }

                if (mCurrentCrossover == null)
                {
                    mCurrentCrossover = CreateDefaultInstruction();
                }
            }

        }

        protected virtual CrossoverInstruction<P, S> CreateDefaultInstruction()
        {
            throw new NotImplementedException();
        }

        protected virtual CrossoverInstruction<P, S> CreateInstructionFromXml(string strategy_name, XmlElement xml)
        {
            throw new NotImplementedException();
        }

        public abstract CrossoverInstructionFactory<P, S> Clone();

        public List<S> Crossover(P pop, params S[] parents)
        {
            if (mCurrentCrossover != null)
            {
                return mCurrentCrossover.Crossover(pop, parents);
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public override string ToString()
        {
            return mCurrentCrossover.ToString();
        }
    }
}
