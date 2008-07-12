using System;

namespace Nmli
{
    public static class Versioning
    {
        public static void PrintVersionACML() { Acml.Externs.acmlinfo(); }

        public static void PrintVersionMKL()       
        {
            int len = 198;
            byte[] buffer = new byte[len];
            try
            {
                Mkl.Externs.MKLGetVersionString(buffer, len);
                Console.WriteLine(System.Text.ASCIIEncoding.ASCII.GetString(buffer));
            }
            catch (EntryPointNotFoundException)
            {
                Console.WriteLine("MKL Version unknown (MKLGetVersionString is not available)");
            }
        }

    }

}
