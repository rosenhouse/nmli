/*
 * These tests are not written to validate the results returned by the native 
 * libraries (these are assumed to be correct), but to test the interface.
 * That is, that the rountine can be located, data can be passed to them,
 * and that a result is returned.
 */

/*
 * Much, if not all the tests in this file are copied verbatim from dnAnalytics  
 * http://www.dnanalytics.net
 */

using System;
using NUnit.Framework;
using Nmli;

namespace NmliTests
{
    [TestFixture]
    [Category("BLAS")]
    [Category("MKL")]
    public class MklBlasTest : BlasTest { public MklBlasTest() : base(Libraries.Mkl.Blas) { } }

    [TestFixture]
    [Category("BLAS")]
    [Category("ACML")]
    public class AcmlBlasTest : BlasTest { public AcmlBlasTest() : base(Libraries.Acml.Blas) { } }

    public abstract class BlasTest
    {
        protected readonly IBlas blas;
        protected BlasTest(IBlas blas) { this.blas = blas; }

        public const float delta = 0.00390625f;
        private float[] farray = new float[] { -1.1f, -2.2f, -3.3f, 0f, 1.1f, 2.2f, -4.4f, 5.5f, 6.6f };
        private double[] darray = new double[] { -1.1, -2.2, -3.3, 0, 1.1, 2.2, -4.4, 5.5, 6.6 };

        #region Level 1

        [Test]
        public void Sdot()
        {
            
            float res = blas.dot(farray.Length, farray, 1, farray, 1);
            float expected = 0;
            for (int i = 0; i < farray.Length; i++)
            {
                expected += farray[i] * farray[i];
            }
            Assert.AreEqual(expected, res, delta);
        }

        [Test]
        public void Ddot()
        {
            double res = blas.dot(darray.Length, darray, 1, darray, 1);
            double expected = 0;
            for (int i = 0; i < farray.Length; i++)
            {
                expected += darray[i] * darray[i];
            }
            Assert.AreEqual(expected, res, delta);
        }


        [Test]
        public void Snrm2()
        {
            float res = blas.nrm2(farray.Length, farray, 1);
            float expected = 10.7778f;
            Assert.AreEqual(expected, res, delta);
        }

        [Test]
        public void Dnrm2()
        {
            double res = blas.nrm2(darray.Length, darray, 1);
            double expected = 10.7778;
            Assert.AreEqual(expected, res, delta);
        }


        [Test]
        public void Sasum()
        {
            float res = blas.asum(farray.Length, farray, 1);
            float expected = 0;
            for (int i = 0; i < farray.Length; i++)
            {
                expected += Math.Abs(farray[i]);
            }
            Assert.AreEqual(expected, res, delta);
        }

        [Test]
        public void Dasum()
        {
            double res = blas.asum(darray.Length, darray, 1);
            double expected = 0;
            for (int i = 0; i < darray.Length; i++)
            {
                expected += Math.Abs(darray[i]);
            }
            Assert.AreEqual(expected, res, delta);
        }



        [Test]
        public void Scopy()
        {
            float[] copy = new float[farray.Length];
            blas.copy(farray.Length, farray, 1, copy, 1);
            for (int i = 0; i < farray.Length; i++)
            {
                Assert.AreEqual(farray[i], copy[i], delta);
            }
        }

        [Test]
        public void Dcopy()
        {
            double[] copy = new double[darray.Length];
            blas.copy(darray.Length, darray, 1, copy, 1);
            for (int i = 0; i < darray.Length; i++)
            {
                Assert.AreEqual(darray[i], copy[i], delta);
            }
        }


        [Test]
        public void Saxpy()
        {
            float[] y = (float[])farray.Clone();
            blas.axpy(y.Length, 2, farray, 1, y, 1);
            float expected = 0;
            for (int i = 0; i < y.Length; i++)
            {
                expected = 2 * farray[i] + farray[i];
                Assert.AreEqual(expected, y[i]);
            }
        }

        [Test]
        public void Daxpy()
        {
            double[] y = (double[])darray.Clone();
            blas.axpy(y.Length, 2, darray, 1, y, 1);
            double expected = 0;
            for (int i = 0; i < y.Length; i++)
            {
                expected = 2 * darray[i] + darray[i];
                Assert.AreEqual(expected, y[i]);
            }
        }


        [Test]
        public void Sscal()
        {
            float[] x = (float[])farray.Clone();
            blas.scal(x.Length, 2, x, 1);
            for (int i = 0; i < x.Length; i++)
            {
                Assert.AreEqual(2 * farray[i], x[i], delta);
            }
        }

        [Test]
        public void Dscal()
        {
            double[] x = (double[])darray.Clone();
            blas.scal(x.Length, 2, x, 1);
            for (int i = 0; i < x.Length; i++)
            {
                Assert.AreEqual(2 * darray[i], x[i], delta);
            }
        }

        #endregion


        #region Level 2

        [Test]
        public void Ssymv()
        {
            float[] a = new float[] { 1, 2, 2, 1, 1, 2, 1, 1, 1 };
            float[] x = new float[] { 1, 1, 1 };
            float[] y = new float[] { 1, 1, 1 };

            blas.symv(UpLo.Upper, 3, 3, a, 3, x, 1, 2, y, 1);
            Assert.AreEqual(11, y[0]);
            Assert.AreEqual(11, y[1]);
            Assert.AreEqual(11, y[2]);
        }

        [Test]
        public void Dsymv()
        {
            double[] a = new double[] { 1, 2, 2, 1, 1, 2, 1, 1, 1 };
            double[] x = new double[] { 1, 1, 1 };
            double[] y = new double[] { 1, 1, 1 };

            blas.symv(UpLo.Upper, 3, 3, a, 3, x, 1, 2, y, 1);
            Assert.AreEqual(11, y[0]);
            Assert.AreEqual(11, y[1]);
            Assert.AreEqual(11, y[2]);
        }


