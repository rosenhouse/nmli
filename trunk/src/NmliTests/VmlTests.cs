using System;

using NUnit.Framework;
using Nmli;

namespace NmliTests
{

    public class VmlTest
    {
        public abstract class GenericTest<N, L> : GenericNumericTest<N, L>
        {
            protected readonly IVml<N> obj = Lib.Vml;

            private double[] darray = new double[] { -1.1, -2.2, -3.3, 0, 0.1, 2.2, -4.4, 5.5, 6.6 };
            private N[] data;

            protected GenericTest() { data = Array.ConvertAll<double, N>(darray, base.of_dbl); }

            [Test]
            public void Ln()
            {
                N[] es = new N[data.Length];
                N[] output = new N[es.Length];
                for (int i = 0; i < es.Length; i++)
                    es[i] = of_dbl(Math.Exp(darray[i]));

                vml.Ln(es.Length, es, output);

                AssertArrayEqual(data, output, delta);
            }


            [Test]
            public void Exp()
            {
                N[] es = new N[data.Length];
                N[] output = new N[es.Length];
                for (int i = 0; i < es.Length; i++)
                    es[i] = of_dbl(Math.Exp(darray[i]));

                vml.Exp(es.Length, data, output);

                AssertArrayEqual(es, output, delta);
            }



            [Test]
            public void Sqr()
            {
                int n = 3;

                N[] output = new N[n];
                N[] expected = new N[n];

                for (int i = 0; i < n; i++)
                    expected[i] = sml.Multiply(data[i], data[i]);

                vml.Sqr(n, data, output);

                AssertArrayEqual(expected, output, delta);

            }
        }

        #region Specific instances

        [TestFixture]
        [Category("VML")]
        [Category("ACML")]
        [Category("Double")]
        public class DoubleACML : GenericTest<double, Libs.ACML> { }


        [TestFixture]
        [Category("VML")]
        [Category("ACML")]
        [Category("Float")]
        public class FloatACML : GenericTest<float, Libs.ACML> { }


        [TestFixture]
        [Category("VML")]
        [Category("MKL")]
        [Category("Double")]
        public class DoubleMKL : GenericTest<double, Libs.MKL> { }


        [TestFixture]
        [Category("VML")]
        [Category("MKL")]
        [Category("Float")]
        public class FloatMKL : GenericTest<float, Libs.MKL> { }

        #endregion
    }
}

