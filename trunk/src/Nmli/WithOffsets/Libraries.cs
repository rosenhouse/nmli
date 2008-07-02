using System;
using System.Collections.Generic;
using System.Text;

namespace Nmli.WithOffsets
{
    public static class Libraries
    {
        static readonly IMultOffsets multTest = new MklWithOffsets();

        public static IMultOffsets TestLib { get { return multTest; } }
    }

    public static class Libraries<N>
    {
        public static IMultOffsets<N> TestLib { get { return (IMultOffsets<N>)Libraries.TestLib; } }
    }
}
