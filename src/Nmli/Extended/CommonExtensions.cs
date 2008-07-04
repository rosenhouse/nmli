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
            private readonly N[] ZeroVec;

            public ExtraFunctions(IMathLibrary<N> ml)
                : base(ml)
            {
                OneVec = new N[] { _1 };
                ZeroVec = new N[] { _0 };
            }


            public N Sum(int n, N[] x)
            {
                return blas.dot(n, x, 1, OneVec, 0);
            }


            /// <summary>
            /// Overwrites the vector with 0s
            /// </summary>
            public void Clear(int n, N[] y) { blas.copy(n, ZeroVec, 0, y, 1); }
        }

        protected readonly ExtraFunctions extras;

        public Extensions1(IMathLibrary<N> ml) : base(ml)
        {
            this.extras = new ExtraFunctions(ml);
        }

    }
}