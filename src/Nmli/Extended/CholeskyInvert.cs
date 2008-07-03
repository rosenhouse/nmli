using System;
using System.Collections.Generic;
using System.Text;

namespace Nmli.Extended
{
    public class CholeskyInverter<T> : Extensions1<T>
    {
        public class NonPositiveDefiniteBlockException : Exception
        {
            readonly int order;
            public NonPositiveDefiniteBlockException(int order) { this.order = order; }
            public int Order { get { return order; } }
        }

        public CholeskyInverter(IMathLibrary<T> ml) : base(ml) {  }

        [ThreadStatic]
        static Workspace<T> scratchProvider;

        public double LnDeterminantOfFactoredMatrix(int n, T[] choleskyDecomposed)
        {
            /*  If the symmetric positive definite matrix A is represented by its 
             *  Cholesky decomposition A = LL T or A = U TU, then the determinant 
             *  of this matrix can be calculated as the product of squares of the 
             *  diagonal elements of L or U.
             * */
            // det = a1^2 * ... * an^2
            // ln(det) = ln(a1^2) + ... + ln(an^2)
            T[] scratch = Workspace<T>.Get(ref scratchProvider, n);

            extras.SquareInto(n, choleskyDecomposed, n + 1, _1, scratch);
            // scratch now contains square of diagonal elements of decomposed matrix

            vml.Ln(n, scratch, scratch);  // inplace natural log
            T g_lndet = extras.Sum(n, scratch); // add up log terms

            return sml.ToDouble(g_lndet);
        }



        /// <summary>
        /// Cholesky-decomposes a symmetric, positive-definite matrix, 
        /// returning the log of its determinant and overwriting matrix with Cholesky-factor
        /// </summary>
        /// <param name="n">The side length of the matrix</param>
        /// <param name="symmetricPositiveDefiniteMatrix">On Input: Upper-triangle contains the matrix to invert.  
        /// On Output: Upper triangle contains the Cholesky factor</param>
        /// <returns>Natural log of the determinant of the original matrix</returns>
        /// <exception cref="NonPositiveDefiniteBlockException">Throws when a block is non-positive definite</exception>
        public double Decompose(int n, T[] symmetricPositiveDefiniteMatrix)
        {
            if (symmetricPositiveDefiniteMatrix.Length < n * n)
                throw new ArgumentException("Matrix is too small!");

            int errCode = 0;

            // decompose
            errCode = lapack.potrf(UpLo.Upper, n, symmetricPositiveDefiniteMatrix, n);

            if (errCode > 0)
                throw new NonPositiveDefiniteBlockException(errCode);
            else if (errCode != 0)
                throw new Exception("Failed on decompose!  Error=" + errCode);

            double lndet = LnDeterminantOfFactoredMatrix(n, symmetricPositiveDefiniteMatrix);

            if (double.IsInfinity(lndet) || double.IsNaN(lndet))
                throw new Exception("Determinant isn't real, so the matrix is singular!");

            return lndet;
        }

        /// <summary>
        /// Inverts a Cholesky-decomposed matrix, overwritting the input matrix with the upper-triangle of its inverse
        /// </summary>
        /// <param name="n">The side length of the matrix</param>
        /// <param name="symmetricPositiveDefiniteMatrix">On Input: The upper triangle of the matrix to invert.
        /// On Output: The upper triangle of its inverse</param>
        public void Invert_Factor(int n, T[] choleskyDecomposed)
        {
            // invert
            int errCode = lapack.potri(UpLo.Upper, n, choleskyDecomposed, n);
            if (errCode > 0)
                throw new NonPositiveDefiniteBlockException(errCode);
            else if (errCode < 0)
                throw new Exception("Argument " + errCode + " for potri was invalid.");
        }

        public double Invert(int n, T[] symmetricPositiveDefiniteMatrix)
        {
            double lnDet = Decompose(n, symmetricPositiveDefiniteMatrix);
            Invert_Factor(n, symmetricPositiveDefiniteMatrix);
            return lnDet;
        }

        /// <summary>
        /// Restores an upper-triangular matrix, such as returned by Invert(), to full-square form by mirroring
        /// the upper triangle across the diagonal
        /// </summary>
        public void CopyMirrorFromUpperTriangle(int n, T[] upperTriangular)
        {
            for (int i = 0; i < n; i++)
                for (int j = i; j < n; j++)
                    upperTriangular[i * n + j] = upperTriangular[j * n + i];
        }
    }

}
