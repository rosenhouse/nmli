using System;


namespace Nmli.WithOffsets
{
    public interface IVml<T> : CollectionAgnostic.IVml<OA<T>> { }

    public interface IBlas<T> : CollectionAgnostic.IBlas<T, OA<T>> { }

    public interface ILapack<T> : CollectionAgnostic.ILapack<OA<T>> { }


    public interface IMultOffsets<T> : CollectionAgnostic.ITest<OA<T>> { }

    public interface IMultOffsets : IMultOffsets<float>, IMultOffsets<double> { }

    unsafe public class MklWithOffsets : IMultOffsets
    {
        void TestMultiDim()
        {
            int n = 2;
            double[,] a = new double[2, 2];
            double[] b = new double[4];
            double[] y = new double[4];

            Array foo = a;
            

            fixed (double* pa = &a[1,0],
                            pb = &b[2],
                            py = &y[2])
            {
                Mkl.UnsafeExterns.vdMul(n, pa, pb, py);
            }
        }

        public void Mul(int n, OA<float> a, OA<float> b, OA<float> y)
        {
            fixed (float* pa = &a.array[a.offset],
                            pb = &b.array[b.offset],
                            py = &y.array[y.offset])
            {
                Mkl.UnsafeExterns.vsMul(n, pa, pb, py);
            }
        }

        public void Mul(int n, OA<double> a, OA<double> b, OA<double> y)
        {
            fixed (double* pa = &a.array[a.offset],
                            pb = &b.array[b.offset],
                            py = &y.array[y.offset])
            {
                Mkl.UnsafeExterns.vdMul(n, pa, pb, py);
            }
        }
    }
}
