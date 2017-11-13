using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MOEA.Core.ProblemModels;
using MOEA.Core.ComponentModels;
using MOEA.ComponentModels.SolutionModels;

namespace MOEA.Benchmarks
{
    public class SYMPARTProblem : IMOOProblem
    {
        private const double a = 1;
        private const double b = 10;
        private const double c = 8;
        private double c1;
        private double c2;
        private double b1;

        public SYMPARTProblem()
        {
	        c1 = (a+c/2);
	        c2 = (c+2*a);
	        b1 = (b/2);
        }

         public bool IsMaximizing()
        {
            return false;
        }

        public bool IsFeasible(MOOSolution s)
        {
            return true;
        }

        public double CalcObjective(MOOSolution s, int objective_index)
        {
            ContinuousVector x = (ContinuousVector)s;
	        double f=0;
	        switch(objective_index)
	        {
	        case 0:
		        SYMPART_f1(x, out f, 6, 3);
		        break;
	        case 1: 
		        SYMPART_f2(x, out f, 6, 3);
		        break;
	        }
	        return f;
        }

        public void SYMPART_f1(ContinuousVector x, out double f1, int nx,int n_obj)
        {
	        double omega =  System.Math.PI / 4.0;
	        double si = System.Math.Sin(omega);
	        double co = System.Math.Cos(omega);
	        // copy array to preserve original values
	        double[] localX = new double[nx];
	        for(int index=0; index < nx; ++index)
	        { 
		        localX[index]=x[index];
	        }

	        int dim;
	        double h1;
	        // hide original x
	        // x = localX;
	        //rotate( nx, x );
	        for( dim=0; dim+1 < nx; dim+=2 ) 
	        {
		        h1 = localX[dim];
		        localX[dim] = co * h1 - si * localX[dim+1];
		        localX[dim+1] = si * h1 + co * localX[dim+1];
	        }
	        double x1 = localX[0], x2 = localX[1];
	        int i, j,xnum;
	        // find tile
	        findTile(x1, x2, out i, out j);
	        // restrict to 9 tiles
	        if (i > 1) i = 1; else if (i < -1) i = -1;
	        if (j > 1) j = 1; else if (j < -1) j = -1;
	        // Get values
	        f1 = 0;
	        for( xnum=0; xnum<nx; xnum++ )
	        {
		        x1 = localX[xnum];
		        if( (xnum % 2) == 0 )
		        {
			        f1 += System.Math.Pow(x1+a-i*c2,2);
		        } 
		        else 
		        {
                    f1 += System.Math.Pow(x1 - j * b, 2);
		        }
	        }
	        f1 /= nx;
        }

        public void SYMPART_f2(ContinuousVector x, out double f2,int nx,int n_obj)
        {
	        double omega =  System.Math.PI / 4.0;
	        double si = System.Math.Sin(omega), co = System.Math.Cos(omega);
	        // copy array to preserve original values
	        double[] localX = new double[nx];
	        for(int index=0; index < nx; ++index)
	        { 
		        localX[index]=x[index];
	        }
	        int dim;
	        double h1;
	        // hide original x
	        // x = localX;
	        //rotate( nx, x );
	        for( dim=0; dim+1 < nx; dim+=2 ) 
	        {
		        h1 = localX[dim];
		        localX[dim] = co * h1 - si * localX[dim+1];
		        localX[dim+1] = si * h1 + co * localX[dim+1];
	        }
	        double x1 = localX[0], x2 = localX[1];
	        int i, j,xnum;
	        // find tile
	        findTile(x1, x2, out i, out j);
	        // restrict to 9 tiles
	        if (i > 1) i = 1; else if (i < -1) i = -1;
	        if (j > 1) j = 1; else if (j < -1) j = -1;
	        // Get values
	
	        f2 = 0;
	        for( xnum=0; xnum<nx; xnum++ )
	        {
		        x1 = localX[xnum];
		        if( (xnum % 2) == 0 )
		        {
			
			        f2 += System.Math.Pow(x1-a-i*c2,2);
		        } 
		        else 
		        {
			
			        f2 += System.Math.Pow(x1-j*b,2);
		        }
	        }
	
	        f2 /= nx;
        }

        /* these variables are needed to speed up rotation */
        public void findTile(double x1, double x2, out int t1, out int t2) 
        {
            t1 = t2 = 0;
	        double xx1 = (x1 < 0) ? -x1 : x1;
	        double xx2 = (x2 < 0) ? -x2 : x2;
	        t1 = (xx1 < c1) ? 0 : ((int)System.Math.Ceiling((xx1-c1)/c2));
	        t2 = (xx2 < b1) ? 0 : ((int)System.Math.Ceiling((xx2-b1)/b));
	        if (x1 < 0) t1 = -(t1);
	        if (x2 < 0) t2 = -(t2);
        }

        /* returns tile number between 0 and 8
        returns - 1 if out of any tile, function does
        not depend on objFct! */
        public int findTileSYMPART(double x1, double x2) 
        {
	        int i, j,dim;
            double[] x = new double[2];
            double h1;	
	        double omega =  System.Math.PI / 4.0;
            double si = System.Math.Sin(omega);
            double co = System.Math.Cos(omega);
	        x[0] = x1;
	        x[1] = x2;
	        //rotate( 2, x );
	        for( dim=0; dim+1 < 2; dim+=2 ) 
	        {
		        h1 = x[dim];
		        x[dim] = co * h1 - si * x[dim+1];
		        x[dim+1] = si * h1 + co * x[dim+1];
	        }
	        findTile(x[0], x[1], out i, out j);
	        // restrict to 9 tiles
	        if (System.Math.Abs(i) > 1 || System.Math.Abs(j) > 1) return -1;
	        return (i + 1) * 3 + (j + 1);
        }

        public int GetObjectiveCount()
        {
            return 2;
        }
   
        public int GetDimensionCount()
        {
            return 6;
        }

        public double GetLowerBound(int index) 
        {
            return -20;
        }

        public double GetUpperBound(int index)
        {
            return 20;
        }
    }
}
