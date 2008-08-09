/*
 * ComplexDouble.cs
 * 
 * Copyright (c) 2003-2005, dnAnalytics. All rights reserved.
*/

#region Using directives

using System;
using System.Runtime.InteropServices;
using System.Text;

#endregion Using directives

namespace Nmli.Complex
{

	///<summary>complex double type.</summary>
	///<remarks>See <see cref="ComplexMath">ComplexMath</see> for complex math functions.</remarks>
	///<seealso cref="dnAnalytics.Nli.ComplexMath"/>
	[Serializable]
	[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]
	public struct ComplexDouble : IFormattable {
		#region Constants

		private static readonly ComplexDouble zero = new ComplexDouble(0);
		private static readonly ComplexDouble one = new ComplexDouble(1);
		private static readonly ComplexDouble nan = new ComplexDouble(Double.NaN, Double.NaN);
		private static readonly ComplexDouble infinity = new ComplexDouble(Double.PositiveInfinity, Double.PositiveInfinity);
        private static readonly ComplexDouble i = new ComplexDouble(0, 1);

		#endregion Constants

		#region Fields

		private readonly double real;
		private readonly double imag;

		#endregion Fields

		#region Constructors

		///<summary>Constructor for complex double precision number type</summary>
		///<param name="real">Real value of complex number expressed as <c>double</c>.</param>
		public ComplexDouble(double real) : this(real, 0) {}

		///<summary>Constructor for complex double precision number type</summary>
		///<param name="real">Real value of complex number expressed as <c>double</c>.</param>
		///<param name="imaginary">Imaginary part of complex number expressed as <c>double</c>.</param>
		public ComplexDouble(double real, double imaginary) {
			this.real = real;
			this.imag = imaginary;
		}

		///<summary>Created a <c>ComplexDouble</c> from the given string. The string can be in the
		///following formats: <c>n</c>, <c>ni</c>, <c>n +/- ni</c>, <c>n,n</c>, <c>n,ni</c>,
		///<c>(n,n)</c>, or <c>(n,ni)</c>, where n is a real number.</summary>
		///<param name="value">The string to create the <c>ComplexDouble</c> from.</param>
		///<exception cref="FormatException">if the n, is not a number.</exception>
		///<exception cref="ArgumentNullException">if s, is <c>null</c>.</exception>
		public ComplexDouble(string value) : this(value, null) {}

		///<summary>Created a <c>ComplexDouble</c> from the given string. The string can be in the
		///following formats: <c>n</c>, <c>ni</c>, <c>n +/- ni</c>, <c>n,n</c>, <c>n,ni</c>,
		///<c>(n,n)</c>, or <c>(n,ni)</c>, where n is a real number.</summary>
		///<param name="value">The string to create the <c>ComplexDouble</c> from.</param>
		///<param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		///<exception cref="FormatException">if the n, is not a number.</exception>
		///<exception cref="ArgumentNullException">if s, is <c>null</c>.</exception>
		public ComplexDouble(string value, IFormatProvider formatProvider) {
			this = ComplexDouble.Parse(value, formatProvider);
		}

		#endregion Constructors

		#region Properties

		///<summary>Constant value of one.</summary>
		public static ComplexDouble One {
			get { return one; }
		}

		///<summary>Constant value of zero.</summary>
		public static ComplexDouble Zero {
			get { return zero; }
		}

		///<summary>Constant value of NaN.</summary>
		public static ComplexDouble NaN {
			get { return nan; }
		}

		///<summary>Constant value of Infinity.</summary>
		public static ComplexDouble Infinity {
			get { return infinity; }
		}

		///<summary>Property to access Real component</summary>
		public double Real {
			get { return this.real; }
		}

		///<summary>Property to access Imaginary component</summary>
		public double Imaginary {
			get { return this.imag; }
		}

		///<summary>Constant value for I.</summary>
		public static ComplexDouble I
		{
			get { return i; }
		}

		/// <summary>
		/// Returns the conjuagate of the <c>ComplexDouble</c>.
		/// </summary>
		public ComplexDouble Conjugate {
			get { return ComplexMath.Conjugate(this); }
		}

