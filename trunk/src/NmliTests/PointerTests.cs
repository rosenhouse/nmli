using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Nmli.WithOffsets;

namespace NmliTests
{
    [TestFixture]
    public class PointerTests
    {
        const float delta = GenericNumericTest<double, Libs.MKL>.delta;

        [Test]
        public void MultNoOffset()
        {
            Nmli.Libraries.Mkl.Vml.Exp(1, new double[] { 1 }, new double[] { 1 });


            int n = 3;
            float[] a = new float[] { 1, 2, 3 };
            float[] b = new float[] { 4, 5, 6 };
            float[] c = new float[] { 0, 0, 0 };

            MklWithOffsets t = new MklWithOffsets();
            t.Mul(n, a, b, c);

            for (int i = 0; i < n; i++)
                Assert.AreEqual(a[i] * b[i], c[i], delta);
            
        }

        

        [Test]
        public void MultWithOffset()
        {
            Nmli.Libraries.Mkl.Vml.Exp(1, new double[] { 1 }, new double[] { 1 });

            int n = 1;
            float[] a = new float[] { 1, 2, 3 };
            float[] b = new float[] { 4, 5, 6 };
            float[] c = new float[] { 0, 0, 0 };

            MklWithOffsets t = new MklWithOffsets();

            t.Mul(n, OA.O(a, 1), OA.O(b, 2), OA.O(c, 0));

            Assert.AreEqual(12, c[0], delta);
            Assert.AreEqual(0, c[1], delta);
            Assert.AreEqual(0, c[2], delta);
        }

    }
}
