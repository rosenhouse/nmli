using System;

namespace Nmli.WithOffsets.Acml
{
    using Externs = Nmli.Acml.UnsafeExterns;

    unsafe class Blas : IBlas
    {
        #region Single

        #region Level 1
        public int imax(int n, OA<float> x, int incX)
        {
            fixed (float* px = &x.array[x.offset])
                return Externs.ismax(n, px, incX)-1;
        }

        public float dot(int n, OA<float> x, int incX, OA<float> y, int incY)
        {
            fixed (float* px = &x.array[x.offset], py = &y.array[y.offset])
                return Externs.sdot(n, px, incX, py, incY);
        }

        public void copy(int n, OA<float> x, int incX, OA<float> y, int incY)
        {
            fixed (float* px = &x.array[x.offset], py = &y.array[y.offset])
                Externs.scopy(n, px, incX, py, incY);
        }

        public void scal(int n, float alpha, OA<float> x, int incX)
        {
            fixed (float* px = &x.array[x.offset])
                Externs.sscal(n, alpha, px, incX);
        }


        public void axpy(int n, float alpha, OA<float> x, int incX, OA<float> y, int incY)
        {
            fixed (float* px = &x.array[x.offset], py = &y.array[y.offset])
                Externs.saxpy(n, alpha, px, incX, py, incY);
        }

        public float nrm2(int n, OA<float> x, int incX)
        {
            fixed (float* px = &x.array[x.offset])
                return Externs.snrm2(n, px, incX);
        }

        public float asum(int n, OA<float> x, int incX)
        {
            fixed (float* px = &x.array[x.offset])
                return Externs.sasum(n, px, incX);
        }
        #endregion

        #region Level 2
        public void symv(UpLo uplo, int n, float alpha, OA<float> a, int lda, OA<float> x, int incX, float beta, OA<float> y, int incY)
        {
            byte b = Utilities.EnumAsAscii(uplo);
            fixed (float* pa = &a.array[a.offset],
                    px = &x.array[x.offset],
                    py = &y.array[y.offset])
                Externs.ssymv(b, n, alpha, pa, lda, px, incX, beta, py, incY);
        }

        public void sbmv(UpLo uplo, int n, int k, float alpha, OA<float> a, int lda, OA<float> x, int incX, float beta, OA<float> y, int incY)
        {
            byte b = Utilities.EnumAsAscii(uplo);
            fixed (float* pa = &a.array[a.offset],
                    px = &x.array[x.offset],
                    py = &y.array[y.offset])
                Externs.ssbmv(b, n, k, alpha, pa, lda, px, incX, beta, py, incY);
        }

        public void gbmv(Transpose transA, int m, int n, int kl, int ku, float alpha, OA<float> a, int lda, OA<float> x, int incX, float beta, OA<float> y, int incY)
        {
            byte tranA = Utilities.EnumAsAscii(transA);
            fixed (float* pa = &a.array[a.offset],
                    px = &x.array[x.offset],
                    py = &y.array[y.offset])
            Externs.sgbmv(tranA, m, n, kl, ku, alpha, pa, lda, px, incX, beta, py, incY);
        }

        public void gemv(Transpose transA, int m, int n, float alpha, OA<float> a, int lda, OA<float> x, int incX, float beta, OA<float> y, int incY)
        {
            byte tranA = Utilities.EnumAsAscii(transA);
            fixed (float* pa = &a.array[a.offset],
                    px = &x.array[x.offset],
                    py = &y.array[y.offset])
            Externs.sgemv(tranA, m, n, alpha, pa, lda, px, incX, beta, py, incY);
        }



        public void ger(int m, int n, float alpha, OA<float> x, int incX, OA<float> y, int incY, OA<float> a, int lda)
        {
            fixed (float* pa = &a.array[a.offset],
                    px = &x.array[x.offset],
                    py = &y.array[y.offset])
            Externs.sger(m, n, alpha, px, incX, py, incY, pa, lda);
        }

        public void syr(UpLo uplo, int n, float alpha, OA<float> x, int incX, OA<float> a, int lda)
        {
            byte b = Utilities.EnumAsAscii(uplo);
            fixed (float* pa = &a.array[a.offset],
                    px = &x.array[x.offset])
                Externs.ssyr(b, n, alpha, px, incX, pa, lda);
        }


