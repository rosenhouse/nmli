using System.Runtime.InteropServices;
using System.Security;

namespace Nmli.Acml
{
    [SuppressUnmanagedCodeSecurity]
    internal static class Externs
    {

        internal const string dllName = "libacml_dll.dll";
        internal const string vml_dllName = "libacml_mv_dll.dll";


        #region VML

        [DllImport(vml_dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void vrda_log(int n, [In] double[] x, [In, Out] double[] y);

        [DllImport(vml_dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void vrsa_logf(int n, [In] float[] x, [In, Out] float[] y);


        [DllImport(vml_dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void vrda_exp(int n, [In] double[] x, [In, Out] double[] y);

        [DllImport(vml_dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void vrsa_expf(int n, [In] float[] x, [In, Out] float[] y);


        #endregion



        #region BLAS



        #region Single

        #region Level 1
        [DllImport(dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern float sdot(int n, [In] float[] x, int incX, [In] float[] y, int incY);

        [DllImport(dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void sscal(int n, float alpha, [In, Out] float[] x, int incX);

        [DllImport(dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void scopy(int n, [In] float[] x, int incX, [In, Out] float[] y, int incY);

        [DllImport(dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void saxpy(int n, float alpha, [In] float[] x, int incX, [In, Out] float[] y, int incY);

        [DllImport(dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern float snrm2(int n, [In] float[] x, int incX);

        [DllImport(dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern float sasum(int n, [In] float[] x, int incX);

        #endregion

        #region Level 2
        [DllImport(dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ssymv(byte uplo, int n, float alpha, [In] float[] a, int lda, [In] float[] x, int incX, float beta, [In, Out] float[] y, int incY);

        [DllImport(dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ssbmv(byte uplo, int n, int k, float alpha, [In] float[] a, int lda, [In] float[] x, int incX, float beta, [In, Out] float[] y, int incY);

        [DllImport(dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void sgbmv(byte transA, int m, int n, int kl, int ku, float alpha, float[] a, int lda, float[] x, int incX, float beta, [In, Out] float[] y, int incY);

        [DllImport(dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void sgemv(byte transA, int m, int n, float alpha, float[] a, int lda, float[] x, int incX, float beta, [In, Out] float[] y, int incY);

        [DllImport(dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void sger(int m, int n, float alpha, float[] x, int incX, float[] y, int incY, [In, Out] float[] a, int lda);

        #endregion

        #endregion




        #region Double


        #region Level 1
        [DllImport(dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern double ddot(int n, [In] double[] x, int incX, [In]double[] y, int incY);

        [DllImport(dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern double dnrm2(int n, [In]double[] x, int incX);

        [DllImport(dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern double dasum(int n, [In]double[] x, int incX);

        [DllImport(dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void dswap(int n, [In, Out] double[] x, int incX, [In, Out] double[] y, int incY);

        [DllImport(dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void dcopy(int n, [In]double[] x, int incX, [In, Out] double[] y, int incY);

        [DllImport(dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void daxpy(int n, double alpha, [In]double[] x, int incX, [In, Out] double[] y, int incY);

        [DllImport(dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void dscal(int n, double alpha, [In, Out] double[] x, int incX);

        #endregion


        #region Level 2

        [DllImport(dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void dsymv(byte uplo, int n, double alpha, [In]double[] a, int lda, [In]double[] x, int incX, double beta, [In, Out] double[] y, int incY);

        [DllImport(dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void dsbmv(byte uplo, int n, int k, double alpha, [In] double[] a, int lda, [In] double[] x, int incX, double beta, [In, Out] double[] y, int incY);

        [DllImport(dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void dgemv(byte transA, int m, int n, double alpha, double[] a, int lda, double[] x, int incX, double beta, [In, Out] double[] y, int incY);

        [DllImport(dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void dgbmv(byte transA, int m, int n, int kl, int ku, double alpha, double[] a, int lda, double[] x, int incX, double beta, [In, Out] double[] y, int incY);

        [DllImport(dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void dger(int m, int n, double alpha, double[] x, int incX, double[] y, int incY, [In, Out] double[] a, int lda);

        #endregion

        #endregion



        #endregion





        #region Lapack


        #region Single

        [DllImport(dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SGETRF(ref int m, ref int n, [In, Out] float[] a, ref int lda, [In, Out] int[] ipiv, ref int info);

        [DllImport(dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SGETRI(ref int n, [In, Out] float[] a, ref int lda, int[] ipiv, [In, Out] float[] work, ref int lwork, ref int info);

        [DllImport(dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SPOTRF(ref byte uplo, ref int n, [In, Out] float[] a, ref int lda, ref int info);

        [DllImport(dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SPOTRI(ref byte uplo, ref int n, [In, Out] float[] a, ref int lda, ref int info);



        [DllImport(dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SPOSV(ref byte UpLo, ref int n, ref int nrhs, [In, Out] float[] a, ref int lda, [In, Out] float[] b, ref int ldb, ref int info);

        #endregion



        #region Double
        [DllImport(dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DGETRF(ref int m, ref int n, [In, Out] double[] a, ref int lda, [In, Out] int[] ipiv, ref int info);

        [DllImport(dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DGETRI(ref int n, [In, Out] double[] a, ref int lda,  int[] ipiv, [In, Out] double[] work, ref int lwork, ref int info);


        [DllImport(dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DPOTRF(ref byte uplo, ref int n, [In, Out] double[] a, ref int lda, ref int info);

        [DllImport(dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DPOTRI(ref byte uplo, ref int n, [In, Out] double[] a, ref int lda, ref int info);



        [DllImport(dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DPOSV(ref byte UpLo, ref int n, ref int nrhs, [In, Out] double[] a, ref int lda, [In, Out] double[] b, ref int ldb, ref int info);

        #endregion



        #endregion


    }
}