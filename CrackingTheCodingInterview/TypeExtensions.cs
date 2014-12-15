using System;
using System.Reflection;

namespace CrackingTheCodingInterview
{
    public static class TypeExtensions
    {
        public static MethodInfo[] PublicStaticMethods(this Type t)
        {
            return t.GetMethods(BindingFlags.Static | BindingFlags.Public);
        }
    }
}