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
        /// Returns a newly allocated generic-typed array containing the given double data
        /// </summary>
        protected T[] new_array(params double[] d)
        {
            if (d == null)
                return null;

            T[] a = new T[d.Length];
            IO.ManagedIO2.Copy<double, T>(d, a);
            return a;
        }


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


            /// <summary>
            /// Copies data from a source triangle of a square matrix to the opposite one.
            /// </summary>
            /// <param name="source">Triangle with the data to be copied</param>
            /// <param name="n">Side length of square matrix</param>
            /// <param name="matrix">Data to be modified</param>
            public void MirrorTriangle(UpLo source, int n, T[] matrix)
            {
                if (source == UpLo.Upper)
                {
                    for (int c = 0; c < n; c++)
                        for (int r = 0; r < c; r++)
                            matrix[r * n + c] = matrix[c * n + r];
                }
                else if(source == UpLo.Lower)
                {
                    for (int c = 0; c < n; c++)
                        for (int r = 0; r < c; r++)
                            matrix[c * n + r] = matrix[r * n + c];
                }
            }


            public void InplaceTransposeSquareMatrix(int n, T[] matrix)
            {
                if (matrix.Length < n * n)
                    throw new ArgumentException("Array length is shorter than n^2");

                T temp;
                for(int i=0; i<n-1; i++)
                    for (int j = i + 1; j < n; j++)
                    {
                        temp = matrix[i * n + j];
                        matrix[i * n + j] = matrix[j * n + i];
                        matrix[j * n + i] = temp;
                    }
            }


            /// <summary>
            /// Copies the transpose of source into target.  
            /// </summary>
            public void CopyTranspose(int srcOutDimLen, int srcInDimLen, T[] source, T[] target)
            {
                if (source.Length < srcOutDimLen * srcInDimLen)
                    throw new ArgumentException("source array is too short.");
                if (target.Length < srcOutDimLen * srcInDimLen)
                    throw new ArgumentException("target array is too short.");

                for (int i = 0; i < srcOutDimLen; i++)
                    for (int j = 0; j < srcInDimLen; j++)
                        target[j * srcOutDimLen + i] = source[i * srcInDimLen + j];
            }
        }

        protected readonly ExtraFunctions extras;

    }
}
