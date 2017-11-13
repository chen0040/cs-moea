using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MOEA.Core.ProblemModels;
using MOEA.AlgorithmModels;
using MOEA.Core.ComponentModels;

namespace MOEA.ComponentModels
{
    public class Population<S> : IMOOPop
        where S : MOOSolution, new()
    {
        protected List<S> mIndividuals = new List<S>();
        protected MOOConfig mConfig = new MOOConfig();
        private S mSolutionFactory=null;
        protected IMOOProblem mProblem;
        private static object mSyncObj = new object();

        public IMOOProblem Problem
        {
            get { return mProblem; }
            set { mProblem = value; }
        }

        public void OptimizeMemory()
        {
            foreach(S s in mIndividuals)
            {
                s.ClearAttributes();
            }
        }

        public double MacroMutationRate
        {
            get { return mConfig.MutationRate; }
            set { mConfig.MutationRate = value; }
        }

        public int SolutionCount
        {
            get { return mIndividuals.Count; }
        }

        public int PopulationSize
        {
            get { return mConfig.PopulationSize; }
            set { mConfig.PopulationSize = value; }
        }

        public int MaxGenerations
        {
            get { return mConfig.MaxGenerations; }
            set { mConfig.MaxGenerations = value; }
        }

        public virtual S SolutionFactory
        {
            get 
            {
                if (mSolutionFactory == null)
                {
                    lock (mSyncObj)
                    {
                        mSolutionFactory = new S();
                        mSolutionFactory.Population = this;
                        mSolutionFactory.Problem = mProblem;
                    }
                }
               
                return mSolutionFactory; 
            }
        }

        public virtual List<S> Solutions
        {
            get { return mIndividuals; }
        }

        public List<ISolution> ToList()
        {
            List<ISolution> solutions = new List<ISolution>();
            foreach (S solution in mIndividuals)
            {
                solutions.Add(solution);
            }
            return solutions;
        }

        public double CrossoverRate
        {
            get { return mConfig.CrossoverRate; }
            set { mConfig.CrossoverRate = value; }
        }

        public Population()
        {
            mConfig = new MOOConfig();
        }

        public void Add(Population<S> pop)
        {
            foreach(S s in pop.Solutions)
            {
                Add(s);
            }
        }

        public void AddSolution(ISolution solution)
        {
            Add(solution as S);
        }

        public void Add(params S[] solutions)
        {
            foreach (S s in solutions)
            {
                Add(s);
            }
        }

        public void Add(List<S> solutions)
        {
            foreach (S s in solutions)
            {
                Add(s);
            }
        }

        public MOOConfig Config
        {
            get { return mConfig; }
        }

        public void LoadConfig(string filename)
        {
            mConfig.Load(filename);
        }

        public virtual ISolution CreateSolution()
        {
            return SolutionFactory.Clone() as S;
        }

        public virtual S this[int index]
        {
            get
            {
                return mIndividuals[index];
            }
        }

        public int Count
        {
            get
            {
                return mIndividuals.Count;
            }
        }

        public virtual void RemoveAt(int index)
        {
            mIndividuals.RemoveAt(index);
        }

        public virtual void Remove(S s)
        {
            mIndividuals.Remove(s);
        }

        public virtual bool Add(S s)
        {
            mIndividuals.Add(s);
            return true;
        }

        public virtual void Clear()
        {
            mIndividuals.Clear();
        }


        public virtual void Sort(Comparison<S> comparison)
        {
            mIndividuals.Sort(comparison);
        }

        public bool IsMaximization
        {
            get { return mProblem.IsMaximizing(); }
        }

        public virtual void Truncate(int size, Comparison<S> comparison)
        {
            Sort(comparison);
            while (mIndividuals.Count > size)
            {
                int solution_index = mIndividuals.Count - 1;
                mIndividuals.RemoveAt(solution_index);
            }
        }

        public virtual bool Replace(S solution_to_remove, S solution_to_add)
        {
            bool is_replaced = false;
            for (int i = 0; i < Count; ++i)
            {
                if (mIndividuals[i] == solution_to_remove)
                {
                    mIndividuals[i] = solution_to_add;
                    is_replaced = true;
               }
           }
            return is_replaced;
        }

        public void Replace(ISolution old_solution, ISolution new_solution)
        {
            Replace(old_solution as S, new_solution as S);
        }

        public ISolution FindSolutionByIndex(int index)
        {
            return mIndividuals[index];
        }
    }
}
