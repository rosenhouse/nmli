using System;
using System.Collections.Generic;
using System.Text;

namespace Nmli.Extended
{
    /// <summary>
    /// Evaluates the probability density function of a multidimentional normal distribution
    /// </summary>
    /// <typeparam name="T">The numeric type</typeparam>
    public class MultinormalPdf<N> : ExtendingFunc<N>
    {
        readonly CholeskyInverter<N> inverter;

        public MultinormalPdf(IMathLibrary<N> ml) : base(ml)
        {
            this.inverter = new CholeskyInverter<N>(ml);
        }


        [ThreadStatic]
        static Workspace<N> scratch1;

        [ThreadStatic]
        static Workspace<N> scratch2;

        static readonly double ln2pi = Math.Log(2 * Math.PI);

        public double LogPDF_FastFull(N[] x, N[] mean, N[] covariance)
        {
            return LogPDF_FastFull(x, mean, covariance, false);
        }

        /// <summary>
        /// Computes the natural log of the probability density function of a multi-normal distribution
        /// </summary>
        /// <param name="x">The point at which to compute the probability density.  Unchanged on exit.</param>
        /// <param name="mean">The mean of the distribution.  Unchanged on exit.</param>
        /// <param name="covariance">The covariance matrix of the distribution.  On exit: The inverse of this covariance matrix </param>
        /// <returns>The natural log of the PDF</returns>
        /// <remarks>
        /// This function uses LAPACK's POSV to solve a system of equations, but does
        /// not refine the solution.  So results are not as accurate as possible.
        /// </remarks>
        /// <remarks>Complexity: O(n^3)  where n=Length of x</remarks>
        public double LogPDF_FastFull(N[] x, N[] mean, N[] covariance, bool debugOut)
        {
            int n = x.Length;
            if (mean.Length < n || covariance.Length < n * n)
                throw new ArgumentOutOfRangeException("The mean and covariance arrays must have size n=" + n + " and n^2, respectively.");

            // make a copy
            N[] tempVector1 = Workspace<N>.Get(ref scratch1, n);
            N[] tempVector2 = Workspace<N>.Get(ref scratch2, n);

            // compute difference of x and mean, storing it in tempVector1
            Array.Copy(mean, tempVector1, n);
            blas.axpy(n, sml.Negate(_1), x, 1, tempVector1, 1);
            Array.Copy(tempVector1, tempVector2, n);


            // Let A = covariance
            //   v = -0.5 * (x-m)^T A^-1 (x-m)

            // First compute: y = A^-1 * (x-m)
            // Which is equivalent to solving for y in:
            //    A*y = (x-m)
            int errCode = lapack.posv(UpLo.Upper, n, 1, covariance, n, tempVector1, n);
            if (errCode != 0)
                throw new Exception("Failed with error " + errCode + " on posv solve attempt.");
            // tempVector1 now contains solutions y


            double d = sml.ToDouble(blas.dot(n, tempVector2, 1, tempVector1, 1));

            // tempVector now contains y, and upper-triangle of covariance contains Cholesky-factor
            double lnDet = inverter.LnDeterminantOfFactoredMatrix(n, covariance);  // ln[determinant]

            if (debugOut)
            {
                Console.WriteLine("Multinormal PDF eval result: ln[det[cov]] = {0} ,  (x-m)^T cov^-1 (x-m) = {2}",
                    lnDet, d);

            }

            return -0.5 * (n * ln2pi + lnDet + d);
        }




        [ThreadStatic]
        static Workspace<N> scratch3;


        /// <summary>
        /// Computes the natural log of the probability density function of a
        /// multi-normal distribution, but ignores the off-diagonal elements
        /// of the covariance matrix.  This result is equivalent to evaluating the 
        /// product of independent univariate normals.
        /// </summary>
        /// <param name="x">The point at which to compute the probability density.  Unchanged on exit.</param>
        /// <param name="mean">On input: The mean of the distribution.  On exit, (mean - x)</param>
        /// <param name="covariance">The covariance matrix of the distribution.  Unchanged on exit. </param>
        /// <returns>The natural log of the PDF</returns>
        /// <remarks>Complexity: O(n)  where n = Length[x]</remarks>
        public double LogPDF_IgnoreOffDiagonals(N[] x, N[] mean, N[] covariance)
        {
            int n = x.Length;
            N[] variance = Workspace<N>.Get(ref scratch3, n);

            // copy the diagonal of covariance into the vector variance
            blas.copy(n, covariance, n + 1, variance, 1);

            return UncorrelatedLogPDF(x, mean, variance);
        }


        /// <summary>
        /// Computes the natural log of the probability density function of 
        /// multi-normal distribution, where the off-diagonal elements are assumed = 0.
        /// This result is equivalent to evaluating the product of
        /// independent univariate normals.
        /// </summary>
        /// <param name="x">Point at which to evaluate the distribution</param>
        /// <param name="mean">Mean</param>
        /// <param name="varianceVector">Vector of autovariances.  Same lenght as x and mean</param>
        /// <returns>Natural log of the PDF</returns>
        /// <remarks>Complexity: O(n)  where n=Length of x</remarks>
        double UncorrelatedLogPDF(N[] x, N[] mean, N[] varianceVector)
        {
            int T = x.Length;
            if (mean.Length < T || varianceVector.Length < T)
                throw new ArgumentException("The mean and covariance arrays must be of appropriate size!");

            N[] tempVector1 = Workspace<N>.Get(ref scratch1, T);
            N[] tempVector2 = Workspace<N>.Get(ref scratch2, T);

            vml.Ln(T, varianceVector, tempVector1);
            double lnDet = sml.ToDouble(extras.Sum(T, tempVector1));
            // ln[determinant] = ln[prod variance diagonal x's] = sum_diagonal ln[x]

            // tempVector2 := mean - x
            vml.Sub(T, mean, x, tempVector2);

            
            // divide difference by variance elementwise and store in tempVector
            vml.Div(T, tempVector2, varianceVector, tempVector1);

            /*  we can replace these two calls (invert, then multiply) with a single
             *  call to divide, above
            // inverse of diagonal matrix just inverts the elements along diagonal
            //vml.Inv(T, variance, variance);

            // 1 * variance * mean + 0 * tempVector stored into tempVector
            //blas.sbmv(UpLo.Upper, T, 0, _1, variance, 1, mean, 1, _0, tempVector, 1);
            */


            // mean . tempvector dotproduct
            N d = blas.dot(T, tempVector2, 1, tempVector1, 1);

            return -0.5 * (T * ln2pi + lnDet + sml.ToDouble(d));
        }

    }
}
