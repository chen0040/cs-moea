using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MOEA.Core.ProblemModels;
using MOEA.ComponentModels.SolutionModels;

namespace MOEA.ComponentModels
{
    using AlgorithmModels;
    using AlgorithmModels.Mutation;
    using AlgorithmModels.Crossover;
    using AlgorithmModels.PopInit;
    using Statistics;
    using MOEA.Core.AlgorithmModels.Crossover;
    using MOEA.Core.ComponentModels;
    using MOEA.Core.AlgorithmModels.Selection;
    using MOEA.Core.AlgorithmModels.PopInit;
    using MOEA.Core.AlgorithmModels.Mutation;

    public class NashNode<S> : MOOSolver, IMOOProblem
        where S : ContinuousVector, new()
    {
        protected Population<S> mPopulation = null;

        protected PopInitInstructionFactory<IMOOPop, S> mPopInitInstructionFactory = null;
        protected CrossoverInstructionFactory<IMOOPop, S> mCrossoverInstructionFactory = null;
        protected MutationInstructionFactory<IMOOPop, S> mMutationInstructionFactory = null;
        protected SelectionInstructionFactory<IMOOPop, S> mReproductionSelectionInstructionFactory = null;

        protected bool mInitialized=false;
        protected int mObjectiveIndex = 0;
        protected IMOOProblem mOriginalProblem = null;

        protected Dictionary<int, double> mGlobalEliteDesignVariables = new Dictionary<int, double>();
        protected Dictionary<int, int> mDesignVariableLocal2GlobalMapping = new Dictionary<int, int>();
        protected Dictionary<int, int> mDesignVariableGlobal2LocalMapping = new Dictionary<int, int>();
        protected Dictionary<int, double> mLocalEliteDesignVariables = new Dictionary<int, double>();

        public Dictionary<int, double> GlobalEliteDesignVariables
        {
            get { return mGlobalEliteDesignVariables; }
        }

        public void UpdateGlobalEliteDesignVariables(Dictionary<int, double> global_elite_design_variables)
        {
            foreach(int global_elite_design_variable_index in mDesignVariableGlobal2LocalMapping.Keys)
            {
                global_elite_design_variables[global_elite_design_variable_index]=mLocalEliteDesignVariables[global_elite_design_variable_index];
            }
        }

        protected virtual S Convert2SolutionForOriginalProblem(S s)
        {
            S original_solution = SolutionFactory.Clone() as S;
            
            S partial_solution = s;
            int dimension_count = mOriginalProblem.GetDimensionCount();
            original_solution.Initialize(dimension_count);
            for (int i = 0; i < dimension_count; ++i)
            {
                if (mGlobalEliteDesignVariables.ContainsKey(i))
                {
                    original_solution[i] = mGlobalEliteDesignVariables[i];
                }
                else
                {
                    original_solution[i] = partial_solution[mDesignVariableGlobal2LocalMapping[i]];
                }
            }
            return original_solution;
        }

        public int GetObjectiveCount()
        {
            return 1;
        }

        /// <summary>
        /// Implement IProblem.CalcObjective interface method
        /// </summary>
        /// <param name="s"></param>
        /// <param name="objective_index"></param>
        /// <returns></returns>
        public double CalcObjective(MOOSolution s, int objective_index)
        {
            S original_solution = Convert2SolutionForOriginalProblem(s as S);
            return mOriginalProblem.CalcObjective(original_solution, mObjectiveIndex);
        }

        public double GetUpperBound(int dimension_index)
        {
            return mOriginalProblem.GetUpperBound(mDesignVariableLocal2GlobalMapping[dimension_index]);
        }

        public double GetLowerBound(int dimension_index)
        {
            return mOriginalProblem.GetLowerBound(mDesignVariableLocal2GlobalMapping[dimension_index]);
        }

        public int GetDimensionCount()
        {
            return mDesignVariableGlobal2LocalMapping.Count;
        }

        public bool IsFeasible(MOOSolution s)
        {
            S original_solution=Convert2SolutionForOriginalProblem(s as S);
            return mOriginalProblem.IsFeasible(original_solution);
        }

        public bool IsMaximizing()
        {
            return mOriginalProblem.IsMaximizing();
        }

        public S SolutionFactory
        {
            get { return mPopulation.SolutionFactory; }
        }

        public Population<S> Population
        {
            get { return mPopulation; }
        }

        public IMOOProblem Problem
        {
            get { return mOriginalProblem; }
        }

        public override bool IsTerminated
        {
            get 
            {
                return mCurrentGeneration >= mPopulation.Config.MaxGenerations;
            }
        }

        protected virtual Population<S> CreatePopulation()
        {
            return new Population<S>();
        }

        protected virtual MutationInstructionFactory<IMOOPop, S> CreateMutationInstructionFactory()
        {
            return new MOOMutationInstructionFactory<IMOOPop, S>();
        }

        protected virtual CrossoverInstructionFactory<IMOOPop, S> CreateCrossoverInstructionFactory()
        {
            return new MOOCrossoverInstructionFactory<IMOOPop, S>();
        }

        protected virtual PopInitInstructionFactory<IMOOPop, S> CreatePopInitInstructionFactory()
        {
            return new MOOPopInitInstructionFactory<IMOOPop, S>();
        }

        protected virtual SelectionInstructionFactory<IMOOPop, S> CreateSelectionInstructionFactory()
        {
            return new SelectionInstructionFactory<IMOOPop, S>();
        }

        public NashNode(IMOOProblem problem, MOOConfig config, Dictionary<int, double> elite_design_variables, Dictionary<int, int> design_variable_local_mapping, int objective_index)
        {
            mGlobalEliteDesignVariables = elite_design_variables;
            mDesignVariableGlobal2LocalMapping = design_variable_local_mapping;
            foreach (int design_variable_global_index in design_variable_local_mapping.Keys)
            {
                mLocalEliteDesignVariables[design_variable_global_index] = -1;
                mDesignVariableLocal2GlobalMapping[design_variable_local_mapping[design_variable_global_index]] = design_variable_global_index;
            }

            mOriginalProblem = problem;
            mObjectiveIndex = objective_index;

            mPopulation = CreatePopulation();
            mPopulation.Problem = this;
            mPopulation.Config.Copy(config);

            mMutationInstructionFactory = CreateMutationInstructionFactory();
            mCrossoverInstructionFactory = CreateCrossoverInstructionFactory();
            mPopInitInstructionFactory = CreatePopInitInstructionFactory();
            mReproductionSelectionInstructionFactory = CreateSelectionInstructionFactory();

        }

        public MOOConfig Config
        {
            get { return mPopulation.Config; }
        }

        public override void Initialize()
        {
            if (mInitialized)
            {
                return;
            }
            mInitialized = true;

            mPopInitInstructionFactory.Initialize(mPopulation);

            Evaluate(mPopulation);
        }

        public virtual void Evaluate(Population<S> pop)
        {
            foreach (MOOSolution s in pop.Solutions)
            {
                s.Evaluate();
            }
        }

        protected virtual void UpdateLocalEliteDesignVariables()
        {
            ContinuousVector partial_solution=(ContinuousVector)mPopulation[0];
            foreach(int global_elite_design_variable_index in mDesignVariableGlobal2LocalMapping.Keys)
            {
                mLocalEliteDesignVariables[global_elite_design_variable_index]=partial_solution[mDesignVariableGlobal2LocalMapping[global_elite_design_variable_index]];
            }
        }

        public override void Evolve()
        {
		    Population<S> offspring = new Population<S>();
		    int populationSize = mPopulation.Config.PopulationSize;

		    while (offspring.Count < populationSize) 
            {
                List<S> tournament_winners = new List<S>();
                List<S> tournament_losers = new List<S>();
                mReproductionSelectionInstructionFactory.Select(mPopulation, tournament_winners, tournament_losers, 2, delegate(S s1, S s2)
                {
                    return s1.FindObjectiveAt(0).CompareTo(s2.FindObjectiveAt(0));
                });

                List<S> children = mCrossoverInstructionFactory.Crossover(mPopulation, tournament_winners.ToArray());

                for (int i = 0; i < children.Count; ++i)
                {
                    mMutationInstructionFactory.Mutate(mPopulation, children[i]);
                    offspring.Add(children[i]);
                }
               
		    }

		    Evaluate(offspring);

		    mPopulation.Add(offspring);

            mPopulation.Truncate(populationSize, delegate(S s1, S s2)
            {
                return s1.FindObjectiveAt(0).CompareTo(s2.FindObjectiveAt(0)) ;
            });

            UpdateLocalEliteDesignVariables();

            mCurrentGeneration++;
        }
    
        public  int MaxCountOfInjectedSolutions 
        {
            get
            {
                return System.Math.Min(((HybridGameConfig)mPopulation.Config).MaxCountOfSolutionTransferred2ANashNode, mPopulation.Count);
            }
        }

        public void AddParetoSolution(S pareto_solution)
        {

 	        ContinuousVector original_solution=(ContinuousVector)pareto_solution;
            ContinuousVector nash_solution=(ContinuousVector)SolutionFactory.Clone();
            nash_solution.Population = mPopulation;
            nash_solution.Problem = this;

            nash_solution.Initialize(mDesignVariableGlobal2LocalMapping.Count);
            foreach(int global_design_variable_index in mDesignVariableGlobal2LocalMapping.Keys)
            {
                nash_solution[mDesignVariableGlobal2LocalMapping[global_design_variable_index]]=original_solution[global_design_variable_index];
            }
        }
    }
}