		/// <summary>
		/// Returns the Absolute of the <c>ComplexDouble</c>.
		/// </summary>
		public double Absolute {
			get { return ComplexMath.Absolute(this); }
		}

		#endregion Properties

		#region Type Conversions

		///<summary>Implicit conversion from double type</summary>
		public static implicit operator ComplexDouble(double value) {
			return new ComplexDouble(value);
		}

		///<summary>Implicit conversion from ComplexFloat type</summary>
		public static implicit operator ComplexDouble(ComplexFloat value) {
			return new ComplexDouble(value.Real, value.Imaginary);
		}

		///<summary>Implicit conversion from ComplexFloat type</summary>
		public static ComplexDouble ToComplexDouble(ComplexFloat value) {
			return new ComplexDouble(value.Real, value.Imaginary);
		}

		///<summary>Convert double type to ComplexDouble</summary>
		///<param name="value"><c>double</c> variable as real to</param>
		public static ComplexDouble ToComplex(double value) {
			return new ComplexDouble(value);
		}

		#endregion

		#region Public Members

		///<summary>Return the Hashcode for the <c>ComplexDouble</c></summary>
		///<returns>The Hashcode representation of <c>ComplexDouble</c></returns>
		public override int GetHashCode() {
			return (int) Math.Exp(ComplexMath.Absolute(this));
		}

		///<summary>Check if <c>ComplexDouble</c> variable is the same as another <c>ComplexDouble</c></summary>
		///<param name="obj"><c>obj</c> to compare present <c>ComplexDouble</c> to.</param>
		///<returns>Returns true if the two objects are the same object, or the the Real and Imaginary 
		///components of both are equal, false otherwise</returns>
		public bool Equals(ComplexDouble obj) {
			return this.Real == obj.Real && this.Imaginary == obj.Imaginary;
		}

		///<summary>Check if <c>ComplexDouble</c> variable is the same as a <c>ComplexFloat</c></summary>
		///<param name="obj"><c>obj</c> to compare present <c>ComplexDouble</c> to.</param>
		///<returns>Returns true if the two objects are the same object, or the the Real and Imaginary 
		///components of both are equal, false otherwise</returns>
		public bool Equals(ComplexFloat obj) {
			return this.Real == obj.Real && this.Imaginary == obj.Imaginary;
		}

		///<summary>Check if <c>ComplexDouble</c> variable is the same as another object</summary>
		///<param name="obj"><c>obj</c> to compare present <c>ComplexDouble</c> to.</param>
		///<returns>Returns true if the two objects are the same object, or the the Real and Imaginary 
		///components of both are equal, false otherwise</returns>
		public override bool Equals(Object obj) {
			if (obj == null) {
				return false;
			}
			if (obj is ComplexDouble) {
				ComplexDouble rightSide = (ComplexDouble) obj;
				return this.Equals(rightSide);
			} else if (obj is ComplexFloat) {
				ComplexFloat rightSide = (ComplexFloat) obj;
				return this.Equals(rightSide);
			} else {
				return false;
			}
		}

		///<summary>Equal operator to compare two <c>ComplexDouble</c> variables</summary>
		///<remarks>Returns false if the two variables are not equals using Equals function</remarks>
		public static bool operator ==(ComplexDouble o1, ComplexDouble o2) {
			return o1.Equals(o2);
		}

		///<summary>Not Equal operator to compare two <c>ComplexDouble</c> variables</summary>
		///<remarks>Returns false if the two variables are equal using Equals function</remarks>
		public static bool operator !=(ComplexDouble o1, ComplexDouble o2) {
			return !o1.Equals(o2);
		}

		///<summary>Positive Operator</summary>
		public static ComplexDouble operator +(ComplexDouble value) {
			return value;
		}

		///<summary>Positive Operator</summary>
		public static ComplexDouble Plus(ComplexDouble value) {
			return value;
		}

		///<summary>Addition Operator</summary>
		public static ComplexDouble operator +(ComplexDouble leftSide, ComplexDouble rightSide) {
			return new ComplexDouble(leftSide.Real + rightSide.Real, leftSide.Imaginary + rightSide.Imaginary);
		}

