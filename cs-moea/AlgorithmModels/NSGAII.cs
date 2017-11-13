using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MOEA.ComponentModels.SolutionModels;
using MOEA.Core.AlgorithmModels.Selection;
using MOEA.Core.AlgorithmModels.Mutation;

namespace MOEA.AlgorithmModels
{
    using MOEA.ComponentModels;
    using Mutation;
    using Crossover;
    using PopInit;
    using Statistics;
    using MOEA.Core.ProblemModels;
    using MOEA.Core.AlgorithmModels.Crossover;
    using MOEA.Core.ComponentModels;
    using MOEA.Core.AlgorithmModels.PopInit;
    using MOEA.Core.AlgorithmModels.Selection;
    using System.Threading.Tasks;

    public class NSGAII : NSGAII<ContinuousVector>
    {
        public NSGAII(IMOOProblem problem)
            : base(problem)
        {

        }

        
    }

    public class NSGAII<S> : NSGAII<NondominatedSortingPopulation<S>, S>
        where S : MOOSolution, new()
    {
        public NSGAII(IMOOProblem problem)
            : base(problem)
        {

        }

        
    }

    public class NSGAII<P, S> : MOOSolver
        where S : MOOSolution, new()
        where P : NondominatedSortingPopulation<S>, new()
    {
        protected bool mParallelEvaluation = false;

        protected P mPopulation = null;

        public S GlobalBestSolution = null;

        public delegate void SolutionEvaluatedHandle(S solution, int solution_index);
        public event SolutionEvaluatedHandle SolutionEvaluated;

        protected PopInitInstructionFactory<P, S> mPopInitInstructionFactory = null;
        protected CrossoverInstructionFactory<P, S> mCrossoverInstructionFactory = null;
        protected MutationInstructionFactory<P, S> mMutationInstructionFactory = null;
        protected SelectionInstructionFactory<P, S> mReproductionSelectionInstructionFactory = null;

        protected bool mInitialized=false;
        protected NondominatedPopulation<S> mArchive = null;

        protected IMOOProblem mProblem = null;

        protected int mMaxParallelTaskCount = 8;

        public int MaxParallelTaskCount
        {
            get { return mMaxParallelTaskCount; }
            set { mMaxParallelTaskCount = value; }
        }

        public S SolutionFactory
        {
            get { return mPopulation.SolutionFactory; }
        }

        public Population<S> Population
        {
            get { return mPopulation; }
        }

        public int PopulationSize
        {
            get { return mPopulation.PopulationSize; }
            set { mPopulation.PopulationSize = value; }
        }

        public int MaxGenerations
        {
            get { return mPopulation.MaxGenerations; }
            set { mPopulation.MaxGenerations = value; }
        }

        public bool ParallelEvaluation
        {
            get { return mParallelEvaluation; }
            set { mParallelEvaluation = value; }
        }

        public IMOOProblem Problem
        {
            get { return mProblem; }
        }

        public override bool IsTerminated
        {
            get 
            {
                return mCurrentGeneration >= mPopulation.MaxGenerations;
            }
        }

        protected virtual NondominatedPopulation<S> CreateArchive()
        {
            NondominatedPopulation<S> archive = new NondominatedPopulation<S>();
            //EpsilonBoxDominanceArchive archive = new EpsilonBoxDominanceArchive(0.1);
            return archive;
        }

        protected virtual P CreatePopulation()
        {
            return new P();
        }

        protected virtual MutationInstructionFactory<P, S> CreateMutationInstructionFactory()
        {
            return new MOOMutationInstructionFactory<P, S>();
        }

        protected virtual CrossoverInstructionFactory<P, S> CreateCrossoverInstructionFactory()
        {
            return new MOOCrossoverInstructionFactory<P, S>();
        }

        protected virtual PopInitInstructionFactory<P, S> CreatePopInitInstructionFactory()
        {
            return new MOOPopInitInstructionFactory<P, S>();
        }

        protected virtual SelectionInstructionFactory<P, S> CreateSelectionInstructionFactory()
        {
            return new SelectionInstructionFactory<P, S>();
        }

        public CrossoverInstruction<P, S> Crossover
        {
            set
            {
                mCrossoverInstructionFactory.CurrentInstruction = value;
            }
        }

        public SelectionInstruction<P, S> ReproductionSelection
        {
            set
            {
                mReproductionSelectionInstructionFactory.CurrentInstruction = value;
            }
        }

        public NSGAII(IMOOProblem problem)
        {
            mProblem = problem;

            mPopulation = CreatePopulation();
            mPopulation.Problem = mProblem;
            
            mArchive = CreateArchive();
            
            mMutationInstructionFactory = CreateMutationInstructionFactory();
            mCrossoverInstructionFactory = CreateCrossoverInstructionFactory();
            mPopInitInstructionFactory = CreatePopInitInstructionFactory();
            mReproductionSelectionInstructionFactory = CreateSelectionInstructionFactory();
        }

        public override void Initialize()
        {
            if (mInitialized)
            {
                return;
            }
            mInitialized = true;

            mPopulation.Clear();
            mPopInitInstructionFactory.Initialize(mPopulation);

            Evaluate(mPopulation);

            mCurrentGeneration = 1;
        }

        public virtual void Evaluate(Population<S> pop)
        {
            int solution_count=pop.SolutionCount;

            if (mParallelEvaluation)
            {
                S[] solutions = new S[solution_count];
                
                for (int i = 0; i < solution_count; ++i)
                {
                    solutions[i] = pop[i]; 
                }

                int parallel_task_batch_count = (int)(System.Math.Ceiling((double)PopulationSize / mMaxParallelTaskCount));

                S reported_solution=null;
                int reported_solution_index=0;
                
                for (int batch_index = 0; batch_index < parallel_task_batch_count; ++batch_index)
                {
                    int task_count = mMaxParallelTaskCount;
                    if (batch_index == parallel_task_batch_count - 1)
                    {
                        task_count = PopulationSize - (parallel_task_batch_count - 1) * mMaxParallelTaskCount;
                    }
                    Task[] tasks = new Task[task_count];
                    for (int i = 0; i < task_count; ++i)
                    {
                        int task_id = i;
                        int solution_index = batch_index * mMaxParallelTaskCount + i;
                        tasks[task_id] = Task.Factory.StartNew(() =>
                            {
                                S s = solutions[solution_index];
                                s.Evaluate();
                            });
                    }

                    Task.WaitAll(tasks);

                    for (int i = 0; i < task_count; ++i)
                    {
                        int solution_index = batch_index * mMaxParallelTaskCount + i;
                        S s = solutions[solution_index];

                        if (reported_solution == null)
                        {
                            reported_solution = s;
                            reported_solution_index = solution_index;
                        }

                        bool is_archivable = mArchive.Add(s);
                        if (mArchive.Count > mPopulation.Config.MaxArchive)
                        {
                            mArchive.Truncate(mPopulation.Config.MaxArchive);
                        }
                        if (is_archivable && s != reported_solution)
                        {
                            if( DistributionModel.GetUniform() < 0.5)
                            {
                                reported_solution = s;
                                reported_solution_index = solution_index;
                            }
                        }
                        if (!is_archivable && s != reported_solution)
                        {
                            s.ClearAttributes();
                        }
                    }
                }

                if (SolutionEvaluated != null)
                {
                    if (reported_solution != null)
                    {
                        SolutionEvaluated(reported_solution, reported_solution_index);
                    }
                }
            }
            else
            {
                for (int i = 0; i < solution_count; ++i)
                {
                    S s = pop[i];
                    s.Evaluate();
                    if (SolutionEvaluated != null)
                    {
                        SolutionEvaluated(s, i);
                    }

                    bool is_archivable = mArchive.Add(s);
                    if (mArchive.Count > mPopulation.Config.MaxArchive)
                    {
                        mArchive.Truncate(mPopulation.Config.MaxArchive);
                    }
                    if (!is_archivable)
                    {
                        s.ClearAttributes();
                    }
                }
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
                    int flag = 0;
                    if ((flag = CompareUtil.ConstraintCompare(s1, s2))==0)
                    {
                        if ((flag = CompareUtil.ParetoObjectiveCompare(s1, s2)) == 0)
                        {
                            flag = CompareUtil.CrowdingDistanceCompare(s1, s2);
                        }
                    }
                    
                    return flag;
                });

                List<S> children = mCrossoverInstructionFactory.Crossover(mPopulation, tournament_winners.ToArray());

                for (int i = 0; i < children.Count; ++i)
                {
                    mMutationInstructionFactory.Mutate(mPopulation, children[i]);
                    offspring.Add(children[i]);
                }
                
		    }

