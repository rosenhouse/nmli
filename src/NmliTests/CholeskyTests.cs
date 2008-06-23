using System;
using System.Collections.Generic;
using NUnit.Framework;
using Nmli;
using Nmli.Extended;

namespace NmliTests
{
    [TestFixture]
    [Category("Extended")]
    [Category("MKL")]
    public class MklCholeskyTest : CholeskyTest { public MklCholeskyTest() : base(Libraries.Mkl) { } }

    [TestFixture]
    [Category("Extended")]
    [Category("ACML")]
    public class AcmlCholeskyTest : CholeskyTest { public AcmlCholeskyTest() : base(Libraries.Acml) { } }



    public abstract class CholeskyTest
    {
        CholeskyInverter<double> di;
        CholeskyInverter<float> fi;

        public const float delta = BlasTest.delta;

        protected CholeskyTest(IMathLibrary ml)
        {
            this.di = new Nmli.Extended.CholeskyInverter<double>(ml);
            this.fi = new Nmli.Extended.CholeskyInverter<float>(ml);
        }

        static readonly double[] testMatrix3x3 =
            new double[] {   77.9087, -77.997, -0.565785,
                             -77.997, 78.1097,  0.619511, 
                           -0.565785, 0.619511, 0.782607   };

        static readonly double[] testMatrix2x2 
            = new double[] { 77.45087, -77.997, -77.997, 78.61027 };

        [Test]
        public void DCholeskyInvert()
        {
            int order = 3;
            double[] ch_result = (double[])testMatrix3x3.Clone();

            di.Invert(order, ch_result);

            double ch_trace = 0;
            for (int i = 0; i < order; i++)
                ch_trace += ch_result[i * order + i];

            Assert.AreEqual(98.2951, ch_trace, delta);
        }

        [Test]
        public void FCholeskyInvert()
        {
            int order = 3;
            float[] ch_result = Array.ConvertAll<double, float>(testMatrix3x3, Convert.ToSingle);

            fi.Invert(order, ch_result);

            float ch_trace = 0;
            for (int i = 0; i < order; i++)
                ch_trace += ch_result[i * order + i];

            Assert.AreEqual(98.2951, ch_trace, delta);
        }


        double Det2x2(double[] m)
        {
            double a = m[0];
            double b = m[1];
            double c = m[2];
            double d = m[3];

            return a * d - b * c;
        }

        float Det2x2(float[] m)
        {
            float a = m[0];
            float b = m[1];
            float c = m[2];
            float d = m[3];

            return a * d - b * c;
        }


        [Test]
        public void DCholeskyDeterminant()
        {
            double[] m = (double[])testMatrix2x2.Clone();

            double knownLnDet = Math.Log(Det2x2(m));

            double testLnDet = di.Invert(2, m);

            Assert.AreEqual(knownLnDet, testLnDet, delta);
        }

        [Test]
        public void FCholeskyDeterminant()
        {
            float[] m = Array.ConvertAll<double, float>(testMatrix2x2, Convert.ToSingle);

            double knownLnDet = Math.Log(Det2x2(m));

            double testLnDet = fi.Invert(2, m);

            Assert.AreEqual(knownLnDet, testLnDet, delta);
        }


    }


}
