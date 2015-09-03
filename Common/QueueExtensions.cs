using System.Collections.Generic;

namespace Common
{
    public static class QueueExtensions
    {
        public static bool IsEmpty<T>(this Queue<T> q)
        {
            return q.Count == 0;
        }
    }
}