using System;
using System.Collections.Generic;
using System.Text;

namespace Nmli
{
    public interface IVml<T>
    {

        /// <summary>
        /// Computes the natural logarithm for each element
        /// </summary>
        /// <param name="n">Number of elements</param>
        /// <param name="x">Input vector</param>
        /// <param name="y">Output vector</param>
        void Ln(int n, T[] x, T[] y);



        /// <summary>
        /// Computes the natural exponential e^x of each element
        /// </summary>
        /// <param name="n">Number of elements</param>
        /// <param name="x">Input vector</param>
        /// <param name="y">Output vector</param>
        void Exp(int n, T[] x, T[] y);

    }

    public interface IVml : IVml<float>, IVml<double> { }

    
}
