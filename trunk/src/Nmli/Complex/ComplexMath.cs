/*
 * ComplexMath.cs
 * 
 * Copyright (c) 2003-2005, dnAnalytics. All rights reserved.
*/
using System;

namespace Nmli.Complex
{

	///<summary>Provides trigonometric, logarithmic, and other common mathematical functions for complex types</summary>
	public sealed class ComplexMath {
		private static readonly ComplexDouble d_i = new ComplexDouble(0,1);
		private static readonly ComplexDouble d_ni = new ComplexDouble(0,-1);
		private static readonly ComplexFloat f_i = new ComplexFloat(0,1);
		private static readonly ComplexFloat f_ni = new ComplexFloat(0,-1);

		private ComplexMath() {}

		///<summary>Return the absolute value of a complex type calculated as the Euclidean norm</summary>
		//Based off Numerical Recipes' Cabs.
		public static double Absolute(ComplexDouble value)
		{
			if (value.IsNaN())
			{
				return double.NaN;
			}
			else if (value.IsInfinity())
			{
				return double.PositiveInfinity;
			}

			double real = System.Math.Abs(value.Real);
			double imag = System.Math.Abs(value.Imaginary);
			if (value.Real == 0)
			{
				return imag;
			}
			else if (value.Imaginary == 0)
			{
				return real;
			}
			else if (real > imag)
			{
				double temp = imag / real;
				return real * System.Math.Sqrt(1.0 + temp * temp);
			}
			else
			{
				double temp = real / imag;
				return imag * System.Math.Sqrt(1.0 + temp * temp);
			}
		}

		///<summary>Calculate the complex argument of a complex type.  Also commonly referred to as the phase.</summary>
		public static double Argument(ComplexDouble value)
		{
			return System.Math.Atan2(value.Imaginary, value.Real);
		}

		///<summary>Return the complex conjugate of a complex type</summary>
		public static ComplexDouble Conjugate(ComplexDouble value)
		{
			return new ComplexDouble(value.Real, -value.Imaginary);
		}

		///<summary>Return the complex cosine of a complex type</summary>
		public static ComplexDouble Cos(ComplexDouble value)
		{
			if (value.IsNaN())
			{
				return ComplexDouble.NaN;
			}
			return Cosh(new ComplexDouble(-value.Imaginary, value.Real));
		}

		///<summary>Return the complex hyperbolic cosine of a complex type</summary>
		public static ComplexDouble Cosh(ComplexDouble value)
		{
			if (value.IsNaN())
			{
				return ComplexDouble.NaN;
			}
			if (value.IsInfinity())
			{
				return ComplexDouble.Infinity;
			}
			return new ComplexDouble(System.Math.Cos(value.Imaginary) * System.Math.Cosh(value.Real), System.Math.Sin(value.Imaginary) * System.Math.Sinh(value.Real));
		}

		///<summary>Return the complex exponential of a complex type</summary>
		public static ComplexDouble Exp(ComplexDouble value)
		{
			if (value.IsNaN())
			{
				return ComplexDouble.NaN;
			}

			double exp = System.Math.Exp(value.Real);

			if (value.Imaginary == 0)
			{
				return new ComplexDouble(exp);
			}

			return new ComplexDouble(exp * System.Math.Cos(value.Imaginary), exp * System.Math.Sin(value.Imaginary));
		}

		///<summary>Raise 'leftSide' to the power of 'rightSide'</summary>
		///<param name="leftSide"><b>ComplexDouble</b> value as base</param>
		///<param name="rightSide"><b>ComplexDouble</b> value as exponent</param>
		public static ComplexDouble Pow(ComplexDouble leftSide, ComplexDouble rightSide)
		{
			return Exp(rightSide * ComplexMath.Log(leftSide));
		}


