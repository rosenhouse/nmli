using System;
using System.Collections.Generic;

using NUnit.Framework;
using Nmli;
using Nmli.Extended;

namespace NmliTests
{
    public class Vectorized2DNormalPDFTests
    {
        public abstract class GenericTest<N, L> : GenericNumericTest<N, L>
        {
            protected readonly Vectorized2DNormalPDF<N> obj = new Vectorized2DNormalPDF<N>(Lib);

            [Test]
            public void TestSquarer()
            {
                N[] toSquare = new N[] { of_dbl(-4), of_dbl(-3), of_dbl(-0.5), of_dbl(0), of_dbl(.7), of_dbl(5) };
                N[] toAddTo = new N[]  { of_dbl(1),  of_dbl(-2), of_dbl(-1),   of_dbl(5), of_dbl(90), of_dbl(0) };
                N alpha = of_dbl(-0.25);
                N beta = of_dbl(3.9);

                N[] expected = new N[toSquare.Length];
                for (int i = 0; i < expected.Length; i++)
                {
                    N betay = sml.Multiply(beta, toAddTo[i]);
                    N sqrd = sml.Multiply(toSquare[i], toSquare[i]);
                    N alpha_sqrd = sml.Multiply(alpha, sqrd);
                    N all = sml.Add(betay, alpha_sqrd);
                    expected[i] = all;
                }

                obj.SquareScaleAdd(toSquare.Length, toSquare, alpha, toAddTo, beta);

                AssertArrayEqual(expected, toAddTo, delta);

            }

            [Test]
            public void TestRSquareds()
            {
                

                N[] xs = new N[] { of_dbl(-2), of_dbl(0.5), of_dbl(1) };
                N[] ys = new N[] { of_dbl(9), of_dbl(1.2), of_dbl(-8.2) };
                N[] r2s = new N[xs.Length];
                for (int i = 0; i < xs.Length; i++)
                {
                    r2s[i] = sml.Add(sml.Multiply(xs[i], xs[i]), sml.Multiply(ys[i], ys[i]));
                    //Console.WriteLine("Element {0} = {1}", i, r2s[i]);
                }

                N[] linear = new N[xs.Length*2];
                for(int i=0; i<xs.Length; i++)
                {
                    linear[2*i] = xs[i];
                    linear[2*i + 1] = ys[i];
                }

                N[] output = new N[xs.Length];


                obj.RSquareds(xs.Length, linear, output, of_dbl(1));

                AssertArrayEqual(r2s, output, delta);
            }

            [Test]
            public void TestComputePDFs()
            {
                int n = 3;
                N[] xs = new N[] { of_dbl(0), of_dbl(-0.5), of_dbl(3.3) };
                N[] ys = new N[] { of_dbl(1), of_dbl(2.1), of_dbl(-1.2) };
                double variance = 8.5;

                N[] diffs2 = new N[n * 2];
                for (int i = 0; i < n; i++)
                {
                    diffs2[2 * i] = xs[i];
                    diffs2[2 * i + 1] = ys[i];
                }
                
                N[] output = new N[n];
                obj.ComputePDFs(n, diffs2, variance, output);

                N[] expected = new N[] { of_dbl(0.0176545), of_dbl(0.0142349), of_dbl(0.00906588) };
                
                AssertArrayEqual(expected, output, delta);
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
