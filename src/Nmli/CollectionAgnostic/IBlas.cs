using System;


namespace Nmli.CollectionAgnostic
{

    /// <summary>
    /// Standard BLAS (http://www.netlib.org/blas/) with a generic array type
    /// </summary>
    /// <typeparam name="T">Elemental data type</typeparam>
    /// <typeparam name="AT">Generic array type (eg pointer, managed array, etc)</typeparam>
    public interface IBlas<T, AT>
    {
        #region Level 1
        /// <summary>
        /// Returns the index of the element with the largest absolute value.  
        /// Indexing is 0-based, and respects incX.
        /// For example, imax(4, {0,1,2,3}, 2) = 1
        /// </summary>
        /// <param name="n">Vector length</param>
        /// <param name="x">Vector</param>
        /// <param name="incX">Increment</param>
        /// <returns>Index of element with largest absolute value</returns>
        int imax(int n, AT x, int incX);

        /// <summary>
        /// Computes a vector-vector dot product.
        /// </summary>
        /// <param name="n">The order of the vectors.</param>
        /// <param name="x">A vector with a length of at least 1+(<paramref name="n"/>-1)*abs(<paramref name="incX"/>).</param>
        /// <param name="incX">The number of elements to increment the index of <paramref name="x"/> by.</param>
        /// <param name="y">A vector with a length of at least 1+(<paramref name="n"/>-1)*abs(<paramref name="incY"/>).</param>
        /// <param name="incY">The number of elements to increment the index of <paramref name="y"/> by.</param>
        /// <returns>The dot product if <paramref name="n"/> &gt; 0, 0 otherwise.</returns>
        T dot(int n, AT x, int incX, AT y, int incY);

        /// <summary>
        /// Copies the values form vector to another, from x to y.
        /// </summary>
        /// <param name="n">The order of the vectors.</param>
        /// <param name="x">A vector to copy from with a length of at least 1+(<paramref name="n"/>-1)*abs(<paramref name="incX"/>).</param>
        /// <param name="incX">The number of elements to increment the index of <paramref name="x"/> by.</param>
        /// <param name="y">A vector to copy to with a length of at least 1+(<paramref name="n"/>-1)*abs(<paramref name="incY"/>).</param>
        /// <param name="incY">The number of elements to increment the index of <paramref name="y"/> by.</param>
        void copy(int n, AT x, int incX, AT y, int incY);

        /// <summary>
        /// Computes the Euclidean norm of a vector.
        /// </summary>
        /// <param name="n">The order of the vector.</param>
        /// <param name="x">A vector with a length of at least 1+(<paramref name="n"/>-1)*abs(<paramref name="incX"/>).</param>
        /// <param name="incX">The number of elements to increment the index of <paramref name="x"/> by.</param>
        /// <returns>The Euclidean norm.</returns>
        T nrm2(int n, AT x, int incX);

        /// <summary>
        /// Scales a vector.
        /// </summary>
        /// <param name="n">The order of the vector.</param>
        /// <param name="alpha">The scalar to scale <paramref name="x"/> by.</param>
        /// <param name="x">A vector with a length of at least 1+(<paramref name="n"/>-1)*abs(<paramref name="incX"/>).</param>
        /// <param name="incX">The number of elements to increment the index of <paramref name="x"/> by.</param>
        void scal(int n, T alpha, AT x, int incX);

        /// <summary>
        /// Performs the operation y = alpha * x + y.
        /// </summary>
        /// <param name="n">The order of the vectors.</param>
        /// <param name="alpha">The scalar to multiply <paramref name="x"/> with.</param>
        /// <param name="x">A vector with a length of at least 1+(<paramref name="n"/>-1)*abs(<paramref name="incX"/>).</param>
        /// <param name="incX">The number of elements to increment the index of <paramref name="x"/> by.</param>
        /// <param name="y">A vector with a length of at least 1+(<paramref name="n"/>-1)*abs(<paramref name="incY"/>).</param>
        /// <param name="incY">The number of elements to increment the index of <paramref name="y"/> by.</param>
        void axpy(int n, T alpha, AT x, int incX, AT y, int incY);

        /// <summary>
        /// Calculate the sum of magnitudes of the vector elements.
        /// </summary>
        /// <param name="n">The order of <paramref name="x"/>.</param>
        /// <param name="x">The vector to sum with a length of at least 1+(<paramref name="n"/>-1)*abs(<paramref name="incX"/>).</param>
        /// <param name="incX">The number of elements to increment the index of <paramref name="x"/> by.</param>
        /// <returns>The sum of magnitudes of the vector elements</returns>
        T asum(int n, AT x, int incX);
        #endregion



