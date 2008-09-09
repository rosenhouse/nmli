using System;


namespace Nmli.Experimental
{
    /// <summary>
    /// Big, slow, and accurate representation of extemely large and small numbers.
    /// </summary>
    public struct BigNum
    {
        readonly double coefficient;
        readonly int exponent;

        private BigNum(double coefficient, int exponent)
        {
            this.coefficient = coefficient;
            this.exponent = exponent;
        }

        public static BigNum Normalize(double coefficient, int exponent)
        {
            if (coefficient == 0)
                return new BigNum(0, 0);
            else
            {
                int deltaExp = (int)Math.Round(Math.Log(Math.Abs(coefficient), 2));
                double newCoef = coefficient / Math.Pow(2, deltaExp);

                return new BigNum(newCoef, exponent + deltaExp);
            }
        }

        #region Casting
        public static BigNum OfDouble(double d) { return Normalize(d, 0); }
        public static BigNum OfSingle(float f) { return OfDouble(f); }

        public static double ToDouble(BigNum bn) { return Math.Pow(2, bn.exponent) * bn.coefficient; }
        public static float ToSingle(BigNum bn) { return (float)(ToDouble(bn)); }


        public static implicit operator BigNum (double d) { return OfDouble(d); }
        public static implicit operator BigNum (float f) { return OfSingle(f); }

        public static explicit operator double (BigNum bn) { return ToDouble(bn); }
        public static explicit operator float (BigNum bn) { return ToSingle(bn); }
        #endregion

        #region Arithmetic
        public static BigNum operator*(BigNum a, BigNum b)
        {
            // a = ac * 2^ae
            // b = bc * 2^be
            // a*b = ac * bc * 2^(ae + be)

            double coef = a.coefficient * b.coefficient;
            int exp = a.exponent + b.exponent;
            return Normalize(coef, exp);
        }

        public static BigNum operator /(BigNum a, BigNum b)
        {
            // a = ac * 2^ae
            // b = bc * 2^be
            // a*b = ac / bc * 2^(ae - be)

            double coef = a.coefficient / b.coefficient;
            int exp = a.exponent - b.exponent;
            return Normalize(coef, exp);
        }

        public static BigNum operator +(BigNum a, BigNum b)
        {
            if (a.exponent >= b.exponent)
            {
                //     a =  ac             * 2^ae
                //     b =  bc * 2^(be-ae) * 2^ae
                // a + b = (ac + bc * 2^(be-ae)) * 2^me

                double bb = b.coefficient * Math.Pow(2, b.exponent - a.exponent);
                return Normalize(a.coefficient + bb, a.exponent);
            }
            else
                return b + a;
        }

        public static BigNum operator -(BigNum a)
        {
            return new BigNum(-1 * a.coefficient, a.exponent);
        }

        public static BigNum operator -(BigNum a, BigNum b) { return a + (-b); }


        public static BigNum Log2(BigNum a)
        {
            return Normalize(Math.Log(a.coefficient, 2) + a.exponent, 0);
        }

        public static BigNum Log(BigNum a, double newBase)
        {
            // Logb(x) = log2(x) / log2(b)
            return Log2(a) / Math.Log(newBase, 2);
        }

        public static BigNum Log(BigNum a) { return Log(a, Math.E); }

        public static BigNum Log10(BigNum a) { return Log(a, 10); }


        public static BigNum Pow2(double exponent)
        {
            // a = 2^exponent
            // Let exponent = i + f
            //    where i is an integer, f is a fraction

            // a = 2^(i + f) = (2^f) * 2^i

            int intPart = (int)Math.Round(exponent);
            double fracPart = exponent - intPart;

            return Normalize(Math.Pow(2, fracPart), intPart);
        }


        public static BigNum Pow(double a, double b)
        {
            // x = a^b
            // Log2[x] = b * Log2[a]
            return Pow2(b * Math.Log(a, 2));
        }

        public static BigNum Exp(double a) { return Pow(Math.E, a); }
        #endregion

        #region Comparison
        public static bool operator ==(BigNum a, BigNum b)
        {
            return (a.exponent == b.exponent) && (a.coefficient == b.coefficient);
        }

        public static bool operator !=(BigNum a, BigNum b)
        {
            return (a.exponent != b.exponent) || (a.coefficient != b.coefficient);
        }

        public static bool operator ==(BigNum a, double b) { return ToDouble(a) == b; }

        public static bool operator !=(BigNum a, double b) { return !(a == b); }

        public static bool operator ==(double a, BigNum b) { return b == a; }

        public static bool operator !=(double a, BigNum b) { return !(b == a); }

        public static bool operator ==(BigNum a, float b) { return ToDouble(a) == b; }

        public static bool operator !=(BigNum a, float b) { return !(a == b); }

        public static bool operator ==(float a, BigNum b) { return b == a; }

        public static bool operator !=(float a, BigNum b) { return !(b == a); }
        #endregion


        public override bool Equals(object obj)
        {
            Type t = obj.GetType();
            if (t == typeof(BigNum))
                return this == (BigNum)obj;
            else if (t == typeof(double))
                return this == (double)obj;
            else if (t == typeof(float))
                return this == (float)obj;
            else
                return false;
        }
        public override int GetHashCode()
        {
            return exponent.GetHashCode() ^ coefficient.GetHashCode();
        }

        public string ToString(string formatString) { return string.Format(formatString, coefficient, exponent); }
        public override string ToString() { return ToString("{0}*2^{1}"); }

        public static bool IsNaNOrInfinity(BigNum x)
        {
            return double.IsNaN(x.coefficient) || double.IsInfinity(x.coefficient)
                || (x.exponent == int.MinValue) || (x.exponent == int.MaxValue);
        }
    }
}
