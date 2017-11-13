using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MOEA.AlgorithmModels;
using MOEA.Core.ComponentModels;

namespace MOEA.ComponentModels
{
    public class NondominatedSortingPopulation<S> : Population<S>
        where S : MOOSolution, new()
    {
        private bool mNondominatedSortRequired = false;

        public override bool Add(S s)
        {
            mNondominatedSortRequired = true;
            return base.Add(s);
        }

        public NondominatedSortingPopulation()
        {

        }

        public override S this[int index]
        {
            get
            {
                if (mNondominatedSortRequired)
                {
                    FastNondominatedSort(this);
                }
                return base[index];
            }
        }

        public override bool Replace(S solution_to_remove, S solution_to_add)
        {
            if (base.Replace(solution_to_remove, solution_to_add))
            {
                mNondominatedSortRequired = true;
                return true;
            }
            return false;
        }

        public override void RemoveAt(int index)
        {
            mNondominatedSortRequired = true;
            base.RemoveAt(index);
        }

        public override void Remove(S s)
        {
            mNondominatedSortRequired = true;
            base.Remove(s);
        }

        public override void Clear()
        {
            mNondominatedSortRequired = true;
            base.Clear();
        }

        public override void Sort(Comparison<S> comparison)
        {
            if (mNondominatedSortRequired)
            {
                FastNondominatedSort(this);
            }
            base.Sort(comparison);
        }


        public override void Truncate(int size, Comparison<S> comparison)
        {
            if (mNondominatedSortRequired)
            {
                FastNondominatedSort(this);
            }
            base.Truncate(size, comparison);
        }


        public void Truncate(int size)
        {
            Truncate(size, delegate(S s1, S s2)
            {
                int flag = CompareUtil.RankCompare(s1, s2);
                if (flag == 0)
                {
                    flag = CompareUtil.CrowdingDistanceCompare(s1, s2);
                }
                return flag;
            });
        }

        public void Prune(int size)
        {
            if (mNondominatedSortRequired)
            {
                FastNondominatedSort(this);
            }

            Sort(delegate(S s1, S s2)
                {
                    return CompareUtil.RankCompare(s1, s2);
                });


            int maxRank = mIndividuals[size - 1].Rank;
            Population<S> front = new Population<S>();

            for (int i = Count - 1; i >= 0; i--)
            {
                S solution = mIndividuals[i];
                int rank = solution.Rank;

                if (rank >= maxRank)
                {
                    mIndividuals.RemoveAt(i);

                    if (rank == maxRank)
                    {
                        front.Add(solution);
                    }
                }
            }

            while (Count + front.Count > size)
            {
                SortUtil.UpdateCrowdingDistance(front);
                front.Truncate(front.Count - 1, delegate(S s1, S s2)
                {
                    return CompareUtil.CrowdingDistanceCompare(s1, s2);
                });
            }

            foreach (S s in front.Solutions)
            {
                Add(s);
            }
        }

        public override List<S> Solutions
        {
            get
            {
                if (mNondominatedSortRequired)
                {
                    FastNondominatedSort(this);
                }
                return base.Solutions;
            }
        } 

        public void FastNondominatedSort(Population<S> population) 
        {
            mNondominatedSortRequired = false;
            SortUtil.FastNondominatedSort(population);
	    }

        
    }
}