		    Evaluate(offspring);

            Merge1(offspring);

            mCurrentGeneration++;
        }

        protected void Merge2(Population<S> children)
        {
            int populationSize = mPopulation.Config.PopulationSize;

            Population<S> offspring = new Population<S>();

            for (int i = 0; i < populationSize; i++)
            {
                S s1 = children[i];
                S s2 = mPopulation[i];
                int flag = 0;
                if ((flag = CompareUtil.ConstraintCompare<S>(s1, s2)) == 0)
                {
                    if ((flag = CompareUtil.ParetoObjectiveCompare<S>(s1, s2)) == 0)
                    {
                        flag = CompareUtil.CrowdingDistanceCompare(s1, s2);
                    }
                }

                if (flag < 0)
                {
                    offspring.Add(children[i]);
                }
                else if (flag > 0)
                {
                    offspring.Add(mPopulation[i]);
                }
                else
                {
                    offspring.Add(children[i]);
                    offspring.Add(mPopulation[i]);
                }
            }

            mPopulation.Clear();

            mPopulation.Add(offspring);

            mPopulation.Prune(populationSize);
        }

        protected void Merge1(Population<S> children)
        {
            int populationSize = mPopulation.Config.PopulationSize;

            mPopulation.Add(children);

            mPopulation.Truncate(populationSize);
        }

        public int NondominatedArchiveSize
        {
            get
            {
                return mArchive.Count;
            }
        }

        public NondominatedPopulation<S> NondominatedArchive
        {
            get
            {
                return mArchive;
            }
        }
    }
}
