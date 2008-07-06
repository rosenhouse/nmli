using System;
using Nmli.Managed;

namespace Nmli.Acml
{
    /// <summary>
    /// ACML implementation of vector math
    /// </summary>
    /// <remarks>
    /// Note that ACML only provides a limited number of vector functions, and those
    /// are only available for x64.  Consequently, we do run-time is64Bit checks,
    /// and if an ACML vector version is not available, we substitute with either a
    /// call to BLAS (e.g. call _sbmv with unit scalars to do a Sqr) or use a purely
    /// managed implementation (e.g. for Ln and Exp)
    ///</remarks>
    class Vml : IVml
    {
        protected readonly ManagedVml managed = new ManagedVml();
        protected readonly Blas blas = new Blas();
        
        public void Ln(int n, float[] x, float[] y)
        {
            //Console.WriteLine("New new method in ACML VML is being correctly used for the interface!");
            if (Utilities.is64Bit)
                Externs.vrsa_logf(n, x, y);
            else
                managed.Ln(n, x, y);
        }

        public void Ln(int n, double[] x, double[] y)
        {
            if (Utilities.is64Bit)
                Externs.vrda_log(n, x, y);
            else
                managed.Ln(n, x, y);
        }


        public void Exp(int n, float[] x, float[] y)
        {
            if (Utilities.is64Bit)
                Externs.vrsa_expf(n, x, y);
            else
                managed.Exp(n, x, y);
        }

        public void Exp(int n, double[] x, double[] y)
        {
            if (Utilities.is64Bit)
                Externs.vrda_exp(n, x, y);
            else
                managed.Exp(n, x, y);
        }

        

        public void Add(int n, float[] a, float[] b, float[] y) { managed.Add(n, a, b, y); }

        public void Sub(int n, float[] a, float[] b, float[] y) { managed.Sub(n, a, b, y); }

        public void Sqr(int n, float[] a, float[] y)
        {
            // treat a as a diagonal matrix and as vector, output to vector y, scaled by 0
            blas.sbmv(UpLo.Lower, n, 0, 1, a, 1, a, 1, 0, y, 1);
        }



        public void Add(int n, double[] a, double[] b, double[] y) { managed.Add(n, a, b, y); }

        public void Sub(int n, double[] a, double[] b, double[] y) { managed.Sub(n, a, b, y); }

        public void Sqr(int n, double[] a, double[] y)
        {
            // treat a as a diagonal matrix and as vector, output to vector y, scaled by 0
            blas.sbmv(UpLo.Lower, n, 0, 1, a, 1, a, 1, 0, y, 1);
        }

        public void Inv(int n, double[] a, double[] y) { managed.Inv(n, a, y); }
        public void Inv(int n, float[] a, float[] y) { managed.Inv(n, a, y); }


        public void Sqrt(int n, float[] a, float[] y) { managed.Sqrt(n, a, y); }
        public void Sqrt(int n, double[] a, double[] y) { managed.Sqrt(n, a, y); }


    }
}
