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
            ExclusiveExterns.AsRefs.vsMul(n, ref a, ref b, ref y);
        }

        public void Div(int n, ref double a, ref double b, ref double y)
        {
            ExclusiveExterns.AsRefs.vsDiv(n, ref a, ref b, ref y);
        }

    }

    public class ExclusiveExtras<N>
    {

        /// <summary>
        /// Multiplies a diagonal matrix by a rectangular matrix:
        ///    D * R -> T
        /// </summary>
        /// <param name="n">Side length of the diagonal matrix, and num rows of the rectangular matrix</param>
        /// <param name="m">Num cols of the rectangular matrix</param>
        /// <param name="diag">The elements of the diagonal matrix (length = n)</param>
        /// <param name="source">The matrix to multiply </param>
        /// <param name="target">On output, contains the product</param>
        /// <remarks>
        /// Does multiple calls to VML.Mul so 
        /// </remarks>
        public static void DiagMult(int n, int m, N[] diag, N[] source, N[] target)
        {


        }

    }
}
