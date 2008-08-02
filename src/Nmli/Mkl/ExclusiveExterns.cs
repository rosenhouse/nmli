using System.Runtime.InteropServices;
using System.Security;

namespace Nmli.Mkl
{
    [SuppressUnmanagedCodeSecurity]
    public static class ExclusiveExterns
    {
        internal const string dllName = Externs.dllName;

        public static class AsArrays
        {

            [DllImport(dllName, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
            public static extern void vsErf(int n, float[] a, float[] y);

            [DllImport(dllName, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
            public static extern void vdErf(int n, double[] a, double[] y);


            [DllImport(dllName, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
            public static extern void vsErfc(int n, float[] a, float[] y);

            [DllImport(dllName, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
            public static extern void vdErfc(int n, double[] a, double[] y);


            [DllImport(dllName, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
            public static extern void vsErfInv(int n, float[] a, float[] y);

            [DllImport(dllName, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
            public static extern void vdErfInv(int n, double[] a, double[] y);



            [DllImport(dllName, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
            internal static extern void vsAbs(int n, float[] a, float[] y);

            [DllImport(dllName, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
            internal static extern void vdAbs(int n, double[] a, double[] y);

        }

        public static class AsRefs
        {
            #region Error function and inverse
            [DllImport(dllName, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
            public static extern void vsErf(int n, ref float a, ref float y);

            [DllImport(dllName, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
            public static extern void vdErf(int n, ref double a, ref double y);


            [DllImport(dllName, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
            public static extern void vsErfc(int n, ref float a, ref float y);

            [DllImport(dllName, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
            public static extern void vdErfc(int n, ref double a, ref double y);


            [DllImport(dllName, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
            public static extern void vsErfInv(int n, ref float a, ref float y);

            [DllImport(dllName, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
            public static extern void vdErfInv(int n, ref double a, ref double y);

            #endregion


            #region Basic arithmetic

            [DllImport(dllName, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
            internal static extern void vsMul(int n, ref float a, ref float b, ref float y);

            [DllImport(dllName, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
            internal static extern void vdMul(int n, ref double a, ref double b, ref double y);


            [DllImport(dllName, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
            internal static extern void vsDiv(int n, ref float a, ref float b, ref float y);

            [DllImport(dllName, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
            internal static extern void vdDiv(int n, ref double a, ref double b, ref double y);


            #endregion
        }
    }

}
