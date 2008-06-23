using System;

namespace Nmli.Mkl
{

    // All VML mathematical functions can perform in-place operations, which
    // means that the same vector can be used as both input and output parameter.
    class Vml : Managed.BaseVml, IVml
    {
        public new void Ln(int n, float[] x, float[] y) { Externs.vsLn(n, x, y); }

        public new void Ln(int n, double[] x, double[] y) { Externs.vdLn(n, x, y); }


        public new void Exp(int n, float[] x, float[] y) { Externs.vsExp(n, x, y); }

        public new void Exp(int n, double[] x, double[] y) { Externs.vdExp(n, x, y); }
    }
}
