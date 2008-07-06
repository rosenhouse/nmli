using System;

namespace Nmli.Mkl
{

    // All VML mathematical functions can perform in-place operations, which
    // means that the same vector can be used as both input and output parameter.
    class Vml : IVml
    {
        public void Ln(int n, float[] x, float[] y) { Externs.vsLn(n, x, y); }

        public void Ln(int n, double[] x, double[] y) { Externs.vdLn(n, x, y); }


        public void Exp(int n, float[] x, float[] y) { Externs.vsExp(n, x, y); }

        public void Exp(int n, double[] x, double[] y) { Externs.vdExp(n, x, y); }



        public void Add(int n, float[] a, float[] b, float[] y) { Externs.vsAdd(n, a, b, y); }

        public void Sub(int n, float[] a, float[] b, float[] y) { Externs.vsSub(n, a, b, y); }

        public void Sqr(int n, float[] a, float[] y) { Externs.vsSqr(n, a, y); }

        public void Inv(int n, float[] a, float[] y) { Externs.vsInv(n, a, y); }

        public void Sqrt(int n, float[] a, float[] y) { Externs.vsSqrt(n, a, y); }



        public void Add(int n, double[] a, double[] b, double[] y) { Externs.vdAdd(n, a, b, y); }

        public void Sub(int n, double[] a, double[] b, double[] y) { Externs.vdSub(n, a, b, y); }

        public void Sqr(int n, double[] a, double[] y) { Externs.vdSqr(n, a, y); }

        public void Inv(int n, double[] a, double[] y) { Externs.vdInv(n, a, y); }

        public void Sqrt(int n, double[] a, double[] y) { Externs.vdSqrt(n, a, y); }
    }
}
