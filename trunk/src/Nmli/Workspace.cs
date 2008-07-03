using System;

namespace Nmli
{
    /// <summary>
    /// Encapsulates a resizable array for use as scratch space in computations.
    ///
    /// To use, first define a static Workspace field and mark it with the ThreadStatic attribute.
    /// Then when scratch space is needed, call the static method Get(,) referencing
    /// the field and providing the buffer size needed.  The field will be initialized
    /// with a fresh Workspace object if necessary, and then a buffer of sufficient length
    /// will be returned.  Allocations are amortized.
    /// 
    /// If more than one scratch buffer needs to be used simultaneously, create separate
    /// workspaces for each, since multiple calls to Get on a single object may return 
    /// references to the same array.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Workspace<T>
    {
        private Workspace() { }

        T[] buffer;

        /// <summary>
        /// Returns an array of T's which is guaranteed to be at least as large as size.
        /// Memory allocations are amortized.
        /// </summary>
        /// <param name="size">The minimum number of elements in the returned array</param>
        /// <returns>A buffer at least as large as specified by the argument</returns>
        T[] Get(int size)
        {
            if (buffer == null)
                buffer = new T[size];
            else if (buffer.Length < size)
                buffer = new T[2 * size];

            return buffer;
        }


        /// <summary>
        /// Ensures that the pointed-to workspace is non-null, and returns a buffer
        /// at least as large as the specified size. 
        /// </summary>
        /// <param name="workspaceField">Persistent storage for the workspace.</param>
        /// <param name="size">The minimum array size necessary.</param>
        /// <returns>A buffer at least as large as the given size.</returns>
        public static T[] Get(ref Workspace<T> workspaceField, int size)
        {
            if (workspaceField == null)
                workspaceField = new Workspace<T>();

            return workspaceField.Get(size);
        }
    }



    // TODO: provide a memory-release mechanism?  Expose it to client code?
}
