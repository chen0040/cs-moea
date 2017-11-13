using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MOEA.Core.ProblemModels;

namespace MOEA.AlgorithmModels
{
    using Statistics;
    using MOEA.ComponentModels.SolutionModels;
    using MOEA.ComponentModels;
    public class HybridGame<S> : MOOSolver
        where S : ContinuousVector, new()
    {
        protected IMOOProblem mProblem;
        protected Dictionary<int, double> mEliteDesignVariables = new Dictionary<int, double>();
        protected List<NashNode<S>> mNashNodes = new List<NashNode<S>>();
        protected HybridGameConfig mConfig = new HybridGameConfig();
        protected ParetoNode<S> mParetoNode;
        protected bool mIsInitialized = false;

        public HybridGame(IMOOProblem problem)
        {
            mProblem = problem;
            int dimension_count=problem.GetDimensionCount();
            for (int i = 0; i < dimension_count; ++i)
            {
                mEliteDesignVariables[i]=0;
            }
        }

        public override bool IsTerminated
        {
            get { return mCurrentGeneration >= mConfig.MaxGenerations; }
        }

        public int NondominatedArchiveSize
        {
            get { return mParetoNode.NondominatedArchiveSize; }
        }

        public NondominatedPopulation<S> NondominatedArchive
        {
            get { return mParetoNode.NondominatedArchive; }
        }

        public HybridGameConfig Config
        {
            get { return mConfig; }
        }

        public override void Initialize()
        {
            if (mIsInitialized) return;
            mIsInitialized = true;

            int dimension_count=mProblem.GetDimensionCount();

	        for(int objective_index=0; objective_index != dimension_count; objective_index++)
	        {
                mEliteDesignVariables[objective_index] = mProblem.GetLowerBound(objective_index) + DistributionModel.GetUniform() * (mProblem.GetUpperBound(objective_index) - mProblem.GetLowerBound(objective_index));
	        }

            int objective_count = mProblem.GetObjectiveCount();
            int design_variable_count_per_objective = dimension_count / objective_count;
	        //Initialize the NashNodes
	        for(int objective_index = 0; objective_index < objective_count ; objective_index++)
	        {
                Dictionary<int, double> ellite_design_variables = new Dictionary<int, double>();
                Dictionary<int, int> design_variable_local_mapping = new Dictionary<int, int>();
                int starting_design_variable_index = objective_index * design_variable_count_per_objective;
                int ending_design_variable_index = (objective_index + 1) * design_variable_count_per_objective;
                for (int j = 0; j < dimension_count; ++j)
                {
                    if (j >= starting_design_variable_index && j < ending_design_variable_index)
                    {
                        design_variable_local_mapping[j] = j - starting_design_variable_index;
                    }
                    else
                    {
                        ellite_design_variables[j] = mEliteDesignVariables[j];
                    }
                }
                NashNode<S> nash_node = new NashNode<S>(mProblem, mConfig, ellite_design_variables, design_variable_local_mapping, objective_index);
                mNashNodes.Add(nash_node);
                mNashNodes[objective_index].Initialize();
	        }

	        //Initialize the ParetoNode and Fixed Array
            mParetoNode = new ParetoNode<S>(mProblem);
            mParetoNode.Population.Config.Copy(mConfig);
            mParetoNode.Initialize();
        }

        protected virtual S CreateSolutionFromEliteDesignVariables()
        {
            S solution=(ContinuousVector)mParetoNode.SolutionFactory.Clone() as S;

            int chromosome_length=mEliteDesignVariables.Count;
            solution.Initialize(chromosome_length);
            for(int i=0; i < chromosome_length; ++i)
            {
                solution[i]=mEliteDesignVariables[i];
            }

            

            return solution;
        }

        public override void Evolve()
        {
	        for(int i=0; i != mNashNodes.Count; i++)
	        {
		        mNashNodes[i].Evolve();
                mNashNodes[i].UpdateGlobalEliteDesignVariables(mEliteDesignVariables);
	        }

            for(int i=0; i !=mNashNodes.Count; ++i)
            {
                List<int> nash_node_needed_global_design_variable_indices=mNashNodes[i].GlobalEliteDesignVariables.Keys.ToList();
                foreach(int elite_design_variable_index in nash_node_needed_global_design_variable_indices)
                {
                    mNashNodes[i].GlobalEliteDesignVariables[elite_design_variable_index]=mEliteDesignVariables[elite_design_variable_index];
                }
            }
	
	        mParetoNode.Evolve();
        
	        mParetoNode.EvaluateAndAdd(CreateSolutionFromEliteDesignVariables());

            for(int i=0; i < mNashNodes.Count; ++i)
            {
                int max_solution_to_transfer=mNashNodes[i].MaxCountOfInjectedSolutions;
                for(int j=0; j < max_solution_to_transfer; ++j)
                {
                    S pareto_solution=mParetoNode.Population[j];
                     mNashNodes[i].AddParetoSolution(pareto_solution);
                    
                }
               
            }
            mCurrentGeneration++;
	       
        }
    }
}