		///<summary>Addition Operator</summary>
		public static ComplexDouble Add(ComplexDouble leftSide, ComplexDouble rightSide) {
			return leftSide + rightSide;
		}

		///<summary>Negate Operator</summary>
		public static ComplexDouble operator -(ComplexDouble value) {
			return new ComplexDouble(-value.Real, -value.Imaginary);
		}

		///<summary>Negate Operator</summary>
		public static ComplexDouble Negate(ComplexDouble value) {
			return -value;
		}

		///<summary>Subtraction Operator</summary>
		public static ComplexDouble operator -(ComplexDouble leftSide, ComplexDouble rightSide) {
			return new ComplexDouble(leftSide.Real - rightSide.Real, leftSide.Imaginary - rightSide.Imaginary);
		}

		///<summary>Subtraction Operator</summary>
		public static ComplexDouble Subtract(ComplexDouble leftSide, ComplexDouble rightSide) {
			return leftSide - rightSide;
		}

		///<summary>Multiplication Operator</summary>
		public static ComplexDouble operator *(ComplexDouble leftSide, ComplexDouble rightSide) {
			return new ComplexDouble(leftSide.Real*rightSide.Real - leftSide.Imaginary*rightSide.Imaginary, leftSide.Real*rightSide.Imaginary + leftSide.Imaginary*rightSide.Real);
		}

		///<summary>Multiplication Operator</summary>
		public static ComplexDouble Multiply(ComplexDouble leftSide, ComplexDouble rightSide) {
			return leftSide*rightSide;
		}

		///<summary>Division Operator</summary>
		public static ComplexDouble operator /(ComplexDouble leftSide, ComplexDouble rightSide) {
			if (rightSide.real == 0.0 && rightSide.Imaginary == 0.0)
			{
				return ComplexDouble.NaN;
			}

			if (rightSide.IsInfinity() && !leftSide.IsInfinity())
			{
				return ComplexDouble.zero;
			}

			double real, imag;
			if (System.Math.Abs(rightSide.real) >= System.Math.Abs(rightSide.imag))
			{
				double temp = rightSide.imag / rightSide.real;
				double denom = rightSide.real + temp * rightSide.imag;
				real = (leftSide.real + temp * leftSide.imag) / denom;
				imag = (leftSide.imag - temp * leftSide.real) / denom;
			}
			else
			{
				double temp = rightSide.real / rightSide.imag;
				double denom = rightSide.imag + temp * rightSide.real;
				real = (leftSide.real * temp + leftSide.imag) / denom;
				imag = (leftSide.imag * temp - leftSide.real) / denom;
			}
			return new ComplexDouble(real, imag);
		}

		///<summary>Division Operator</summary>
		public static ComplexDouble Divide(ComplexDouble leftSide, ComplexDouble rightSide) {
			return leftSide/rightSide;
		}

		///<summary>Tests whether the the complex number is not a number.</summary>
		///<returns>True if either the real or imaginary components are NaN, false otherwise.</returns>
		public bool IsNaN() {
			if (this == nan) {
				return true;
			} else {
				return (Double.IsNaN(real) || Double.IsNaN(imag));
			}
		}

		///<summary>Tests whether the the complex number is infinite.</summary>
		///<returns>True if either the real or imaginary components are infinite, false otherwise.</returns>
		public bool IsInfinity() {
			if (this == infinity) {
				return true;
			} else {
				return (Double.IsInfinity(real) || Double.IsInfinity(imag));
			}
		}

		// --- IFormattable Interface --
		///<summary>A string representation of this <c>ComplexDouble</c>.</summary>
		///<returns>The string representation of the value of <c>this</c> instance.</returns>
		public override string ToString() {
			return ToString(null, null);
		}

		///<summary>A string representation of this <c>ComplexDouble</c>.</summary>
		///<param name="format">A format specification.</param>
		///<returns>The string representation of the value of <c>this</c> instance as specified by format.</returns>
		public string ToString(string format) {
			return ToString(format, null);
		}

		///<summary>A string representation of this <c>ComplexDouble</c>.</summary>
		///<param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		///<returns>The string representation of the value of <c>this</c> instance as specified by provider.</returns>
		public string ToString(IFormatProvider formatProvider) {
			return ToString(null, formatProvider);
		}

