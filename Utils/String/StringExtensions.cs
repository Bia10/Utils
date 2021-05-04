using System;
using System.Linq;

namespace Utils.String
{
    public static class StringExtensions
    {

        public static bool Valid(this string str)
        {
            return (!string.IsNullOrEmpty(str) && !string.IsNullOrWhiteSpace(str));
        }

        public static bool IsDigitOnly(this string str)
        {
            if (!Valid(str))
                throw new InvalidOperationException("Input string in wrong format!");

            return str.All(char.IsDigit);
        }

        public static string StringBetweenStrings(this string input, string start, string end)
        {
            if (!Valid(input))
                throw new InvalidOperationException("Input string in wrong format!");

            var indexOfStart = input.IndexOf(start, StringComparison.Ordinal) + start.Length;
            var indexOfEnd = input.IndexOf(end, indexOfStart, StringComparison.Ordinal);

            return string.IsNullOrEmpty(end) ? input[indexOfStart..] : input[indexOfStart..indexOfEnd];
        }

        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            if (!Valid(source))
                throw new InvalidOperationException("Input string in wrong format!");

            return source?.IndexOf(toCheck, comp) >= 0;
        }
    }
}