using System;


namespace Nmli.Mkl
{
    using Nmli.WithOffsets;

    interface IExtras<N>
    {
        /// <summary>
        /// Replaces y with the elements of a, rounded to the nearest integer
        /// </summary>
        /// <param name="n">Number of elements to round</param>
        /// <param name="a">Elements to be rounded (length=n)</param>
        /// <param name="y">Buffer to hold output (length=n)</param>
        void Round(int n, N[] a, N[] y);


        
    }

    unsafe class MklExtras : IExtras<float>, IExtras<double>
    {
        public void Round(int n, float[] a, float[] y) { ExclusiveExterns.AsArrays.vsRound(n, a, y); }
        public void Round(int n, double[] a, double[] y) { ExclusiveExterns.AsArrays.vdRound(n, a, y); }



        
        
    }

    public unsafe static class ExclusiveExtras<N>
    {
        static readonly IVml<N> vml = (IVml<N>)(new Nmli.WithOffsets.Mkl.Vml());

        static OA<N> oa(N[] array, int offset) { return OA.O<N>(array, offset); }

        /// <summary>
        /// Multiplies a diagonal matrix by a rectangular matrix:
        ///    D * R -> T
        /// </summary>
        /// <param name="n">Side length of the diagonal matrix, and num rows of the rectangular matrix</param>
        /// <param name="m">Num cols of the rectangular matrix</param>
        /// <param name="diag">The elements of the diagonal matrix (length = n)</param>
        /// <param name="source">The matrix to multiply (n rows by m columns)</param>
        /// <param name="target">Filled with the product (n rows by m columns)</param>
        /// <remarks>
        /// Calls VML.Mul once for each column of R.  Source and target may be the same array.
        /// </remarks>
        public static void DiagMult(int n, int m, N[] diag, N[] source, N[] target)
        {
            OA<N> diag_p = oa(diag, 0);
            for (int c = 0; c < m; c++)
                vml.Mul(n, diag_p, oa(source, c*n), oa(target, c*n));
        }

        /// <summary>
        /// Multiplies the inverse of a diagonal matrix by a rectangular matrix:
        ///    D^-1 * R -> T
        /// </summary>
        /// <param name="n">Side length of the diagonal matrix, and num rows of the rectangular matrix</param>
        /// <param name="m">Num cols of the rectangular matrix</param>
        /// <param name="diag">The elements of the diagonal matrix to invert and multiply (length = n)</param>
        /// <param name="source">The matrix to multiply (n rows by m columns)</param>
        /// <param name="target">Filled with the product (n rows by m columns)</param>
        /// <remarks>
        /// Calls VML.Mul once for each column of R.    Source and target may be the same array.
        /// </remarks>
        public static void DiagInvMult(int n, int m, N[] diag, N[] source, N[] target)
        {
            OA<N> diag_p = oa(diag, 0);
            for (int c = 0; c < m; c++)
                vml.Div(n, oa(source, c * n), diag_p, oa(target, c * n));
        }


        static readonly IExtras<N> extras = (IExtras<N>)(new MklExtras());

        public static void Round(int n, N[] a, N[] y) { extras.Round(n, a, y); }
    }

}
