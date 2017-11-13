using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MOEA.Core.ComponentModels;

namespace MOEA.Core.AlgorithmModels.Selection
{
    using System.Xml;
    using MOEA.Core.ComponentModels;
    using Statistics;
    using MOEA.Core.AlgorithmModels.Selection;

    public class SelectionInstruction_Tournament<P, S> : SelectionInstruction<P, S>
        where P : IPop
        where S : ISolution
    {
        private int mTournamentSize = 2;

        public SelectionInstruction_Tournament()
        {

        }

        public SelectionInstruction_Tournament(int tournament_size)
        {
            mTournamentSize = tournament_size;
        }

        public int TournamentSize
        {
            get { return mTournamentSize; }
            set { mTournamentSize = value; }
        }

        public SelectionInstruction_Tournament(XmlElement xml_level1)
            : base(xml_level1)
        {
            foreach (XmlElement xml_level2 in xml_level1.ChildNodes)
            {
                if (xml_level2.Name == "param")
                {
                    string attrname = xml_level2.Attributes["name"].Value;
                    string attrvalue = xml_level2.Attributes["value"].Value;
                    if (attrname == "tournament_size")
                    {
                        int value = 0;
                        int.TryParse(attrvalue, out value);
                        mTournamentSize = value;
                    }
                }
            }
        }

        public override S Select(P pop, Comparison<S> comparer)
        {
            HashSet<S> tournament = new HashSet<S>();
            while (tournament.Count < mTournamentSize)
            {
                int r = DistributionModel.NextInt(pop.PopulationSize);
                tournament.Add((S)pop.FindSolutionByIndex(r));
            }

            List<S> programs = tournament.ToList();

            programs.Sort(comparer);

            return programs[0];
        }

        public void RandomShuffle(List<ISolution> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                ISolution value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public override void Select(P pop, List<S> best_pair, List<S> worst_pair, int tournament_count, Comparison<S> comparer)
        {
            List<ISolution> temp_pop = pop.ToList();

            int tsize = mTournamentSize;
            if (tsize > temp_pop.Count)
            {
                tsize = temp_pop.Count;
            }

            for (int i = 0; i < tournament_count; ++i)
            {
                List<S> tournament1 = new List<S>();

                RandomShuffle(temp_pop);
                for (int j = 0; j < tsize; j++)
                {
                    S s = (S)temp_pop[j];
                    tournament1.Add(s);
                }

                tournament1.Sort(comparer);

                best_pair.Add(tournament1[0]);
                worst_pair.Add(tournament1[tournament1.Count - 1]);
            }
        }

        public override SelectionInstruction<P, S> Clone()
        {
            SelectionInstruction_Tournament<P, S> clone = new SelectionInstruction_Tournament<P, S>();
            clone.mTournamentSize = mTournamentSize;
            return clone;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(">> Name: SelectionInstruction_Tournament");
            sb.AppendFormat(">> Tournament Size: {0}", mTournamentSize);

            return sb.ToString();
        }
    }
}
