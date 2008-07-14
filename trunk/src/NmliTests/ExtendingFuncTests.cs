using System;
using NUnit.Framework;
using Nmli;

namespace NmliTests
{
    public static class ExtendingFuncTests
    {

        public abstract class GenericTest<N, L> : GenericNumericTest<N, L>
        {

            [Test]
            public void MirrorTriangle()
            {
                int n =3;
                
                N[] x = new_array(1, 2, 3);
                

                // generate a lower triangular matrix
                N[] lowerTriangular = new N[n * n];
                blas.syr(UpLo.Lower, n, _1, x, 1, lowerTriangular, n);
                // note that if you were to write this out in "normal" left-to-right, top-to-bottom
                // ordering, it would appear as
                N[] ColMajorLowerTri = new_array(1, 2, 3,
                                                 0, 4, 6,
                                                 0, 0, 9);
                AssertArrayEqual(ColMajorLowerTri, lowerTriangular, "Validating ColMajorLowerTri: ");




                // generate an upper triangular matrix
                N[] upperTriangular = new N[n * n];
                blas.syr(UpLo.Upper, n, _1, x, 1, upperTriangular, n);
                // note that if you were to write this out in "normal" left-to-right, top-to-bottom
                // ordering, it would appear as
                N[] ColMajorUpperTri = new_array(1, 0, 0,
                                                 2, 4, 0,
                                                 3, 6, 9);
                // this is because ACML uses "column major" ordering, and while MKL provides optional
                // row-major ordering in cblas, we don't expose it, so as to keep things simple.
                AssertArrayEqual(ColMajorUpperTri, upperTriangular, "Validating ColMajorUpperTri: ");





                N[] full            = new_array( 1, 2, 3,
                                                 2, 4, 6,
                                                 3, 6, 9 );

                
                N[] testMatrix = new N[n*n];

                Array.Copy(lowerTriangular, testMatrix, n * n);
                extras.MirrorTriangle(UpLo.Lower, n, testMatrix);
                AssertArrayEqual(full, testMatrix, "Checking mirror from lower: ");

                Array.Copy(upperTriangular, testMatrix, n * n);
                extras.MirrorTriangle(UpLo.Upper, n, testMatrix);
                AssertArrayEqual(full, testMatrix, "Checking mirror from upper: ");
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