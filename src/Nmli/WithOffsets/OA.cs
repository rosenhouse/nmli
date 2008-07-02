using System;


namespace Nmli.WithOffsets
{

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
        public static OA<T> O<T>(T[] array, int offset) { return new OA<T>(array, offset); }
    }
}
