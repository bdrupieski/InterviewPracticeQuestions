using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Common
{
    public class PerformanceHelper
    {
        public static void PerformanceTestPublicStaticMethods<T>(params object[] methodArgs)
        {
            var methods = typeof(T).PublicStaticMethods();
            PerformanceTest(methods, methodArgs);
        }

        public static void PerformanceTestPublicStaticMethods<T, T2>(params object[] methodArgs)
        {
            var methods = typeof(T).PublicStaticMethods().Select(x => x.MakeGenericMethod((typeof(T2))));
            PerformanceTest(methods, methodArgs);
        }

        public static void PerformanceTestAction(Action action, string description)
        {
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < 1000000; i++)
            {
                action();
            }
            sw.Stop();
            Console.WriteLine("{0} ms for {1}", sw.ElapsedMilliseconds, description);
        }

        private static void PerformanceTest(IEnumerable<MethodInfo> methods, object[] methodArgs)
        {
            foreach (var m in methods)
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