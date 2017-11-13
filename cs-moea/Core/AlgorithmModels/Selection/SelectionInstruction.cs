using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MOEA.Core.AlgorithmModels.Selection
{
    using System.Xml;

    public abstract class SelectionInstruction<P, S>
    {
        public SelectionInstruction()
        {

        }

        public SelectionInstruction(XmlElement xml_level1)
        {

        }

        public abstract S Select(P pop, Comparison<S> comparer);
        public abstract void Select(P lgpPop, List<S> best_pair, List<S> worst_pair, int tournament_count, Comparison<S> comparer);
        public abstract SelectionInstruction<P, S> Clone();
    }
}
