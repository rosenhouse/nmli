using System;
using Nmli.Abstract;

namespace Nmli.Managed
{
    class BaseSml : ISml
    {
        #region Units
        public double One { get { return 1; } }
        public double Zero { get { return 0; } }

        float ICalcRing<float>.One { get { return 1; } }
        float ICalcAdditiveGroup<float>.Zero { get { return 0; } }
        #endregion


        #region Operations
        public double Add(double x, double y) { return x + y; }
        public double Negate(double x) { return -x; }
        public double Subtract(double x, double y) { return x - y; }

        public double Multiply(double x, double y) { return x * y; }
        public double Invert(double x) { return 1.0 / x; }
        public double Divide(double x, double y) { return x / y; }


        public float Add(float x, float y) { return x + y; }
        public float Negate(float x) { return -x; }
        public float Subtract(float x, float y) { return x - y; }

        public float Multiply(float x, float y) { return x * y; }
        public float Invert(float x) { return 1.0f / x; }
        public float Divide(float x, float y) { return x / y; }
        #endregion


        #region Conversions
        public double OfDouble(double x) { return x; }
        public double ToDouble(double x) { return x; }

        float ISml<float>.OfDouble(double x) { return (float)x; }
        public double ToDouble(float x) { return x; }

        public double OfInt(int x) { return x; }
        public int ToInt(double x) { return (int)x; }

        float ISml<float>.OfInt(int x) { return x; }
        public int ToInt(float x) { return (int)x; }
        #endregion


        #region Order
        public bool LessThan(double x, double y) { return x < y; }
        public bool GreaterThan(double x, double y) { return x > y; }
        public bool LessThanOrEqualTo(double x, double y) { return x <= y; }
        public bool GreaterThanOrEqualTo(double x, double y) { return x >= y; }
        public bool EqualTo(double x, double y) { return x == y; }
        public bool NotEqualTo(double x, double y) { return x != y; }

        public bool LessThan(float x, float y) { return x < y; }
        public bool GreaterThan(float x, float y) { return x > y; }
        public bool LessThanOrEqualTo(float x, float y) { return x <= y; }
        public bool GreaterThanOrEqualTo(float x, float y) { return x >= y; }
        public bool EqualTo(float x, float y) { return x == y; }
        public bool NotEqualTo(float x, float y) { return x != y; }
        #endregion
    }
}
