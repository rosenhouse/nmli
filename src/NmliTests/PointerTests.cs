using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Nmli.WithOffsets;

namespace NmliTests
{
    [TestFixture]
    public class PointerTests : GenericNumericTest<double, Libs.MKL>
    {
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



        [Test]
        public void ArrayElementRef()
        {
            double mean = 0;
            double stdev = 1;
            double x = 3;
            double z = (x - mean) / (Math.Sqrt(2) * stdev);

            double[] foo = new double[3];
            Nmli.Mkl.ExclusiveExterns.AsRefs.vdErfc(1, ref z, ref foo[1]);

            double[] expected = new double[] { 0, 0.0026998, 0 };

            AssertArrayEqual(expected, foo);

        }
    }
}
