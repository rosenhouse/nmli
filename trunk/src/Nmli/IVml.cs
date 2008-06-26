using System;
using System.Collections.Generic;
using System.Text;

namespace Nmli
{
    public interface IVml<T>
    {

        /// <summary>
        /// Computes the natural logarithm elementwise
        /// </summary>
        /// <param name="n">Number of elements</param>
        /// <param name="x">Input vector</param>
        /// <param name="y">Output vector</param>
        void Ln(int n, T[] x, T[] y);


        /// <summary>
        /// Computes the natural exponential e^x elementwise
        /// </summary>
        /// <param name="n">Number of elements</param>
        /// <param name="x">Input vector</param>
        /// <param name="y">Output vector</param>
        void Exp(int n, T[] x, T[] y);


        /// <summary>
        /// Adds two vectors elementwise: y = a + b
        /// </summary>
        /// <param name="n">Number of elements</param>
        /// <param name="a">Addend a</param>
        /// <param name="b">Addend b</param>
        /// <param name="y">Sum</param>
        void Add(int n, T[] a, T[] b, T[] y);

        /// <summary>
        /// Subtracts two vectors elementwise: y = a - b
        /// </summary>
        /// <param name="n">Number of elements</param>
        /// <param name="a">Minuend</param>
        /// <param name="b">Subtrahend</param>
        /// <param name="y">Difference</param>
        void Sub(int n, T[] a, T[] b, T[] y);

        /// <summary>
        /// Squares each element of a vector
        /// </summary>
        /// <param name="n">Number of elements</param>
        /// <param name="a">Input (to be squared)</param>
        /// <param name="y">Output (squared)</param>
        void Sqr(int n, T[] a, T[] y);

    }

    public interface IVml : IVml<float>, IVml<double> { }

    
}
