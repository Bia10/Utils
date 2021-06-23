using System;
using System.Collections.Generic;
using System.Linq;
using Utils.Types.Char;

namespace Utils.Types.String
{
    public static class StringExtensions
    {
        public static bool Valid(this string str)
        {
            return !string.IsNullOrEmpty(str) && !string.IsNullOrWhiteSpace(str);
        }

        public static string Tail(this string str, int tailLength)
        {
            if (!str.Valid())
                throw new InvalidOperationException("Input string in wrong format!");

            return tailLength >= str.Length ? str : str[^tailLength..];
        }

        public static string Head(this string str, int headLength)
        {
            if (!str.Valid())
                throw new InvalidOperationException("Input string in wrong format!");

            return headLength >= str.Length ? str : str[..headLength];
        }

        public static string Left(this string str, int count)
        {
            if (!str.Valid())
                throw new InvalidOperationException("Input string in wrong format!");

            return str[..Math.Min(str.Length, count)];
        }

        public static string Right(this string str, int count)
        {
            if (!str.Valid())
                throw new InvalidOperationException("Input string in wrong format!");

            return str.Substring(Math.Max(str.Length - count, 0),
                Math.Min(count, str.Length));
        }

        public static string Middle(this string str, int start)
        {
            if (!str.Valid())
                throw new InvalidOperationException("Input string in wrong format!");

            return str[Math.Min(start, str.Length)..];
        }

        public static string Middle(this string str, int start, int count)
        {
            if (!str.Valid())
                throw new InvalidOperationException("Input string in wrong format!");

            return str.Substring(Math.Min(start, str.Length),
                Math.Min(count, Math.Max(str.Length - start, 0)));
        }

        // by digit we mean only decimal digital number
        public static bool IsDigitsOnly(this string str)
        {
            if (!str.Valid())
                throw new InvalidOperationException("Input string in wrong format!");

            return str.ToCharArray().All(ch => ch.IsDigitNumber());
        }

        public static string GetDigitsOnly(this string str)
        {
            if (!str.Valid())
                throw new InvalidOperationException("Input string in wrong format!");

            return new string(str.Where(ch => ch.IsDigitNumber()).ToArray());
        }

        public static bool EndsWithDigits(this string str, int stopIndexFromEnd)
        {
            var indexOfLastStr = str.Length - 1;

            if (!str.Valid())
                throw new InvalidOperationException("Input string in wrong format!");
            if (stopIndexFromEnd > indexOfLastStr || stopIndexFromEnd < 0)
                throw new InvalidOperationException("StopIndex outside bounds of string!");

            for (var i = indexOfLastStr; i > indexOfLastStr - stopIndexFromEnd; i--)
            {
                var currentString = str.Substring(i, 1);
                if (currentString.Equals(" ")) continue;
                if (!currentString.IsDigitsOnly()) return false;
            }

            return true;
        }

        public static string GetDigitsFromEnd(this string str, int stopIndex)
        {
            var indexOfLastStr = str.Length - 1;

            if (!str.Valid())
                throw new InvalidOperationException("Input string in wrong format!");
            if (stopIndex > indexOfLastStr || stopIndex < 0)
                throw new InvalidOperationException("StopIndex outside bounds of string!");

            var indexList = new List<int>();
            for (var i = indexOfLastStr; i > indexOfLastStr - stopIndex; i--)
            {
                var currentString = str.Substring(i, 1);
                if (currentString.Equals(" ")) continue;
                if (currentString.IsDigitsOnly()) indexList.Add(i);
            }

            indexList.Sort();
            var result = string.Empty;
            foreach (var index in indexList)
                result += str.Substring(index, 1);

            return result;
        }

        public static bool ContainsSpecialDigitChar(this string str)
        {
            if (!str.Valid())
                throw new InvalidOperationException("Input string in wrong format!");

            var specialChars = new[] {"-", ".", "e", "E"};

            return str.ContainsAny(specialChars);
        }

        public static bool ContainsAny(this string str, params string[] strings)
        {
            if (!str.Valid())
                throw new InvalidOperationException("Input string in wrong format!");
            if (!strings.Any())
                throw new InvalidOperationException("Input params empty!");

            return strings.Any(str.Contains); 
        }

        public static bool ContainsAnySequenceOf(this string str, List<char> charArray)
        {
            if (!str.Valid())
                throw new InvalidOperationException("Input string in wrong format!");
            if (!charArray.Any())
                throw new InvalidOperationException("Input params empty!");

            var matchingChars = charArray.Where(char1 =>
                str.ToCharArray().Any(char2 => char2.Equals(char1)));

            foreach (var ch in matchingChars)
            {
                charArray.Remove(ch);

                var startIndex = str.IndexOf(ch);
                var length = Math.Min(str.Length - str.IndexOf(ch), charArray.Count);

                return str.Substring(startIndex, length).ContainsAnySequenceOf(charArray);
            }

            return false;
        }

        public static string StringBetweenStrings(this string str, string start, string end)
        {
            if (!str.Valid())
                throw new InvalidOperationException("Input string in wrong format!");

            var indexOfStart = str.IndexOf(start, StringComparison.Ordinal) + start.Length;
            var indexOfEnd = str.IndexOf(end, indexOfStart, StringComparison.Ordinal);

            return string.IsNullOrEmpty(end) ? str[indexOfStart..] : str[indexOfStart..indexOfEnd];
        }

        public static string RemoveLineBreaks(this string str)
        {
            if (!str.Valid())
                throw new InvalidOperationException("Input string in wrong format!");

            return str.Replace("\r", string.Empty)
                .Replace("\n", string.Empty);
        }