        [Test]
        public void Ssbmv()
        {
            float[] a = new float[] { 10, 1, 1, 1, 1, 1 };
            float[] x = new float[] { 1, 1, 1 };
            float[] y = new float[] { 1, 1, 1 };

            blas.sbmv(UpLo.Upper, 3, 1, 3, a, 2, x, 1, 2, y, 1);
            Assert.AreEqual(8, y[0]);
            Assert.AreEqual(11, y[1]);
            Assert.AreEqual(8, y[2]);
        }

        [Test]
        public void Dsbmv()
        {
            double[] a = new double[] { 10, 1, 1, 1, 1, 1 };
            double[] x = new double[] { 1, 1, 1 };
            double[] y = new double[] { 1, 1, 1 };

            //DSBMV  (UPLO,       N, K, ALPHA, A, LDA, X, INCX, BETA, Y, INCY)
            blas.sbmv(UpLo.Upper, 3, 1,   3,   a,  2,  x,  1,     2,  y, 1);
            Assert.AreEqual(8, y[0]);
            Assert.AreEqual(11, y[1]);
            Assert.AreEqual(8, y[2]);
        }


        [Test]
        public void Sgemv()
        {
            float alpha = 2;
            float beta = 3;
            float[] a = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            float[] x = new float[] { 1, 1, 1 };
            float[] y = new float[] { 1, 1, 1 };

            blas.gemv(Transpose.Trans, 3, 3, alpha, a, 3, x, 1, beta, y, 1);
            for (int i = 0; i < y.Length; i++)
            {
                Assert.AreEqual(9, y[i]);
            }
        }

        [Test]
        public void Dgemv()
        {
            double alpha = 2;
            double beta = 3;
            double[] a = new double[] { 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            double[] x = new double[] { 1, 1, 1 };
            double[] y = new double[] { 1, 1, 1 };

            blas.gemv(Transpose.Trans, 3, 3, alpha, a, 3, x, 1, beta, y, 1);
            for (int i = 0; i < y.Length; i++)
            {
                Assert.AreEqual(9, y[i]);
            }
        }


        [Test]
        public void Dger()
        {
            double alpha = 2;
            double[] a = new double[] { 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            double[] x = new double[] { 1, 1, 1 };
            double[] y = new double[] { 1, 1, 1 };

            blas.ger(3, 3, alpha, x, 1, y, 1, a, 3);
            for (int i = 0; i < a.Length; i++)
            {
                Assert.AreEqual(3, a[i]);
            }
        }

        [Test]
        public void Sger()
        {
            float alpha = 2;
            float[] a = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            float[] x = new float[] { 1, 1, 1 };
            float[] y = new float[] { 1, 1, 1 };

            blas.ger(3, 3, alpha, x, 1, y, 1, a, 3);
            for (int i = 0; i < a.Length; i++)
            {
                Assert.AreEqual(3, a[i]);
            }
        }

        #endregion 




        [Test]
        public void MyGemvTest1()
        {
            int rows = 3;
            int cols = 2;
            double[] A = new double[] { 1, 2, 3, 4, 5, 6 };
            double[] x = new double[] { 1, 2 };

            double[] output = new double[rows];
            blas.gemv(Transpose.NoTrans, rows, cols, 1, A, rows, x, 1, 0, output, 1);

            Assert.AreEqual(9, output[0], delta);
            Assert.AreEqual(12, output[1], delta);
            Assert.AreEqual(15, output[2], delta);
        }

        [Test]
        public void MyGemvTest2()
        {
            int rows = 2;
            int cols = 3;
            double[] A = new double[] { 1, 2, 3, 4, 5, 6 };
            double[] x = new double[] { 1, 1 };

            double[] output = new double[cols];
            blas.gemv(Transpose.Trans, rows, cols, 1, A, rows, x, 1, 0, output, 1);

            Assert.AreEqual(3, output[0], delta);
            Assert.AreEqual(7, output[1], delta);
            Assert.AreEqual(11, output[2], delta);
        }
    }



    public static class GenericBlasTests
    {

        public abstract class GenericTest<N, L> : GenericNumericTest<N, L>
        {
            N[] array;

            protected GenericTest()
            {
                float[] farray = new float[] { -1.1f, -2.2f, -3.3f, 0f, 1.2f, 2.3f, -4.4f, 5.5f, 6.6f };
                this.array = new N[farray.Length];
                Nmli.IO.ManagedIO2.Copy<float, N>(farray, array);
            }

            [Test]
            public void imax()
            {
                int i_min1 = blas.imax(array.Length, array, 1);
                Assert.AreEqual(8, i_min1);  // 0-based indexing

                int i_min3 = blas.imax(3, array, 3);
                Assert.AreEqual(2, i_min3);  // index is with respect to incX
            }

        }



        [TestFixture]
        [Category("BLAS")]
        [Category("ACML")]
        [Category("Double")]
        public class DoubleACML : GenericTest<double, Libs.ACML> { }


        [TestFixture]
        [Category("BLAS")]
        [Category("ACML")]
        [Category("Float")]
        public class FloatACML : GenericTest<float, Libs.ACML> { }


        [TestFixture]
        [Category("BLAS")]
        [Category("MKL")]
        [Category("Double")]
        public class DoubleMKL : GenericTest<double, Libs.MKL> { }


        [TestFixture]
        [Category("BLAS")]
        [Category("MKL")]
        [Category("Float")]
        public class FloatMKL : GenericTest<float, Libs.MKL> { }

    }
}