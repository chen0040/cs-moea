using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MOEA.Core
{
    public class BaseSolver<T> : ISolver<T>
    {
        public delegate bool TerminationEvaluationMethod(double? improvement, int iteration);

        public delegate void SteppedHandle(BaseSolution<T> solution, int step);
        public event SteppedHandle Stepped;
        public delegate void SolutionUpdatedHandle(BaseSolution<T> solution, int step);
        public event SolutionUpdatedHandle SolutionUpdated;

        protected void OnStepped(BaseSolution<T> solution, int step)
        {
            if (Stepped != null)
            {
                Stepped(solution, step);
            }
        }

        protected void OnSolutionUpdated(BaseSolution<T> solution, int step)
        {
            if (SolutionUpdated != null)
            {
                SolutionUpdated(solution, step);
            }
        }
    }
}
