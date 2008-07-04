using System;

namespace Nmli.WithOffsets.Mkl
{
    using Externs = Nmli.Mkl.UnsafeExterns;

    unsafe class Blas : IBlas
    {
        #region Single

        #region Level 1

        public float nrm2(int n, OA<float> x, int incX)
        {
            fixed (float* px = &x.array[x.offset])
                return Externs.cblas_snrm2(n, px, incX);
        }

        public float asum(int n, OA<float> x, int incX)
        {
            fixed (float* px = &x.array[x.offset])
                return Externs.cblas_sasum(n, px, incX);
        }

        public float dot(int n, OA<float> x, int incX, OA<float> y, int incY)
        {
            fixed (float* px = &x.array[x.offset], py = &y.array[y.offset])
                return Externs.cblas_sdot(n, px, incX, py, incY);
        }

        public void copy(int n, OA<float> x, int incX, OA<float> y, int incY)
        {
            fixed (float* px = &x.array[x.offset], py = &y.array[y.offset])
                Externs.cblas_scopy(n, px, incX, py, incY);
        }

        public void scal(int n, float alpha, OA<float> x, int incX)
        {
            fixed (float* px = &x.array[x.offset])
                Externs.cblas_sscal(n, alpha, px, incX);
        }

        public void axpy(int n, float alpha, OA<float> x, int incX, OA<float> y, int incY)
        {
            fixed (float* px = &x.array[x.offset], py = &y.array[y.offset])
                Externs.cblas_saxpy(n, alpha, px, incX, py, incY);
        }

        #endregion


        #region Level 2

        public void symv(UpLo uplo, int n, float alpha, OA<float> a, int lda, OA<float> x, int incX, float beta, OA<float> y, int incY)
        {
            fixed (float* pa = &a.array[a.offset],
                    px = &x.array[x.offset],
                    py = &y.array[y.offset])
                Externs.cblas_ssymv(Order.Column, uplo, n, alpha, pa, lda, px, incX, beta, py, incY);
        }

        public void sbmv(UpLo uplo, int n, int k, float alpha, OA<float> a, int lda, OA<float> x, int incX, float beta, OA<float> y, int incY)
        {
            fixed (float* pa = &a.array[a.offset],
                    px = &x.array[x.offset],
                    py = &y.array[y.offset])
                Externs.cblas_ssbmv(Order.Column, uplo, n, k, alpha, pa, lda, px, incX, beta, py, incY);
        }

        public void gemv(Transpose transA, int m, int n, float alpha, OA<float> a, int lda, OA<float> x, int incX, float beta, OA<float> y, int incY)
        {
            fixed (float* pa = &a.array[a.offset],
                    px = &x.array[x.offset],
                    py = &y.array[y.offset])
            Externs.cblas_sgemv(Order.Column, transA, m, n, alpha, pa, lda, px, incX, beta, py, incY);
        }

        public void gbmv(Transpose transA, int m, int n, int kl, int ku, float alpha, OA<float> a, int lda, OA<float> x, int incX, float beta, OA<float> y, int incY)
        {
            fixed (float* pa = &a.array[a.offset],
                    px = &x.array[x.offset],
                    py = &y.array[y.offset])
                Externs.cblas_sgbmv(Order.Column, transA, m, n, kl, ku, alpha, pa, lda, px, incX, beta, py, incY);
        }

        public void ger(int m, int n, float alpha, OA<float> x, int incX, OA<float> y, int incY, OA<float> a, int lda)
        {
            fixed (float* pa = &a.array[a.offset],
                    px = &x.array[x.offset],
                    py = &y.array[y.offset])
                Externs.cblas_sger(Order.Column, m, n, alpha, px, incX, py, incY, pa, lda);
        }



        #endregion


        #endregion

        #region Double

        #region Level 1

        public double nrm2(int n, OA<double> x, int incX)
        {
            fixed (double* px = &x.array[x.offset])
                return Externs.cblas_dnrm2(n, px, incX);
        }

        public double asum(int n, OA<double> x, int incX)
        {
            fixed (double* px = &x.array[x.offset])
                return Externs.cblas_dasum(n, px, incX);
        }

        public double dot(int n, OA<double> x, int incX, OA<double> y, int incY)
        {
            fixed (double* px = &x.array[x.offset], py = &y.array[y.offset])
                return Externs.cblas_ddot(n, px, incX, py, incY);
        }

        public void copy(int n, OA<double> x, int incX, OA<double> y, int incY)
        {
            fixed (double* px = &x.array[x.offset], py = &y.array[y.offset])
                Externs.cblas_dcopy(n, px, incX, py, incY);
        }

        public void scal(int n, double alpha, OA<double> x, int incX)
        {
            fixed (double* px = &x.array[x.offset])
                Externs.cblas_dscal(n, alpha, px, incX);
        }

        public void axpy(int n, double alpha, OA<double> x, int incX, OA<double> y, int incY)
        {
            fixed (double* px = &x.array[x.offset], py = &y.array[y.offset])
                Externs.cblas_daxpy(n, alpha, px, incX, py, incY);
        }

        #endregion


        #region Level 2

        public void symv(UpLo uplo, int n, double alpha, OA<double> a, int lda, OA<double> x, int incX, double beta, OA<double> y, int incY)
        {
            fixed (double* pa = &a.array[a.offset],
                    px = &x.array[x.offset],
                    py = &y.array[y.offset])
                Externs.cblas_dsymv(Order.Column, uplo, n, alpha, pa, lda, px, incX, beta, py, incY);
        }

        public void sbmv(UpLo uplo, int n, int k, double alpha, OA<double> a, int lda, OA<double> x, int incX, double beta, OA<double> y, int incY)
        {
            fixed (double* pa = &a.array[a.offset],
                    px = &x.array[x.offset],
                    py = &y.array[y.offset])
                Externs.cblas_dsbmv(Order.Column, uplo, n, k, alpha, pa, lda, px, incX, beta, py, incY);
        }

        public void gemv(Transpose transA, int m, int n, double alpha, OA<double> a, int lda, OA<double> x, int incX, double beta, OA<double> y, int incY)
        {
            fixed (double* pa = &a.array[a.offset],
                    px = &x.array[x.offset],
                    py = &y.array[y.offset])
                Externs.cblas_dgemv(Order.Column, transA, m, n, alpha, pa, lda, px, incX, beta, py, incY);
        }

        public void gbmv(Transpose transA, int m, int n, int kl, int ku, double alpha, OA<double> a, int lda, OA<double> x, int incX, double beta, OA<double> y, int incY)
        {
            fixed (double* pa = &a.array[a.offset],
                    px = &x.array[x.offset],
                    py = &y.array[y.offset])
                Externs.cblas_dgbmv(Order.Column, transA, m, n, kl, ku, alpha, pa, lda, px, incX, beta, py, incY);
        }

        public void ger(int m, int n, double alpha, OA<double> x, int incX, OA<double> y, int incY, OA<double> a, int lda)
        {
            fixed (double* pa = &a.array[a.offset],
                    px = &x.array[x.offset],
                    py = &y.array[y.offset])
                Externs.cblas_dger(Order.Column, m, n, alpha, px, incX, py, incY, pa, lda);
        }



        #endregion


        #endregion

    }
}
