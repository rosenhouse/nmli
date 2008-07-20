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
            

            //Workspace query
            N[] work = Workspace<N>.Get(ref ws_work, 1);
            lapack.gels(Transpose.NoTrans, ra, ca, ra, a, ra, z, max_dim, work, -1);
            int lwork = to_int(work[0]);
            work = Workspace<N>.Get(ref ws_work, lwork);


            // Now solve overdetermined equation
            //    A.Z = B for Z: ca x ra
            lapack.gels(Transpose.NoTrans, ra, ca, ra, a, ra, z, max_dim, work, lwork);


            //
            //    Then Z = X.A^t for X
            //    by doing Z^t = A.X^t  (also overdetermined)
            
            // transpose Z, by first copying to a
            Array.Copy(z, a, ra * ca);
            // then copy-back w/ transpose
            //extras.CopyTranspose(

        }


    }
}
