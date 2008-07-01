using System;
using System.Collections.Generic;

using NUnit.Framework;
using Nmli;
using Nmli.Extended;

namespace NmliTests
{
    public class CommonExtensionsTests
    {
        public abstract class GenericTest<N, L> : GenericNumericTest<N, L>
        {
            protected readonly Extensions1<N>.ExtraFunctions obj = new Extensions1<N>.ExtraFunctions(Lib);

            [Test]
            public void TestSquareInto()
            {
                int stepSize = 2;
                int n = 3;

                N[] toSquare = new N[] { of(-4), of(-3), of(-0.5), of(0), of(.7), of(5) };
                N[] output = new N[n];

                N alpha = of(-0.25);

                N[] expected = new N[n];

                for (int i = 0; i < n*stepSize; i+=stepSize)
                {
                    N sqrd = sml.Multiply(toSquare[i], toSquare[i]);
                    expected[i/stepSize] = sml.Multiply(alpha, sqrd);
                }
                obj.SquareInto(n, toSquare, stepSize, alpha, output);

                AssertArrayEqual(expected, output, delta);

            }


            [Test]
            public void TestSum()
            {
                int n = 3;
                N[] x = new N[] { of(-3), of(5), of(0.1) };

                N sum = obj.Sum(n, x);

                Assert.AreEqual(2.1, to(sum), delta);

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