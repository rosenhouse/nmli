using System;


namespace Nmli.Mkl
{
    using Nmli.WithOffsets;
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
        /// Calls VML.Mul once for each column of R.
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
        /// Calls VML.Mul once for each column of R.
        /// </remarks>
        public static void DiagInvMult(int n, int m, N[] diag, N[] source, N[] target)
        {
            OA<N> diag_p = oa(diag, 0);
            for (int c = 0; c < m; c++)
                vml.Div(n, oa(source, c * n), diag_p, oa(target, c * n));
        }
    }
}
