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


        public class ExtraFunctions
        {

            private readonly T[] OneVec;
            private readonly T[] ZeroVec;

            [ThreadStatic]
            private static T[] setConstVec;

            private readonly IMathLibrary<T> ml;
             
            public ExtraFunctions(IMathLibrary<T> ml)
            {
                this.ml = ml;

                OneVec = new T[] { ml.Sml.One };
                ZeroVec = new T[] { ml.Sml.Zero };
            }


            public T Sum(int n, T[] x) { return ml.Blas.dot(n, x, 1, OneVec, 0); }


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

                ml.Blas.copy(n, setConstVec, 0, y, 1);
            }

        }

        protected readonly ExtraFunctions extras;

    }
}
