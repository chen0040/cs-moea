# cs-moea

Multi-Objective Evolutionary Algorithms implemented in .NET

# Install

```bash
Install-Package cs-moea -Version 1.0.1
```

# Features

The following MOEAs are supported:

* MOEAD
* NSGA-II
* GDE-3
* HAP-MOEA
* Hybrid-Game

The library supports both multi-objective and multi-constraints optimization problem in which the solutions are continuous vectors.

# Usage

Please refer to the sample codes in the cs-moea-samples project for how to use the library to solve various optimization problems. 

The cs-moea-samples-gui-winforms project shows the demo of the multi-objective optimization using these algorithm with a GUI that shows the pareto front of the MOEA results. A number of benchmarks
are included for comparing various MOEA implementations:

* NDND
* NGPD
* TNK
* OKA2
* SYMPART

The details these implementations can be found in MOEA.Benchmarks namespace of the cs-moea project.

The section below provides some details on how to do this using various MOEAs.

## NSGA-II to solve NDND 

The following sample codes show how to use NSGA-II to solve the NDND multi-objective optimization problem:

```cs 
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
NondominatedPopulation<ContinuousVector> paretoFront = algorithm.NondominatedArchive;
```

Where the NDNDProblem class is defined as below:

```cs 
public class NDNDProblem: IMOOProblem
{
	public int GetObjectiveCount()
	{
		return 2;
	}

	public int GetDimensionCount()
	{
		return 2;
	}

	public bool IsFeasible(MOOSolution s)
	{
		return true;
	}

	public bool IsMaximizing()
	{
		return false;
	}

	public double CalcObjective(MOOSolution s, int objective_index)
	{
		ContinuousVector x = (ContinuousVector)s;

		double f1 = 1 - System.Math.Exp((-4) * x[0]) * System.Math.Pow(System.Math.Sin(5 * System.Math.PI * x[0]), 4);
		if (objective_index == 0)
		{
			return f1;
		}
		else
		{
			double f2, g, h;
			if (x[1] > 0 && x[1] < 0.4)
				g = 4 - 3 * System.Math.Exp(-2500 * (x[1] - 0.2) * (x[1] - 0.2));
			else
				g = 4 - 3 * System.Math.Exp(-25 * (x[1] - 0.7) * (x[1] - 0.7));
			double a = 4;
			if (f1 < g)
				h = 1 - System.Math.Pow(f1 / g, a);
			else
				h = 0;
			f2 = g * h;
			return f2;
		}
	}

	public double GetUpperBound(int dimension_index)
	{
		return 1;
	}

	public double GetLowerBound(int dimension_index)
	{
		return 0;
	}
}
```

## GDE3 to solve NDND

The following sample codes show how to use GDE-3 to solve the NDND multi-objective optimization problem:

```cs 
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
NondominatedPopulation<ContinuousVector> paretoFront = algorithm.NondominatedArchive;
```

## HAP-MOEA 

The following sample codes show how to use HAP-MOEA to solve the NDND multi-objective optimization problem:

```cs 
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
```

## Hybrid-Game

The following sample codes show how to use Hybrid-Game to solve the NDND multi-objective optimization problem:

```cs 

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
```