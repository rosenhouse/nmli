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

        /// <summary>
        /// The scalar 0 typed for this class
        /// </summary>
        protected readonly T _0;

        /// <summary>
        /// The scalar 1 typed for this class
        /// </summary>
        protected readonly T _1;

        protected ExtendingFunc(IMathLibrary<T> ml)
        {
            this.ml = ml;
            this.blas = ml.Blas;
            this.lapack = ml.Lapack;
            this.vml = ml.Vml;
            this.sml = ml.Sml;
            this.io = ml.Io;

            this._0 = sml.Zero;
            this._1 = sml.One;
        }
    }
}
