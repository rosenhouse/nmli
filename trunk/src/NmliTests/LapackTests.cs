/*
 * These tests are not written to validate the results returned by the native 
 * libraries (these are assumed to be correct), but to test the interface.
 * That is, that the rountine can be located, data can be passed to them,
 * and that a result is returned.
 */

/*
 * Much, if not all the tests in this file are copied verbatim from dnAnalytics  
 * http://www.dnanalytics.net
 */

using NUnit.Framework;
using Nmli;
using System;

namespace NmliTests
{
    [TestFixture]
    [Category("LAPACK")]
    [Category("MKL")]
    public class MklLapackTest : LapackTest { public MklLapackTest() : base(Libraries.Mkl.Lapack) { } }

    [TestFixture]
    [Category("LAPACK")]
    [Category("ACML")]
    public class AcmlLapackTest : LapackTest { public AcmlLapackTest() : base(Libraries.Acml.Lapack) { } }

   
    public abstract class LapackTest
    {
        protected readonly ILapack lapack;
        protected LapackTest(ILapack lapack) { this.lapack = lapack; }

        public const float delta = BlasTest.delta;

        [Test]
        public void Spotrf()
        {
            float[] a = new float[] { 1, 2, 3, 0, 4, 5, 0, 0, 6 };

            lapack.potrf(UpLo.Upper, 3, a, 3);
            Assert.AreEqual(1, a[0], delta);
            Assert.AreEqual(2, a[1], delta);
            Assert.AreEqual(3, a[2], delta);
            Assert.AreEqual(0, a[3], delta);
            Assert.AreEqual(2, a[4], delta);
            Assert.AreEqual(5, a[5], delta);
            Assert.AreEqual(0, a[6], delta);
            Assert.AreEqual(0, a[7], delta);
            Assert.AreEqual(2.4494, a[8], delta);
        }

        [Test]
        public void Dpotrf()
        {
            double[] a = new double[] { 1, 2, 3, 0, 4, 5, 0, 0, 6 };

            lapack.potrf(UpLo.Upper, 3, a, 3);
            Assert.AreEqual(1, a[0], delta);
            Assert.AreEqual(2, a[1], delta);
            Assert.AreEqual(3, a[2], delta);
            Assert.AreEqual(0, a[3], delta);
            Assert.AreEqual(2, a[4], delta);
            Assert.AreEqual(5, a[5], delta);
            Assert.AreEqual(0, a[6], delta);
            Assert.AreEqual(0, a[7], delta);
            Assert.AreEqual(2.4494, a[8], delta);
        }


        [Test]
        public void Spotri()
        {
            float[] a = new float[] { 1, 2, 3, 0, 4, 5, 0, 0, 6 };

            lapack.potrf(UpLo.Upper, 3, a, 3);
            lapack.potri(UpLo.Upper, 3, a, 3);

            Assert.AreEqual(1, a[0], delta);
            Assert.AreEqual(2, a[1], delta);
            Assert.AreEqual(3, a[2], delta);
            Assert.AreEqual(0, a[3], delta);
            Assert.AreEqual(.25, a[4], delta);
            Assert.AreEqual(5, a[5], delta);
            Assert.AreEqual(0, a[6], delta);
            Assert.AreEqual(0, a[7], delta);
            Assert.AreEqual(0.1667, a[8], delta);
        }

        [Test]
        public void Dpotri()
        {
            double[] a = new double[] { 1, 2, 3, 0, 4, 5, 0, 0, 6 };

            lapack.potrf(UpLo.Upper, 3, a, 3);
            lapack.potri(UpLo.Upper, 3, a, 3);

            Assert.AreEqual(1, a[0], delta);
            Assert.AreEqual(2, a[1], delta);
            Assert.AreEqual(3, a[2], delta);
            Assert.AreEqual(0, a[3], delta);
            Assert.AreEqual(.25, a[4], delta);
            Assert.AreEqual(5, a[5], delta);
            Assert.AreEqual(0, a[6], delta);
            Assert.AreEqual(0, a[7], delta);
            Assert.AreEqual(0.1667, a[8], delta);
        }

        
    }
}