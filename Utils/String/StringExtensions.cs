using System;
using System.Linq;

namespace Utils.String
{
    public static class StringExtensions
    {
        public static bool IsDigitOnly(this string str)
        {
            return str.All(char.IsDigit);
        }

        public static string StringBetweenStrings(this string input, string start, string end)
        {
            var indexOfStart = input.IndexOf(start, StringComparison.Ordinal) + start.Length;
            var indexOfEnd = input.IndexOf(end, indexOfStart, StringComparison.Ordinal);

            return string.IsNullOrEmpty(end) ? input[indexOfStart..] : input[indexOfStart..indexOfEnd];
        }

        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source?.IndexOf(toCheck, comp) >= 0;
        }
    }
}