using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Nmli.IO
{

    public static class ManagedIO2
    {
        private delegate void StreamCopier(Stream source, Stream target, long numElements);

        public static int GetSize<T>() { return Marshal.SizeOf(typeof(T)); }
        static bool IsF32<T>() { return (typeof(T) == typeof(float)); }
        static bool IsF64<T>() { return (typeof(T) == typeof(double)); }

        const string InvalidGenericTypeErrMsg = "Only valid generic types are float32 and float64";

        static void ss_CopyDirectly<T>(Stream source, Stream target, long numElements)
        {
            long numBytes = numElements * GetSize<T>();

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

        static void ss_CopyF32ToF64(Stream source, Stream target, long numElements)
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

        static void ss_CopyF64ToF32(Stream source, Stream target, long numElements)
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


        static void ss_Copy<TIn, TOut>(Stream source, Stream target, long numElements)
        {
            if (typeof(TIn) == typeof(TOut))
                ss_CopyDirectly<TIn>(source, target, numElements);
            else
            {
                if (IsF32<TIn>() && IsF64<TOut>())
                    ss_CopyF32ToF64(source, target, numElements);
                else if (IsF64<TIn>() && IsF32<TOut>())
                    ss_CopyF64ToF32(source, target, numElements);
                else
                    throw new Exception(string.Format(
                        "Unclear how to copy streams of incompatible types {0} and {1}",
                        typeof(TIn).Name, typeof(TOut).Name));
            }
        }

        /// <summary>
        /// Encapsulates the given array as a memory stream and performs the given action on that stream.
        /// </summary>
        public static void ArrayAsStream<T>(T[] array, FileAccess access, Action<Stream> action)
        {
            StreamActionOnArray<T>(array, access, action);
        }

        /// <summary>
        /// Encapsulates the given array as a memory stream and performs the given action on that stream.
        /// </summary>
        public static void ArrayAsStream<T>(T[,] array, FileAccess access, Action<Stream> action)
        {
            StreamActionOnArray<T>(array, access, action);
        }

        /// <summary>
        /// Encapsulates the given array as a memory stream and performs the given action on that stream.
        /// </summary>
        static unsafe void StreamActionOnArray<TArray>(Array array, FileAccess fileAccess,
            Action<Stream> action)
        {
            long numElements = array.Length;
            long numBytes = numElements * GetSize<TArray>();

            GCHandle gch = GCHandle.Alloc(array, GCHandleType.Pinned);
            try
            {
                byte* bp = (byte*)(Marshal.UnsafeAddrOfPinnedArrayElement(array, 0).ToPointer());
                UnmanagedMemoryStream ums = new UnmanagedMemoryStream(bp, numBytes, numBytes, fileAccess );
                action(ums);
            }
            finally
            {
                gch.Free();
            }
        }


        static void sa_Copy<TIn, TOut>(Stream source, Array target)
        {
            StreamActionOnArray<TOut>(target, FileAccess.Write,
                delegate(Stream t) { ss_Copy<TIn, TOut>(source, t, target.Length); });
        }

        static void as_Copy<TIn, TOut>(Array source, Stream target)
        {
            StreamActionOnArray<TIn>(source, FileAccess.Read,
                delegate(Stream s) { ss_Copy<TIn, TOut>(s, target, source.Length); });
        }

        static void aa_Copy<TIn, TOut>(Array source, Array target)
        {
            if (source.Length != target.Length)
                throw new ArgumentException("Source and target must have the same length.");

            StreamActionOnArray<TIn>(source, FileAccess.Read,
                delegate(Stream s) { sa_Copy<TIn, TOut>(s, target); });
        }



        public static void Copy<TIn, TOut>(Stream source, TOut[] target) { sa_Copy<TIn, TOut>(source, target); }
        public static void Copy<TIn, TOut>(Stream source, TOut[,] target) { sa_Copy<TIn, TOut>(source, target); }
        public static void Copy<TIn, TOut>(Stream source, TOut[,,] target) { sa_Copy<TIn, TOut>(source, target); }
        public static void Copy<TIn, TOut>(Stream source, TOut[,,,] target) { sa_Copy<TIn, TOut>(source, target); }
        public static void Copy<TIn, TOut>(Stream source, TOut[,,,,] target) { sa_Copy<TIn, TOut>(source, target); }

        public static void Copy<TIn, TOut>(TIn[] source, Stream target) { as_Copy<TIn, TOut>(source, target); }
        public static void Copy<TIn, TOut>(TIn[,] source, Stream target) { as_Copy<TIn, TOut>(source, target); }
        public static void Copy<TIn, TOut>(TIn[,,] source, Stream target) { as_Copy<TIn, TOut>(source, target); }
        public static void Copy<TIn, TOut>(TIn[,,,] source, Stream target) { as_Copy<TIn, TOut>(source, target); }
        public static void Copy<TIn, TOut>(TIn[,,,,] source, Stream target) { as_Copy<TIn, TOut>(source, target); }


        public static void Copy<TIn, TOut>(TIn[] source, TOut[] target) { aa_Copy<TIn, TOut>(source, target); }
        public static void Copy<TIn, TOut>(TIn[] source, TOut[,] target) { aa_Copy<TIn, TOut>(source, target); }
        public static void Copy<TIn, TOut>(TIn[] source, TOut[,,] target) { aa_Copy<TIn, TOut>(source, target); }
        public static void Copy<TIn, TOut>(TIn[] source, TOut[,,,] target) { aa_Copy<TIn, TOut>(source, target); }
        public static void Copy<TIn, TOut>(TIn[] source, TOut[,,,,] target) { aa_Copy<TIn, TOut>(source, target); }

        public static void Copy<TIn, TOut>(TIn[,] source, TOut[] target) { aa_Copy<TIn, TOut>(source, target); }
        public static void Copy<TIn, TOut>(TIn[,] source, TOut[,] target) { aa_Copy<TIn, TOut>(source, target); }
        public static void Copy<TIn, TOut>(TIn[,] source, TOut[,,] target) { aa_Copy<TIn, TOut>(source, target); }
        public static void Copy<TIn, TOut>(TIn[,] source, TOut[,,,] target) { aa_Copy<TIn, TOut>(source, target); }
        public static void Copy<TIn, TOut>(TIn[,] source, TOut[,,,,] target) { aa_Copy<TIn, TOut>(source, target); }

        public static void Copy<TIn, TOut>(TIn[,,] source, TOut[] target) { aa_Copy<TIn, TOut>(source, target); }
        public static void Copy<TIn, TOut>(TIn[,,] source, TOut[,] target) { aa_Copy<TIn, TOut>(source, target); }
        public static void Copy<TIn, TOut>(TIn[,,] source, TOut[,,] target) { aa_Copy<TIn, TOut>(source, target); }
        public static void Copy<TIn, TOut>(TIn[,,] source, TOut[,,,] target) { aa_Copy<TIn, TOut>(source, target); }
        public static void Copy<TIn, TOut>(TIn[,,] source, TOut[,,,,] target) { aa_Copy<TIn, TOut>(source, target); }

        public static void Copy<TIn, TOut>(TIn[,,,] source, TOut[] target) { aa_Copy<TIn, TOut>(source, target); }
        public static void Copy<TIn, TOut>(TIn[,,,] source, TOut[,] target) { aa_Copy<TIn, TOut>(source, target); }
        public static void Copy<TIn, TOut>(TIn[,,,] source, TOut[,,] target) { aa_Copy<TIn, TOut>(source, target); }
        public static void Copy<TIn, TOut>(TIn[,,,] source, TOut[,,,] target) { aa_Copy<TIn, TOut>(source, target); }
        public static void Copy<TIn, TOut>(TIn[,,,] source, TOut[,,,,] target) { aa_Copy<TIn, TOut>(source, target); }

        public static void Copy<TIn, TOut>(TIn[,,,,] source, TOut[] target) { aa_Copy<TIn, TOut>(source, target); }
        public static void Copy<TIn, TOut>(TIn[,,,,] source, TOut[,] target) { aa_Copy<TIn, TOut>(source, target); }
        public static void Copy<TIn, TOut>(TIn[,,,,] source, TOut[,,] target) { aa_Copy<TIn, TOut>(source, target); }
        public static void Copy<TIn, TOut>(TIn[,,,,] source, TOut[,,,] target) { aa_Copy<TIn, TOut>(source, target); }
        public static void Copy<TIn, TOut>(TIn[,,,,] source, TOut[,,,,] target) { aa_Copy<TIn, TOut>(source, target); }


    }

}
