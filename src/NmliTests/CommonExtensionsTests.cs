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