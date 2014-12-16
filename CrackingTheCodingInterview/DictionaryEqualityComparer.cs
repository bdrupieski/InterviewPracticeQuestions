using System.Collections.Generic;
using System.Linq;

namespace CrackingTheCodingInterview
{
    public class DictionaryEqualityComparer<TKey, TValue> : EqualityComparer<Dictionary<TKey, TValue>>
    {
        public override bool Equals(Dictionary<TKey, TValue> x, Dictionary<TKey, TValue> y)
        {
            return x.Count == y.Count && !x.Except(y).Any();
        }

        public override int GetHashCode(Dictionary<TKey, TValue> obj)
        {
            if (obj == null)
                return 0;

            return obj.GetHashCode();
        }
    }
}