using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOEA.AlgorithmModels;
using MOEA.Benchmarks;
using MOEA.ComponentModels.SolutionModels;
using MOEA.Core.ComponentModels;

namespace MOEA
{
    class Program
    {
        static void Main(string[] args)
        {
            RunHAPMOEA<ContinuousVector>();

        }

        static void RunGDE3<S>()
            where S : ContinuousVector, new()
        {
            GDE3<S> algorithm = new GDE3<S>(new TNKProblem());

            algorithm.PopulationSize = 100;


            algorithm.Initialize();

            while (!algorithm.IsTerminated)
            {
                algorithm.Evolve();
                Console.WriteLine("Current Generation: {0}", algorithm.CurrentGeneration);
                Console.WriteLine("Size of Archive: {0}", algorithm.NondominatedArchiveSize);
            }
        }

        static void RunHAPMOEA<S>()
            where S : MOOSolution, new()
        {
            HAPMOEA<S> algorithm = new HAPMOEA<S>(new NDNDProblem());

            algorithm.Config.PopulationSize = 100;


            algorithm.Initialize();

            while (!algorithm.IsTerminated)
            {
                algorithm.Evolve();
                Console.WriteLine("Current Generation: {0}", algorithm.CurrentGeneration);
                Console.WriteLine("Size of Archive: {0}", algorithm.NondominatedArchiveSize);
            }
        }

        static void RunHybridGame<S>()
            where S : ContinuousVector, new()
        {
            HybridGame<S> algorithm = new HybridGame<S>(new NDNDProblem());

            algorithm.Config.PopulationSize = 100;


            algorithm.Initialize();

            while (!algorithm.IsTerminated)
            {
                algorithm.Evolve();
                Console.WriteLine("Current Generation: {0}", algorithm.CurrentGeneration);
                Console.WriteLine("Size of Archive: {0}", algorithm.NondominatedArchiveSize);
            }
        }

        static void RunNSGAII<S>()
            where S : MOOSolution, new()
        {
            NSGAII<S> algorithm = new NSGAII<S>(new NDNDProblem());

            algorithm.PopulationSize = 100;

            algorithm.Initialize();

            while (!algorithm.IsTerminated)
            {
                algorithm.Evolve();
                Console.WriteLine("Current Generation: {0}", algorithm.CurrentGeneration);
                Console.WriteLine("Size of Archive: {0}", algorithm.NondominatedArchiveSize);
            }
        }
    }
}
