using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MOEA.Core.ProblemModels
{
    public class IGPEnvironment
    {
        public delegate int GetFitnessCaseCountHandle();
        public delegate IGPFitnessCase CreateFitnessCaseHandle(int i);

        public event GetFitnessCaseCountHandle GetFitnessCaseCountTriggered;
        public event CreateFitnessCaseHandle CreateFitnessCaseTriggered;

        public int GetFitnessCaseCount()
        {
            if (GetFitnessCaseCountTriggered != null)
            {
                return GetFitnessCaseCountTriggered();
            }
            return 0;
        }

        public IGPFitnessCase CreateFitnessCase(int index)
        {
            if (CreateFitnessCaseTriggered != null)
            {
                return CreateFitnessCaseTriggered(index);
            }
            return null;
        }

        public object Data = null;

        public List<IGPFitnessCase> FitnessCases = null;

        public IGPEnvironment() { }
    }
}
