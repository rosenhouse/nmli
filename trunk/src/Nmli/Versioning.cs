using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Diagnostics;

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


        static string programBanner = null;


        static string GetAssemblyInfo(AssemblyName an)
        {
            string s = an.Name + " v" + an.Version.ToString();
            if (an.ProcessorArchitecture != ProcessorArchitecture.None)
                s += " (" + an.ProcessorArchitecture.ToString() + ")";
            return s;
        }

        static string RecursiveGetAssemblyInfo(Assembly a)
        {
            AssemblyName[] ans = a.GetReferencedAssemblies();
            Comparison<AssemblyName> c = delegate(AssemblyName x, AssemblyName y) { return String.Compare(x.Name, y.Name); };
            Array.Sort(ans, c);
            string[] info = Array.ConvertAll<AssemblyName, string>(ans, GetAssemblyInfo);
            const string leaf = " |- ";
            return leaf + string.Join(Environment.NewLine + leaf, info);
        }

        static string GetCopyright(Assembly a)
        {
            AssemblyName an = a.GetName();
            object[] customAttribs = a.GetCustomAttributes(true);
            string cprt = "";
            foreach (object o in customAttribs)
            {
                if (o.GetType() == typeof(AssemblyCopyrightAttribute))
                    cprt = (o as AssemblyCopyrightAttribute).Copyright;
            }
            return cprt;
        }

        static string ExecutionEnvironment
        {
            get { return "Running on \'" + Environment.MachineName + "\', " + Environment.OSVersion; }
        }

        public static string ProgramBanner
        {
            get
            {
                if (programBanner == null)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine();
                    Assembly a = Assembly.GetCallingAssembly();

                    sb.AppendLine(GetAssemblyInfo(a.GetName()) + " : " + GetCopyright(a));
                    sb.AppendLine(RecursiveGetAssemblyInfo(a));
                    sb.AppendLine(ExecutionEnvironment);
                    sb.AppendLine("Execution began at " + DateTime.Now.ToString());
#if DEBUG
                    sb.AppendLine("---> RUNNING DEBUG BUILD.  PERFORMANCE IS SUB-OPTIMAL.");
#endif
                    programBanner = sb.ToString();
                }
                return programBanner;
            }
        }
    }

}