        #region Level 2
        /// <summary>
        /// Multiplies a symmetric matrix (a) and a vector (x) and adds the result to another vector (y). y = alpha * a * x + beta * y.
        /// </summary>
        /// <param name="uplo">The <see cref="UpLo"/> specifying if <paramref name="a"/>'s data is stored in the upper or lower
        /// triangular part of the matrix.</param>
        /// <param name="n">The order of <paramref name="a"/>. Must be greater than zero.</param>
        /// <param name="alpha">A scalar to scale <paramref name="a"/> by.</param>
        /// <param name="a">The matrix to multiply. <paramref name="a"/> must have the dimensions <paramref name="lda"/> by <paramref name="n"/>.</param>
        /// <param name="lda">The first dimension of <paramref name="a"/>. <paramref name="lda"/> &gt;= max(1,<paramref name="n"/>).</param>
        /// <param name="x">The vector to multiply with. <b>x</b> must have a length of at least 1 + (<paramref name="n"/> - 1)*abs(<paramref name="incX"/>).</param>
        /// <param name="incX">The number of elements to increment the index of <paramref name="x"/> by.</param>
        /// <param name="beta">A scalar to scale <paramref name="y"/> by.</param>
        /// <param name="y">On entry, the vector to add to the result of alpha*a*x. On exit, contains
        /// the result of alpha*a*x + beta*y. <b>y</b> must have a length of at least 1 + (<paramref name="n"/> - 1)*abs(<paramref name="incY"/>).</param>
        /// <param name="incY">The number of elements to increment the index of <paramref name="y"/> by.</param>
        void symv(UpLo uplo, int n, T alpha, AT a, int lda, AT x, int incX, T beta, AT y, int incY);

        /// <summary>
        /// Multiplies a symmetric, band matrix (a) and a vector (x) and adds the result to another vector (y). y = alpha * a * x + beta * y.
        /// </summary>
        /// <param name="uplo">The <see cref="BlasUpLoType"/> specifying if <paramref name="a"/>'s data is stored in the upper or lower
        /// triangular part of the matrix.</param>
        /// <param name="n">The order of <paramref name="a"/>. Must be greater than zero.</param>
        /// <param name="k">The number of super-diagonals of <paramref name="a"/>. Must be non-negative.</param>
        /// <param name="alpha">A scalar to scale <paramref name="a"/> by.</param>
        /// <param name="a">The matrix to multiply. <paramref name="a"/> must have the dimensions <paramref name="lda"/> by <paramref name="n"/>.</param>
        /// <param name="lda">The first dimension of <paramref name="a"/>. <paramref name="lda"/> must be at least k+1.</param>
        /// <param name="x">The vector to multiply with. <c>x</c> must have a length of at least 1 + (<paramref name="n"/> - 1)*abs(<paramref name="incX"/>).</param>
        /// <param name="incX">The number of elements to increment the index of <paramref name="x"/> by.</param>
        /// <param name="beta">A scalar to scale <paramref name="y"/> by.</param>
        /// <param name="y">On entry, the vector to add to the result of alpha*a*x. On exit, contains
        /// the result of alpha*a*x + beta*y. <c>y</c> must have a length of at least 1 + (<paramref name="n"/> - 1)*abs(<paramref name="incY"/>).</param>
        /// <param name="incY">The number of elements to increment the index of <paramref name="y"/> by.</param>
        void sbmv(UpLo uplo, int n, int k, T alpha, AT a, int lda, AT x, int incX, T beta, AT y, int incY);


