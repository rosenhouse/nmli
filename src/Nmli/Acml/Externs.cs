using System.Runtime.InteropServices;
using System.Security;

namespace Nmli.Acml
{
    [SuppressUnmanagedCodeSecurity]
    internal static class Externs
    {

        internal const string dllName = "libacml_dll.dll";
        internal const string vml_dllName = "libacml_mv_dll.dll";

        [DllImport(dllName, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void acmlinfo();


        #region VML

        [DllImport(vml_dllName, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void vrda_log(int n, double[] x, double[] y);

        [DllImport(vml_dllName, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void vrsa_logf(int n, float[] x, float[] y);


        [DllImport(vml_dllName, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void vrda_exp(int n, double[] x, double[] y);

        [DllImport(vml_dllName, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void vrsa_expf(int n, float[] x, float[] y);


        #endregion



        #region BLAS



        #region Single

        #region Level 1
        [DllImport(dllName, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int isamax(int n, float[] x, int incX);

        [DllImport(dllName, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        internal static extern float sdot(int n, float[] x, int incX, float[] y, int incY);

        [DllImport(dllName, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void sscal(int n, float alpha, float[] x, int incX);

        [DllImport(dllName, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void scopy(int n, float[] x, int incX, float[] y, int incY);

        [DllImport(dllName, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void saxpy(int n, float alpha, float[] x, int incX, float[] y, int incY);

        [DllImport(dllName, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        internal static extern float snrm2(int n, float[] x, int incX);

        [DllImport(dllName, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        internal static extern float sasum(int n, float[] x, int incX);

        #endregion

        #region Level 2
        [DllImport(dllName, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ssymv(byte uplo, int n, float alpha, float[] a, int lda, float[] x, int incX, float beta, float[] y, int incY);

        [DllImport(dllName, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ssbmv(byte uplo, int n, int k, float alpha, float[] a, int lda, float[] x, int incX, float beta, float[] y, int incY);

        [DllImport(dllName, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void sgbmv(byte transA, int m, int n, int kl, int ku, float alpha, float[] a, int lda, float[] x, int incX, float beta, float[] y, int incY);

        [DllImport(dllName, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void sgemv(byte transA, int m, int n, float alpha, float[] a, int lda, float[] x, int incX, float beta, float[] y, int incY);

        [DllImport(dllName, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void sger(int m, int n, float alpha, float[] x, int incX, float[] y, int incY, float[] a, int lda);

        [DllImport(dllName, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void stbsv(byte uplo, byte transA, byte diag, int n, int k, float[] a, int lda, float[] x, int incX);

        [DllImport(dllName, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ssyr(byte uplo, int n, float alpha, float[] x, int incX, float[] a, int lda);


        #endregion

        
        [DllImport(dllName, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void sgemm(byte transa, byte transb, int m, int n, int k, 
            float alpha, float[] a, int lda, float[] b, int ldb, float beta, float[] c, int ldc);

        #endregion




        #region Double


        #region Level 1
        [DllImport(dllName, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int idamax(int n, double[] x, int incX);

        [DllImport(dllName, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        internal static extern double ddot(int n, double[] x, int incX, [In]double[] y, int incY);

        [DllImport(dllName, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        internal static extern double dnrm2(int n, [In]double[] x, int incX);

        [DllImport(dllName, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        internal static extern double dasum(int n, [In]double[] x, int incX);

        [DllImport(dllName, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void dswap(int n, double[] x, int incX, double[] y, int incY);

        [DllImport(dllName, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void dcopy(int n, [In]double[] x, int incX, double[] y, int incY);

        [DllImport(dllName, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void daxpy(int n, double alpha, [In]double[] x, int incX, double[] y, int incY);

        [DllImport(dllName, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void dscal(int n, double alpha, double[] x, int incX);

        #endregion


        #region Level 2

        [DllImport(dllName, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void dsymv(byte uplo, int n, double alpha, [In]double[] a, int lda, [In]double[] x, int incX, double beta, double[] y, int incY);

        [DllImport(dllName, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void dsbmv(byte uplo, int n, int k, double alpha, double[] a, int lda, double[] x, int incX, double beta, double[] y, int incY);

        [DllImport(dllName, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void dgemv(byte transA, int m, int n, double alpha, double[] a, int lda, double[] x, int incX, double beta, double[] y, int incY);

        [DllImport(dllName, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void dgbmv(byte transA, int m, int n, int kl, int ku, double alpha, double[] a, int lda, double[] x, int incX, double beta, double[] y, int incY);

        [DllImport(dllName, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void dger(int m, int n, double alpha, double[] x, int incX, double[] y, int incY, double[] a, int lda);

        [DllImport(dllName, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void dtbsv(byte uplo, byte transA, byte diag, int n, int k, double[] a, int lda, double[] x, int incX);

        [DllImport(dllName, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void dsyr(byte uplo, int n, double alpha, double[] x, int incX, double[] a, int lda);

        #endregion


        [DllImport(dllName, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void dgemm(byte transa, byte transb, int m, int n, int k,
            double alpha, double[] a, int lda, double[] b, int ldb, double beta, double[] c, int ldc);


        #endregion



        #endregion





        #region Lapack


        #region Single

        [DllImport(dllName, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SGETRF(ref int m, ref int n, float[] a, ref int lda, int[] ipiv, ref int info);

        [DllImport(dllName, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SGETRI(ref int n, float[] a, ref int lda, int[] ipiv, float[] work, ref int lwork, ref int info);

        [DllImport(dllName, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SPOTRF(ref byte uplo, ref int n, float[] a, ref int lda, ref int info);

        [DllImport(dllName, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SPOTRI(ref byte uplo, ref int n, float[] a, ref int lda, ref int info);



        [DllImport(dllName, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SPOSV(ref byte uplo, ref int n, ref int nrhs, float[] a, ref int lda, float[] b, ref int ldb, ref int info);


        [DllImport(dllName, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SGELS(ref byte trans, ref int m, ref int n, ref int nrhs, 
            float[] a, ref int lda, float[] b, ref int ldb, float[] work, ref int lwork, ref int info);

        
        #endregion



        #region Double
        [DllImport(dllName, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DGETRF(ref int m, ref int n, double[] a, ref int lda, int[] ipiv, ref int info);

        [DllImport(dllName, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DGETRI(ref int n, double[] a, ref int lda,  int[] ipiv, double[] work, ref int lwork, ref int info);


        [DllImport(dllName, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DPOTRF(ref byte uplo, ref int n, double[] a, ref int lda, ref int info);

        [DllImport(dllName, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DPOTRI(ref byte uplo, ref int n, double[] a, ref int lda, ref int info);



        [DllImport(dllName, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DPOSV(ref byte UpLo, ref int n, ref int nrhs, double[] a, ref int lda, double[] b, ref int ldb, ref int info);

        [DllImport(dllName, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DGELS(ref byte trans, ref int m, ref int n, ref int nrhs,
            double[] a, ref int lda, double[] b, ref int ldb, double[] work, ref int lwork, ref int info);

        

        #endregion



        #endregion


    }
}