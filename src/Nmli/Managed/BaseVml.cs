using System;

namespace Nmli.Managed
{
    internal class BaseVml
    {

        protected void Ln(int n, float[] x, float[] y)
        {
            for (int i = 0; i < n; i++)
                y[i] = (float)(Math.Log(x[i]));
        }

        protected void Ln(int n, double[] x, double[] y)
        {
            for (int i = 0; i < n; i++)
                y[i] = Math.Log(x[i]);
        }


        protected void Exp(int n, float[] x, float[] y)
        {
            for (int i = 0; i < n; i++)
                y[i] = (float)Math.Exp(x[i]);
        }


        protected void Exp(int n, double[] x, double[] y)
        {
            for (int i = 0; i < n; i++)
                y[i] = Math.Exp(x[i]);
        }

    }
    
}
