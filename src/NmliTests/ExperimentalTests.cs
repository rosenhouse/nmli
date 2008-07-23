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

        public void ValidateTruncatedNormal(double mean, double stdev, double minVal, double uniformSample)
        {
            double delta = 0.01;

            double massBelow = TNS.CDF(mean, stdev, minVal);
            double val = TNS.UniformToTruncatedNormal(mean, stdev, minVal, uniformSample);

            double cdf_at_val = TNS.CDF(mean, stdev, val);
            Assert.AreEqual(uniformSample * (1 - massBelow), cdf_at_val - massBelow, delta);
        }

        [Test]
        public void UniformToTruncatedNormal1()
        {
            double delta = 0.01;
            double mean = 0;
            double stdev = 1;
            double minValue = 0;
            double massBelow = TNS.CDF(mean, stdev, minValue);

            double uniformSample = 0.5;

            double val = TNS.UniformToTruncatedNormal(mean, stdev, minValue, uniformSample);

            double expected = 0.67449;
            Assert.AreEqual(expected, val, delta);

            double cdf_at_val = TNS.CDF(mean, stdev, val);
            Assert.AreEqual(uniformSample * (1 - massBelow), cdf_at_val - massBelow);
        }


        [Test]
        public void UniformToTruncatedNormal2()
        {
            double mean = 5;
            double stdev = 2.3;
            double minValue = 3;
            double uniformSample = 0.665;
            ValidateTruncatedNormal(mean, stdev, minValue, uniformSample);
        }

        [Test]
        public void TruncatedSampler()
        {
            int nSamples = 10000;
            double avg = 0;


            double mean = -4.5;
            double stdev = 0.2;
            double minValue = -3.5;

            for (int i = 0; i < nSamples; i++)
                avg = (i * avg + TruncatedNormalSampler.SampleTruncatedNormal(mean, stdev, minValue)) / (i + 1);

            double expected = -3.4627;
            double delta = 0.01;

            Assert.AreEqual(expected, avg, delta);
        }

    }
}
