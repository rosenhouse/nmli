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
    public static class BlasTests
    {

        public abstract class GenericTest<N, L> : GenericNumericTest<N, L>
        {
            N[] array;

            protected GenericTest()
            {
                this.array = new_array(-1.1, -2.2, -3.3, 0, 1.2, 2.3, 4.4, 5.5, 6.6);
            }


            #region Level 1

            [Test]
            public void imax()
            {
                int i_min1 = blas.imax(array.Length, array, 1);
                Assert.AreEqual(8, i_min1);  // 0-based indexing

                int i_min3 = blas.imax(3, array, 3);
                Assert.AreEqual(2, i_min3);  // index is with respect to incX
            }


            [Test]
            public void dot()
            {
                N res = blas.dot(array.Length, array, 1, array, 1);
                double expected = 0;
                for (int i = 0; i < array.Length; i++)
                {
                    double x = to_dbl(array[i]);
                    expected += x * x;
                }
                Assert.AreEqual(expected, to_dbl(res), delta);
            }



            [Test]
            public void nrm2()
            {
                N res = blas.nrm2(array.Length, array, 1);
                double expected = 10.8093;
                Assert.AreEqual(expected, to_dbl(res), delta);
            }



            [Test]
            public void asum()
            {
                N res = blas.asum(array.Length, array, 1);
                double expected = 0;
                for (int i = 0; i < array.Length; i++)
                    expected += Math.Abs(to_dbl(array[i]));

                Assert.AreEqual(expected, to_dbl(res), delta);
            }



            [Test]
            public void copy()
            {
                N[] copy = new N[array.Length];
                blas.copy(array.Length, array, 1, copy, 1);
                AssertArrayEqual(array, copy);

                N[] copy3 = new N[array.Length * 3];
                blas.copy(2, array, 2, copy3, 3);

                Assert.AreEqual(to_dbl(array[0]), to_dbl(copy3[0]));
                Assert.AreEqual(to_dbl(array[2]), to_dbl(copy3[3]));
            }




            [Test]
            public void axpy()
            {
                N[] y = (N[])array.Clone();
                blas.axpy(y.Length, of_dbl(2), array, 1, y, 1);
                for (int i = 0; i < y.Length; i++)
                    Assert.AreEqual(3 * to_dbl(array[i]), to_dbl(y[i]), delta);
            }


            [Test]
            public void scal()
            {
                N[] x = (N[])array.Clone();
                blas.scal(x.Length, of_dbl(2), x, 1);
                for (int i = 0; i < x.Length; i++)
                    Assert.AreEqual(2 * to_dbl(array[i]), to_dbl(x[i]), delta);
            }

            #endregion



            #region Level 2

            [Test]
            public void symv()
            {
                N[] a = new_array( 1, 2, 2, 1, 1, 2, 1, 1, 1 );
                N[] x = new_array( 1, 1, 1 );
                N[] y = new_array(1, 1, 1);

                blas.symv(UpLo.Upper, 3, of_dbl(3), a, 3, x, 1, of_dbl(2), y, 1);

                AssertArrayEqual(new_array(11, 11, 11), y);
            }


            [Test]
            public void sbmv()
            {
                N[] a = new_array( 10, 1, 1, 1, 1, 1 );
                N[] x = new_array( 1, 1, 1 );
                N[] y = new_array( 1, 1, 1 );

                //DSBMV  (UPLO,       N, K, ALPHA,     A, LDA, X, INCX, BETA, Y, INCY)
                blas.sbmv(UpLo.Upper, 3, 1, of_dbl(3), a, 2,   x, 1, of_dbl(2), y, 1);

                AssertArrayEqual(new_array(8, 11, 8), y);
            }



            [Test]
            public void gemv()
            {
                N alpha = of_dbl(2);
                N beta = of_dbl(3);
                N[] a = new_array( 1, 1, 1, 1, 1, 1, 1, 1, 1 );
                N[] x = new_array( 1, 1, 1 );
                N[] y = new_array( 1, 1, 1 );

                blas.gemv(Transpose.Trans, 3, 3, alpha, a, 3, x, 1, beta, y, 1);

                for (int i = 0; i < y.Length; i++)
                    Assert.AreEqual(of_dbl(9), y[i]);
            }

            [Test]
            public void gemv1()
            {
                int rows = 3;
                int cols = 2;
                N[] A = new_array(1, 2, 3, 4, 5, 6);
                N[] x = new_array(1, 2);

                N[] output = new N[rows];
                blas.gemv(Transpose.NoTrans, rows, cols, _1, A, rows, x, 1, _0, output, 1);

                AssertArrayEqual(new_array(9, 12, 15), output);
            }

            [Test]
            public void gemv2()
            {
                int rows = 2;
                int cols = 3;
                N[] A = new_array(1, 2, 3, 4, 5, 6);
                N[] x = new_array(1, 1);

                N[] output = new N[cols];
                blas.gemv(Transpose.Trans, rows, cols, _1, A, rows, x, 1, _0, output, 1);

                AssertArrayEqual(new_array(3, 7, 11), output);
            }


            [Test]
            public void ger()
            {
                N alpha = of_dbl(2);
                N[] a = new_array( 1, 1, 1, 1, 1, 1, 1, 1, 1 );
                N[] x = new_array( 1, 1, 1 );
                N[] y = new_array( 1, 1, 1 );

                blas.ger(3, 3, alpha, x, 1, y, 1, a, 3);
                for (int i = 0; i < a.Length; i++)
                    Assert.AreEqual(of_dbl(3), a[i]);
            }

            
            #endregion 


            [Test]
            public void gemm()
            {
                N[] myArray = new_array(1, 2, 3,    4, 5, 6);

                N[] result = new N[2 * 2];
                N[] expected = new_array(14, 32, 32, 77);

                int m = 2; // rows of A, rows of C
                int n = 2; // cols of B, cols of C
                int k = 3; // cols of A, rows of B

                N[] a = myArray; // a^t = A : m x k
                N[] b = myArray; // b   = B : k x n
                N[] c = result;        // c = A * B = C: nFrames x nFrames = m x n

                int lda = k;
                int ldb = k;
                int ldc = m;

                N alpha = _1;
                N beta = _0;

                blas.gemm(Transpose.Trans, Transpose.NoTrans, m, n, k,
                    alpha, a, lda, b, ldb, beta, c, ldc);


                AssertArrayEqual(expected, result);

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