using System;

using NUnit.Framework;
using Nmli;
using Nmli.Experimental;

namespace NmliTests
{
    using TNS = TruncatedNormalSampler;

    [TestFixture]
    public class ExperimentalTests
    {
        [Test]
        public void CDF()
        {
            double delta = 0.01;
            double mean = 5.6;
            double stdev = 0.5;
            double x = 6;

            double cdf = TNS.CDF(mean, stdev, x);

            double expected = 0.788145;

            Assert.AreEqual(expected, cdf, delta);
        }

        [Test]
        public void InvCDF()
        {
            double delta = 0.01;
            double mean = -2.0;
            double stdev = 1.9;
            double q = 0.948616;

            double x = TNS.InvCDF(mean, stdev, q);

            double expected = 1.1;

            Assert.AreEqual(expected, x, delta);

        }

        [Test]
        public void UniformToTruncatedNormal1()
        {
            double mean = 0;
            double stdev = 1;
            double minValue = 0;

            double uniformSample = 0.5;
            double val = TruncatedNormalSampler.UniformToTruncatedNormal(mean, stdev, minValue, uniformSample);

            double expected = 0.67449;
            double delta = 0.01;

            Assert.AreEqual(expected, val, delta);
        }

        [Test]
        public void UniformToTruncatedNormal2()
        {
            double delta = 0.01;

            double mean = 5;
            double stdev = 2.3;
            double minValue = 3;

            double massBelow = TNS.CDF(mean, stdev, minValue);
            Assert.AreEqual(0.192269, massBelow, delta);

            double uniformSample = 0.666667; // proportion of mass left to consume
            double val = TNS.UniformToTruncatedNormal(mean, stdev, minValue, uniformSample);

            double cdf_at_val = TNS.CDF(mean, stdev, val);
            Assert.AreEqual(uniformSample, TNS.InvCDF(mean, stdev, cdf_at_val));

            Assert.AreEqual((1 - massBelow) * uniformSample + massBelow, cdf_at_val);
        }





        [Test]
        public void TruncatedSampler1()
        {
            int nSamples = 100000;
            double sum = 0;


            double mean = 0;
            double stdev = 1;
            double minValue = -2;

            for (int i = 0; i < nSamples; i++)
                sum += TruncatedNormalSampler.SampleTruncatedNormal(mean, stdev, minValue);

            double avg = sum / nSamples;

            double expected = 0.053991;
            double delta = 0.01;

            Assert.AreEqual(expected, avg, delta);
        }


        [Test]
        public void TruncatedSampler2()
        {
            int nSamples = 100000;
            double sum = 0;


            double mean = 2;
            double stdev = 1;
            double minValue = 2;

            for (int i = 0; i < nSamples; i++)
                sum += TruncatedNormalSampler.SampleTruncatedNormal(mean, stdev, minValue);

            double avg = sum / nSamples;

            double expected = 1.39894;
            double delta = 0.01;

            Assert.AreEqual(expected, avg, delta);
        }

    }
}
