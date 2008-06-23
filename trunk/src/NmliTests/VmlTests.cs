using System;

using NUnit.Framework;
using Nmli;

namespace NmliTests
{
    [TestFixture]
    [Category("VML")]
    [Category("MKL")]
    public class MklVmlTest : VmlTest { public MklVmlTest() : base(Libraries.Mkl.Vml) { } }

    [TestFixture]
    [Category("VML")]
    [Category("ACML")]
    public class AcmlVmlTest : VmlTest { public AcmlVmlTest() : base(Libraries.Acml.Vml) { } }

    public abstract class VmlTest
    {
        protected readonly IVml vml;
        protected VmlTest(IVml vml) { this.vml = vml; }

        private const float delta = 1e-4f;
        private float[] farray = new float[] { -1.1f, -2.2f, -3.3f, 0f, 1.1f, 2.2f, -4.4f, 5.5f, 6.6f };
        private double[] darray = new double[] { -1.1, -2.2, -3.3, 0, 1.1, 2.2, -4.4, 5.5, 6.6 };

        [Test]
        public void Sln()
        {
            float[] es = new float[farray.Length];
            float[] output = new float[es.Length];
            for (int i = 0; i < es.Length; i++)
                es[i] = (float)Math.Exp(farray[i]);

            vml.Ln(es.Length, es, output);

            for (int i = 0; i < es.Length; i++)
                Assert.AreEqual(farray[i], output[i], delta);
        }

        [Test]
        public void Dln()
        {
            double[] es = new double[darray.Length];
            double[] output = new double[es.Length];
            for (int i = 0; i < es.Length; i++)
                es[i] = Math.Exp(darray[i]);

            vml.Ln(es.Length, es, output);

            for (int i = 0; i < es.Length; i++)
                Assert.AreEqual(darray[i], output[i], delta);
        }




        [Test]
        public void Sexp()
        {
            float[] es = (float[])farray.Clone();
            float[] output = new float[es.Length];

            vml.Exp(es.Length, es, output);

            for (int i = 0; i < es.Length; i++)
                Assert.AreEqual(Math.Exp(farray[i]), output[i], delta);
        }

        [Test]
        public void Dexp()
        {
            double[] es = (double[])darray.Clone();
            double[] output = new double[es.Length];

            vml.Exp(es.Length, es, output);

            for (int i = 0; i < es.Length; i++)
                Assert.AreEqual(Math.Exp(darray[i]), output[i], delta);
        }



    }
}
