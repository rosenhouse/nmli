using System;

namespace Nmli.Acml
{
    class Blas : IBlas
    {


        #region Single

        #region Level 1
        public int imax(int n, float[] x, int incX) { return Externs.isamax(n, x, incX) - 1; }

        public float dot(int n, float[] x, int incX, float[] y, int incY) { return Externs.sdot(n, x, incX, y, incY); }

        public void copy(int n, float[] x, int incX, float[] y, int incY) { Externs.scopy(n, x, incX, y, incY); }

        public void scal(int n, float alpha, float[] x, int incX) { Externs.sscal(n, alpha, x, incX); }

        public void axpy(int n, float alpha, float[] x, int incX, float[] y, int incY) { Externs.saxpy(n, alpha, x, incX, y, incY); }

        public float nrm2(int n, float[] x, int incX) { return Externs.snrm2(n, x, incX); }

        public float asum(int n, float[] x, int incX) { return Externs.sasum(n, x, incX); }
        #endregion

        #region Level 2
        public void symv(UpLo uplo, int n, float alpha, float[] a, int lda, float[] x, int incX, float beta, float[] y, int incY)
        {
            byte b = Utilities.EnumAsAscii(uplo);
            Externs.ssymv(b, n, alpha, a, lda, x, incX, beta, y, incY);
        }

        public void sbmv(UpLo uplo, int n, int k, float alpha, float[] a, int lda, float[] x, int incX, float beta, float[] y, int incY)
        {
            byte b = Utilities.EnumAsAscii(uplo);
            Externs.ssbmv(b, n, k, alpha, a, lda, x, incX, beta, y, incY);
        }

        public void gbmv(Transpose transA, int m, int n, int kl, int ku, float alpha, float[] a, int lda, float[] x, int incX, float beta, float[] y, int incY)
        {
            byte tranA = Utilities.EnumAsAscii(transA);
            Externs.sgbmv(tranA, m, n, kl, ku, alpha, a, lda, x, incX, beta, y, incY);
        }

        public void gemv(Transpose transA, int m, int n, float alpha, float[] a, int lda, float[] x, int incX, float beta, float[] y, int incY)
        {
            byte tranA = Utilities.EnumAsAscii(transA);
            Externs.sgemv(tranA, m, n, alpha, a, lda, x, incX, beta, y, incY);
        }

        public void ger(int m, int n, float alpha, float[] x, int incX, float[] y, int incY, float[] a, int lda)
        {
            Externs.sger(m, n, alpha, x, incX, y, incY, a, lda);
        }

        public void tbsv(UpLo uplo, Transpose transA, Diag diag,
            int n, int k, float[] a, int lda, float[] x, int incX)
        {
            byte ul = Utilities.EnumAsAscii(uplo);
            byte tranA = Utilities.EnumAsAscii(transA);
            byte dia = Utilities.EnumAsAscii(diag);
            Externs.stbsv(ul, tranA, dia, n, k, a, lda, x, incX);
        }

        public void syr(UpLo uplo, int n, float alpha, float[] x, int incX, float[] a, int lda)
        {
            byte ul = Utilities.EnumAsAscii(uplo);
            Externs.ssyr(ul, n, alpha, x, incX, a, lda);
        }


        #endregion

        public void gemm(Transpose transa, Transpose transb, int m, int n, int k, float alpha, float[] a, int lda, float[] b, int ldb, float beta, float[] c, int ldc)
        {
            byte tranA = Utilities.EnumAsAscii(transa);
            byte tranB = Utilities.EnumAsAscii(transb);
            Externs.sgemm(tranA, tranB, m, n, k, alpha, a, lda, b, ldb, beta, c, ldc);
        }

        #endregion




        #region Double

        #region Level 1
        public int imax(int n, double[] x, int incX) { return Externs.idamax(n, x, incX) - 1; }

        public double dot(int n, double[] x, int incX, double[] y, int incY) { return Externs.ddot(n, x, incX, y, incY); }

        public void copy(int n, double[] x, int incX, double[] y, int incY) { Externs.dcopy(n, x, incX, y, incY); }

        public void scal(int n, double alpha, double[] x, int incX) { Externs.dscal(n, alpha, x, incX); }

        public void axpy(int n, double alpha, double[] x, int incX, double[] y, int incY) { Externs.daxpy(n, alpha, x, incX, y, incY); }

        public double nrm2(int n, double[] x, int incX) { return Externs.dnrm2(n, x, incX); }

        public double asum(int n, double[] x, int incX) { return Externs.dasum(n, x, incX); }

        #endregion

        #region Level 2
        public void symv(UpLo uplo, int n, double alpha, double[] a, int lda, double[] x, int incX, double beta, double[] y, int incY)
        {
            byte b = Utilities.EnumAsAscii(uplo);
            Externs.dsymv(b, n, alpha, a, lda, x, incX, beta, y, incY);
        }

        public void sbmv(UpLo uplo, int n, int k, double alpha, double[] a, int lda, double[] x, int incX, double beta, double[] y, int incY)
        {
            byte b = Utilities.EnumAsAscii(uplo);
            Externs.dsbmv(b, n, k, alpha, a, lda, x, incX, beta, y, incY);
        }

        public void gbmv(Transpose transA, int m, int n, int kl, int ku, double alpha, double[] a, int lda, double[] x, int incX, double beta, double[] y, int incY)
        {
            byte tranA = Utilities.EnumAsAscii(transA);
            Externs.dgbmv(tranA, m, n, kl, ku, alpha, a, lda, x, incX, beta, y, incY);
        }

        public void gemv(Transpose transA, int m, int n, double alpha, double[] a, int lda, double[] x, int incX, double beta, double[] y, int incY)
        {
            byte tranA = Utilities.EnumAsAscii(transA);
            Externs.dgemv(tranA, m, n, alpha, a, lda, x, incX, beta, y, incY);
        }


        public void ger(int m, int n, double alpha, double[] x, int incX, double[] y, int incY, double[] a, int lda)
        {
            Externs.dger(m, n, alpha, x, incX, y, incY, a, lda);
        }

        public void tbsv(UpLo uplo, Transpose transA, Diag diag, int n, int k, double[] a, int lda, double[] x, int incX)
        {
            byte ul = Utilities.EnumAsAscii(uplo);
            byte tranA = Utilities.EnumAsAscii(transA);
            byte dia = Utilities.EnumAsAscii(diag);
            Externs.dtbsv(ul, tranA, dia, n, k, a, lda, x, incX);
        }

        public void syr(UpLo uplo, int n, double alpha, double[] x, int incX, double[] a, int lda)
        {
            byte ul = Utilities.EnumAsAscii(uplo);
            Externs.dsyr(ul, n, alpha, x, incX, a, lda);
        }
        #endregion

        public void gemm(Transpose transa, Transpose transb, int m, int n, int k, double alpha, double[] a, int lda, double[] b, int ldb, double beta, double[] c, int ldc)
        {
            byte tranA = Utilities.EnumAsAscii(transa);
            byte tranB = Utilities.EnumAsAscii(transb);
            Externs.dgemm(tranA, tranB, m, n, k, alpha, a, lda, b, ldb, beta, c, ldc);
        }

        #endregion




        
    }
}
