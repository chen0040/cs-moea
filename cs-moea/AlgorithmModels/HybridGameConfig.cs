using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MOEA.AlgorithmModels
{
    public class HybridGameConfig : MOOConfig
    {
        private int mMaxCountOfSolutionTransferred2ANashNode = 3;
        public int MaxCountOfSolutionTransferred2ANashNode
        {
            get { return mMaxCountOfSolutionTransferred2ANashNode; }
            set { mMaxCountOfSolutionTransferred2ANashNode = value; }
        }
    }
}
