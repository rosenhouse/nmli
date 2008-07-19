using System;


namespace Nmli.CollectionAgnostic
{

    /// <summary>
    /// Standard LAPACK (http://www.netlib.org/lapack/)
    /// </summary>
    /// <typeparam name="AT">Generic array type (eg pointer, managed array, etc)</typeparam>
    public interface ILapack<AT>
    {
        #region Real, Symmetric, Positive-Definite (Cholesky)
        /// <summary>
        /// _POTRF computes the Cholesky factorization of a real symmetric positive definite matrix A.
        /// The factorization has the form
        /// A = U**T * U,  if UpLo = 'U', or
        /// A = L  * L**T,  if UpLo = 'L', where U is an upper triangular matrix and L is lower triangular.
        /// This is the block version of the algorithm, calling Level 3 BLAS.
        /// </summary>
        /// <param name="uplo">'U':  Upper triangle of A is stored;
        /// 'L':  Lower triangle of A is stored.</param>
        /// <param name="n">The order of the matrix A.  N >= 0.</param>
        /// <param name="a">On entry, the symmetric matrix A.  If UpLo = 'U', the leading
        /// N-by-N upper triangular part of A contains the upper
        /// triangular part of the matrix A, and the strictly lower
        /// triangular part of A is not referenced.  If UpLo = 'L', the
        /// leading N-by-N lower triangular part of A contains the lower
        /// triangular part of the matrix A, and the strictly upper
        /// triangular part of A is not referenced.
        ///
        /// On exit, if INFO = 0, the factor U or L from the Cholesky
        /// factorization A = U**T*U or A = L*L**T.</param>
        /// <param name="lda">The leading dimension of the array A.  LDA >= max(1,N).</param>
        /// <returns>0:  successful exit
        /// negative:  if INFO = -i, the i-th argument had an illegal value
        /// postive:  if INFO = i, the leading minor of order i is not
        /// positive definite, and the factorization could not be completed.</returns>
        int potrf(UpLo uplo, int n, AT a, int lda);

        /// <summary>
        /// _POTRI computes the inverse of a real symmetric positive definite matrix A 
        /// using the Cholesky factorization A = U**T*U or A = L*L**T computed by POTRF.
        /// </summary>
        /// <param name="uplo">'U':  Upper triangle of A is stored;
        /// 'L':  Lower triangle of A is stored.</param>
        /// <param name="n">The order of the matrix A.  N >= 0.</param>
        /// <param name="a">On entry, the triangular factor U or L from the
        /// Cholesky factorization A = U**T*U or A = L*L**T, as computed by DPOTRF.
        /// On exit, the upper or lower triangle of the (symmetric) inverse of A, 
        /// overwriting the input factor U or L.</param>
        /// <param name="lda">The leading dimension of the array A.  LDA >= max(1,N).</param>
        /// <returns>0:  successful exit
        /// negative:  if INFO = -i, the i-th argument had an illegal value
        /// postive:  if INFO = i, the (i,i) element of the factor U or L is
        /// zero, and the inverse could not be computed.</returns>
        int potri(UpLo uplo, int n, AT a, int lda);



        /// <summary>
        /// SPOSV computes the solution to a real system of linear equations
        /// A * X = B,
        /// where A is an N-by-N symmetric positive definite matrix and X and B
        /// are N-by-NRHS matrices.
        /// 
        /// The Cholesky decomposition is used to factor A as
        /// A = U**T* U,  if UPLO = 'U', or
        /// A = L * L**T,  if UPLO = 'L',
        /// where U is an upper triangular matrix and L is a lower triangular
        /// matrix.  The factored form of A is then used to solve the system of
        /// equations A * X = B.
        /// </summary>
        /// <param name="uplo"> U:  Upper triangle of A is stored;
        /// L:  Lower triangle of A is stored.</param>
        /// <param name="n">The number of linear equations, i.e., the order of the matrix A.  N >= 0.</param>
        /// <param name="nrhs">The number of right hand sides, i.e., the number of columns of the matrix B.  NRHS >= 0.</param>
        /// <param name="a">REAL array, dimension (LDA,N)
        ///  On entry, the symmetric matrix A.  If UPLO = 'U', the leading
        ///  N-by-N upper triangular part of A contains the upper
        ///  triangular part of the matrix A, and the strictly lower
        ///  triangular part of A is not referenced.  If UPLO = 'L', the
        ///  leading N-by-N lower triangular part of A contains the lower
        ///  triangular part of the matrix A, and the strictly upper
        ///  triangular part of A is not referenced.
        /// 
        ///  On exit, if INFO = 0, the factor U or L from the Cholesky
        ///  factorization A = U**T*U or A = L*L**T.</param>
        /// <param name="lda">The leading dimension of the array A.  LDA >= max(1,N).</param>
        /// <param name="b">(input/output) REAL array, dimension (LDB,NRHS)
        /// On entry, the N-by-NRHS right hand side matrix B.
        /// On exit, if INFO = 0, the N-by-NRHS solution matrix X.</param>
        /// <param name="ldb">The leading dimension of the array B.  LDB >= max(1,N).</param>
        /// <returns> 0:  successful exit
        ///        lt 0:  if INFO = -i, the i-th argument had an illegal value
        ///        gt 0:  if INFO = i, the leading minor of order i of A is not
        ///                positive definite, so the factorization could not be
        ///                completed, and the solution has not been computed.</returns>
        int posv(UpLo uplo, int n, int nrhs, AT a, int lda, AT b, int ldb);

        #endregion


        #region Linear least-squares

        int gels(Transpose trans, int m, int n, int nrhs, AT a, int lda, AT b, int ldb, AT work, int lwork);

        #endregion
    }
}