		/// <summary>
		/// Return an approximation to Log(1 + x)
		/// </summary>
		/// <param name="x">The real parameter</param>
		/// <returns>The value of Log(1+x)</returns>
		private static double Log1p(double x)
		{
			if (x < -1.0)
			{
				throw new ArgumentOutOfRangeException();
			}
			else if (x > 1.0E+15)
			{
				return System.Math.Log(x);
			}
			else if (System.Math.Abs(x) < 1.0E-16)
			{
				return x;
			}
			else
			{
				double y = 1.0 + x;
				return System.Math.Log(y) - ((y - 1) - x) / y;
			}

		}

		/// <summary>
		/// Return the value of Log(Abs(z)).
		/// </summary>
		/// <param name="value">The complex number.</param>
		/// <returns>The calculated value.</returns>
		private static double LogAbs(ComplexDouble value)
		{

			double xabs = System.Math.Abs(value.Real);
			double yabs = System.Math.Abs(value.Imaginary);

			double max, u;
			if (xabs >= yabs)
			{
				max = xabs;
				u = yabs / xabs;
			}
			else
			{
				max = yabs;
				u = xabs / yabs;
			}

			return (System.Math.Log(max) + 0.5 * ComplexMath.Log1p(u * u));
		}



		///<summary>Return the complex logarithm of a complex type</summary>
		public static ComplexDouble Log(ComplexDouble value)
		{
			if (value.IsNaN())
			{
				return ComplexDouble.NaN;
			}
			return new ComplexDouble(ComplexMath.LogAbs(value), System.Math.Atan2(value.Imaginary, value.Real));
			/* double r = ComplexMath.Absolute(value);
			 double i = System.Math.Atan2(value.Imaginary, value.Real);
			 if (i > System.Math.PI)
			 {
				 i -= (2.0 * System.Math.PI);
			 }
			 return new ComplexDouble(System.Math.Log(r), i);*/
		}

		///<summary>Given two complex types return the one with the maximum norm</summary>
		public static ComplexDouble Max(ComplexDouble v1, ComplexDouble v2)
		{
			if (Norm(v1) >= Norm(v2))
			{
				return v1;
			}
			else
			{
				return v2;
			}
		}

		///<summary>Return the Euclidean norm of a complex type</summary>
		public static double Norm(ComplexDouble value)
		{
			return ComplexMath.Absolute(value);
		}

		///<summary>Return the polar representation of a complex type</summary>
		public static ComplexDouble Polar(ComplexDouble value)
		{
			return new ComplexDouble(ComplexMath.Absolute(value), System.Math.Atan2(value.Imaginary, value.Real));
		}

		///<summary>Returns the sine of a complex type</summary>
		public static ComplexDouble Sin(ComplexDouble value)
		{
			if (value.IsNaN())
			{
				return ComplexDouble.NaN;
			}
			return new ComplexDouble(System.Math.Sin(value.Real) * System.Math.Cosh(value.Imaginary), System.Math.Cos(value.Real) * System.Math.Sinh(value.Imaginary));
		}

		///<summary>Returns the hyperbolic sine of a complex type</summary>
		public static ComplexDouble Sinh(ComplexDouble value)
		{
			if (value.IsNaN())
			{
				return ComplexDouble.NaN;
			}
			if (value.Imaginary == 0)
			{
				return new ComplexDouble(System.Math.Sinh(value.Real));
			}
			if (value.Real == 0)
			{
				return new ComplexDouble(0, System.Math.Sin(value.Imaginary));
			}
			return new ComplexDouble(System.Math.Sinh(value.Real) * System.Math.Cos(value.Imaginary), System.Math.Cosh(value.Real) * System.Math.Sin(value.Imaginary));
		}

