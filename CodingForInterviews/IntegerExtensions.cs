namespace CodingForInterviews
{
    public static class IntegerExtensions
    {
        public static bool IsBetween(this int a, int low, int high)
        {
            return a >= low && a <= high;
        }
    }
}