        #endregion

        #endregion


        #region Double

        #region Level 1
        public int imax(int n, OA<double> x, int incX)
        {
            fixed (double* px = &x.array[x.offset])
                return Externs.idmax(n, px, incX)-1;
        }

        public double dot(int n, OA<double> x, int incX, OA<double> y, int incY)
        {
            fixed (double* px = &x.array[x.offset], py = &y.array[y.offset])
                return Externs.ddot(n, px, incX, py, incY);
        }

        public void copy(int n, OA<double> x, int incX, OA<double> y, int incY)
        {
            fixed (double* px = &x.array[x.offset], py = &y.array[y.offset])
                Externs.dcopy(n, px, incX, py, incY);
        }

        public void scal(int n, double alpha, OA<double> x, int incX)
        {
            fixed (double* px = &x.array[x.offset])
                Externs.dscal(n, alpha, px, incX);
        }


        public void axpy(int n, double alpha, OA<double> x, int incX, OA<double> y, int incY)
        {
            fixed (double* px = &x.array[x.offset], py = &y.array[y.offset])
                Externs.daxpy(n, alpha, px, incX, py, incY);
        }

        public double nrm2(int n, OA<double> x, int incX)
        {
            fixed (double* px = &x.array[x.offset])
                return Externs.dnrm2(n, px, incX);
        }

        public double asum(int n, OA<double> x, int incX)
        {
            fixed (double* px = &x.array[x.offset])
                return Externs.dasum(n, px, incX);
        }
        #endregion

        #region Level 2
        public void symv(UpLo uplo, int n, double alpha, OA<double> a, int lda, OA<double> x, int incX, double beta, OA<double> y, int incY)
        {
            byte b = Utilities.EnumAsAscii(uplo);
            fixed (double* pa = &a.array[a.offset],
                    px = &x.array[x.offset],
                    py = &y.array[y.offset])
                Externs.dsymv(b, n, alpha, pa, lda, px, incX, beta, py, incY);
        }

        public void sbmv(UpLo uplo, int n, int k, double alpha, OA<double> a, int lda, OA<double> x, int incX, double beta, OA<double> y, int incY)
        {
            byte b = Utilities.EnumAsAscii(uplo);
            fixed (double* pa = &a.array[a.offset],
                    px = &x.array[x.offset],
                    py = &y.array[y.offset])
                Externs.dsbmv(b, n, k, alpha, pa, lda, px, incX, beta, py, incY);
        }

        public void gbmv(Transpose transA, int m, int n, int kl, int ku, double alpha, OA<double> a, int lda, OA<double> x, int incX, double beta, OA<double> y, int incY)
        {
            byte tranA = Utilities.EnumAsAscii(transA);
            fixed (double* pa = &a.array[a.offset],
                    px = &x.array[x.offset],
                    py = &y.array[y.offset])
                Externs.dgbmv(tranA, m, n, kl, ku, alpha, pa, lda, px, incX, beta, py, incY);
        }

        public void gemv(Transpose transA, int m, int n, double alpha, OA<double> a, int lda, OA<double> x, int incX, double beta, OA<double> y, int incY)
        {
            byte tranA = Utilities.EnumAsAscii(transA);
            fixed (double* pa = &a.array[a.offset],
                    px = &x.array[x.offset],
                    py = &y.array[y.offset])
                Externs.dgemv(tranA, m, n, alpha, pa, lda, px, incX, beta, py, incY);
        }



        public void ger(int m, int n, double alpha, OA<double> x, int incX, OA<double> y, int incY, OA<double> a, int lda)
        {
            fixed (double* pa = &a.array[a.offset],
                    px = &x.array[x.offset],
                    py = &y.array[y.offset])
                Externs.dger(m, n, alpha, px, incX, py, incY, pa, lda);
        }

        public void syr(UpLo uplo, int n, double alpha, OA<double> x, int incX, OA<double> a, int lda)
        {
            byte b = Utilities.EnumAsAscii(uplo);
            fixed (double* pa = &a.array[a.offset],
                    px = &x.array[x.offset])
                Externs.ssyr(b, n, alpha, px, incX, pa, lda);
        }

        #endregion

        #endregion



    }
}