		///<summary>Returns the square root of a complex type</summary>
		public static ComplexDouble Sqrt(ComplexDouble value)
		{
			ComplexDouble result;
			if (value.IsNaN())
			{
				return ComplexDouble.NaN;
			}

			if (value.IsInfinity())
			{
				return ComplexDouble.Infinity;
			}

			if ((value.Real == 0.0) && (value.Imaginary == 0.0))
			{
				result = ComplexDouble.Zero;
			}
			else
			{
				double x, y, t;

				x = System.Math.Abs(value.Real);
				y = System.Math.Abs(value.Imaginary);
				if (x >= y)
				{
					t = y / x;
					t = System.Math.Sqrt(x) * System.Math.Sqrt(0.5 * (1.0 + System.Math.Sqrt(1.0 + t * t)));
				}
				else
				{
					t = x / y;
					t = System.Math.Sqrt(y) * System.Math.Sqrt(0.5 * (t + System.Math.Sqrt(1.0 + t * t)));
				}

				if (value.Real >= 0.0)
				{
					result = new ComplexDouble(t, value.Imaginary / (2.0 * t));
				}
				else if (value.Imaginary >= 0.0)
				{
					result = new ComplexDouble(value.Imaginary / (2.0 * t), t);
				}
				else
				{
					result = new ComplexDouble(-value.Imaginary / (2.0 * t), -t);
				}
			}

			return result;
		}

    	///<summary>Returns the tangent of a complex type</summary>
		public static ComplexDouble Tan(ComplexDouble value)
		{
			if (value.IsNaN())
			{
				return ComplexDouble.NaN;
			}

			double real = 2.0 * value.Real;
			double imag = 2.0 * value.Imaginary;
			double denom = System.Math.Cos(real) + System.Math.Cosh(imag);

			return new ComplexDouble(System.Math.Sin(real) / denom, System.Math.Sinh(imag) / denom);

		}

		///<summary>Returns the hyperbolic tangent of a complex type</summary>
		public static ComplexDouble Tanh(ComplexDouble value)
		{
			if (value.IsNaN())
			{
				return ComplexDouble.NaN;
			}

			double real = 2.0 * value.Real;
			double imag = 2.0 * value.Imaginary;
			double denom = System.Math.Cosh(real) + System.Math.Cos(imag);

			return new ComplexDouble(System.Math.Sinh(real) / denom, System.Math.Sin(imag) / denom);
		}

		/// <summary>Returns the inverse sine of a complex type.</summary>
		public static ComplexDouble Asin(ComplexDouble value)
		{
			if (value.IsNaN())
			{
				return ComplexDouble.NaN;
			}
			ComplexDouble ni = new ComplexDouble(0, -1);
			return ni * ComplexMath.Log(ComplexDouble.I * value + ComplexMath.Sqrt(1 - value * value));

		}

		/// <summary>Returns the inverse cosine of a complex type.</summary>
		public static ComplexDouble Acos(ComplexDouble value)
		{
			if (value.IsNaN())
			{
				return ComplexDouble.NaN;
			}
			ComplexDouble ni = new ComplexDouble(0, -1);
			return ni * ComplexMath.Log(value + ComplexDouble.I * ComplexMath.Sqrt(1 - value * value));
		}

		/// <summary>Returns the inverse tangent of a complex type.</summary>
		public static ComplexDouble Atan(ComplexDouble value)
		{
			if (value.IsNaN())
			{
				return ComplexDouble.NaN;
			}
			return (1 / (ComplexDouble.I * 2)) * (ComplexMath.Log((1 + ComplexDouble.I * value)) - ComplexMath.Log((1 - ComplexDouble.I * value)));
		}

		///<summary>Returns the inverse hyperbolic sine of a complex type</summary>
		public static ComplexDouble Asinh(ComplexDouble value)
		{
			if (value.IsNaN())
			{
				return ComplexDouble.NaN;
			}
			return -ComplexMath.Log(ComplexMath.Sqrt(1 + value * value) - value);
		}

		///<summary>Returns the inverse hyperbolic cosine of a complex type</summary>
		public static ComplexDouble Acosh(ComplexDouble value)
		{
			if (value.IsNaN())
			{
				return ComplexDouble.NaN;
			}
			return 2 * ComplexMath.Log((ComplexMath.Sqrt(((value + 1) / 2)) + ComplexMath.Sqrt((value - 1) / 2)));
		}

