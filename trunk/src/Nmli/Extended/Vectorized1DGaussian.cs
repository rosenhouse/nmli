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
        private readonly N[] Onex2;

        public Vectorized1DGaussian(IMathLibrary<N> ml)
            : base(ml)
        {
            Onex2 = new N[] { _1, _1 };
        }


        [ThreadStatic]
        static Workspace<N> scratchProvider;

        private static readonly double ln2pi = Math.Log(2 * Math.PI);


        public void SquareScaleAdd(int n, N[] toSquare, N alpha, N[] addResultToThis, N beta)
        {
            blas.sbmv(UpLo.Lower, n, 0, alpha, toSquare, 1, toSquare, 1, beta, addResultToThis, 1);
        }

        /// <summary>
        /// output[i] = scalar * ( diff2[2*i]^2  + diff2[2*i+1]^2 )
        /// </summary>
        public void RSquareds(int n, N[] diff2, N[] output, N scalar)
        {
            if (scratchProvider == null)
                scratchProvider = new Workspace<N>();

            N[] temp = scratchProvider.Get(2 * n);

            SquareScaleAdd(2 * n, diff2, _1, temp, _0);

            blas.gemv(Transpose.Trans, 2, n, scalar, temp, 2, Onex2, 1, _0, output, 1);
        }


        /// <summary>
        /// Computes the prob density func of a vector of gaussians over a separate vector of test points.
        /// Output is the outer "product" of evaluating the gaussian at the test points
        /// </summary>
        /// <param name="nPoints">Number of test points</param>
        /// <param name="y">De-meaned test points (length = nPoints)</param>
        /// <param name="nGaussians">Number of gaussians to evaluate</param>
        /// <param name="amplitude">Gaussian amplitudes (length = nGaussians)</param>
        /// <param name="variance">Input: Gaussian stdevs (length = nGaussians).  Output: 1/stdevs</param>
        /// <param name="output">Output: Every Gaussian evaluated at every point (length = nPoints * nGaussians)</param>
        public void ComputePDFs(int nPoints, N[] y,
            int nGaussians, N[] amplitude, N[] stdevs, N[] output)
        {
            int sz = nPoints * nGaussians;
            Array.Clear(output, 0, sz);

            vml.Inv(nGaussians, stdevs, stdevs);
            
            // build outer product...
            //    nRows (inner dim), nCols, scalar,    x,   incX, y, incY,outer prod, lda
            blas.ger(nGaussians,    nPoints, _1,    stdevs,   1,  y,   1,  output,  nGaussians);

            // square y/stdev quotient...
            vml.Sqr(sz, output, output);

            // scale by -0.5...
            blas.scal(sz, sml.OfDouble(-0.5), output, 1);

            // exponentiate
            vml.Exp(sz, output, output);


            throw new NotImplementedException();
            // rescale by amplitude
            //blas.scal(sz, 
            // maybe ?lagtm


        }
    }
}
