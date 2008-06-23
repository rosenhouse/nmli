using System;
using System.Collections.Generic;
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

            public IBlas Blas { get { return blas; } }
            public ILapack Lapack { get { return lapack; } }
            public IVml Vml { get { return vml; } }
            public ISml Sml { get { return sml; } }

            internal MathLibrary(IBlas blas, ILapack lapack, IVml vml)
            {
                this.blas = blas;
                this.lapack = lapack;
                this.vml = vml;
                this.sml = new Managed.BaseSml();
            }

            #region Interfaces
            IBlas<float> IMathLibrary<float>.Blas { get { return blas; } }
            ILapack<float> IMathLibrary<float>.Lapack { get { return lapack; } }
            IVml<float> IMathLibrary<float>.Vml { get { return vml; } }
            ISml<float> IMathLibrary<float>.Sml { get { return sml; } }

            IBlas<double> IMathLibrary<double>.Blas { get { return blas; } }
            ILapack<double> IMathLibrary<double>.Lapack { get { return lapack; } }
            IVml<double> IMathLibrary<double>.Vml { get { return vml; } }
            ISml<double> IMathLibrary<double>.Sml { get { return sml; } }

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

        static bool loadedMkl = false;
        static bool loadedAcml = false;

        static void windowsLoad(string path)
        {
            IntPtr address = LoadLibrary(path);
            if (address == IntPtr.Zero)
                throw new InvalidOperationException("Unable to load " + path);
        }

        static void tryLoad(string path)
        {
            if (Utilities.isWindows)
            {
                //Environment.CurrentDirectory = Path.GetDirectoryName(Utilities.CurrentDllPath);
                string p = Path.GetFullPath(path);

                Environment.SetEnvironmentVariable("PATH",
                    Path.GetDirectoryName(p) + ";"
                    + Environment.GetEnvironmentVariable("PATH"));

                windowsLoad(p);
            }
            else
            {
                throw new NotImplementedException("Running on an unsupported operating system.  I don't know how to load native libraries.");
            }
        }

        public static IMathLibrary Mkl
        {
            get
            {
                if (!loadedMkl)
                {
                    tryLoad(mklPath);
                    loadedMkl = true;
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
                    loadedAcml = true;
                }
                return acml;
            }
        }

        public static ISml Sml { get { return sml; } }

        static Libraries()
        {
            mkl = new MathLibrary(new Mkl.Blas(), new Mkl.Lapack(), new Mkl.Vml());
            acml = new MathLibrary(new Acml.Blas(), new Acml.Lapack(), new Acml.Vml());
            sml = new Managed.BaseSml();
            Console.WriteLine("Native Math Library Interface is loading on " + _bitness);
        }

        public static IMathLibrary Default
        {
            get
            {
                LibraryImplementations def = Utilities.PreferredImplementation;

                if (def == LibraryImplementations.ACML)
                    return Acml;
                else if (def == LibraryImplementations.MKL)
                    return Mkl;
                else
                    throw new Exception("Unrecognized library is preferred.");
            }
        }
    }
}
