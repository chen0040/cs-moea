using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOEA.AlgorithmModels;
using MOEA.Benchmarks;
using MOEA.ComponentModels;
using MOEA.ComponentModels.SolutionModels;
using MOEA.Core.ComponentModels;

namespace MOEA
{
    class Program
    {
        static void Main(string[] args)
        {
            RunHAPMOEA();

        }

        static void RunGDE3()
        {
            GDE3<ContinuousVector> algorithm = new GDE3<ContinuousVector>(new TNKProblem());

            algorithm.PopulationSize = 100;


            algorithm.Initialize();

            while (!algorithm.IsTerminated)
            {
                algorithm.Evolve();
                Console.WriteLine("Current Generation: {0}", algorithm.CurrentGeneration);
                Console.WriteLine("Size of Archive: {0}", algorithm.NondominatedArchiveSize);
            }
            ContinuousVector finalSolution = algorithm.GlobalBestSolution;
        }

        static void RunHAPMOEA()
        {
            HAPMOEA<ContinuousVector> algorithm = new HAPMOEA<ContinuousVector>(new NDNDProblem());

            algorithm.Config.PopulationSize = 100;


            algorithm.Initialize();

            while (!algorithm.IsTerminated)
            {
                algorithm.Evolve();
                Console.WriteLine("Current Generation: {0}", algorithm.CurrentGeneration);
                Console.WriteLine("Size of Archive: {0}", algorithm.NondominatedArchiveSize);
            }
            NondominatedPopulation<ContinuousVector> paretoFront = algorithm.NondominatedArchive;
        }

        static void RunHybridGame()
        {
            HybridGame<ContinuousVector> algorithm = new HybridGame<ContinuousVector>(new NDNDProblem());

            algorithm.Config.PopulationSize = 100;


            algorithm.Initialize();

            while (!algorithm.IsTerminated)
            {
                algorithm.Evolve();
                Console.WriteLine("Current Generation: {0}", algorithm.CurrentGeneration);
                Console.WriteLine("Size of Archive: {0}", algorithm.NondominatedArchiveSize);
            }
            NondominatedPopulation<ContinuousVector> paretoFront = algorithm.NondominatedArchive;
        }

        static void RunNSGAII()
        {
            NSGAII<ContinuousVector> algorithm = new NSGAII<ContinuousVector>(new NDNDProblem());

            algorithm.PopulationSize = 100;

            algorithm.Initialize();

            while (!algorithm.IsTerminated)
            {
                algorithm.Evolve();
                Console.WriteLine("Current Generation: {0}", algorithm.CurrentGeneration);
                Console.WriteLine("Size of Archive: {0}", algorithm.NondominatedArchiveSize);
            }
            ContinuousVector finalSolution = algorithm.GlobalBestSolution;
        }
    }
}
