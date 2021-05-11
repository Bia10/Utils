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

        public static T To<T>(this string str)  //TODO: validation
        {
            if (!str.Valid())
                throw new InvalidOperationException("Input string in wrong format!");

            return typeof(T) switch
            {
                { } intType when intType == typeof(int) =>
                    (T) Convert.ChangeType(int.Parse(str), typeof(T)),
                { } floatType when floatType == typeof(float) => 
                    (T) Convert.ChangeType(float.Parse(str), typeof(T)),
                { } doubleType when doubleType == typeof(double) =>
                    (T) Convert.ChangeType(double.Parse(str), typeof(T)),
                { } decimalType when decimalType == typeof(decimal) =>
                    (T) Convert.ChangeType(decimal.Parse(str), typeof(T)),
                { } longType when longType == typeof(long) => 
                    (T) Convert.ChangeType(long.Parse(str), typeof(T)),

                _ => throw new InvalidOperationException("Input type is not supported!")
            };
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