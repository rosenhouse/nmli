using System;


namespace Nmli.Acml
{
    class Vml : Managed.BaseVml, IVml
    {
        public new void Ln(int n, float[] x, float[] y)
        {
            //Console.WriteLine("New new method in ACML VML is being correctly used for the interface!");
            if (Utilities.is64Bit)
                Externs.vrsa_logf(n, x, y);
            else
                base.Ln(n, x, y);
        }

        public new void Ln(int n, double[] x, double[] y)
        {
            if (Utilities.is64Bit)
                Externs.vrda_log(n, x, y);
            else
                base.Ln(n, x, y);
        }


        public new void Exp(int n, float[] x, float[] y)
        {
            if (Utilities.is64Bit)
                Externs.vrsa_expf(n, x, y);
            else
                base.Exp(n, x, y);
        }

        public new void Exp(int n, double[] x, double[] y)
        {
            if (Utilities.is64Bit)
                Externs.vrda_exp(n, x, y);
            else
                base.Exp(n, x, y);
        }
    }
}
