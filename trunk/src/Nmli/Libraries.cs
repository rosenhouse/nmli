using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;

namespace Nmli
{
    public static class Libraries
    {
        class MathLibrary : IMathLibrary
        {
            readonly IBlas blas;
            readonly ILapack lapack;
            readonly IVml vml;
            readonly ISml sml;
            readonly IIO io;
            readonly WithOffsets.IBlas wo_blas;

            public IBlas Blas { get { return blas; } }
            public ILapack Lapack { get { return lapack; } }
            public IVml Vml { get { return vml; } }
            public ISml Sml { get { return sml; } }
            public IIO Io { get { return io; } }
            public WithOffsets.IBlas WithOffsets_Blas { get { return wo_blas; } }

            internal MathLibrary(IBlas blas, ILapack lapack, IVml vml, ISml sml, IIO io,
                WithOffsets.IBlas wo_blas)
            {
                this.blas = blas;
                this.lapack = lapack;
                this.vml = vml;
                this.sml = sml;
                this.io = io;
                this.wo_blas = wo_blas;
            }

            #region Interfaces
            IBlas<float> IMathLibrary<float>.Blas { get { return blas; } }
            ILapack<float> IMathLibrary<float>.Lapack { get { return lapack; } }
            IVml<float> IMathLibrary<float>.Vml { get { return vml; } }
            ISml<float> IMathLibrary<float>.Sml { get { return sml; } }
            IIO<float> IMathLibrary<float>.Io { get { return io; } }
            WithOffsets.IBlas<float> IMathLibrary<float>.WithOffsets_Blas { get { return wo_blas; } }

            IBlas<double> IMathLibrary<double>.Blas { get { return blas; } }
            ILapack<double> IMathLibrary<double>.Lapack { get { return lapack; } }
            IVml<double> IMathLibrary<double>.Vml { get { return vml; } }
            ISml<double> IMathLibrary<double>.Sml { get { return sml; } }
            IIO<double> IMathLibrary<double>.Io { get { return io; } }
            WithOffsets.IBlas<double> IMathLibrary<double>.WithOffsets_Blas { get { return wo_blas; } }

            #endregion
        }

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        static extern IntPtr LoadLibrary(string lpFileName);

        static readonly char _ps = System.IO.Path.DirectorySeparatorChar;
        static readonly string _acml = "ACML" + _ps + Nmli.Acml.Externs.dllName;
        static readonly string _Intel = "MKL" + _ps + Nmli.Mkl.Externs.dllName;
        static readonly string _bitness = Utilities.is64Bit ? "x64" : "x86";
        static readonly string libsPath = Utilities.NativeLibraryRoot + _ps + _bitness + _ps;
        static readonly string acmlPath = libsPath + _acml;
        static readonly string mklPath = libsPath + _Intel;

        static readonly MathLibrary mkl;
        static readonly MathLibrary acml;
        static readonly ISml sml;
        static readonly IIO io;

        static bool loadedMkl = false;
        static bool loadedAcml = false;

        static void windowsLoad(string path)
        {
            IntPtr address = LoadLibrary(path);
            if (address == IntPtr.Zero)
            {
                int errCode = Marshal.GetLastWin32Error();
                throw new InvalidOperationException("Unable to load " + path);
            }
            else
                Console.WriteLine("Successfully loaded native library " + path);
        }

        static void tryLoad(string path)
        {
            if (Utilities.isWindows)
            {
                string p = Path.GetFullPath(path);
                Utilities.TempSwitchCurDir(Path.GetDirectoryName(p), delegate() { windowsLoad(p); });
            }
            else
            {
                Console.WriteLine("No LoadLibrary() on this platform.  We'll let Mono handle that.");
            }
        }

        public static IMathLibrary Mkl
        {
            get
            {
                if (!loadedMkl)
                {
                    tryLoad(mklPath);
                    Versioning.PrintVersionMKL();
                    loadedMkl = true;
                    Console.WriteLine();
                }
                return mkl;
            }
        }

        public static IMathLibrary Acml
        {
            get
            {
                if (!loadedAcml)
                {
                    tryLoad(acmlPath);
                    Versioning.PrintVersionACML();
                    loadedAcml = true;
                    Console.WriteLine();
                }
                return acml;
            }
        }

        public static ISml Sml { get { return sml; } }
        public static IIO IO { get { return io; } }

        static Libraries()
        {
            sml = new Managed.BaseSml();
            io = new IO.ManagedIO();

            mkl = new MathLibrary(new Mkl.Blas(), new Mkl.Lapack(), new Mkl.Vml(),
                sml, io, new Nmli.WithOffsets.Mkl.Blas());
            acml = new MathLibrary(new Acml.Blas(), new Acml.Lapack(), new Acml.Vml(),
                sml, io, new Nmli.WithOffsets.Acml.Blas());

            Console.WriteLine("Native Math Library Interface is loading on " + _bitness);

            defaultImp = Utilities.PreferredImplementation;
            defaultFull = getDefault();
        }

        internal static readonly LibraryImplementations defaultImp;
        static readonly IMathLibrary defaultFull;

        static IMathLibrary getDefault()
        {
            if (defaultImp == LibraryImplementations.ACML)
                return Acml;
            else if (defaultImp == LibraryImplementations.MKL)
                return Mkl;
            else
                throw new Exception("Unrecognized library preference.");
        }

        public static IMathLibrary Default { get { return defaultFull; } }
    }


    /// <summary>
    /// Provides a data type-agnostic view of native math library implementations
    /// </summary>
    /// <typeparam name="N">Primitive data type.  Currently only float or double.</typeparam>
    /// <remarks>
    /// In this future, we plan on supporting complex float and complex double types also 
    /// </remarks>
    public static class Libraries<N>
    {
        static bool IsSingle { get { return (typeof(N) == typeof(float)); } }
        static bool IsDouble { get { return (typeof(N) == typeof(double)); } }

        static void ValidateType()
        {
            const string msg = "Data type '{0}' is not supported by NMLI";
            if (!IsSingle && !IsDouble)
                throw new NotSupportedException(
                    string.Format(msg, (typeof(N)).FullName));
        }

        static Libraries() { ValidateType(); }


        public static IMathLibrary<N> Default { get { return (IMathLibrary<N>)(Libraries.Default); } }

        public static IMathLibrary<N> Mkl { get { return (IMathLibrary<N>)(Libraries.Mkl); } }

        public static IMathLibrary<N> Acml { get { return (IMathLibrary<N>)(Libraries.Acml); } }

        public static ISml<N> Sml { get { return (ISml<N>)(Libraries.Sml); } }

        public static IIO<N> IO { get { return (IIO<N>)(Libraries.IO); } }

    }
}
