using System;
using System.Collections.Generic;
using System.Text;

namespace Nmli.Extended
{
    /// <summary>
    /// Evaluates the probability density function of a multidimentional normal distribution
    /// </summary>
    /// <typeparam name="T">The numeric type</typeparam>
    public class MultinormalPdf<N> : Extensions1<N>
    {
        readonly CholeskyInverter<N> inverter;

        public MultinormalPdf(IMathLibrary<N> ml) : base(ml)
        {
            this.inverter = new CholeskyInverter<N>(ml);
        }


        [ThreadStatic]
        static Workspace<N> scratchProvider;

        static readonly double ln2pi = Math.Log(2 * Math.PI);

        /// <summary>
        /// Computes the natural log of the probability density function of a multi-normal distribution
        /// </summary>
        /// <param name="x">The point at which to compute the probability density.  Unchanged on exit.</param>
        /// <param name="mean">On input: The mean of the distribution.  On exit, (mean - x)</param>
        /// <param name="covariance">The covariance matrix of the distribution.  On exit: The inverse of this covariance matrix </param>
        /// <returns>The natural log of the PDF</returns>
        /// <remarks>This function computes the full inverse of the covariance matrix.
        /// LogPDF_Fast gets the same answer without computing the full inverse.
        /// </remarks>
        public double LogPDF(N[] x, N[] mean, N[] covariance)
        {
            int n = x.Length;
            if (mean.Length < n || covariance.Length < n * n)
                throw new ArgumentException("The mean and covariance arrays must be of appropriate size!");


            N[] tempVector = Workspace<N>.Get(ref scratchProvider, n);

            // inverts covariance in-place and returns the log of the determinant
            double lnDet = inverter.Invert(n, covariance);

            // compute difference of x and mean
            blas.axpy(n, sml.Negate(_1), x, 1, mean, 1);

            // 1 * covariance * mean + 0 * tempVector stored into tempVector
            blas.symv(UpLo.Upper, n, _1, covariance, n, mean, 1, _0, tempVector, 1);

            // mean . tempvector dotproduct
            N d = blas.dot(n, mean, 1, tempVector, 1);

            return -0.5 * (n * ln2pi + lnDet + sml.ToDouble(d));
        }


        /// <summary>
        /// Computes the natural log of the probability density function of a multi-normal distribution
        /// </summary>
        /// <param name="x">The point at which to compute the probability density.  Unchanged on exit.</param>
        /// <param name="mean">On input: The mean of the distribution.  On exit, (mean - x)</param>
        /// <param name="covariance">The covariance matrix of the distribution.  On exit: The inverse of this covariance matrix </param>
        /// <returns>The natural log of the PDF</returns>
        /// <remarks>
        /// This function is a somewhat faster, slightly less acurate version of LogPDF
        /// </remarks>
        public double LogPDF_Fast(N[] x, N[] mean, N[] covariance)
        {
            int n = x.Length;
            if (mean.Length < n || covariance.Length < n * n)
                throw new ArgumentOutOfRangeException("The mean and covariance arrays must have size n=" + n + " and n^2, respectively.");


            // compute difference of x and mean, storing it in mean
            blas.axpy(n, sml.Negate(_1), x, 1, mean, 1);

            // make a copy
            N[] tempVector = Workspace<N>.Get(ref scratchProvider, n);
            blas.copy(n, mean, 1, tempVector, 1);


            // Let A = covariance
            //   v = -0.5 * (x-m)^T A^-1 (x-m)

            // First compute: y = A^-1 * (x-m)
            // Which is equivalent to solving for y in:
            //    A*y = (x-m)

            int errCode = lapack.posv(UpLo.Upper, n, 1, covariance, n, tempVector, n);
            if (errCode != 0)
                throw new Exception("Failed with error " + errCode + " on posv solve attempt.");

            double d = sml.ToDouble(blas.dot(n, mean, 1, tempVector, 1));

            // tempVector now contains y, and upper-triangle of covariance contains Cholesky-factor
            double lnDet = inverter.LnDeterminantOfFactoredMatrix(n, covariance);  // ln[determinant]

            return -0.5 * (n * ln2pi + lnDet + d);
        }


        
        public double LogScaledSSE(N[] diffs, N[] scalingFactors)
        {
            int T = diffs.Length;
            N[] tempVector = Workspace<N>.Get(ref scratchProvider, T);

            vml.Sqr(T, scalingFactors, tempVector);

            vml.Ln(T, tempVector, tempVector);  // inplace natural log
            N mlndet = extras.Sum(T, tempVector); // add up log terms
            double lndet = -sml.ToDouble(mlndet);  
            if (double.IsInfinity(lndet) || double.IsNaN(lndet))
                throw new Exception("Determinant isn't real, so the matrix is singular!  We can't invert it.");


            // 1 * scalingFactor * difs + 0 * tempVector stored into tempVector
            blas.sbmv(UpLo.Upper, T, 0, _1, scalingFactors, 1, diffs, 1, _0, tempVector, 1);

            // diffs . tempvector dotproduct
            N d = blas.dot(T, diffs, 1, tempVector, 1);

            return -0.5 * (T * ln2pi + lndet + sml.ToDouble(d));
        }


        public double UncorrelatedLogPDF(N[] x, N[] mean, N[] variance)
        {
            int T = x.Length;
            if (mean.Length < T || variance.Length < T)
                throw new ArgumentException("The mean and covariance arrays must be of appropriate size!");

            N[] tempVector = Workspace<N>.Get(ref scratchProvider, T);

            vml.Ln(T, variance, tempVector);
            double lnDet = sml.ToDouble(extras.Sum(T, tempVector));
            // ln[determinant] = ln[prod variance diagonal x's] = sum_diagonal ln[x]

            // inverse of diagonal matrix just inverts the elements along diagonal
            vml.Inv(T, variance, variance);

            // compute difference of x and mean
            blas.axpy(T, sml.Negate(_1), x, 1, mean, 1);

            // 1 * variance * mean + 0 * tempVector stored into tempVector
            blas.sbmv(UpLo.Upper, T, 0, _1, variance, 1, mean, 1, _0, tempVector, 1);
 
            // mean . tempvector dotproduct
            N d = blas.dot(T, mean, 1, tempVector, 1);

            return -0.5 * (T * ln2pi + lnDet + sml.ToDouble(d));
        }

    }
}
