using System;
using System.Collections.Generic;
using System.Text;

namespace Nmli.Extended
{
    /// <summary>
    /// Evaluates the PDFs of a vector of 1D Gaussians
    /// </summary>
    /// <typeparam name="N"></typeparam>
    public class Vectorized1DGaussian<N> : ExtendingFunc<N>
    {
        private readonly Nmli.WithOffsets.IMultOffsets<N> wo_testLib
            = Nmli.WithOffsets.Libraries<N>.TestLib;

        public Vectorized1DGaussian(IMathLibrary<N> ml) : base(ml) { }

        /// <summary>
        /// Computes the prob density func of a vector of gaussians over a separate vector of test points.
        /// Output is the outer "product" of evaluating the gaussian at the test points, where
        /// the Gaussian form changes slowly (outer dim) and test points change quickly (inner dim).
        /// </summary>
        /// <param name="nPoints">Number of test points</param>
        /// <param name="x">De-meaned test points (length = nPoints)</param>
        /// <param name="nGaussians">Number of gaussians to evaluate</param>
        /// <param name="amplitude">Gaussian amplitudes (length = nGaussians)</param>
        /// <param name="variance">Input: Gaussian stdevs (length = nGaussians).  Output: 1/stdevs</param>
        /// <param name="output">Output: Every Gaussian evaluated at every point (length = nGaussians * nPoints)</param>
        public void ComputePDFs(int nPoints, N[] x,
            int nGaussians, N[] amplitude, N[] stdevs, N[] output)
        {
            int sz = nPoints * nGaussians;
            Array.Clear(output, 0, sz);

            vml.Inv(nGaussians, stdevs, stdevs);

            // build outer product...
            //        nRows,    nCols,   scalar, x, incX,   y,  incY, outer prod, lda
            blas.ger(nPoints, nGaussians,  _1,   x,   1,  stdevs, 1,  output,  nPoints);


            // square x/stdev quotient...
            vml.Sqr(sz, output, output);

            // scale by -0.5...
            blas.scal(sz, sml.OfDouble(-0.5), output, 1);

            // exponentiate
            vml.Exp(sz, output, output);


            throw new NotImplementedException();

            // scale up each multi-point, single-gaussian column by the appropriate amplitude
            for (int g = 0; g < nGaussians; g++)
            {
                WithOffsets.OA<N> targetSection = WithOffsets.OA.O(output, g * nPoints);
                //blas.scal(nPoints, amplitude[g], targetSection, 1);
            }

        }
    }
}
