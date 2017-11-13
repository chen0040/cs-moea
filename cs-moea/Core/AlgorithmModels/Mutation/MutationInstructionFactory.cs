using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MOEA.Core.AlgorithmModels.Mutation
{
    using System.Xml;

    public abstract class MutationInstructionFactory<P, S>
    {
        protected string mFilename;
        protected MutationInstruction<P, S> mCurrentInstruction;

        public virtual string InstructionType
        {
            set
            {

            }
        }

        public MutationInstructionFactory()
        {
            mCurrentInstruction = LoadDefaultInstruction();
        }

        public MutationInstructionFactory(string filename)
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

        protected virtual MutationInstruction<P, S> LoadDefaultInstruction()
        {
            throw new NotImplementedException();
        }

        protected virtual MutationInstruction<P, S> LoadInstructionFromXml(string selected_strategy, XmlElement xml)
        {
            throw new NotImplementedException();
        }



        public abstract MutationInstructionFactory<P, S> Clone();

        public void Mutate(P pop, S child)
        {
            if (mCurrentInstruction != null)
            {
                mCurrentInstruction.Mutate(pop, child);
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
            return "Mutation Instruction Factory";
        }

    }
}
