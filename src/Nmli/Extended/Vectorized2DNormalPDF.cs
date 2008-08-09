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
        private readonly N[] Onex2;

        public Vectorized2DNormalPDF(IMathLibrary<N> ml) : base(ml)
        {
            Onex2 = new N[] { _1, _1 };
        }

        /// <summary>
        /// Adds the pair (constX, constY) to every pair (a[2*i], a[2*i+1])
        /// </summary>
        /// <param name="n">Number of pairs</param>
        /// <param name="a">Contains pairs to be translated (length=2n)</param>
        /// <param name="constX">Added to first coordinate of all pairs</param>
        /// <param name="constY">Added to second coordinate of all pairs</param>
        public void AddConstantPairwise(int n, N[] a, N constX, N constY)
        {
            N[] temp = new N[1];

            // add first term
            temp[0] = constX;
            blas.axpy(n, _1, temp, 0, a, 2);

            // add second term
            temp[0] = constY;
            wo_blas.axpy(n, _1, temp, 0, oa(a, 1), 2);
        }




        [ThreadStatic]
        static Workspace<N> scratchProvider;

        private static readonly double ln2pi = Math.Log(2 * Math.PI);

        /// <summary>
        /// output[i] = scalar * ( diff2[2*i]^2  + diff2[2*i+1]^2 )
        /// </summary>
        public void RSquareds(int n, N[] diff2, N[] output, N scalar)
        {
            N[] temp = Workspace<N>.Get(ref scratchProvider, 2 * n);

            // square elements of diff2 into temp
            blas.sbmv(UpLo.Lower, 2*n, 0, _1, diff2, 1, diff2, 1, _0, temp, 1);

            blas.gemv(Transpose.Trans, 2, n, scalar, temp, 2, Onex2, 1, _0, output, 1);
        }

        public void ComputePDFs(int n, N[] diff2, double variance, N[] output)
        {
            ComputePDFs(n, diff2, variance, output, false);
        }

        static readonly IVml<N> mvml = (IVml<N>)new Nmli.Managed.ManagedVml();

        
        /// <summary>
        /// Turns out that the MKL's Exp is slower than our managed version for small arrays.
        /// </summary>
        public void ComputePDFs(int n, N[] diff2, double variance, N[] output, bool managedExp)
        {
            N scalar1 = sml.OfDouble(-1 / (2 * variance));

            N scalar2 = sml.OfDouble(-1 * Math.Log(2 * Math.PI * variance));

            // puts scaled squared distances into output
            RSquareds(n, diff2, output, scalar1);

            // adds scalar2 to every element of output
            blas.axpy(n, scalar2, Onex2, 0, output, 1);

            if (managedExp)
                mvml.Exp(n, output, output);
            else
                vml.Exp(n, output, output);
        }

    }
}
