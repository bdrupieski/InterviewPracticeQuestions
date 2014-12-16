using System;
using System.Diagnostics;

namespace CrackingTheCodingInterview
{
    public class PerformanceHelper
    {
        public static void PerformanceTestPublicStaticMethods<T>(params object[] methodArgs)
        {
            foreach (var m in typeof(T).PublicStaticMethods())
            {
                var sw = Stopwatch.StartNew();
                for (int i = 0; i < 1000000; i++)
                {
                    m.Invoke(null, methodArgs);
                }
                sw.Stop();
                Console.WriteLine("{0} ms for {1}", sw.ElapsedMilliseconds, m);
            }
        }
    }
}