using System;
using System.Collections.Generic;
using System.Text;

namespace Nmli.Extended
{
    public class CommonExtensions<N> : ExtendingFunc<N>
    {
        private readonly N[] OneVec;

        public CommonExtensions(IMathLibrary<N> ml) : base(ml)
        {
            OneVec = new N[] { _1 };
        }



        public void SquareInto(int n, N[] x, int incX, N scalar, N[] output)
        {
            blas.sbmv(UpLo.Lower, n, 0, scalar, x, incX, x, incX, _0, output, 1);
        }

        public void SquareInto(int n, N[] x, int incX, N[] output)
        {
            blas.sbmv(UpLo.Lower, n, 0, _1, x, incX, x, incX, _0, output, 1);
        }



        public N Sum(int n, N[] x)
        {
            return blas.dot(n, x, 1, OneVec, 0);
        }

        public N Sum(int n, N[] x, int incX)
        {
            return blas.dot(n, x, incX, OneVec, 0);
        }


        public void ManagedInplaceInvert(int n, N[] x)
        {
            for (int i = 0; i < n; i++)
                x[i] = sml.Invert(x[i]);
        }
    }
}
