using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MOEA.Core
{
    public class BaseSolution<T>
    {
        protected Dictionary<string, object> mAttributes = new Dictionary<string, object>();
        public object this[string attr_name]
        {
            get
            {
                if (mAttributes.ContainsKey(attr_name))
                {
                    return mAttributes[attr_name];
                }
                return null;
            }
            set
            {
                mAttributes[attr_name] = value;
            }
        }

        public bool HasAttribute(string attr_name)
        {
            return mAttributes.ContainsKey(attr_name);
        }

        protected T[] mValues;
        public T[] Values
        {
            get { return mValues; }
            set { mValues = value; }
        }



        public virtual double GetDistance2(BaseSolution<T> rhs)
        {
            return System.Math.Sqrt(GetDistanceSq2(rhs));
        }

        protected double mCost = double.MaxValue;
        public double Cost
        {
            get { return mCost; }
            set { mCost = value; }
        }

        public T this[int index]
        {
            get { return mValues[index]; }
            set { mValues[index] = value; }
        }

        public bool IsBetterThan(BaseSolution<T> rhs)
        {
            return mCost < rhs.Cost;
        }

        public virtual double GetDistanceSq2(BaseSolution<T> rhs)
        {
            throw new NotImplementedException();
        }

        public bool TryUpdateSolution(T[] new_solution, double new_cost, out double? improvement)
        {
            improvement = null;

            if (mCost > new_cost)
            {
                improvement = mCost - new_cost;
                mCost = new_cost;
                mValues = (T[])new_solution.Clone();
                return true;
            }
            else if (mValues == null)
            {
                int dimension = new_solution.Length;
                mValues = new T[dimension];
                for (int i = 0; i < dimension; ++i)
                {
                    mValues[i] = new_solution[i];
                }
                mCost = new_cost;
                return true;
            }

            return false;
        }

        public BaseSolution()
        {
            mCost = double.MaxValue;
        }

        public BaseSolution(T[] values, double cost)
        {
            mValues = (T[])values.Clone();
            mCost = cost;
        }

        public BaseSolution(int dimension)
        {
            mValues = new T[dimension];
            mCost = double.MaxValue;
        }

        public int Length
        {
            get { return mValues.Length; }
        }

        public virtual BaseSolution<T> Clone()
        {
            return new BaseSolution<T>(mValues, mCost);
        }
    }
}
