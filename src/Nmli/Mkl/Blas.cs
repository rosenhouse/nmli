using System;
using System.Collections.Generic;
using System.Text;

namespace Nmli.Mkl
{
    class Blas : IBlas
    {

        #region Single

        #region Level 1

        public float nrm2(int n, float[] x, int incX) { return Externs.cblas_snrm2(n, x, incX); }

        public float asum(int n, float[] x, int incX) { return Externs.cblas_sasum(n, x, incX); }

        public float dot(int n, float[] x, int incX, float[] y, int incY) { return Externs.cblas_sdot(n, x, incX, y, incY); }

        public void copy(int n, float[] x, int incX, float[] y, int incY) { Externs.cblas_scopy(n, x, incX, y, incY); }

        public void scal(int n, float alpha, float[] x, int incX) { Externs.cblas_sscal(n, alpha, x, incX); }

        public void axpy(int n, float alpha, float[] x, int incX, float[] y, int incY) { Externs.cblas_saxpy(n, alpha, x, incX, y, incY); }

        #endregion


        #region Level 2

        public void symv(UpLo uplo, int n, float alpha, float[] a, int lda, float[] x, int incX, float beta, float[] y, int incY)
        {
            Externs.cblas_ssymv(Order.Column, uplo, n, alpha, a, lda, x, incX, beta, y, incY);
        }

        public void sbmv(UpLo uplo, int n, int k, float alpha, float[] a, int lda, float[] x, int incX, float beta, float[] y, int incY)
        {
            Externs.cblas_ssbmv(Order.Column, uplo, n, k, alpha, a, lda, x, incX, beta, y, incY);
        }

        public void gemv(Transpose transA, int m, int n, float alpha, float[] a, int lda, float[] x, int incX, float beta, float[] y, int incY)
        {
            Externs.cblas_sgemv(Order.Column, transA, m, n, alpha, a, lda, x, incX, beta, y, incY);
        }

        public void gbmv(Transpose transA, int m, int n, int kl, int ku, float alpha, float[] a, int lda, float[] x, int incX, float beta, float[] y, int incY)
        {
            Externs.cblas_sgbmv(Order.Column, transA, m, n, kl, ku, alpha, a, lda, x, incX, beta, y, incY);
        }

        public void ger(int m, int n, float alpha, float[] x, int incX, float[] y, int incY, float[] a, int lda)
        {
            Externs.cblas_sger(Order.Column, m, n, alpha, x, incX, y, incY, a, lda);
        }



        #endregion


        #endregion




        #region Double

        #region Level 1

        public double dot(int n, double[] x, int incX, double[] y, int incY) { return Externs.cblas_ddot(n, x, incX, y, incY); }

        public void copy(int n, double[] x, int incX, double[] y, int incY) { Externs.cblas_dcopy(n, x, incX, y, incY); }

        public void scal(int n, double alpha, double[] x, int incX) { Externs.cblas_dscal(n, alpha, x, incX); }

        public void axpy(int n, double alpha, double[] x, int incX, double[] y, int incY) { Externs.cblas_daxpy(n, alpha, x, incX, y, incY); }


        public double nrm2(int n, double[] x, int incX) { return Externs.cblas_dnrm2(n, x, incX); }

        public double asum(int n, double[] x, int incX) { return Externs.cblas_dasum(n, x, incX); }

        #endregion


        #region Level 2

        public void symv(UpLo uplo, int n, double alpha, double[] a, int lda, double[] x, int incX, double beta, double[] y, int incY)
        {
            Externs.cblas_dsymv(Order.Column, uplo, n, alpha, a, lda, x, incX, beta, y, incY);
        }

        public void sbmv(UpLo uplo, int n, int k, double alpha, double[] a, int lda, double[] x, int incX, double beta, double[] y, int incY)
        {
            Externs.cblas_dsbmv(Order.Column, uplo, n, k, alpha, a, lda, x, incX, beta, y, incY);
        }

        public void gemv(Transpose transA, int m, int n, double alpha, double[] a, int lda, double[] x, int incX, double beta, double[] y, int incY)
        {
            Externs.cblas_dgemv(Order.Column, transA, m, n, alpha, a, lda, x, incX, beta, y, incY);
        }

        public void gbmv(Transpose transA, int m, int n, int kl, int ku, double alpha, double[] a, int lda, double[] x, int incX, double beta, double[] y, int incY)
        {
            Externs.cblas_dgbmv(Order.Column, transA, m, n, kl, ku, alpha, a, lda, x, incX, beta, y, incY);
        }

        public void ger(int m, int n, double alpha, double[] x, int incX, double[] y, int incY, double[] a, int lda)
        {
            Externs.cblas_dger(Order.Column, m, n, alpha, x, incX, y, incY, a, lda);
        }
        #endregion

        #endregion

    }
}
