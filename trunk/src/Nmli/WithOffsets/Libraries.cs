

namespace Nmli.WithOffsets
{
    public static class Libraries
    {
        static readonly IMultOffsets multTest = new MklWithOffsets();

        public static IMultOffsets TestLib { get { return multTest; } }

        static readonly IBlas acmlBlas = new Acml.Blas();
        public static IBlas AcmlBlas { get { return acmlBlas; } }

        static readonly IBlas mklBlas = new Mkl.Blas();
        public static IBlas MklBlas { get { return mklBlas; } }

        public static IBlas DefaultBlas
        {
            get
            {
                if (Nmli.Libraries.defaultImp == LibraryImplementations.ACML)
                    return acmlBlas;
                else if (Nmli.Libraries.defaultImp == LibraryImplementations.MKL)
                    return mklBlas;
                else
                    throw new System.Exception("Unrecognzied library preference.");
            }
        }
    }


    public static class Libraries<N>
    {
        public static IMultOffsets<N> TestLib { get { return (IMultOffsets<N>)Libraries.TestLib; } }

        public static IBlas<N> AcmlBlas { get { return (IBlas<N>)Libraries.AcmlBlas; } }

        public static IBlas<N> MklBlas { get { return (IBlas<N>)Libraries.MklBlas; } }
    }
}