		///<summary>A string representation of this <c>ComplexDouble</c>.</summary>
		///<param name="format">A format specification.</param>
		///<param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		///<returns>The string representation of the value of <c>this</c> instance as specified by format and provider.</returns>
		///<exception cref="FormatException">if the n, is not a number.</exception>
		///<exception cref="ArgumentNullException">if s, is <c>null</c>.</exception>		
		public String ToString(string format, IFormatProvider formatProvider) {
			if (IsNaN()) {
				return "NaN";
			}
			if (IsInfinity()) {
				return "IsInfinity";
			}

			StringBuilder ret = new StringBuilder();

			ret.Append(real.ToString(format, formatProvider));
			if (imag < 0) {
				ret.Append(" ");
			} else {
				ret.Append(" + ");
			}
			ret.Append(imag.ToString(format, formatProvider)).Append("i");

			return ret.ToString();
		}

		/// <summary>Creates a <c>ComplexDouble</c> based on a string. The string can be in the
		///following formats: <c>n</c>, <c>ni</c>, <c>n +/- ni</c>, <c>n,n</c>, <c>n,ni</c>,
		///<c>(n,n)</c>, or <c>(n,ni)</c>, where n is a real number.</summary>
		/// <param name="value">the string to parse.</param>
		/// <returns>a <c>ComplexDouble</c> containing the value of the string.</returns>
		public static ComplexDouble Parse(String value) {
			return Parse(value, null);
		}

		/// <summary>Creates a <c>ComplexDouble</c> based on a string. The string can be in the
		///following formats: <c>n</c>, <c>ni</c>, <c>n +/- ni</c>, <c>n,n</c>, <c>n,ni</c>,
		///<c>(n,n)</c>, or <c>(n,ni)</c>, where n is a real number.</summary>
		/// <param name="value">the string to parse.</param>
		///<param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
		/// <returns>a <c>ComplexDouble</c> containing the value of the string.</returns>
		public static ComplexDouble Parse(String value, IFormatProvider formatProvider) {
			if (value == null) {
				throw new ArgumentNullException(value, "value cannot be null.");
			}
			value = value.Trim();
			if (value.Length == 0) {
				throw new FormatException();
			}

			//check if one character strings are valid
			if (value.Length == 1) {
				if (String.Compare(value, "i") == 0) {
					return new ComplexDouble(0, 1);
				} else {
					return new ComplexDouble(Double.Parse(value, formatProvider));
				}
			}

			//strip out parens
			if (value.StartsWith("(")) {
				if (!value.EndsWith(")")) {
					throw new FormatException();
				} else {
					value = value.Substring(1, value.Length - 2);
				}
			}

			string real = value;
			string imag = "0";

			//comma separated
			int index = value.IndexOf(',');
			if (index > -1) {
				real = value.Substring(0, index);
				imag = value.Substring(index + 1, value.Length - index - 1);
			} else {
				index = value.IndexOf('+', 1);
				if (index > -1) {
					real = value.Substring(0, index);
					imag = value.Substring(index + 1, value.Length - index - 1);
				} else {
					index = value.IndexOf('-', 1);
					if (index > -1) {
						real = value.Substring(0, index);
						imag = value.Substring(index, value.Length - index);
					}
				}
			}

			//see if we have numbers in the format xxxi
			if (real.EndsWith("i")) {
				if (!imag.Equals("0")) {
					throw new FormatException();
				} else {
					imag = real.Substring(0, real.Length - 1);
					real = "0";
				}
			}
			if (imag.EndsWith("i")) {
				imag = imag.Substring(0, imag.Length - 1);
			}
			//handle cases of - n, + n
			if (real.StartsWith("-")) {
				real = "-" + real.Substring(1, real.Length - 1).Trim();
			}
			if (imag.StartsWith("-")) {
				imag = "-" + imag.Substring(1, imag.Length - 1).Trim();
			}

			ComplexDouble ret;
			try {
				ret = new ComplexDouble(Double.Parse(real.Trim(), formatProvider), Double.Parse(imag.Trim(), formatProvider));
			} catch (Exception) {
				throw new FormatException();
			}
			return ret;
		}

		#endregion Public Members
	}
}