using System;


namespace Nmli.Experimental
{
    public class TruncatedNormalSampler
    {

        [ThreadStatic]
        static Random rng;
        static Random Rng
        {
            get
            {
                if (rng == null)
                    rng = new Random();

                return rng;
            }
        }



        static readonly double sqrt2 = Math.Sqrt(2);

        public static double CDF(double mean, double stdev, double x)
        {
            // x -> z
            double z = (x - mean) / (sqrt2 * stdev);

            // z -> y
            double y = 0;
            Nmli.Mkl.ExclusiveExterns.AsRefs.vdErf(1, ref z, ref y);

            // y -> q
            double q = 0.5 * (1 + y);
            return q;
        }

        public static double InvCDF(double mean, double stdev, double q)
        {
            // q -> y
            double y = 2 * q - 1;

            // y -> z
            double z = 0;
            Nmli.Mkl.ExclusiveExterns.AsRefs.vdErfInv(1, ref y, ref z);

            // z -> x
            double x = z * (sqrt2 * stdev) + mean;
            return x;
        }

        static bool throwOnErrors = false;
        public static bool ThrowOnErrors { get { return throwOnErrors; } set { throwOnErrors = value; } }

        /// <summary>
        /// Maps a Uniform(0,1] random variable to a truncated normal.  Note this function is deterministic.
        /// </summary>
        /// <param name="mean">Mean of untruncated normal</param>
        /// <param name="stdev">Stdev of untruncated normal</param>
        /// <param name="minValue">Lower bound of support of truncated distribution</param>
        /// <param name="uniformSample">The sample to map, should be on (0,1]</param>
        /// <returns>A truncated normal sample</returns>
        public static double UniformToTruncatedNormal(double mean, double stdev, double minValue, double uniformSample)
        {
            double a = 1 - CDF(mean, stdev, minValue); // P[X >= minValue]

            if (a == 0)
            {
                if (throwOnErrors)
                    throw new ArgumentOutOfRangeException("minValue", 
                        "The given normal distribution has insufficient mass above minValue");
                else
                    return minValue;
            }

            double restrictedSample = a * (1 - uniformSample); // sample from Uniform (0, a]
            double toInvert = 1 - restrictedSample; // sample from Uniform [1-a, 1)

            double mapped = InvCDF(mean, stdev, toInvert);
            return Math.Max(mapped, minValue); // because sometimes errors propogate through
            

        }

        /// <summary>
        /// Generates a random sample from a truncated normal random variable
        /// </summary>
        /// <param name="mean">Mean of untruncated normal</param>
        /// <param name="stdev">Stdev of untruncated normal</param>
        /// <param name="minValue">Lower bound of support of truncated distribution</param>
        /// <returns>A random sample from the truncated normal random variable</returns>
        public static double SampleTruncatedNormal(double mean, double stdev, double minValue)
        {
            double uniformSample = 1 - Rng.NextDouble(); // sample from Uniform (0, 1]
            return UniformToTruncatedNormal(mean, stdev, minValue, uniformSample);
        }
    }
}
