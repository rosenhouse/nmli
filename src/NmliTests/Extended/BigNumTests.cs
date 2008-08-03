using System;
using NUnit.Framework;
using Nmli;
using Nmli.Extended;
using Nmli.Experimental;

namespace NmliTests.Extended
{
    [TestFixture]
    public class BigNumTests
    {
        const double delta = 0.0000000000001;

        [Test]
        public void TestCasting()
        {
            BigNum n = 4;
            Assert.AreEqual(4, (double)n);

            Assert.AreEqual((BigNum)(-3), -3.0);

        }

        [Test]
        public void TestAdditionAndSubtraction()
        {
            BigNum a = 400.5;
            BigNum b = -300;
            Assert.AreEqual(100.5, (double)(a+b), delta);
            Assert.AreEqual(100.5, (double)(b + a), delta);
            Assert.AreEqual(700.5, (double)(a - b), delta);
            Assert.AreEqual(-700.5, (double)(b - a), delta);
        }


        [Test]
        public void TestMultiplicationAndDivision()
        {
            BigNum a = 6;
            BigNum b = -9;

            Assert.AreEqual(-54.0, (double)(a * b), delta);
            Assert.AreEqual(-54.0, (double)(b * a), delta);
            Assert.AreEqual(-2.0/3.0, (double)(a / b), delta);
            Assert.AreEqual(-1.5, (double)(b / a), delta);
        }

        [Test]
        public void TestLogarithms()
        {
            double a = 6.2;
            double b = 9.1;

            Assert.AreEqual(Math.Log(a, b), (double)BigNum.Log(a, b), delta);
            Assert.AreEqual(Math.Log(b, a), (double)BigNum.Log(b, a), delta);

            Assert.AreEqual(Math.Log(a * b), (double)(BigNum.Log(a) + BigNum.Log(b)), delta);
            Assert.AreEqual(Math.Log10(a / b), (double)(BigNum.Log10(a) - BigNum.Log10(b)), delta);

            Assert.AreEqual(7, (double)BigNum.Log2(1 << 7), delta);
        }


        [Test]
        public void TestPow()
        {
            double a = 3.2;
            double b = -2.1;

            Assert.AreEqual(Math.Pow(a, b), (double)BigNum.Pow(a, b), delta);
            Assert.AreEqual(Math.Pow(b, a), (double)BigNum.Pow(b, a), delta);

            Assert.AreEqual(1 << 7, (double)BigNum.Pow2(7), delta);
        }


    }
}
