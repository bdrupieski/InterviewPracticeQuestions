using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace RandomlyEncounteredQuestions
{
    public class PascalsTriangle
    {
        public IEnumerable<IEnumerable<int>> GeneratePascalsTriangle()
        {
            yield return new[] { 1 };

            IEnumerable<int> next = new[] { 1, 1 };

            while (true)
            {
                yield return next;

                var added = next.Zip(next.Skip(1), (x, y) => x + y);

                next = new[] { 1 }.Concat(added).Concat(new[] { 1 }).ToArray();
            }
        }

        [Test]
        public void PascalsTriangleTest()
        {
            var p = GeneratePascalsTriangle().Take(7);

            int[][] t =
            {
                new[] { 1 },
                new[] { 1, 1 },
                new[] { 1, 2, 1 },
                new[] { 1, 3, 3, 1 },
                new[] { 1, 4, 6, 4, 1 },
                new[] { 1, 5, 10, 10, 5, 1 },
                new[] { 1, 6, 15, 20, 15, 6, 1 },
            };

            CollectionAssert.AreEqual(p, t);
        }
    }
}