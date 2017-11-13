using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MOEA.ComponentModels.SolutionModels;
using MOEA.Core.ComponentModels;

namespace MOEA.AlgorithmModels.Crossover
{
    using System.Xml;
    using MOEA.ComponentModels;
    using Statistics;
    using MOEA.Core.AlgorithmModels.Crossover;

    public class CrossoverInstruction_Uniform<P, S> : CrossoverInstruction<P, S>
        where S : MOOSolution
        where P : IMOOPop
    {
        public CrossoverInstruction_Uniform()
            : base()
        {
           
        }

        public CrossoverInstruction_Uniform(XmlElement xml_level1)
            : base(xml_level1)
        {
            
        }

        public override CrossoverInstruction<P, S> Clone()
        {
            CrossoverInstruction_Uniform<P, S> clone = new CrossoverInstruction_Uniform<P, S>();
            return clone;
        }

        public override List<S> Crossover(P pop, params S[] parents)
        {
            List<S> results = new List<S>();

            for (int i = 0; i < parents.Length; i += 2)
            {
                int child1_index = i;
                int child2_index = i + 1;
                if (child2_index >= parents.Length) child2_index = 0;
                S child1 = parents[child1_index].Clone() as S;
                S child2 = parents[child2_index].Clone() as S;
                child1.UniformCrossover(child2);
                results.Add(child1);
                results.Add(child2);
            }

            return results;
                
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(">> Name: CrossoverInstruction_Uniform\n");
            
            return sb.ToString();
        }
    }
}