        /// <summary>
        /// Multiplies a band matrix (a) and a vector (x) and adds the result to another vector (y). y = alpha * a * x + beta * y.
        /// </summary>
        /// <param name="transA">The <see cref="BlasTransType"/> specifying whether and how to transpose <paramref name="a"/>.</param>
        /// <param name="m">The number of rows of <paramref name="a"/>. Must be greater than zero.</param>
        /// <param name="n">The number of columns of <paramref name="a"/>. Must be greater than zero.</param>
        /// <param name="kl">The number of sub-diagonals of <paramref name="a"/>. <paramref name="kl"/>&gt;=0.</param>
        /// <param name="ku">The number of super-diagonals of <paramref name="a"/>. <paramref name="ku"/>&gt;=0.</param>
        /// <param name="alpha">A scalar to scale <paramref name="a"/> by.</param>
        /// <param name="a">The matrix to multiply. <paramref name="a"/> must have the dimensions <paramref name="lda"/> by <paramref name="n"/>.</param>
        /// <param name="lda">The first dimension of <paramref name="a"/>. <paramref name="lda"/> must be at least ku+kl+1.</param>
        /// <param name="x">The vector to multiply with. If <paramref name="transA"/> == <see cref="BlasTransType.NoTrans"/>, then x must have a 
        /// length of at least 1 + (<paramref name="n"/> - 1)*abs(<paramref name="incX"/>). Otherwise, x must have a 
        /// length of at least 1 + (<paramref name="m"/> - 1)*abs(<paramref name="incX"/>).</param>
        /// <param name="incX">The number of elements to increment the index of <paramref name="x"/> by.</param>
        /// <param name="beta">A scalar to scale <paramref name="y"/> by.</param>
        /// <param name="y">On entry, the vector to add to the result of alpha*a*x. On exit, contains
        /// the result of alpha*a*x + beta*y. If <paramref name="transA"/> == <see cref="BlasTransType.NoTrans"/>, then y must have a 
        /// length of at least 1 + (<paramref name="m"/> - 1)*abs(<paramref name="incY"/>). Otherwise, y must have a 
        /// length of at least 1 + (<paramref name="n"/> - 1)*abs(<paramref name="incY"/>).</param>
        /// <param name="incY">The number of elements to increment the index of <paramref name="y"/> by.</param>
        void gbmv(Transpose transA, int m, int n, int kl, int ku, T alpha, AT a, int lda, AT x, int incX, T beta, AT y, int incY);


        /// <summary>
        /// Multiplies a matrix (a) and a vector (x) and adds the result to another vector (y). y = alpha * a * x + beta * y.
        /// </summary>
        /// <param name="transA">The <see cref="BlasTransType"/> specifying whether and how to transpose <paramref name="a"/>.</param>
        /// <param name="m">The number of rows of <paramref name="a"/>. Must be greater than zero.</param>
        /// <param name="n">The number of columns of <paramref name="a"/>. Must be greater than zero.</param>
        /// <param name="alpha">A scalar to scale <paramref name="a"/> by.</param>
        /// <param name="a">The matrix to multiply. <paramref name="a"/> must have the dimensions <paramref name="lda"/> by <paramref name="n"/>.</param>
        /// <param name="lda">The first dimension of <paramref name="a"/>. <paramref name="lda"/> &gt;= max(1,<paramref name="m"/>).</param>
        /// <param name="x">The vector to multiply with. If <paramref name="transA"/> == <see cref="BlasTransType.NoTrans"/>, then <c>x</c> must have a 
        /// length of at least 1 + (<paramref name="n"/> - 1)*abs(<paramref name="incX"/>). Otherwise, <c>x</c> must have a 
        /// length of at least 1 + (<paramref name="m"/> - 1)*abs(<paramref name="incX"/>).</param>
        /// <param name="incX">The number of elements to increment the index of <paramref name="x"/> by.  Must be non-zero.</param>
        /// <param name="beta">A scalar to scale <paramref name="y"/> by.</param>
        /// <param name="y">On entry, the vector to add to the result of alpha*a*x. On exit, contains
        /// the result of alpha*a*x + beta*y. If <paramref name="transA"/> == <see cref="BlasTransType.NoTrans"/>, then <c>y</c> must have a 
        /// length of at least 1 + (<paramref name="m"/> - 1)*abs(<paramref name="incY"/>). Otherwise, <c>y</c> must have a 
        /// length of at least 1 + (<paramref name="n"/> - 1)*abs(<paramref name="incY"/>).</param>
        /// <param name="incY">The number of elements to increment the index of <paramref name="y"/> by.</param>
        void gemv(Transpose transA, int m, int n, T alpha, AT a, int lda, AT x, int incX, T beta, AT y, int incY);


        /// <summary>
        /// A rank-1 update of a general matrix. A = alpha * x * y' + A.
        /// </summary>
        /// <param name="m">The number of rows of <paramref name="a"/>. Must be greater than zero.</param>
        /// <param name="n">The number of columns of <paramref name="a"/>. Must be greater than zero.</param>
        /// <param name="alpha">A scalar to multiply the matrix created by <paramref name="x"/> and <paramref name="y"/>' with.</param>
        /// <param name="x">The vector to multiply with the transpose of <paramref name="y"/>. <c>x</c> must have a length of at least 1 + (<paramref name="m"/> - 1)*abs(<paramref name="incX"/>).</param>
        /// <param name="incX">The number of elements to increment the index of <paramref name="x"/> by.</param>
        /// <param name="y">The vector to multiply with <paramref name="x"/>. <c>y</c> must have a length of at least 1 + (<paramref name="n"/> - 1)*abs(<paramref name="incY"/>).</param>
        /// <param name="incY">The number of elements to increment the index of <paramref name="y"/> by.</param>
        /// <param name="a">On entry, the matrix to add to the product of <paramref name="x"/> and <paramref name="y"/>'. On exit, 
        /// it contains the result of alpha*x*y'+a.</param>
        /// <param name="lda">The first dimension of <paramref name="a"/>. <paramref name="lda"/> must be at least m.</param>
        void ger(int m, int n, T alpha, AT x, int incX, AT y, int incY, AT a, int lda);

