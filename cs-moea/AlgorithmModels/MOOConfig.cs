using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MOEA.AlgorithmModels
{
    public class MOOConfig
    {
        private int mPopulationSize=100;
        private double mMutationRate = 0.01;
        private double mCrossoverRate = 0.5;
        private int mMaxGenerations = 1000;
        private int mMaxArchive = 100;

        public double CrossoverRate
        {
            get { return mCrossoverRate; }
            set { mCrossoverRate = value; }
        }

        public double MutationRate
        {
            get { return mMutationRate; }
            set { mMutationRate = value; }
        }

        public int PopulationSize
        {
            get { return mPopulationSize; }
            set { mPopulationSize = value; }
        }

        public int MaxArchive
        {
            get { return mMaxArchive; }
            set { mMaxArchive = value; }
        }

        public int MaxGenerations
        {
            get { return mMaxGenerations; }
            set { mMaxGenerations = value;  }
        }

        public void Load(string filename)
        {

        }

        public virtual void Copy(MOOConfig config)
        {
            mMaxArchive = config.MaxArchive;
            mMaxGenerations = config.MaxGenerations;
            mMutationRate = config.MutationRate;
            mCrossoverRate = config.CrossoverRate;
            mPopulationSize = config.PopulationSize;
        }
    }
}
