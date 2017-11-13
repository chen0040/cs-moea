using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MOEA.ComponentModels.SolutionModels
{
    using MOEA.Core.ProblemModels;
    using Statistics;
    using MOEA.Core.ComponentModels;
    public class ContinuousVector : MOOSolution
    {
        protected List<double> mData = new List<double>();

        public ContinuousVector(IMOOPop pop, IMOOProblem problem)
            : base(pop, problem)
        {

        }

        public ContinuousVector()
        {

        }

        public double this[int index]
        {
            get
            {
                return mData[index];
            }
            set
            {
                mData[index] = value;
            }
        }

        public int Length
        {
            get { return mData.Count; }
        }

        public void Initialize(int chromosome_length)
        {
            mData.Clear();
            for (int i = 0; i < chromosome_length; ++i)
            {
                mData.Add(0);
            }
        }

        public override void InitializeRandomly(int chromosome_length)
        {
            mData.Clear();
            int dimension_count = mProblem.GetDimensionCount();
            double lower_bound = 0;
            double upper_bound = 0;
            for (int dimension = 0; dimension < dimension_count; ++dimension)
            {
                lower_bound = mProblem.GetLowerBound(dimension);
                upper_bound = mProblem.GetUpperBound(dimension);

                mData.Add(lower_bound + DistributionModel.GetUniform() * (upper_bound - lower_bound));
            }
        }

        public override void MutateUniformly(double mutation_rate)
        {
            double lower_bound = 0;
            double upper_bound = 0;
            int dimension_count=mData.Count;
            for (int dimension = 0; dimension != dimension_count; ++dimension)
            {
                if (DistributionModel.GetUniform() < mutation_rate)
                {
                    lower_bound = mProblem.GetLowerBound(dimension);
                    upper_bound = mProblem.GetUpperBound(dimension);

                    mData[dimension] = lower_bound + DistributionModel.GetUniform() * (upper_bound - lower_bound);
                }
                
            }
        }

        public override void OnePointCrossover(MOOSolution rhs)
        {
            if (mData.Count == 1) return;

            int cut_point = 1;
            if (mData.Count > 2)
            {
                cut_point = DistributionModel.NextInt(mData.Count);
            }

            ContinuousVector rhs_vector = (ContinuousVector)rhs;

            double temp = 0;
            for (int dimension = 0; dimension != cut_point; ++dimension)
            {
                temp=this[dimension];
                this[dimension] = rhs_vector[dimension];
                rhs_vector[dimension] = temp;
                
            }
        }

        public override void UniformCrossover(MOOSolution rhs)
        {
            ContinuousVector rhs_vector = (ContinuousVector)rhs;

            int length = mData.Count;
            for (int dimension = 0; dimension != length; ++dimension)
            {
                double val1=this[dimension];
                double val2=rhs_vector[dimension];
                if (DistributionModel.GetUniform() < 0.5)
                {
                    this[dimension] = val2;
                }

                if (DistributionModel.GetUniform() < 0.5)
                {
                    rhs_vector[dimension] = val1;
                }
            }
        }

        public override ISolution Clone()
        {
            ContinuousVector clone = new ContinuousVector(mPopulation, mProblem);
            clone.Copy(this);

            int dimension_count=mData.Count;
            for (int dimension = 0; dimension < dimension_count; ++dimension)
            {
                clone.mData.Add(mData[dimension]);
            }
            return clone;
        }



        public double[] ToArray()
        {
            return mData.ToArray();
        }
    }
}
