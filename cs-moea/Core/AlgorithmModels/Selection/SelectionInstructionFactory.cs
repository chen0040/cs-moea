using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MOEA.Core.ComponentModels;

namespace MOEA.Core.AlgorithmModels.Selection
{
    using System.Xml;

    public class SelectionInstructionFactory<P, S>
        where S : ISolution
        where P : IPop
    {
        protected string mFilename;
        protected SelectionInstruction<P, S> mCurrentInstruction;

        public virtual string InstructionType
        {
            set
            {

            }
        }

        public SelectionInstructionFactory()
        {
            mCurrentInstruction = LoadDefaultInstruction();
        }

        public SelectionInstructionFactory(string filename)
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
                if (mCurrentInstruction == null)
                {
                    mCurrentInstruction = LoadDefaultInstruction();
                }
            }

        }

        protected virtual SelectionInstruction<P, S> LoadInstructionFromXml(string selected_strategy, XmlElement xml_level1)
        {
            if (selected_strategy == "tournament")
            {
                return new SelectionInstruction_Tournament<P, S>(xml_level1);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        protected virtual SelectionInstruction<P, S> LoadDefaultInstruction()
        {
            return new SelectionInstruction_Tournament<P, S>();
        }

        public SelectionInstruction<P, S> CurrentInstruction
        {
            get { return mCurrentInstruction; }
            set { mCurrentInstruction = value; }
        }

        public virtual SelectionInstructionFactory<P, S> Clone()
        {
            SelectionInstructionFactory<P, S> clone = new SelectionInstructionFactory<P, S>(mFilename);
            return clone;
        }

        public virtual void Select(P pop, List<S> best_pair, List<S> worst_pair, int tournament_size, Comparison<S> comparer)
        {
            if (mCurrentInstruction != null)
            {
                mCurrentInstruction.Select(pop, best_pair, worst_pair, tournament_size, comparer);
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public virtual S Select(P pop, Comparison<S> comparer)
        {
            if (mCurrentInstruction != null)
            {
                return mCurrentInstruction.Select(pop, comparer);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public override string ToString()
        {
            if (mCurrentInstruction != null)
            {
                return mCurrentInstruction.ToString();
            }
            return "TGP Selection Instruction Factory";
        }
    }
}
