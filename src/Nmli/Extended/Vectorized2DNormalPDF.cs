using System;
using System.Collections.Generic;
using System.Text;

namespace Nmli.Extended
{
    /// <summary>
    /// Evaluates the PDFs of a vector of symmetric 2D Normal distributions with a single common variance.
    /// </summary>
    /// <typeparam name="N"></typeparam>
    public class Vectorized2DNormalPDF<N> : ExtendingFunc<N>
    {
        private readonly N[] One1;

        public Vectorized2DNormalPDF(IMathLibrary<N> ml)
            : base(ml)
        {
            One1 = new N[] { _1, _1 };
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

            N[] temp = scratchProvider.Get(2*n);

            SquareScaleAdd(2 * n, diff2, _1, temp, _0);

            blas.gemv(Transpose.Trans, 2, n, scalar, temp, 2, One1, 1, _0, output, 1);
        }

        public void ComputePDFs(int n, N[] diff2, double variance, N[] output)
        {
            N scalar1 = sml.OfDouble(-1 / (2 * variance));
            
            N scalar2 = sml.OfDouble(-1 * Math.Log(2 * Math.PI * variance));

            // puts scaled squared distances into output
            RSquareds(n, diff2, output, scalar1);

            // adds scalar2 to every element of output
            blas.axpy(n, scalar2, One1, 0, output, 1);

            vml.Exp(n, output, output);
        }

        /*
         * 
avgx,avgy  ;  valx,valy

diffx=avgx-valx
diffy=avgy-valy

sqr_dist=diffx^2+diffy^2
scale1*Exp(scale0*sqr_dist)
         * */
    }
}
