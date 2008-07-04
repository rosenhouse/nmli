using System;
using Nmli.Abstract;

namespace Nmli.Managed
{
    class BaseSml : ISml
    {
        public double OfDouble(double x) { return x; }

        public double ToDouble(double x) { return x; }

        public double One { get { return 1; } }

        public double Zero { get { return 0; } }

        public double Invert(double x) { return 1.0 / x; }

        public double Divide(double x, double y) { return x / y; }

        public double Multiply(double x, double y) { return x * y; }

        public double Add(double x, double y) { return x + y; }

        public double Negate(double x) { return -x; }

        public double Subtract(double x, double y) { return x - y; }


        float ISml<float>.OfDouble(double x) { return (float)x; }
        public double ToDouble(float x) { return x; }

        public double OfInt(int x) { return x; }
        float ISml<float>.OfInt(int x) { return x; }

        

        public float Invert(float x) { return 1.0f / x; }

        public float Divide(float x, float y) { return x / y; }


        float ICalcRing<float>.One { get { return 1; } }

        public float Multiply(float x, float y) { return x * y; }

        public float Add(float x, float y) { return x + y; }
        public float Negate(float x) { return -x; }

        public float Subtract(float x, float y) { return x - y; }

        float ICalcAdditiveGroup<float>.Zero { get { return 0; } }


        public int ToInt(double x) { return (int)x; }

        public int ToInt(float x) { return (int)x; }

        

    }
}
