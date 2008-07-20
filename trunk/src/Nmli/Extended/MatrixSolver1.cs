using System;
using System.Collections.Generic;
using System.Text;

namespace Nmli.Extended
{
    /// <summary>
    /// Exposes a method to solve the equation A.X.A^t = B for X.   X and B must be square.
    /// </summary>
    public class MatrixSolver1<N> : ExtendingFunc<N>
    {
        public MatrixSolver1(IMathLibrary<N> ml) : base(ml) { }

        [ThreadStatic]
        Workspace<N> ws_z;

        [ThreadStatic]
        Workspace<N> ws_a;

        [ThreadStatic]
        Workspace<N> ws_work;

        /// <summary>
        /// Solves the equation A.X.A^t = B for X, with X and B square.
        /// Uses the LAPACK GELS solver twice, so when A has more rows than cols, 
        /// the steps yield the lease-squares solution to the overdetermined system.
        /// When A has more columns than rows, we get the minimum-2-norm solution to
        /// the underdetermined system.
        /// </summary>
        /// <param name="ra">Rows of A</param>
        /// <param name="ca">Columns of A</param>
        /// <param name="A">Input: A matrix (ra x ca).  Unchanged on exit.</param>
        /// <param name="B">Input: B matrix (ra x ra square).  Unchanged on exit.</param>
        /// <param name="X">Output: X matrix, to solve for (ca x ca square).</param>
        public void Solve(int ra, int ca, N[] A, N[] B, N[] X)
        {
            // A: ra x ca
            // B: ra x ra
            // X: ca x ca
            // A.X.A^t = B

            // Z: ca x ra

            // make local copy b/c gels modifies a
            N[] a = Workspace<N>.Get(ref ws_a, ra * ca);
            Array.Copy(A, a, ra*ca);


            // make local copy b/c gels modifies b, and may need more room than caller allocated
            int max_dim = Math.Max(ra, ca);
            N[] z = Workspace<N>.Get(ref ws_z, max_dim * max_dim);
            Array.Copy(B, z, ra * ra);
            

            // Workspace query
            N[] work = Workspace<N>.Get(ref ws_work, 1);
            lapack.gels(Transpose.NoTrans, ra, ca, ra, a, ra, z, max_dim, work, -1);
            int lwork = to_int(work[0]);
            work = Workspace<N>.Get(ref ws_work, lwork);


            // Now solve overdetermined equation
            //    A.Z = B for Z: ca x ra
            int info = lapack.gels(Transpose.NoTrans, ra, ca, ra, a, ra, z, max_dim, work, lwork);
            if (info != 0)
                throw new Exception("GELS failed with error " + info);

            // first ca rows of each of the ra columns of Z contain the solutions
            // so we collect them into a
            for (int c = 0; c < ra; c++)
                Array.Copy(z, c * max_dim, a, c * ca, ca);
            
            // then we tranpose a back into z
            extras.CopyTranspose(ca, ra, a, z);
            // so it now actually holds z^t


            //
            //    Now we solve   Z = X.A^t   for X
            //       by doing  Z^t = A.X^t   for X^t
            

            // re-place A into a
            Array.Copy(A, a, ra*ca);

            // workspace query
            lapack.gels(Transpose.NoTrans, ra, ca, ca, a, ra, z, max_dim, work, -1);
            lwork = to_int(work[0]);
            work = Workspace<N>.Get(ref ws_work, lwork);

            info = lapack.gels(Transpose.NoTrans, ra, ca, ca, a, ra, z, max_dim, work, lwork);
            if (info != 0)
                throw new Exception("GELS failed with error " + info);

            // first ca rows of each of the ca columns of z now contains X^t
            // so we collect them into x
            for (int c = 0; c < ca; c++)
                Array.Copy(z, c * max_dim, X, c * ca, ca);

            // and finally inplace tranpose x^t -> x
            extras.InplaceTransposeSquareMatrix(ca, X);
        }


    }
}