        public static string ReplaceLineBreaks(this string str, string replacement)
        {
            if (!str.Valid())
                throw new InvalidOperationException("Input string in wrong format!");

            return str.Replace("\r\n", replacement)
                .Replace("\r", replacement)
                .Replace("\n", replacement);
        }

        public static string SurroundWithDoubleQuotes(this string str)
        {
            if (!str.Valid())
                throw new InvalidOperationException("Input string in wrong format!");

            return SurroundWith(str, "\"");
        }

        public static string SurroundWith(this string str, string endMark)
        {
            if (!str.Valid())
                throw new InvalidOperationException("Input string in wrong format!");

            return endMark + str + endMark;
        }

        public static T To<T>(this string str)  //TODO: unsigned types, more checks
        {
            if (!str.Valid())
                throw new InvalidOperationException("Input string in wrong format!");

            switch (typeof(T))
            {
                case { } sbyteType when sbyteType == typeof(sbyte): // int8
                    {
                    if (!str.IsDigitsOnly() && !str.Contains("-"))
                        throw new InvalidOperationException("Input string in wrong format, non-digit char other then '-' present.");
                    if (str.ToCharArray().Length > 3)
                        throw new InvalidOperationException("Input string in wrong format, too many digits maximum for int8 is 3.");

                    sbyte value;
                    try
                    {
                        value = sbyte.Parse(str);
                    }
                    catch (Exception ex)
                    {
                        throw new InvalidOperationException("Failed to parse input to numeric type!", ex);
                    }

                    return (T)Convert.ChangeType(value, typeof(T));
                }
                case { } shortType when shortType == typeof(short): // int16
                    {
                        if (!str.IsDigitsOnly() && !str.Contains("-"))
                            throw new InvalidOperationException("Input string in wrong format, non-digit char other then '-' present.");
                        if (str.ToCharArray().Length > 5)
                            throw new InvalidOperationException("Input string in wrong format, too many digits maximum for int16 is 5.");

                        short value;
                        try
                        {
                            value = short.Parse(str);
                        }
                        catch (Exception ex)
                        {
                            throw new InvalidOperationException("Failed to parse input to numeric type!", ex);
                        }

                        return (T)Convert.ChangeType(value, typeof(T));
                    }
                case { } intType when intType == typeof(int): // int32
                    {
                        if (!str.IsDigitsOnly() && !str.Contains("-"))
                            throw new InvalidOperationException("Input string in wrong format, non-digit char other then '-' present.");
                        if (str.ToCharArray().Length > 10)
                            throw new InvalidOperationException("Input string in wrong format, too many digits maximum for int32 is 10.");

                        int value;
                        try 
                        {
                            value = int.Parse(str);
                        }
                        catch (Exception ex) 
                        {
                            throw new InvalidOperationException("Failed to parse input to numeric type!", ex);
                        }
                            
                        return (T) Convert.ChangeType(value, typeof(T));
                    }

                case { } longType when longType == typeof(long): // int64
                    {
                        if (!str.IsDigitsOnly() && !str.Contains("-"))
                            throw new InvalidOperationException("Input string in wrong format, non-digit char other then '-' present.");
                        if (str.ToCharArray().Length > 19)
                            throw new InvalidOperationException("Input string in wrong format, too many digits maximum for int64 is 19.");

                        long value;
                        try 
                        {
                            value = long.Parse(str);
                        }
                        catch (Exception ex)
                        {
                            throw new InvalidOperationException("Failed to parse input to numeric type!", ex);
                        }

                        return (T) Convert.ChangeType(value, typeof(T));
                    }
                case { } floatType when floatType == typeof(float):
                    {
                        if (!str.IsDigitsOnly() && !str.ContainsSpecialDigitChar())
                            throw new InvalidOperationException("Input string in wrong format, non-digit char other then '-' present.");
                        if (str.ToCharArray().Length > 9)
                            throw new InvalidOperationException("Input string in wrong format, too many digits maximum for float is 9.");
                      
                        float value;
                        try 
                        {
                            value = float.Parse(str);
                        }
                        catch (Exception ex)
                        {
                            throw new InvalidOperationException("Failed to parse input to numeric type!", ex);
                        }

                        return (T) Convert.ChangeType(value, typeof(T));
                    }
                case { } doubleType when doubleType == typeof(double):
                    {
                        if (!str.IsDigitsOnly() && !str.ContainsSpecialDigitChar())
                            throw new InvalidOperationException("Input string in wrong format, non-digit char other then '-' present.");
                        if (str.ToCharArray().Length > 17)
                            throw new InvalidOperationException("Input string in wrong format, too many digits maximum for double is 17.");

                        double value;
                        try 
                        {
                            value = double.Parse(str);
                        }
                        catch (Exception ex)
                        {
                            throw new InvalidOperationException("Failed to parse input to numeric type!", ex);
                        }

                        return (T) Convert.ChangeType(value, typeof(T));
                    }
                case { } decimalType when decimalType == typeof(decimal):
                    {
                        if (!str.IsDigitsOnly() && !str.ContainsSpecialDigitChar())
                            throw new InvalidOperationException("Input string in wrong format, non-digit char other then '-' present.");
                        if (str.ToCharArray().Length > 29)
                            throw new InvalidOperationException("Input string in wrong format, too many digits maximum for decimal is 29.");

                        decimal value;
                        try 
                        {
                            value = decimal.Parse(str);
                        }
                        catch (Exception ex)
                        {
                            throw new InvalidOperationException("Failed to parse input to numeric type!", ex);
                        }

                        return (T) Convert.ChangeType(value, typeof(T));
                    }

                default:
                    throw new InvalidOperationException("Input type is not supported!");
            }
        }
    }
}