using System;

namespace Nmli.CollectionAgnostic
{
    
    public interface ITest<AT>
    {
        void Mul(int n, AT a, AT b, AT y);
    }


    /// <summary>
    /// Vector math library functions for a generic array type 
    /// </summary>
    /// <typeparam name="AT">Generic array type (eg pointer, managed array, etc)</typeparam>
    public interface IVml<AT>
    {
        /// <summary>
        /// Computes the natural logarithm elementwise
        /// </summary>
        /// <param name="n">Number of elements</param>
        /// <param name="x">Input vector</param>
        /// <param name="y">Output vector</param>
        void Ln(int n, AT x, AT y);


        /// <summary>
        /// Computes the natural exponential e^x elementwise
        /// </summary>
        /// <param name="n">Number of elements</param>
        /// <param name="x">Input vector</param>
        /// <param name="y">Output vector</param>
        void Exp(int n, AT x, AT y);


        /// <summary>
        /// Adds two vectors elementwise: y = a + b
        /// </summary>
        /// <param name="n">Number of elements</param>
        /// <param name="a">Addend a</param>
        /// <param name="b">Addend b</param>
        /// <param name="y">Sum</param>
        void Add(int n, AT a, AT b, AT y);

        /// <summary>
        /// Subtracts two vectors elementwise: y = a - b
        /// </summary>
        /// <param name="n">Number of elements</param>
        /// <param name="a">Minuend</param>
        /// <param name="b">Subtrahend</param>
        /// <param name="y">Difference</param>
        void Sub(int n, AT a, AT b, AT y);

        /// <summary>
        /// Squares each element of a vector
        /// </summary>
        /// <param name="n">Number of elements</param>
        /// <param name="a">Input (to be squared)</param>
        /// <param name="y">Output (squared)</param>
        void Sqr(int n, AT a, AT y);


        /// <summary>
        /// Inverts each element of a vector
        /// </summary>
        /// <param name="n">Number of elements</param>
        /// <param name="a">Input (to be inverted)</param>
        /// <param name="y">Output (inverse of input)</param>
        void Inv(int n, AT a, AT y);


        /// <summary>
        /// Square-roots each element of a vector
        /// </summary>
        /// <param name="n">Number of elements</param>
        /// <param name="a">Input (to be square-rooted)</param>
        /// <param name="y">Output (square roots)</param>
        void Sqrt(int n, AT a, AT y);
    }
}
