using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
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
                throw new ArgumentException("Input string in wrong format!");

            return tailLength >= str.Length ? str : str[^tailLength..];
        }

        public static string Head(this string str, int headLength)
        {
            if (!str.Valid())
                throw new ArgumentException("Input string in wrong format!");

            return headLength >= str.Length ? str : str[..headLength];
        }

        public static string HeadSizeOfStrB(this string str, string strB)
        {
            return str.Head(strB.Length);
        }

        public static string Left(this string str, int count)
        {
            if (!str.Valid())
                throw new ArgumentException("Input string in wrong format!");

            return str[..Math.Min(str.Length, count)];
        }

        public static string Right(this string str, int count)
        {
            if (!str.Valid())
                throw new ArgumentException("Input string in wrong format!");

            return str.Substring(Math.Max(str.Length - count, 0),
                Math.Min(count, str.Length));
        }

        public static string Middle(this string str, int start)
        {
            if (!str.Valid())
                throw new ArgumentException("Input string in wrong format!");

            return str[Math.Min(start, str.Length)..];
        }

        public static string Middle(this string str, int start, int count)
        {
            if (!str.Valid())
                throw new ArgumentException("Input string in wrong format!");

            return str.Substring(Math.Min(start, str.Length),
                Math.Min(count, Math.Max(str.Length - start, 0)));
        }

        public static bool StringEqualTo(this string str, string strB)
        {
            if (!str.Valid())
                throw new ArgumentException("Input string in wrong format!");

            var result = string.CompareOrdinal(str, strB);

            return result == 0;
        }

        public static bool CharHashEqualTo(this string str, string strB)
        {
            if (!str.Valid())
                throw new ArgumentException("Input string in wrong format!");

            var aSet = new HashSet<char>(str);
            var bSet = new HashSet<char>(strB);

            var areHashEqual = aSet.SetEquals(bSet);

            return areHashEqual;
        }

        public static bool StrHashEqualTo(this IEnumerable<string> str, IEnumerable<string> strB)
        {
            var aSet = new HashSet<string>(str);
            var bSet = new HashSet<string>(strB);

            var areHashEqual = aSet.SetEquals(bSet);

            return areHashEqual;
        }

        public static bool StrictlyCharEqualTo(this string str, string strB)
        {
            if (!str.Valid())
                throw new ArgumentException("Input string in wrong format!");

            var areHashEqual = str.CharHashEqualTo(strB);
            var areStringEqual = str.StringEqualTo(strB);

            return areHashEqual && areStringEqual;
        }

        // by digit we mean only decimal digital number
        public static bool IsDigitsOnly(this string str)
        {
            if (!str.Valid())
                throw new ArgumentException("Input string in wrong format!");

            return str.ToCharArray().All(ch => ch.IsDigitNumber());
        }

        public static string GetDigitsOnly(this string str)
        {
            if (!str.Valid())
                throw new ArgumentException("Input string in wrong format!");

            return new string(str.Where(ch => ch.IsDigitNumber()).ToArray());
        }

        public static bool EndsWithDigits(this string str, int stopIndexFromEnd)
        {
            var indexOfLastStr = str.Length - 1;

            if (!str.Valid())
                throw new ArgumentException("Input string in wrong format!");
            if (stopIndexFromEnd > indexOfLastStr || stopIndexFromEnd < 0)
                throw new ArgumentException("StopIndex outside bounds of string!");

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
                throw new ArgumentException("Input string in wrong format!");
            if (stopIndex > indexOfLastStr || stopIndex < 0)
                throw new ArgumentException("StopIndex outside bounds of string!");

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
                throw new ArgumentException("Input string in wrong format!");

            var specialChars = new[] {"-", ".", "e", "E"};

            return str.ContainsAny(specialChars);
        }

        public static bool ContainsAny(this string str, params string[] strings)
        {
            if (!str.Valid())
                throw new ArgumentException("Input string in wrong format!");
            if (!strings.Any())
                throw new ArgumentException("Input params empty!");

            return strings.Any(str.Contains); 
        }

        public static bool ContainsAnySequenceOf(this string str, List<char> charArray)
        {
            if (!str.Valid())
                throw new ArgumentException("Input string in wrong format!");
            if (!charArray.Any())
                throw new ArgumentException("Input params empty!");

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

        public static string ReplaceAny(this string str, string[] toReplace, string replacement = "")
        {
            if (!str.Valid())
                throw new ArgumentException("Input string in wrong format!");
            if (!toReplace.Any())
                throw new ArgumentException("Input params empty!");

            foreach (var strToReplace in toReplace)
                if(str.Contains(strToReplace))
                    str = str.Replace(strToReplace, replacement);

            return str;
        }

        public static string ReplaceAt(this string str, int index, int length, string replacement = "")
        {
            if (!str.Valid())
                throw new ArgumentException("Input string in wrong format!");

            return str.Remove(index, Math.Min(length, str.Length - index))
                .Insert(index, replacement);
        }

        public static string ReplaceForbiddenFilenameChars(this string str, string replacement = "")
        {
            if (!str.Valid())
                throw new ArgumentException("Input string in wrong format!");

            var forbiddenChars = new[] { "<", ">", ":", "\"", "/", "\\", "|", "?", "*"};

            return str.ReplaceAny(forbiddenChars, replacement);
        }

        public static string StringBetweenStrings(this string str, string start, string end)
        {
            if (!str.Valid())
                throw new ArgumentException("Input string in wrong format!");

            var indexOfStart = str.IndexOf(start, StringComparison.Ordinal) + start.Length;
            var indexOfEnd = str.IndexOf(end, indexOfStart, StringComparison.Ordinal);

            return string.IsNullOrEmpty(end) ? str[indexOfStart..] : str[indexOfStart..indexOfEnd];
        }

        public static string RemoveLineBreaks(this string str)
        {
            if (!str.Valid())
                throw new ArgumentException("Input string in wrong format!");

            return str.Replace("\r", string.Empty)
                .Replace("\n", string.Empty);
        }

        public static string ReplaceLineBreaks(this string str, string replacement)
        {
            if (!str.Valid())
                throw new ArgumentException("Input string in wrong format!");

            return str.Replace("\r\n", replacement)
                .Replace("\r", replacement)
                .Replace("\n", replacement);
        }

        public static string SurroundWithDoubleQuotes(this string str)
        {
            if (!str.Valid())
                throw new ArgumentException("Input string in wrong format!");

            return SurroundWith(str, "\"");
        }

        public static string SurroundWith(this string str, string endMark)
        {
            if (!str.Valid())
                throw new ArgumentException("Input string in wrong format!");

            return endMark + str + endMark;
        }

        public static string FirstCharToUpper(this string str)
        {
            if (!str.Valid())
                throw new ArgumentException("Input string in wrong format!");

            if (str.Length == 1)
                return char.ToUpper(str[0]).ToString();

            return char.ToUpper(str[0]) + str[1..];
        }

        public static string[] SplitAt(this string str, params int[] index)
        {
            if (!str.Valid())
                throw new ArgumentException("Input string in wrong format!");

            index = index.Distinct().OrderBy(x => x).ToArray();
            var output = new string[index.Length + 1];
            var pos = 0;

            for (var i = 0; i < index.Length; pos = index[i++])
                output[i] = str[pos..index[i]];

            output[index.Length] = str[pos..];
            return output;
        }

        public static string[] SplitIfNotPrecededByChar(this string str, string splitPattern, char precedingChar)
        {
            if (!str.Valid())
                throw new ArgumentException("Input string in wrong format!");

            var result = Array.Empty<string>();

            foreach (var index in str.AllIndicesOf(splitPattern))
            {
                if (str.ToCharArray()[index - 1] == precedingChar)
                    continue;

                result = str.SplitAt(index);
            }

            return result;
        }

        public static IEnumerable<int> AllIndicesOf(this string str, string pattern)
        {
            if (!str.Valid())
                throw new ArgumentException("Input string in wrong format!");

            //Knuth–Morris–Pratt algorithm
            var M = pattern.Length;
            var N = str.Length;
            var lps = pattern.GetLongestPrefixSuffix();
            int i = 0, j = 0;

            while (i < N)
            {
                if (pattern[j] == str[i])
                {
                    j++;
                    i++;
                }
                if (j == M)
                {
                    yield return i - j;
                    j = lps[j - 1];
                }
                else if (i < N && pattern[j] != str[i])
                {
                    if (j != 0)
                    {
                        j = lps[j - 1];
                    }
                    else
                    {
                        i++;
                    }
                }
            }
        }

        public static int[] GetLongestPrefixSuffix(this string str)
        {
            var lps = new int[str.Length];
            var length = 0;
            var i = 1;

            while (i < str.Length)
            {
                if (str[i] == str[length])
                {
                    length++;
                    lps[i] = length;
                    i++;
                }
                else
                {
                    if (length != 0)
                    {
                        length = lps[length - 1];
                    }
                    else
                    {
                        lps[i] = length;
                        i++;
                    }
                }
            }

            return lps;
        }

        public static byte[] ToByteArray(this string str, Encoding encoding)
        {
            if (!str.Valid())
                throw new ArgumentException("Input string in wrong format!");

            return encoding.GetBytes(str);
        }

        public static byte[] EncodeToBytes(this string str)
        {
            if (!str.Valid())
                throw new ArgumentException("Input string in wrong format!");

            var bytes = new byte[str.Length * sizeof(char)];

            try
            {
                Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to copy chars to bytes!", ex);
            }

            return bytes;
        }

        public static byte[] DecodeToBytes(this string str)
        {
            if (!str.Valid())
                throw new ArgumentException("Input string in wrong format!");

            var even = str[0] == '0';
            var bytes = new byte[(str.Length - 1) * sizeof(char) + (even ? 0 : -1)];
            var chars = str.ToCharArray();

            try
            {
                Buffer.BlockCopy(chars, 2, bytes, 0, bytes.Length);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to copy chars to bytes!", ex);
            }

            return bytes;
        }

        public static T GetValueFromEnumDescription<T>(this string str)
            where T : System.Enum
        {
            if (!str.Valid())
                throw new ArgumentException("Input string in wrong format!");

            foreach (var field in typeof(T).GetFields())
            {
                if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute))
                    is DescriptionAttribute attribute)
                {
                    if (attribute.Description == str)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == str)
                        return (T)field.GetValue(null);
                }
            }

            throw new ArgumentException("Not found.", nameof(str));
            //return default;
        }

        public static T To<T>(this string str)  //TODO: unsigned types, more checks
        {
            if (!str.Valid())
                throw new ArgumentException("Input string in wrong format!");

            switch (typeof(T))
            {
                case { } sbyteType when sbyteType == typeof(sbyte): // int8
                    {
                        if (!str.IsDigitsOnly() && !str.Contains("-"))
                            throw new ArgumentException("Input string in wrong format, non-digit char other then '-' present.");
                        if (str.ToCharArray().Length > 3)
                            throw new ArgumentException("Input string in wrong format, too many digits maximum for int8 is 3.");

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

        public static T ToEnum<T>(this string str, bool ignoreCase = true) 
            where T : struct
        {
            if (!str.Valid())
                throw new ArgumentException("Input string in wrong format!");

            return System.Enum.TryParse<T>(str, ignoreCase, out var result) 
                ? result : default;
        }
    }
}