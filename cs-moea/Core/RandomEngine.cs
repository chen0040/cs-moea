using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Statistics;

namespace MOEA.Core
{
    public class RandomEngine
    {
        private static Gaussian mGaussian = new Gaussian();


        public static double Gauss(double mu, double sigma)
        {
            return mu + mGaussian.Next() * sigma;
        }

        public static double NextDouble()
        {
            return DistributionModel.GetUniform();
        }

        public static int NextInt(int upper_bound)
        {
            return (int)System.Math.Floor(NextDouble() * upper_bound);
        }

        public static bool NextBoolean()
        {
            return NextDouble() < 0.5;
        }
    }
}
