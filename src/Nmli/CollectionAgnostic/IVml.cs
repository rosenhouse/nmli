using System;

namespace Nmli.CollectionAgnostic
{
    
    public interface ITest<AT>
    {
        void Mul(int n, AT a, AT b, AT y);
    }

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
    }
}


namespace Nmli.WithOffsets
{

    public interface IVml<T> : CollectionAgnostic.IVml<OA<T>> { }


    public interface IMultOffsets<T> : CollectionAgnostic.ITest<OA<T>> { }

    unsafe public class MklTest : IMultOffsets<float>
    {
        public void Mul(int n, OA<float> a, OA<float> b, OA<float> y)
        {
            fixed (float* pa = &a.array[a.offset], 
                            pb = &b.array[b.offset],
                            py = &y.array[y.offset])
            {
                Mkl.UnsafeExterns.vsMul(n, pa, pb, py);
            }
        }
    }



    public struct OA<T>
    {
        internal readonly T[] array;
        internal readonly int offset;

        public T[] Array { get { return array; } }
        public int Offset { get { return offset; } }

        public OA(T[] array, int offset)
        {
            this.array = array;
            this.offset = offset;
        }

        public static implicit operator OA<T>(T[] ar) { return new OA<T>(ar, 0); }

    }

    
    public static class OA
    {
        public static OA<T> _<T>(T[] array, int offset) { return new OA<T>(array, offset); }
    }



}
