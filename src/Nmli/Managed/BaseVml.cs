using System;

namespace Nmli.Managed
{
    public class ManagedVml : IVml
    {

        public void Ln(int n, float[] x, float[] y)
        {
            for (int i = 0; i < n; i++)
                y[i] = (float)(Math.Log(x[i]));
        }

        public void Ln(int n, double[] x, double[] y)
        {
            for (int i = 0; i < n; i++)
                y[i] = Math.Log(x[i]);
        }


        public void Exp(int n, float[] x, float[] y)
        {
            for (int i = 0; i < n; i++)
                y[i] = (float)Math.Exp(x[i]);
        }

        public void Exp(int n, double[] x, double[] y)
        {
            for (int i = 0; i < n; i++)
                y[i] = Math.Exp(x[i]);
        }



        public void Add(int n, float[] a, float[] b, float[] y)
        {
            for (int i = 0; i < n; i++)
                y[i] = a[i] + b[i];
        }

        public void Sub(int n, float[] a, float[] b, float[] y)
        {
            for (int i = 0; i < n; i++)
                y[i] = a[i] - b[i];
        }

        public void Sqr(int n, float[] a, float[] y)
        {
            for (int i = 0; i < n; i++)
                y[i] = a[i] * a[i];
        }


        public void Add(int n, double[] a, double[] b, double[] y)
        {
            for (int i = 0; i < n; i++)
                y[i] = a[i] + b[i];
        }

        public void Sub(int n, double[] a, double[] b, double[] y)
        {
            for (int i = 0; i < n; i++)
                y[i] = a[i] - b[i];
        }

        public void Sqr(int n, double[] a, double[] y)
        {
            for (int i = 0; i < n; i++)
                y[i] = a[i] * a[i];
        }

        
    }
    
}
