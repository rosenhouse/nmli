using System;
using System.Collections.Generic;

using NUnit.Framework;
using Nmli;

namespace NmliTests
{
    public static class Libs
    {
        public sealed class ACML { }
        public sealed class MKL  { }

        public static IMathLibrary<N> Get<N, L>()
        {
            Type n = typeof(N);
            Type l = typeof(L);

            IMathLibrary<N> ml;
            if (l == typeof(ACML))
                ml = Libraries<N>.Acml;
            else if (l == typeof(MKL))
                ml = Libraries<N>.Mkl;
            else
                throw new ArgumentException("Unrecognized library preference.");

            return ml;
        }
    }

    public abstract class GenericNumericTest<N, L> : ExtendingFunc<N>
    {
        public const double delta = 0.00390625f;

        public static IMathLibrary<N> Lib { get { return Libs.Get<N, L>(); } }

        private static readonly ISml<N> _sml = (ISml<N>)Libraries.Sml;
        
        protected N of(double d) { return _sml.OfDouble(d); }
        protected double to(N n) { return _sml.ToDouble(n); }

        //protected ISml<N> sml { get { return _sml; } }

        protected GenericNumericTest() : base(Lib) { }

        protected void AssertArrayEqual(N[] expected, N[] actual, double delta)
        {
            int m = expected.Length;
            int n = actual.Length;
            
            Assert.AreEqual(expected.Length, actual.Length,
                "Expected array length={0}, but actual array length={1}", m, n);
            
            for (int i = 0; i < n; i++)
            {
                double ex = to(expected[i]);
                double ac = to(actual[i]);
                Assert.AreEqual(ex,ac, delta, "Expected {0} but got {1} at index {2}", ex, ac, i);
            }
        }
    }

}
