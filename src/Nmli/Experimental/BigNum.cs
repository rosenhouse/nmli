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
            int deltaExp = (int)Math.Round(Math.Log(coefficient, 2));
            double newCoef = coefficient / Math.Pow(2, deltaExp);

            return new BigNum(newCoef, exponent + deltaExp);
        }

        public static BigNum OfDouble(double d) { return Normalize(d, 0); }
        public static BigNum OfSingle(float f) { return OfDouble(f); }

        public static double ToDouble(BigNum bn) { return Math.Pow(2, bn.exponent) * bn.coefficient; }
        public static float ToSingle(BigNum bn) { return (float)(ToDouble(bn)); }


        public static implicit operator BigNum (double d) { return OfDouble(d); }
        public static implicit operator BigNum (float f) { return OfSingle(f); }

        public static explicit operator double (BigNum bn) { return ToDouble(bn); }
        public static explicit operator float (BigNum bn) { return ToSingle(bn); }


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
            throw new NotImplementedException();
        }
    }
}