        /// <summary>
        /// A rank-1 update of a symmetric matrix.  A = alpha * x * x' + A
        /// </summary>
        /// <param name="uplo">The <see cref="BlasUpLoType"/> specifying if <paramref name="a"/>'s data is stored in the upper or lower
        /// triangular part of the matrix.</param>
        /// <param name="n">The order of <paramref name="a"/>. Must be greater than zero.</param>
        /// <param name="alpha">A scalar to scale x * x' by.</param>
        /// <param name="x">Vector length n</param>
        /// <param name="incX">Increment index for X</param>
        /// <param name="a">Matrix to be updated.  a += alpha * x * x' </param>
        /// <param name="lda">Leading dimension of <paramref name="a"/></param>
        void syr(UpLo uplo, int n, T alpha, AT x, int incX, AT a, int lda);


        #endregion


        /// <summary>
        /// Computes a scalar-matrix-matrix product and adds the result to a scalar-matrix product.
        ///      C := alpha*op(A)*op(B) + beta*C
        /// </summary>
        /// <remarks>The ?gemm routines perform a matrix-matrix operation with general matrices. The operation is defined as 
        ///    <code>C := alpha*op(A)*op(B) + beta*C</code>
        /// where:
        ///   op(x) is one of op(x) = x, or op(x) = x', or op(x) = conjg(x'),
        ///   alpha and beta are scalars,
        ///   A, B and C are matrices:
        ///   op(A) is an m-by-k matrix,
        ///   op(B) is a k-by-n matrix,
        ///   C is an m-by-n matrix.
        /// </remarks>
        /// <param name="transa">Specifies whether and how to transpose matrix A</param>
        /// <param name="transb">Specifies whether and how to transpose matrix B</param>
        /// <param name="m">Num rows of op(A) and rows of C</param>
        /// <param name="n">Num cols of op(B) and cols of C</param>
        /// <param name="k">Num cols of op(A) and rows of op(B)</param>
        /// <param name="alpha">Scalar of op(A)*op(B)</param>
        /// <param name="a">Matrix A</param>
        /// <param name="lda">Leading dim of A.  If op(A)=A then lda>=m, else lda>=k</param>
        /// <param name="b">Matrix B</param>
        /// <param name="ldb">Leading dim of B.  If op(B)=B then ldb>=k, else ldb>=n</param>
        /// <param name="beta">Scalar of C before add</param>
        /// <param name="c">Matrix C</param>
        /// <param name="ldc">Leading dim of C.  ldc>=m</param>
        void gemm(Transpose transa, Transpose transb, int m, int n, int k,
            T alpha, AT a, int lda, AT b, int ldb, T beta, AT c, int ldc);


        /// <summary>
        /// Performs a rank-n update of a symmetric matrix
        ///    C := alpha*A*A' + beta*C  or    C := alpha*A'*A + beta*C
        /// </summary>
        /// <remarks>
        /// The ?syrk routines perform a matrix-matrix operation using symmetric matrices. The operation is defined as 
        /// <code>C := alpha*A*A' + beta*C</code>
        /// or 
        /// <code>C := alpha*A'*A + beta*C</code>
        /// where:
        /// alpha and beta are scalars,
        /// C is an n-by-n symmetric matrix,
        /// A is an n-by-k matrix in the first case and a k-by-n matrix in the second case.
        /// </remarks>
        /// <param name="uplo">Write to the upper or lower part of C</param>
        /// <param name="trans">If notrans, then C:= alpha*A*A' + beta*C, else C:=alpha*A'*A + beta*C</param>
        /// <param name="n">Order of C</param>
        /// <param name="k">If notrans then k = numcols(a)   else k = numrows(a)</param>
        /// <param name="alpha">Scalar of matrix product</param>
        /// <param name="a">Matrix a</param>
        /// <param name="lda">Leading dim of A.  If notrans then lda>=n else lda >= k</param>
        /// <param name="beta">Scalar of C before addition</param>
        /// <param name="c">Matrix c</param>
        /// <param name="ldc">Leading dim of C.  ldc >= n</param>
        void syrk(UpLo uplo, Transpose trans, int n, int k, T alpha, AT a, int lda, T beta, AT c, int ldc);
    }
}
