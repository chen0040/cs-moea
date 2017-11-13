using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MOEA.Core.ComponentModels;

namespace MOEA.AlgorithmModels.PopInit
{
    using System.Xml;
    using MOEA.ComponentModels;
    using Statistics;
    using MOEA.Core.AlgorithmModels.PopInit;

    public class PopInitInstruction_Random<P, S> : PopInitInstruction<P, S>
        where S : MOOSolution
        where P : IMOOPop
    {
        public PopInitInstruction_Random()
        {

        }

        public PopInitInstruction_Random(XmlElement xml_level1)
            : base(xml_level1)
        {
            
        }

        public override void Initialize(P pop)
        {
	        int iPopulationSize=pop.PopulationSize;


            int chromosome_length = pop.Problem.GetDimensionCount();


	        for(int i=0; i<iPopulationSize; i++)
	        {
                //Console.WriteLine("{0}: ", i);
                S program = pop.CreateSolution() as S;

                program.InitializeRandomly(chromosome_length);

                pop.AddSolution(program);
	        }
        }

        public override PopInitInstruction<P, S> Clone()
        {
            PopInitInstruction_Random<P, S> clone = new PopInitInstruction_Random<P, S>();
            return clone;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(">> Name: PopInitInstruction_Random\n");

            return sb.ToString();
        }
    }
}
