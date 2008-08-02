using System;

namespace Nmli.Mkl
{
    interface IRefVML<N>
    {
        void Mul(int n, ref N a, ref N b, ref N y);

        void Div(int n, ref N a, ref N b, ref N y);
    }

    class MKLRefVML : IRefVML<float>, IRefVML<double>
    {
        public void Mul(int n, ref float a, ref float b, ref float y)
        {
            ExclusiveExterns.AsRefs.vsMul(n, ref a, ref b, ref y);
        }

        public void Div(int n, ref float a, ref float b, ref float y)
        {
            ExclusiveExterns.AsRefs.vsDiv(n, ref a, ref b, ref y);
        }

        
        public void Mul(int n, ref double a, ref double b, ref double y)
        {
            ExclusiveExterns.AsRefs.vdMul(n, ref a, ref b, ref y);
        }

        public void Div(int n, ref double a, ref double b, ref double y)
        {
            ExclusiveExterns.AsRefs.vdDiv(n, ref a, ref b, ref y);
        }

    }

    public static class ExclusiveExtras<N>
    {
        static readonly IRefVML<N> refVml = (IRefVML<N>)(new MKLRefVML());


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
            for (int c = 0; c < m; c++)
                refVml.Mul(n, ref diag[0], ref source[c * n], ref target[c * n]);
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
            for (int c = 0; c < m; c++)
                refVml.Div(n, ref source[c * n], ref diag[0], ref target[c * n]);
        }
    }
}
