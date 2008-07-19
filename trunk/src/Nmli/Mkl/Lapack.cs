using System;
using System.Collections.Generic;
using System.Text;

namespace Nmli.Mkl
{
    class Lapack : ILapack
    {

        #region Double
        public int potrf(UpLo uplo, int n, double[] a, int lda)
        {
            int info = 0;
            byte b = Utilities.EnumAsAscii(uplo);
            Externs.DPOTRF(ref b, ref n, a, ref lda, ref info);
            return info;
        }

        public int potri(UpLo uplo, int n, double[] a, int lda)
        {
            int info = 0;
            byte b = Utilities.EnumAsAscii(uplo);
            Externs.DPOTRI(ref b, ref n, a, ref lda, ref info);
            return info;
        }


        public int posv(UpLo uplo, int n, int nrhs, double[] a, int lda, double[] b, int ldb)
        {
            int info = 0;
            byte u = Utilities.EnumAsAscii(uplo);
            Externs.DPOSV(ref u, ref n, ref nrhs, a, ref lda, b, ref ldb, ref info);
            return info;
        }


        public int posv(UpLo uplo, int n, int nrhs, float[] a, int lda, float[] b, int ldb)
        {
            int info = 0;
            byte u = Utilities.EnumAsAscii(uplo);
            Externs.SPOSV(ref u, ref n, ref nrhs, a, ref lda, b, ref ldb, ref info);
            return info;
        }

        public int getrf(int m, int n, double[] a, int lda, ref int[] ipiv)
        {
            int info = 0;
            Externs.DGETRF(ref m, ref n, a, ref lda, ipiv, ref info);
            return info;
        }

        public int getri(int n, double[] a, int lda, int[] ipiv, ref double[] work, int lwork)
        {
            int info = 0;
            Externs.DGETRI(ref n, a, ref lda, ipiv, work, ref lwork, ref info);
            return info;
        }

        public int gels(Transpose trans, int m, int n, int nrhs, double[] a, int lda, double[] b, int ldb, double[] work, int lwork)
        {
            int info = 0;
            byte t = Utilities.EnumAsAscii(trans);
            Externs.DGELS(ref t, ref m, ref n, ref nrhs, a, ref lda, b, ref ldb, work, ref lwork, ref info);
            return info;
        }

        #endregion




        #region Single


        public int potrf(UpLo uplo, int n, float[] a, int lda)
        {
            int info = 0;
            byte b = Utilities.EnumAsAscii(uplo);
            Externs.SPOTRF(ref b, ref n, a, ref lda, ref info);
            return info;
        }

        public int potri(UpLo uplo, int n, float[] a, int lda)
        {
            int info = 0;
            byte b = Utilities.EnumAsAscii(uplo);
            Externs.SPOTRI(ref b, ref n, a, ref lda, ref info);
            return info;
        }

        public int getrf(int m, int n, float[] a, int lda, ref int[] ipiv)
        {
            int info = 0;
            Externs.SGETRF(ref m, ref n, a, ref lda, ipiv, ref info);
            return info;
        }

        public int getri(int n, float[] a, int lda, int[] ipiv, ref float[] work, int lwork)
        {
            int info = 0;
            Externs.SGETRI(ref n, a, ref lda, ipiv, work, ref lwork, ref info);
            return info;
        }

        public int gels(Transpose trans, int m, int n, int nrhs, float[] a, int lda, float[] b, int ldb, float[] work, int lwork)
        {
            int info = 0;
            byte t = Utilities.EnumAsAscii(trans);
            Externs.SGELS(ref t, ref m, ref n, ref nrhs, a, ref lda, b, ref ldb, work, ref lwork, ref info);
            return info;
        }
        #endregion




    }
}