		///<summary>Returns the inverse hyperbolic tangent of a complex type</summary>
		public static ComplexDouble Atanh(ComplexDouble value)
		{
			if (value.IsNaN())
			{
				return ComplexDouble.NaN;
			}
			return .5 * (ComplexMath.Log(1 + value) - ComplexMath.Log(1 - value));
		}

		///<summary>Return the absolute value of a complex type calculated as the Euclidean norm</summary>
		//Based off Numerical Recipes' Cabs.
		public static float Absolute(ComplexFloat value)
		{
			if (value.IsNaN())
			{
				return float.NaN;
			}
			else if (value.IsInfinity())
			{
				return float.PositiveInfinity;
			}

			float real = System.Math.Abs(value.Real);
			float imag = System.Math.Abs(value.Imaginary);
			if (value.Real == 0)
			{
				return imag;
			}
			else if (value.Imaginary == 0)
			{
				return real;
			}
			else if (real > imag)
			{
				double temp = imag / real;
				return (float)(real * System.Math.Sqrt(1.0 + temp * temp));
			}
			else
			{
				double temp = real / imag;
				return (float)(imag * System.Math.Sqrt(1.0 + temp * temp));
			}
		}

		///<summary>Calculate the complex argument of a complex type.  Also commonly referred to as the phase.</summary>
		public static float Argument(ComplexFloat value)
		{
			return (float)System.Math.Atan2(value.Imaginary, value.Real);
		}

		///<summary>Return the complex conjugate of a complex type</summary>
		public static ComplexFloat Conjugate(ComplexFloat value)
		{
			return new ComplexFloat(value.Real, -value.Imaginary);
		}

		///<summary>Return the complex cosine of a complex type</summary>
		public static ComplexFloat Cos(ComplexFloat value)
		{
			if (value.IsNaN())
			{
				return ComplexFloat.NaN;
			}
			return Cosh(new ComplexFloat(-value.Imaginary, value.Real));
		}

		///<summary>Return the complex hyperbolic cosine of a complex type</summary>
		public static ComplexFloat Cosh(ComplexFloat value)
		{
			if (value.IsNaN())
			{
				return ComplexFloat.NaN;
			}
			if (value.IsInfinity())
			{
				return ComplexFloat.Infinity;
			}
			return new ComplexFloat((float)(System.Math.Cos(value.Imaginary) * System.Math.Cosh(value.Real)), (float)(System.Math.Sin(value.Imaginary) * System.Math.Sinh(value.Real)));
		}

		///<summary>Return the complex exponential of a complex type</summary>
		public static ComplexFloat Exp(ComplexFloat value)
		{
			if (value.IsNaN())
			{
				return ComplexFloat.NaN;
			}

			double exp = System.Math.Exp(value.Real);

			if (value.Imaginary == 0)
			{
				return new ComplexFloat((float)exp);
			}

			return new ComplexFloat((float)(exp * System.Math.Cos(value.Imaginary)), (float)(exp * System.Math.Sin(value.Imaginary)));
		}

		///<summary>Raise 'leftSide' to the power of 'rightSide'</summary>
		///<param name="leftSide"><b>ComplexFloat</b> value as base</param>
		///<param name="rightSide"><b>ComplexFloat</b> value as exponent</param>
		public static ComplexFloat Pow(ComplexFloat leftSide, ComplexFloat rightSide)
		{
			return Exp(rightSide * ComplexMath.Log(leftSide));
		}


		/// <summary>
		/// Return an approximation to Log(1 + x)
		/// </summary>
		/// <param name="x">The real parameter</param>
		/// <returns>The value of Log(1+x)</returns>
		private static float Log1p(float x)
		{
			if (x < -1.0)
			{
				throw new ArgumentOutOfRangeException();
			}
			else if (x > 1.0E+7)
			{
				return (float)System.Math.Log(x);
			}
			else if (System.Math.Abs(x) < 1.0E-7)
			{
				return x;
			}
			else
			{
				double y = 1.0 + x;
				return (float)(System.Math.Log(y) - ((y - 1) - x) / y);
			}

		}

