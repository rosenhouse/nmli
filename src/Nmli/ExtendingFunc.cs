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
        }
    }
}
