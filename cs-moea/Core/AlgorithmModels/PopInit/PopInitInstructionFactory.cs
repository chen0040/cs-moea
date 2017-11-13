using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MOEA.Core.AlgorithmModels.PopInit
{
    using System.Xml;

    public class PopInitInstructionFactory<P, S>
    {
        protected string mFilename;
        protected PopInitInstruction<P, S> mCurrentInstruction;

        public virtual string InstructionType
        {
            set
            {

            }
        }

        public PopInitInstructionFactory()
        {
            mCurrentInstruction = LoadDefaultInstruction();
        }



        public PopInitInstructionFactory(string filename)
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

        protected virtual PopInitInstruction<P, S> LoadInstructionFromXml(string selected_strategy, XmlElement xml_level1)
        {
            throw new NotImplementedException();
        }

        protected virtual PopInitInstruction<P, S> LoadDefaultInstruction()
        {
            throw new NotImplementedException();
        }

        public virtual PopInitInstructionFactory<P, S> Clone()
        {
            PopInitInstructionFactory<P, S> clone = new PopInitInstructionFactory<P, S>(mFilename);
            return clone;
        }

        public virtual void Initialize(P pop)
        {
            if (mCurrentInstruction != null)
            {
                mCurrentInstruction.Initialize(pop);
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
            return "Pop Init Instruction Factory";
        }

        public PopInitInstruction<P, S> CurrentInstruction
        {
            get { return mCurrentInstruction; }
            set { mCurrentInstruction = value; }
        }
    }
}
