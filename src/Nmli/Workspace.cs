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

        T[] get_local(int size)
        {
            if (buffer == null)
                buffer = new T[size];
            else if (buffer.Length < size)
                buffer = new T[2 * size];

            return buffer;
        }

        /// <summary>
        /// Ensures that the pointed-to workspace is non-null, and returns a buffer
        /// at least as large as the specified size.  Memory allocations are amortized.
        /// </summary>
        /// <param name="field">Persistent storage for the workspace.</param>
        /// <param name="size">The minimum array size necessary.</param>
        /// <returns>A buffer at least as large as the given size.</returns>
        public static T[] Get(ref Workspace<T> field, int size)
        {
            if (field == null)
                field = new Workspace<T>();

            return field.get_local(size);
        }

        /// <summary>
        /// Releases any allocated memory encapsulated in the given workspace, 
        /// allowing it to be garbage collected.
        /// </summary>
        /// <param name="field">The workspace to empty</param>
        public static void Empty(ref Workspace<T> field)
        {
            field.buffer = null;
            field = null;
        }

        /// <summary>
        /// Returns the number of elements in the backing array of the referenced workspace.
        /// If the reference is null or is an unused Workspace, returns 0.
        /// </summary>
        public static int GetCurrentSize(ref Workspace<T> field)
        {
            if ((field == null) || (field.buffer == null))
                return 0;
            else
                return field.buffer.Length;
        }
    }
}
