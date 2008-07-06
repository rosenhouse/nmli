using System;
using System.Collections.Generic;
using System.Text;

namespace Nmli
{
    public abstract class ExtendingFunc<T>
    {
        protected readonly IMathLibrary<T> ml;
        protected readonly IBlas<T> blas;
        protected readonly ILapack<T> lapack;
        protected readonly IVml<T> vml;
        protected readonly ISml<T> sml;
        protected readonly IIO<T> io;
        protected readonly WithOffsets.IBlas<T> wo_blas;

        /// <summary>
        /// The scalar 0 typed for this class
        /// </summary>
        protected readonly T _0;

        /// <summary>
        /// The scalar 1 typed for this class
        /// </summary>
        protected readonly T _1;

        protected Nmli.WithOffsets.OA<T> oa(T[] array, int offset)
        {
            return Nmli.WithOffsets.OA.O(array, offset);
        }

        protected T of_dbl(double d) { return sml.OfDouble(d); }
        protected double to_dbl(T x) { return sml.ToDouble(x); }
        protected int to_int(T x) { return sml.ToInt(x); }

        /// <summary>
        /// Initializes with a custom math library
        /// </summary>
        /// <param name="ml">The custom math library</param>
        protected ExtendingFunc(IMathLibrary<T> ml)
        {
            this.ml = ml;
            this.blas = ml.Blas;
            this.lapack = ml.Lapack;
            this.vml = ml.Vml;
            this.sml = ml.Sml;
            this.io = ml.Io;
            this.wo_blas = ml.WithOffsets_Blas;

            this._0 = sml.Zero;
            this._1 = sml.One;

            this.extras = new ExtraFunctions(ml);
        }

        /// <summary>
        /// Initializes with the default math library, determined by the environment variable NMLI_IMPL
        /// </summary>
        protected ExtendingFunc() : this(Libraries<T>.Default) { }





        public class ExtraFunctions : ExtendingFunc<T>
        {

            private readonly T[] OneVec;
            private readonly T[] ZeroVec;

            [ThreadStatic]
            private static T[] setConstVec;

            public ExtraFunctions(IMathLibrary<T> ml)
                : base(ml)
            {
                OneVec = new T[] { _1 };
                ZeroVec = new T[] { _0 };
            }


            public T Sum(int n, T[] x) { return blas.dot(n, x, 1, OneVec, 0); }


            /// <summary>
            /// Sets every element of a vector equal to a single constant value
            /// </summary>
            /// <param name="n">Vector length</param>
            /// <param name="y">Vector to modify</param>
            /// <param name="constantValue">Value to use</param>
            public void SetToConstant(int n, T[] y, T constantValue)
            {
                if (setConstVec == null)
                    setConstVec = new T[1];

                setConstVec[0] = constantValue;

                blas.copy(n, setConstVec, 0, y, 1);
            }

            /// <summary>
            /// Overwrites the vector with 0s
            /// </summary>
            public void Clear(int n, T[] y)
            {
                //SetToConstant(n, y, _0);
                // maybe we should benchmark these two to choose which one is best?
                Array.Clear(y, 0, n);
            }
        }

        protected readonly ExtraFunctions extras;

    }
}
