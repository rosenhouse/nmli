using System;

namespace Nmli
{
    public class Workspace<T>
    {
        T[] buffer;

        /// <summary>
        /// Returns an array of T's which is guaranteed to be at least as large as size.
        /// Memory allocations are amoritized.
        /// </summary>
        /// <param name="size">The minimum number of elements in the returned array</param>
        /// <returns>A buffer at least as large as specified by the argument</returns>
        public T[] Get(int size)
        {
            if (buffer == null)
                buffer = new T[size];
            else if (buffer.Length < size)
                buffer = new T[2 * size];

            return buffer;
        }
    }



    // TODO: provide a memory-release mechanism?  Expose it to client code?
}
