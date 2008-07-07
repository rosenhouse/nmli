using System;
using System.Collections.Generic;

using NUnit.Framework;
using Nmli;
using Nmli.Extended;

namespace NmliTests
{
    public class MultinormalPdfTest
    {
        public abstract class GenericTest<N, L> : GenericNumericTest<N, L>
        {
            protected readonly MultinormalPdf<N> pdf = new MultinormalPdf<N>(Lib);

            [Test]
            public void TestFullCovMatrix()
            {
                N[] mean = new N[] { of(2), of(-1) };
                N[] cov = new N[] { of(1), of(0.3), of(0.3), of(1) };
                N[] x = new N[] { of(2.5), of(-0.3) };

                double lp = pdf.LogPDF_FastFull(x, mean, cov);

                Assert.AreEqual(-2.08193, lp, delta);
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
