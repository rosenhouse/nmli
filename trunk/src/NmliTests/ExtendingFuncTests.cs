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


            [Test]
            public void InplaceSquareTranspose()
            {
                int n = 3;

                N[] testmatrix = new_array(1, 2, 3,
                                           4, 5, 6,
                                           7, 8, 9);

                N[] transpose = new_array(1, 4, 7,
                                          2, 5, 8,
                                          3, 6, 9);

                extras.InplaceTransposeSquareMatrix(n, testmatrix);
                AssertArrayEqual(transpose, testmatrix);
            }


            [Test]
            public void CopyTranspose()
            {
                int innerDimLen = 3;
                int outerDimLen = 2;

                N[] source = new_array(1, 2, 3,
                                       4, 5, 6 );

                N[] transpose = new_array(1, 4,
                                          2, 5,
                                          3, 6 );

                N[] target = new N[innerDimLen * outerDimLen];

                extras.CopyTranspose(outerDimLen, innerDimLen, source, target);

                AssertArrayEqual(transpose, target);
            }


            [Test]
            public void Reposition()
            {
                int nrows = 3;
                int ncols = 2;
                int ld_compact = nrows;
                int ld_expanded = 5;

                N[] compact = new_array(1, 2, 3,
                                        4, 5, 6);

                N[] expanded = new_array(1, 2, 3, 0, 0,
                                         4, 5, 6, 0, 0);

                N[] target1 = new N[ncols * ld_expanded];
                extras.Reposition(nrows, ld_compact, ld_expanded, ncols, compact, target1);
                AssertArrayEqual(expanded, target1);

                N[] target2 = new N[ncols * ld_compact];
                extras.Reposition(nrows, ld_expanded, ld_compact, ncols, expanded, target2);
                AssertArrayEqual(compact, target2);
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