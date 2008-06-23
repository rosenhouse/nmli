using System;

using NUnit.Framework;
using Nmli;

namespace NmliTests
{

    [TestFixture]
    [Category("Sml")]
    [Category("MKL")]
    public class MklSmlTest : SmlTest { public MklSmlTest() : base(Libraries.Mkl) { } }

    [TestFixture]
    [Category("Sml")]
    [Category("ACML")]
    public class AcmlSmlTest : SmlTest { public AcmlSmlTest() : base(Libraries.Acml) { } }


    public abstract class SmlTest
    {
        protected readonly IMathLibrary ml;
        protected SmlTest(IMathLibrary ml) { this.ml = ml; }

        private const float delta = 1e-4f;
        private float[] farray = new float[] { -1.1f, -2.2f, -3.3f, 0f, 1.1f, 2.2f, -4.4f, 5.5f, 6.6f };
        private double[] darray = new double[] { -1.1, -2.2, -3.3, 0, 1.1, 2.2, -4.4, 5.5, 6.6 };


        [Test]
        public void SAsFloat64()
        {
            foreach (float f in farray)
                Assert.AreEqual(f, ml.Sml.ToDouble(f), delta);
        }


        [Test]
        public void DAsFloat64()
        {
            foreach (double d in darray)
                Assert.AreEqual(d, ml.Sml.ToDouble(d), delta);
        }
    }
}
