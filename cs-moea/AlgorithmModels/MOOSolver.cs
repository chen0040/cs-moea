using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MOEA.AlgorithmModels
{
    public abstract class MOOSolver
    {
        protected int mCurrentGeneration;

        public int CurrentGeneration
        {
            get
            {
                return mCurrentGeneration;
            }
        }

        public abstract void Initialize();
        public abstract void Evolve();
        public abstract bool IsTerminated
        {
            get;
        }
        
    }
}
