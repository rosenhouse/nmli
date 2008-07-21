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

        public static void PrintArray(N[] a)
        {
            foreach (N x in a)
                Console.Write("{0:00.00}, ", x);
            Console.WriteLine();
        }


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
            
            //   A.Z = B
            // Z: ca x ra


            int max_dim = Math.Max(ra, ca);

            // make local copies b/c gels modifies a and b

            N[] a = Workspace<N>.Get(ref ws_a, max_dim * max_dim);
            N[] z = Workspace<N>.Get(ref ws_z, max_dim * max_dim);

            
            Array.Copy(A, a, ra*ca);
            extras.Reposition(ra, ra, max_dim, ra, B, z);
            

            // Workspace query
            N[] work = Workspace<N>.Get(ref ws_work, 1);
            lapack.gels(Transpose.NoTrans, ra, ca, ra, a, ra, z, max_dim, work, -1);
            int lwork = to_int(work[0]);
            work = Workspace<N>.Get(ref ws_work, lwork);


            // Now solve
            //    A.Z = B for Z: ca x ra
            int info = lapack.gels(Transpose.NoTrans, ra, ca, ra, a, ra, z, max_dim, work, lwork);
            if (info != 0)
                throw new Exception("GELS failed with error " + info);


            Console.WriteLine("1st GELS call yields full solution buffer=");
            PrintArray(z);

            // first ca rows of each of the ra columns of Z contain the solutions
            // so we collect them into a
            extras.Reposition(ca, max_dim, ca, ra, z, a);

            Console.WriteLine("Collected z=");
            PrintArray(a);

            // then we tranpose a back into z
            extras.CopyTranspose(ca, ra, a, z);
            // so it now actually holds z^t
            // Z^t : ra x ca
            Console.WriteLine("Or z transpose=");
            PrintArray(z);


            // re-expand
            extras.Reposition(ra, ra, max_dim, ca, z, a);
            // copy-back
            Array.Copy(a, z, max_dim * ca);


            //
            //    Now we solve   Z = X.A^t   for X
            //       by doing  Z^t = A.X^t   for X^t
            
            // re-place A into a
            Array.Copy(A, a, ra*ca);


            Console.WriteLine("A =");
            PrintArray(a);

            // workspace query
            lapack.gels(Transpose.NoTrans, ra, ca, ca, a, ra, z, max_dim, work, -1);
            lwork = to_int(work[0]);
            work = Workspace<N>.Get(ref ws_work, lwork);

            info = lapack.gels(Transpose.NoTrans, ra, ca, ca, a, ra, z, max_dim, work, lwork);
            if (info != 0)
                throw new Exception("GELS failed with error " + info);

            Console.WriteLine("2nd GELS call yields full solution buffer=");
            PrintArray(z);

            // first ca rows of each of the ca columns of z now contains X^t
            // so we collect them into x
            for (int c = 0; c < ca; c++)
                Array.Copy(z, c * max_dim, X, c * ca, ca);

            Console.WriteLine("Collected x^t=");
            PrintArray(X);


            // and finally inplace tranpose x^t -> x
            extras.InplaceTransposeSquareMatrix(ca, X);
        }


    }
}
