using System.Collections.Generic;

namespace Common
{
    public static class ListExtensions
    {
        public static void Swap<T>(this List<T> list, int a, int b)
        {
            T temp = list[a];
            list[a] = list[b];
            list[b] = temp;
        }
    }
}