		/// <summary>
		/// Return the value of Log(Abs(z)).
		/// </summary>
		/// <param name="value">The complex number.</param>
		/// <returns>The calculated value.</returns>
		private static double LogAbs(ComplexFloat value)
		{

			double xabs = System.Math.Abs(value.Real);
			double yabs = System.Math.Abs(value.Imaginary);

			double max, u;
			if (xabs >= yabs)
			{
				max = xabs;
				u = yabs / xabs;
			}
			else
			{
				max = yabs;
				u = xabs / yabs;
			}

			return System.Math.Log(max) + 0.5 * ComplexMath.Log1p(u * u);
		}



		///<summary>Return the complex logarithm of a complex type</summary>
		public static ComplexFloat Log(ComplexFloat value)
		{
			if (value.IsNaN())
			{
				return ComplexFloat.NaN;
			}
			return new ComplexFloat((float)ComplexMath.LogAbs(value), (float)System.Math.Atan2(value.Imaginary, value.Real));

		}

		///<summary>Given two complex types return the one with the maximum norm</summary>
		public static ComplexFloat Max(ComplexFloat v1, ComplexFloat v2)
		{
			if (Norm(v1) >= Norm(v2))
			{
				return v1;
			}
			else
			{
				return v2;
			}
		}

		///<summary>Return the Euclidean norm of a complex type</summary>
		public static float Norm(ComplexFloat value)
		{
			return ComplexMath.Absolute(value);
		}

		///<summary>Return the polar representation of a complex type</summary>
		public static ComplexFloat Polar(ComplexFloat value)
		{
			return new ComplexFloat(ComplexMath.Absolute(value), (float)System.Math.Atan2(value.Imaginary, value.Real));
		}

		///<summary>Returns the sine of a complex type</summary>
		public static ComplexFloat Sin(ComplexFloat value)
		{
			if (value.IsNaN())
			{
				return ComplexFloat.NaN;
			}
			return new ComplexFloat((float)(System.Math.Sin(value.Real) * System.Math.Cosh(value.Imaginary)), (float)(System.Math.Cos(value.Real) * System.Math.Sinh(value.Imaginary)));
		}

		///<summary>Returns the hyperbolic sine of a complex type</summary>
		public static ComplexFloat Sinh(ComplexFloat value)
		{
			if (value.IsNaN())
			{
				return ComplexFloat.NaN;
			}
			if (value.Imaginary == 0)
			{
				return new ComplexFloat((float)System.Math.Sinh(value.Real));
			}
			if (value.Real == 0)
			{
				return new ComplexFloat(0, (float)System.Math.Sin(value.Imaginary));
			}
			return new ComplexFloat((float)(System.Math.Sinh(value.Real) * System.Math.Cos(value.Imaginary)), (float)(System.Math.Cosh(value.Real) * System.Math.Sin(value.Imaginary)));
		}

		///<summary>Returns the square root of a complex type</summary>
		public static ComplexFloat Sqrt(ComplexFloat value)
		{
			ComplexFloat result;
			if (value.IsNaN())
			{
				return ComplexFloat.NaN;
			}

			if (value.IsInfinity())
			{
				return ComplexFloat.Infinity;
			}

			if ((value.Real == 0.0) && (value.Imaginary == 0.0))
			{
				result = ComplexFloat.Zero;
			}
			else
			{
				double x, y, t;

				x = System.Math.Abs(value.Real);
				y = System.Math.Abs(value.Imaginary);
				if (x >= y)
				{
					t = y / x;
					t = System.Math.Sqrt(x) * System.Math.Sqrt(0.5 * (1.0 + System.Math.Sqrt(1.0 + t * t)));
				}
				else
				{
					t = x / y;
					t = System.Math.Sqrt(y) * System.Math.Sqrt(0.5 * (t + System.Math.Sqrt(1.0 + t * t)));
				}

				if (value.Real >= 0.0)
				{
					result = new ComplexFloat((float)t, (float)(value.Imaginary / (2.0 * t)));
				}
				else if (value.Imaginary >= 0.0)
				{
					result = new ComplexFloat((float)(value.Imaginary / (2.0 * t)), (float)t);
				}
				else
				{
					result = new ComplexFloat((float)(-value.Imaginary / (2.0 * t)), (float)-t);
				}
			}

			return result;
		}

