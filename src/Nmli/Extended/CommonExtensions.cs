using System;
using System.Collections.Generic;
using System.Text;

namespace Nmli.Extended
{
    public class Extensions1<N> : ExtendingFunc<N>
    {
        public class ExtraFunctions : ExtendingFunc<N>
        {

            private readonly N[] OneVec;

            public ExtraFunctions(IMathLibrary<N> ml)
                : base(ml) { OneVec = new N[] { _1 }; }

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

        }

        protected readonly ExtraFunctions extras;

        public Extensions1(IMathLibrary<N> ml) : base(ml)
        {
            this.extras = new ExtraFunctions(ml);
        }

    }
}
