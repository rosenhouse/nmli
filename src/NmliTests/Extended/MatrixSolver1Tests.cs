using System;
using System.Collections.Generic;

using NUnit.Framework;
using Nmli;
using Nmli.Extended;

namespace NmliTests
{
    public class MatrixSolver1Tests
    {
        public abstract class GenericTest<N, L> : GenericNumericTest<N, L>
        {
            protected readonly MatrixSolver1<N> solver = new MatrixSolver1<N>(Lib);

            //[Test]
            public void TestSolveFullyDetermined()
            {
                int ra = 2;
                int ca = 2;
                N[] A = new_array(1, 2, 3, 4);

                N[] B = new_array(52, 74, 76, 108);

                N[] X = new N[ca * ca];
                solver.Solve(ra, ca, A, B, X);

                AssertArrayEqual(new_array(1, 2, 3, 4), X);
            }

            //[Test]
            public void TestSolveOverDetermined()
            {
                int ra = 3;
                int ca = 2;
                N[] A = new_array(1, 2, 3,   4, 5, 6);

                N[] B = new_array(85, 113, 141,
                                  116, 154, 192,
                                  147, 195, 243);

                N[] X = new N[ca * ca];
                solver.Solve(ra, ca, A, B, X);

                AssertArrayEqual(new_array(1, 2, 3, 4), X);
            }


            //[Test]
            public void TestSolveUnderDetermined()
            {
                throw new NotImplementedException();
            }

            [Test]
            public void TestSolveTrivial1()
            {
                int ra = 1;
                int ca = 1;
                N[] A = new_array(2);

                N[] B = new_array(12);

                N[] X = new N[ca * ca];
                solver.Solve(ra, ca, A, B, X);

                AssertArrayEqual(new_array(3), X);
            }
            [Test]
            public void TestSolveTrivial2()
            {
                int ra = 2;
                int ca = 1;
                N[] A = new_array(3, 4);

                N[] B = new_array(45, 60, 60, 80);

                N[] X = new N[ca * ca];
                solver.Solve(ra, ca, A, B, X);

                AssertArrayEqual(new_array(5), X);
            }

            [Test]
            public void TestSolveTrivial3()
            {
                int ra = 1;
                int ca = 2;
                N[] A = new_array(1,1);

                N[] B = new_array(2);

                N[] X = new N[ca * ca];
                solver.Solve(ra, ca, A, B, X);

                AssertArrayEqual(new_array(0.5,0.5,0.5,0.5), X);
            }


        }

        [TestFixture]
        [Category("Extended")]
        [Category("ACML")]
        [Category("Double")]
        public class DoubleACML : GenericTest<double, Libs.ACML> { }


        [TestFixture]
        [Category("Extended")]
        [Category("ACML")]
        [Category("Float")]
        public class FloatACML : GenericTest<float, Libs.ACML> { }


        [TestFixture]
        [Category("Extended")]
        [Category("MKL")]
        [Category("Double")]
        public class DoubleMKL : GenericTest<double, Libs.MKL> { }


        [TestFixture]
        [Category("Extended")]
        [Category("MKL")]
        [Category("Float")]
        public class FloatMKL : GenericTest<float, Libs.MKL> { }
    }
}
