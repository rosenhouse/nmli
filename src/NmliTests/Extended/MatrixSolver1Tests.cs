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


            [Test]
            public void TestSolve1x1()
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
            public void TestSolve2x1()
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
            public void TestSolve3x1()
            {
                int ra = 3;
                int ca = 1;
                N[] A = new_array(-.5, 2, .25);

                N[] B = new_array(1, -4, -0.5, -4, 16, 2, -0.5, 2, 0.25);

                N[] X = new N[ca * ca];
                solver.Solve(ra, ca, A, B, X);

                AssertArrayEqual(new_array(4), X);
            }

            [Test]
            public void TestSolve1x2()
            {
                int ra = 1;
                int ca = 2;
                N[] A = new_array(1,1);

                N[] B = new_array(2);

                N[] X = new N[ca * ca];
                solver.Solve(ra, ca, A, B, X);

                AssertArrayEqual(new_array(0.5,0.5,0.5,0.5), X);
            }

            [Test]
            public void TestSolve1x3()
            {
                int ra = 1;
                int ca = 3;
                N[] A = new_array(1.5, -0.5, 2);

                N[] B = new_array(20.0473);

                N[] expected = new_array( 1.068,  -.356, 1.423, 
                                          -.356,   .119, -.474,
                                          1.423,  -.474, 1.898);

                N[] X = new N[ca * ca];
                solver.Solve(ra, ca, A, B, X);

                AssertArrayEqual(expected, X);
            }

            [Test]
            public void TestSolve2x2()
            {
                int ra = 2;
                int ca = 2;
                N[] A = new_array(0.5, -2, 3, 0.5);

                N[] B = new_array(9.375, 18.75,
                                  18.75, 12.5);

                N[] expected = new_array(1.5, -3, -3, 2);

                N[] X = new N[ca * ca];
                solver.Solve(ra, ca, A, B, X);

                AssertArrayEqual(expected, X);
            }



            [Test]
            public void TestSolve3x2()
            {
                int ra = 3;
                int ca = 2;
                N[] A = new_array(1, .5, .25,    0, 0.5, 1);

                N[] B = new_array(   1,    0.1,    -0.55,
                                   0.1,    0.15,  0.225,
                                 -0.55,   0.225, .8625);

                N[] expected = new_array(1,-.8,-.8, 1.2);

                N[] X = new N[ca * ca];
                solver.Solve(ra, ca, A, B, X);

                AssertArrayEqual(expected, X);
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
