using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Nmli.IO
{
    public static class ManagedIO2<N>
    {
        delegate void StreamCopier(Stream source, Stream target, long numElements);

        static readonly int size_of_N;
        static readonly bool N_is_f32;
        static readonly bool N_is_f64;

        const string InvalidGenericTypeErrMsg = "Only valid generic types are float32 and float64";

        static ManagedIO2()
        {
            Type t = typeof(N);
            size_of_N = Marshal.SizeOf(t);

            if (t == typeof(float))
                N_is_f32 = true;
            else if (t == typeof(double))
                N_is_f64 = true;
            else
                throw new InvalidOperationException(InvalidGenericTypeErrMsg);
        }


        static void CopyDirectly(Stream source, Stream target, long numElements)
        {
            long numBytes = numElements * size_of_N;

            int bufSize = 256 * 4;
            byte[] buffer = new byte[bufSize];

            long position = 0;
            while (position < numBytes)
            {
                long bytesLeft = numBytes - position;
                int bytesToCopyNow = (bufSize <= bytesLeft) ? bufSize : (int)(bytesLeft);

                source.Read(buffer, 0, bytesToCopyNow);
                target.Write(buffer, 0, bytesToCopyNow);

                position += bytesToCopyNow;
            }
            target.Flush();
        }

        static void CopyF32ToF64(Stream source, Stream target, long numElements)
        {
            BinaryReader br = new BinaryReader(source);
            BinaryWriter bw = new BinaryWriter(target);
            for (long i = 0; i < numElements; i++)
            {
                double d = br.ReadSingle();
                bw.Write(d);
            }
            target.Flush();
        }

        static void CopyF64ToF32(Stream source, Stream target, long numElements)
        {
            BinaryReader br = new BinaryReader(source);
            BinaryWriter bw = new BinaryWriter(target);
            for (long i = 0; i < numElements; i++)
            {
                float f = (float)(br.ReadDouble());
                bw.Write(f);
            }
            target.Flush();
        }


        static unsafe void ReadIntoArray(Stream source, Array target, 
            long numElements, StreamCopier sc)
        {
            GCHandle gch = GCHandle.Alloc(target, GCHandleType.Pinned);
            try
            {
                IntPtr p = Marshal.UnsafeAddrOfPinnedArrayElement(target, 0);
                byte* b = (byte*)p;
                long numBytes = numElements * size_of_N;
                UnmanagedMemoryStream ums = new UnmanagedMemoryStream(b, numBytes);
                sc(source, ums, numElements);
            }
            finally
            {
                gch.Free();
            }
        }

        static unsafe void WriteFromArray(Array source, Stream target,
            long numElements, StreamCopier sc)
        {
            GCHandle gch = GCHandle.Alloc(source, GCHandleType.Pinned);
            try
            {
                IntPtr p = Marshal.UnsafeAddrOfPinnedArrayElement(source, 0);
                byte* b = (byte*)p;
                long numBytes = numElements * size_of_N;
                UnmanagedMemoryStream ums = new UnmanagedMemoryStream(b, numBytes);
                sc(ums, target, numElements);
            }
            finally
            {
                gch.Free();
            }
        }



        public static void ReadF64s(Stream source, Array target)
        {
            int numElements = target.Length;

            StreamCopier sc;
            if (N_is_f64)
                sc = CopyDirectly;
            else if (N_is_f32)
                sc = CopyF64ToF32;
            else
                throw new InvalidOperationException(InvalidGenericTypeErrMsg);

            ReadIntoArray(source, target, numElements, sc);
        }

        public static void ReadF32s(Stream source, Array target)
        {
            int numElements = target.Length;

            StreamCopier sc;
            if (N_is_f32)
                sc = CopyDirectly;
            else if (N_is_f64)
                sc = CopyF32ToF64;
            else
                throw new InvalidOperationException(InvalidGenericTypeErrMsg);

            ReadIntoArray(source, target, numElements, sc);
        }


        public static void WriteF64s(Array source, Stream target)
        {
            int numElements = source.Length;

            StreamCopier sc;
            if (N_is_f64)
                sc = CopyDirectly;
            else if (N_is_f32)
                sc = CopyF64ToF32;
            else
                throw new InvalidOperationException(InvalidGenericTypeErrMsg);

            WriteFromArray(source, target, numElements, sc);
        }

        public static void WriteF32s(Array source, Stream target)
        {
            int numElements = source.Length;

            StreamCopier sc;
            if (N_is_f32)
                sc = CopyDirectly;
            else if (N_is_f64)
                sc = CopyF32ToF64;
            else
                throw new InvalidOperationException(InvalidGenericTypeErrMsg);

            WriteFromArray(source, target, numElements, sc);
        }
    }
}