		///<summary>Returns the tangent of a complex type</summary>
		public static ComplexFloat Tan(ComplexFloat value)
		{
			if (value.IsNaN())
			{
				return ComplexFloat.NaN;
			}

			double real = 2.0 * value.Real;
			double imag = 2.0 * value.Imaginary;
			double denom = System.Math.Cos(real) + System.Math.Cosh(imag);

			return new ComplexFloat((float)(System.Math.Sin(real) / denom), (float)(System.Math.Sinh(imag) / denom));

		}

		///<summary>Returns the hyperbolic tangent of a complex type</summary>
		public static ComplexFloat Tanh(ComplexFloat value)
		{
			if (value.IsNaN())
			{
				return ComplexFloat.NaN;
			}

			double real = 2.0 * value.Real;
			double imag = 2.0 * value.Imaginary;
			double denom = System.Math.Cosh(real) + System.Math.Cos(imag);

			return new ComplexFloat((float)(System.Math.Sinh(real) / denom), (float)(System.Math.Sin(imag) / denom));
		}

		/// <summary>Returns the inverse sine of a complex type.</summary>
		public static ComplexFloat Asin(ComplexFloat value)
		{
			if (value.IsNaN())
			{
				return ComplexFloat.NaN;
			}
			ComplexFloat ni = new ComplexFloat(0, -1);
			return ni * ComplexMath.Log(ComplexFloat.I * value + ComplexMath.Sqrt(1 - value * value));

		}

		/// <summary>Returns the inverse cosine of a complex type.</summary>
		public static ComplexFloat Acos(ComplexFloat value)
		{
			if (value.IsNaN())
			{
				return ComplexFloat.NaN;
			}
			ComplexFloat ni = new ComplexFloat(0, -1);
			return ni * ComplexMath.Log(value + ComplexFloat.I * ComplexMath.Sqrt(1 - value * value));
		}

		/// <summary>Returns the inverse tangent of a complex type.</summary>
		public static ComplexFloat Atan(ComplexFloat value)
		{
			if (value.IsNaN())
			{
				return ComplexFloat.NaN;
			}
			return (1 / (ComplexFloat.I * 2)) * (ComplexMath.Log((1 + ComplexFloat.I * value)) - ComplexMath.Log((1 - ComplexFloat.I * value)));
		}

		///<summary>Returns the inverse hyperbolic sine of a complex type</summary>
		public static ComplexFloat Asinh(ComplexFloat value)
		{
			if (value.IsNaN())
			{
				return ComplexFloat.NaN;
			}
			return -ComplexMath.Log(ComplexMath.Sqrt(1 + value * value) - value);
		}

		///<summary>Returns the inverse hyperbolic cosine of a complex type</summary>
		public static ComplexFloat Acosh(ComplexFloat value)
		{
			if (value.IsNaN())
			{
				return ComplexFloat.NaN;
			}
			return 2 * ComplexMath.Log((ComplexMath.Sqrt(((value + 1) / 2)) + ComplexMath.Sqrt((value - 1) / 2)));
		}

		///<summary>Returns the inverse hyperbolic tangent of a complex type</summary>
		public static ComplexFloat Atanh(ComplexFloat value)
		{
			if (value.IsNaN())
			{
				return ComplexFloat.NaN;
			}
			
			return .5f * (ComplexMath.Log(1 + value) - ComplexMath.Log(1 - value));
		}
	}
}