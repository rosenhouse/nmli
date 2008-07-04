using System;
using System.Collections.Generic;
using System.Text;

namespace Nmli
{
    namespace Abstract
    {
        public interface ICalcAdditiveGroup<N>
        {
            N Add(N x, N y);
            N Negate(N x);
            N Subtract(N x, N y);
            N Zero { get; }
        }

        public interface ICalcRing<N> : ICalcAdditiveGroup<N>
        {
            N One { get; }
            N Multiply(N x, N y);
        }

        public interface ICalcField<N> : ICalcRing<N>
        {
            N Invert(N x);
            N Divide(N x, N y);
        }
    }

    public interface ISml<N> : Abstract.ICalcField<N>
    {
        /// <summary>
        /// Converts a 8-byte (64-bit) double-precision floating point number to the generic type.
        /// Implementation should be small enough that the JITer inlines it.
        /// </summary>
        /// <param name="x">A 64-bit float</param>
        /// <returns>The numeric type of this VML</returns>
        N OfDouble(double x);


        /// <summary>
        /// Converts the argument type to a 8-byte (64-bit) double-precision floating point number.
        /// Implementation should be small enough that the JITer inlines it.
        /// </summary>
        /// <param name="x">The number</param>
        /// <returns>The number as a 64-bit float</returns>
        double ToDouble(N x);


        /// <summary>
        /// Converts the argument type to a 4-byte (32-bit) signed integer.
        /// Implementation should be small enough that the JITer inlines it.
        /// </summary>
        /// <param name="x">32-bit signed integer</param>
        /// <returns>Integer as a generic number type</returns>
        N OfInt(int x);
    }

    public interface ISml : ISml<double>, ISml<float> { }

    
}
