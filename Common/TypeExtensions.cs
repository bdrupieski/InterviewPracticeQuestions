using System;
using System.Reflection;

namespace Common
{
    public static class TypeExtensions
    {
        public static MethodInfo[] PublicStaticMethods(this Type t)
        {
            return t.GetMethods(BindingFlags.Static | BindingFlags.Public);
        }
    }
}