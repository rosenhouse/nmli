using System;


namespace Nmli
{
    /// <summary>
    /// Standard BLAS (http://www.netlib.org/blas/)
    /// </summary>
    /// <typeparam name="T">The number type, eg Single or Double</typeparam>
    public interface IBlas<T>
    {
        #region Level 1
        /// <summary>
        /// Computes a vector-vector dot product.
        /// </summary>
        /// <param name="n">The order of the vectors.</param>
        /// <param name="x">A vector with a length of at least 1+(<paramref name="n"/>-1)*abs(<paramref name="incX"/>).</param>
        /// <param name="incX">The number of elements to increment the index of <paramref name="x"/> by.</param>
        /// <param name="y">A vector with a length of at least 1+(<paramref name="n"/>-1)*abs(<paramref name="incY"/>).</param>
        /// <param name="incY">The number of elements to increment the index of <paramref name="y"/> by.</param>
        /// <returns>The dot product if <paramref name="n"/> &gt; 0, 0 otherwise.</returns>
        T dot(int n, T[] x, int incX, T[] y, int incY);

        /// <summary>
        /// Copies the values form vector to another, from x to y.
        /// </summary>
        /// <param name="n">The order of the vectors.</param>
        /// <param name="x">A vector to copy from with a length of at least 1+(<paramref name="n"/>-1)*abs(<paramref name="incX"/>).</param>
        /// <param name="incX">The number of elements to increment the index of <paramref name="x"/> by.</param>
        /// <param name="y">A vector to copy to with a length of at least 1+(<paramref name="n"/>-1)*abs(<paramref name="incY"/>).</param>
        /// <param name="incY">The number of elements to increment the index of <paramref name="y"/> by.</param>
        void copy(int n, T[] x, int incX, T[] y, int incY);

        /// <summary>
        /// Computes the Euclidean norm of a vector.
        /// </summary>
        /// <param name="n">The order of the vector.</param>
        /// <param name="x">A vector with a length of at least 1+(<paramref name="n"/>-1)*abs(<paramref name="incX"/>).</param>
        /// <param name="incX">The number of elements to increment the index of <paramref name="x"/> by.</param>
        /// <returns>The Euclidean norm.</returns>
        T nrm2(int n, T[] x, int incX);

        /// <summary>
        /// Scales a vector.
        /// </summary>
        /// <param name="n">The order of the vector.</param>
        /// <param name="alpha">The scalar to scale <paramref name="x"/> by.</param>
        /// <param name="x">A vector with a length of at least 1+(<paramref name="n"/>-1)*abs(<paramref name="incX"/>).</param>
        /// <param name="incX">The number of elements to increment the index of <paramref name="x"/> by.</param>
        void scal(int n, T alpha, T[] x, int incX);

        /// <summary>
        /// Performs the operation y = alpha * x + y.
        /// </summary>
        /// <param name="n">The order of the vectors.</param>
        /// <param name="alpha">The scalar to multiply <paramref name="x"/> with.</param>
        /// <param name="x">A vector with a length of at least 1+(<paramref name="n"/>-1)*abs(<paramref name="incX"/>).</param>
        /// <param name="incX">The number of elements to increment the index of <paramref name="x"/> by.</param>
        /// <param name="y">A vector with a length of at least 1+(<paramref name="n"/>-1)*abs(<paramref name="incY"/>).</param>
        /// <param name="incY">The number of elements to increment the index of <paramref name="y"/> by.</param>
        void axpy(int n, T alpha, T[] x, int incX, T[] y, int incY);

        /// <summary>
        /// Calculate the sum of magnitudes of the vector elements.
        /// </summary>
        /// <param name="n">The order of <paramref name="x"/>.</param>
        /// <param name="x">The vector to sum with a length of at least 1+(<paramref name="n"/>-1)*abs(<paramref name="incX"/>).</param>
        /// <param name="incX">The number of elements to increment the index of <paramref name="x"/> by.</param>
        /// <returns>The sum of magnitudes of the vector elements</returns>
        T asum(int n, T[] x, int incX);
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
        void symv(UpLo uplo, int n, T alpha, T[] a, int lda, T[] x, int incX, T beta, T[] y, int incY);

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
        void sbmv(UpLo uplo, int n, int k, T alpha, T[] a, int lda, T[] x, int incX, T beta, T[] y, int incY);


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
        void gbmv(Transpose transA, int m, int n, int kl, int ku, T alpha, T[] a, int lda, T[] x, int incX, T beta, T[] y, int incY);


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
        void gemv(Transpose transA, int m, int n, T alpha, T[] a, int lda, T[] x, int incX, T beta, T[] y, int incY);


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
        void ger(int m, int n, T alpha, T[] x, int incX, T[] y, int incY, T[] a, int lda);




        #endregion

    }

    /// <summary>
    /// Standard BLAS for Single and Double types
    /// </summary>
    public interface IBlas : IBlas<double>, IBlas<float> { }

}
