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
            if (!str.Valid())
                throw new InvalidOperationException("Input string in wrong format!");

            return str.All(char.IsDigit);
        }

        public static string GetDigitsOnly(this string str)
        {
            if (!str.Valid())
                throw new InvalidOperationException("Input string in wrong format!");

            return new string(str.Where(char.IsDigit).ToArray());
        }

        public static string StringBetweenStrings(this string str, string start, string end)
        {
            if (!str.Valid())
                throw new InvalidOperationException("Input string in wrong format!");

            var indexOfStart = str.IndexOf(start, StringComparison.Ordinal) + start.Length;
            var indexOfEnd = str.IndexOf(end, indexOfStart, StringComparison.Ordinal);

            return string.IsNullOrEmpty(end) ? str[indexOfStart..] : str[indexOfStart..indexOfEnd];
        }

        public static bool Contains(this string str, string toCheck, StringComparison comp)
        {
            if (!str.Valid())
                throw new InvalidOperationException("Input string in wrong format!");

            return str.IndexOf(toCheck, comp) >= 0;
        }
    }
}