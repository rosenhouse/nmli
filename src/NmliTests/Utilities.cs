using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

using System.Reflection;

namespace NmliTests
{
    public class Utilities
    {
        public static void Report(string msg, ConsoleColor cc)
        {
            ConsoleColor c = Console.ForegroundColor;
            Console.ForegroundColor = cc;
            Console.WriteLine(msg);
            Console.ForegroundColor = c;
        }

        static IEnumerable<Type> GetFixtures()
        {
            foreach (Type t in Assembly.GetExecutingAssembly().GetTypes())
                if (t.GetCustomAttributes(typeof(TestFixtureAttribute), false).Length > 0)
                    yield return t;

        }

        public static void RunAll()
        {
            foreach (Type t in GetFixtures())
            {
                Object fixture = t.GetConstructor(Type.EmptyTypes).Invoke(null);
                Console.WriteLine("Starting to test {0}...", t.Name);
                foreach (MethodInfo mi in t.GetMethods())
                {
                    if (mi.GetCustomAttributes(typeof(TestAttribute), false).Length > 0)
                    {
                        Console.Write("\tRunning test {0} in {1}...", mi.Name, t.Name);
                        try
                        {
                            mi.Invoke(fixture, null);
                            Console.WriteLine("OK.");
                        }
                        catch (Exception e)
                        {
                            Report("Failed with " + e.ToString(), ConsoleColor.Red);
                        }
                    }
                }
                Console.WriteLine("Done testing {0}.", t.Name);
            }
            Console.WriteLine("Testing complete.");
        }
    }
}
