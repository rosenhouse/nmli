using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace Nmli
{
    public static class Utilities
    {
        /// <summary>
        /// Convert an (MKL-centric) UpLo type into an ASCII byte suitable for Fortran interfaces
        /// </summary>
        public static byte EnumAsAscii(UpLo e)
        {
            if (e == UpLo.Upper)
                return 85; // ASCII 'U'
            else
                return 76; // ASCII 'L'
        }

        /// <summary>
        /// Convert an (MKL-centric) Transpose type into an ASCII byte suitable for Fortran interfaces
        /// </summary>
        public static byte EnumAsAscii(Transpose t)
        {
            if (t == Transpose.Trans)
                return 84; // ASCII 'T';

            if (t == Transpose.NoTrans)
                return 78; // ASCII 'N' 

            //else if (t == Transpose.ConjTrans)
            return 67; // ASCII 'C'
        }

        /// <summary>
        /// Convert an (MKL-centric) Diag type into an ASCII byte suitable for Fortran interfaces
        /// </summary>
        public static byte EnumAsAscii(Diag d)
        {
            if (d == Diag.Unit)
                return 85; // ASCII 'U'
            else
                return 78; // ASCII 'N'
        }


        /// <summary>
        /// Is true when the CLR is running as a 64-bit process, false otherwise.
        /// This is static readonly, so the JIT compiler should correctly optimize-out 
        /// dead-code conditionals.
        /// </summary>
        /// <remarks>We use this to determine which native DLLs to load at runtime</remarks>
        public static readonly bool is64Bit = (IntPtr.Size == 8);
        
        public static readonly bool isWindows = (Environment.OSVersion.Platform == PlatformID.Win32NT);


        public static string GetEnvVar(string name)
        {
            string value = Environment.GetEnvironmentVariable(name);
            if (value == null)
                throw new Exception("The environment variable '" + name + "' must be set.");
            else
                return value;
        }

        public const string PATH_ROOT_ENV_VAR_NAME = "NMLI_PATH";
        public const string DEFAULT_IMPL_ENV_VAR_NAME = "NMLI_IMPL";
        static readonly char _ps = System.IO.Path.DirectorySeparatorChar;

        static string BinPath()
        {
            const string tmpl = "The path '{0}' (derived from the environment variable '{1}') does not exist.";

            string path = GetEnvVar(PATH_ROOT_ENV_VAR_NAME);
            if (!(path.EndsWith("" + _ps)))
                path += _ps;
            path += "bin" + _ps;
            if (Directory.Exists(path))
                return path;
            else
                throw new DirectoryNotFoundException(string.Format(tmpl, path, PATH_ROOT_ENV_VAR_NAME));
        }

        public static string NativeLibraryRoot { get { return BinPath(); } }

        static string DefaultImp()
        {
            string imp = GetEnvVar(DEFAULT_IMPL_ENV_VAR_NAME);
            if (imp == null)
            {
                Console.WriteLine("Warning: The environment variable '"
                    + DEFAULT_IMPL_ENV_VAR_NAME + "' is not set.  Defaulting to MKL.");
                imp = "MKL";
            }
            return imp;
        }

        public static LibraryImplementations PreferredImplementation
        {
            get
            {
                return (LibraryImplementations)Enum.Parse(typeof(LibraryImplementations), 
                    DefaultImp(), true);
            }
        }


        public delegate void Func();
        public static void TempSwitchCurDir(string tempCurDir, Func f)
        {
            string cd = Environment.CurrentDirectory;
            try
            {
                Environment.CurrentDirectory = tempCurDir;
                f();
                Environment.CurrentDirectory = cd;
            }
            catch (Exception)
            {
                Environment.CurrentDirectory = cd;
                throw;
            }
        }

    }



    public class DisposableCollection<T> : IDisposable where T : IDisposable
    {
        IEnumerable<T> collection;
        public DisposableCollection(IEnumerable<T> collection)
        {
            this.collection = collection;
        }

        bool isDisposed = false;

        public void Dispose()
        {
            if (!isDisposed)
            {
                foreach (T x in collection)
                    x.Dispose();

                isDisposed = true;
                collection = null;
            }
        }
    }
}
