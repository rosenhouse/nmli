using System;
using System.IO;

namespace Nmli
{
    public interface IIO<N>
    {
        /// <summary>
        /// Writes the first n elements of a buffer, preserving the data type
        /// </summary>
        void Write(BinaryWriter bw, int n, N[] buffer);
        
        /// <summary>
        /// Writes the first n elements of a buffer as 32-bit floats
        /// </summary>
        void WriteF32(BinaryWriter bw, int n, N[] buffer);
        

        /// <summary>
        /// Writes the first n elements of a buffer as 64-bit floats
        /// </summary>
        /// <param name="bw"></param>
        /// <param name="n"></param>
        /// <param name="buffer"></param>
        void WriteF64(BinaryWriter bw, int n, N[] buffer);

        
        /// <summary>
        /// Reads n elements into a buffer
        /// </summary>
        void Read(BinaryReader br, int n, N[] buffer);
        
        /// <summary>
        /// Reads n 32-bit floats into a buffer
        /// </summary>
        void ReadF32(BinaryReader br, int n, N[] buffer);
        
        /// <summary>
        /// Reads n 64-bit floats into a buffer
        /// </summary>
        void ReadF64(BinaryReader br, int n, N[] buffer);
    }

    public interface IIO : IIO<float>, IIO<double> { }

    
}
