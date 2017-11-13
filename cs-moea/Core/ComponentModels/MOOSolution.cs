using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MOEA.Core.ComponentModels
{
    using ProblemModels;
    public class MOOSolution : ISolution
    {
        protected List<double> mObjectives = new List<double>();
        protected List<double> mConstaints = new List<double>();
        private Dictionary<string, object> mTags = new Dictionary<string, object>();

        protected int mRank = 0;
        protected double mCrowdingDistance = 0;

        protected IMOOProblem mProblem;
        protected IMOOPop mPopulation;

        public MOOSolution(IMOOPop population, IMOOProblem problem)
        {
            mPopulation = population;
            mProblem = problem;
        }

        public virtual void ClearAttributes()
        {
            mTags.Clear();
        }

        public virtual object this[string id]
        {
            get
            {
                if (mTags.ContainsKey(id))
                {
                    return mTags[id];
                }
                return null;
            }
            set
            {
                mTags[id] = value;
            }
        }

        public MOOSolution()
        {

        }

        public IMOOPop Population
        {
            get { return mPopulation; }
            set { mPopulation = value; }
        }

        public int Rank
        {
            get { return mRank; }
            set { mRank = value; }
        }

        public double CrowdingDistance
        {
            get { return mCrowdingDistance; }
            set { mCrowdingDistance = value; }
        }

        public int ObjectiveCount
        {
            get
            {
                return mObjectives.Count;
            }
        }

        public int ConstraintCount
        {
            get
            {
                return mConstaints.Count;
            }
        }

        // Xianshun:
        // Objective for minimization problem
        public double FindObjectiveAt(int index)
        {
            return mObjectives[index];
        }

        public double FindObjectiveAt(int index, bool remove_adjustment)
        {
            if (remove_adjustment)
            {
                if (mProblem.IsMaximizing())
                {
                    return -mObjectives[index];
                }
            }
            return mObjectives[index];
        }

        public double FindConstraintAt(int index)
        {
            return mConstaints[index];
        }

        public virtual void InitializeRandomly(int chromosome_length)
        {

        }

        public virtual void MutateUniformly(double mutation_rate)
        {

        }

        public virtual void OnePointCrossover(MOOSolution rhs)
        {

        }

        public virtual void Copy(MOOSolution rhs)
        {
            mObjectives.Clear();
            mConstaints.Clear();

            int objective_count = rhs.mObjectives.Count;
            for (int objective_index = 0; objective_index < objective_count; ++objective_index)
            {
                mObjectives.Add(rhs.mObjectives[objective_index]);
            }
            int constraint_count = rhs.mConstaints.Count;
            for (int constraint_index = 0; constraint_index < constraint_count; ++constraint_index)
            {
                mConstaints.Add(rhs.mConstaints[constraint_index]);
            }

            mTags.Clear();
            Dictionary<string, object> tags = rhs.mTags;
            foreach (string tag_id in tags.Keys)
            {
                mTags[tag_id] = tags[tag_id];
            }

            mRank = rhs.mRank;
            mCrowdingDistance = rhs.mCrowdingDistance;

            mPopulation = rhs.mPopulation;
            mProblem = rhs.mProblem;
        }

        public virtual void Evaluate()
        {
            int objective_count = mProblem.GetObjectiveCount();
            while (mObjectives.Count < objective_count)
            {
                mObjectives.Add(0);
            }
            for (int objective_index = 0; objective_index < objective_count; ++objective_index)
            {
                int sign = mProblem.IsMaximizing() ? (-1) : 1;
                mObjectives[objective_index] = sign * mProblem.CalcObjective(this, objective_index);
            }
        }

        public virtual void Evaluate(int objective_index)
        {
            int objective_count = mProblem.GetObjectiveCount();
            while (mObjectives.Count < objective_count)
            {
                mObjectives.Add(0);
            }
            int sign = mProblem.IsMaximizing() ? (-1) : 1;
            mObjectives[objective_index] = sign * mProblem.CalcObjective(this, objective_index);
        }

        public virtual ISolution Clone()
        {
            MOOSolution clone = new MOOSolution(mPopulation, mProblem);
            clone.Copy(this);

            return clone;
        }


        public IMOOProblem Problem
        {
            get { return mProblem; }
            set { mProblem = value; }
        }

        public virtual void UniformCrossover(MOOSolution rhs)
        {

        }
    }
}
