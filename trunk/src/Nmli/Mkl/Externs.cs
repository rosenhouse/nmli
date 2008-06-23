using System.Runtime.InteropServices;
using System.Security;

namespace Nmli.Mkl
{
    [SuppressUnmanagedCodeSecurity]
    internal static class Externs
    {
        internal const string dllName = "mkl.dll";

        #region VML

        [DllImport(dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void vdLn(int n, [In] double[] x, [In, Out] double[] y);

        [DllImport(dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void vdExp(int n, [In] double[] x, [In, Out] double[] y);



        [DllImport(dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void vsLn(int n, [In] float[] x, [In, Out] float[] y);
        
        [DllImport(dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void vsExp(int n, [In] float[] x, [In, Out] float[] y);


        #endregion

        #region CBlas


        #region Single

        #region Level 1
        [DllImport(dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern float cblas_sdot(int n, float[] x, int incX, float[] y, int incY);

        [DllImport(dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern float cblas_snrm2(int n, float[] x, int incX);

        [DllImport(dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern float cblas_sasum(int n, float[] x, int incX);

        [DllImport(dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void cblas_sswap(int n, [In, Out] float[] x, int incX, [In, Out] float[] y, int incY);

        [DllImport(dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void cblas_scopy(int n, float[] x, int incX, [In, Out] float[] y, int incY);

        [DllImport(dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void cblas_saxpy(int n, float alpha, float[] x, int incX, [In, Out] float[] y, int incY);

        [DllImport(dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void cblas_sscal(int n, float alpha, [In, Out] float[] x, int incX);
        #endregion


        #region Level 2
        [DllImport(dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void cblas_ssymv(Order order, UpLo UpLo, int n, float alpha, float[] a, int lda, float[] x, int incX, float beta, [In, Out] float[] y, int incY);

        [DllImport(dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void cblas_ssbmv(Order order, UpLo uplo, int n, int k, float alpha, float[] a, int lda, float[] x, int incX, float beta, [In, Out] float[] y, int incY);

        [DllImport(dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void cblas_sgemv(Order order, Transpose transA, int m, int n, float alpha, float[] a, int lda, float[] x, int incX, float beta, [In, Out] float[] y, int incY);

        [DllImport(dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void cblas_sgbmv(Order order, Transpose transA, int m, int n, int kl, int ku, float alpha, float[] a, int lda, float[] x, int incX, float beta, [In, Out] float[] y, int incY);

        [DllImport(dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void cblas_sger(Order order, int m, int n, float alpha, float[] x, int incX, float[] y, int incY, [In, Out] float[] a, int lda);

        #endregion

        #endregion



        #region Double

        #region Level 1
        [DllImport(dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern double cblas_ddot(int n, double[] x, int incX, double[] y, int incY);

        [DllImport(dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern double cblas_dnrm2(int n, double[] x, int incX);

        [DllImport(dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern double cblas_dasum(int n, double[] x, int incX);

        [DllImport(dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void cblas_dswap(int n, [In, Out] double[] x, int incX, [In, Out] double[] y, int incY);

        [DllImport(dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void cblas_dcopy(int n, double[] x, int incX, [In, Out] double[] y, int incY);

        [DllImport(dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void cblas_daxpy(int n, double alpha, double[] x, int incX, [In, Out] double[] y, int incY);

        [DllImport(dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void cblas_dscal(int n, double alpha, [In, Out] double[] x, int incX);
        #endregion


        #region Level 2
        [DllImport(dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void cblas_dsymv(Order order, UpLo UpLo, int n, double alpha, double[] a, int lda, double[] x, int incX, double beta, [In, Out] double[] y, int incY);

        [DllImport(dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void cblas_dsbmv(Order order, UpLo uplo, int n, int k, double alpha, double[] a, int lda, double[] x, int incX, double beta, [In, Out] double[] y, int incY);

        [DllImport(dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void cblas_dgemv(Order order, Transpose transA, int m, int n, double alpha, double[] a, int lda, double[] x, int incX, double beta, [In, Out] double[] y, int incY);

        [DllImport(dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void cblas_dgbmv(Order order, Transpose transA, int m, int n, int kl, int ku, double alpha, double[] a, int lda, double[] x, int incX, double beta, [In, Out] double[] y, int incY);

        [DllImport(dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void cblas_dger(Order order, int m, int n, double alpha, double[] x, int incX, double[] y, int incY, [In, Out] double[] a, int lda);

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
        internal static extern void SPOTRF(ref byte UpLo, ref int n, [In, Out] float[] a, ref int lda, ref int info);

        [DllImport(dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SPOTRI(ref byte UpLo, ref int n, [In, Out] float[] a, ref int lda, ref int info);



        [DllImport(dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SPOSV(ref byte UpLo, ref int n, ref int nrhs, [In, Out] float[] a, ref int lda, [In, Out] float[] b, ref int ldb, ref int info);

        #endregion



        #region Double
        [DllImport(dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DGETRF(ref int m, ref int n, [In, Out] double[] a, ref int lda, [In, Out] int[] ipiv, ref int info);

        [DllImport(dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DGETRI(ref int n, [In, Out] double[] a, ref int lda, int[] ipiv, [In, Out] double[] work, ref int lwork, ref int info);

        

        [DllImport(dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DPOTRF(ref byte UpLo, ref int n, [In, Out] double[] a, ref int lda, ref int info);

        [DllImport(dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DPOTRI(ref byte UpLo, ref int n, [In, Out] double[] a, ref int lda, ref int info);


        [DllImport(dllName, ExactSpelling = true, SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DPOSV(ref byte UpLo, ref int n, ref int nrhs, [In, Out] double[] a, ref int lda, [In, Out] double[] b, ref int ldb, ref int info);

        #endregion


        #endregion

    }
}