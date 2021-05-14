using System;
using System.Linq;

namespace Utils.String
{
    public static class StringExtensions
    {
        public static bool Valid(this string str)
        {
            return !string.IsNullOrEmpty(str) && !string.IsNullOrWhiteSpace(str);
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

        public static string StringBetweenStrings(this string str, string start, string end)
        {
            if (!str.Valid())
                throw new InvalidOperationException("Input string in wrong format!");

            var indexOfStart = str.IndexOf(start, StringComparison.Ordinal) + start.Length;
            var indexOfEnd = str.IndexOf(end, indexOfStart, StringComparison.Ordinal);

            return string.IsNullOrEmpty(end) ? str[indexOfStart..] : str[indexOfStart..indexOfEnd];
        }

        public static T To<T>(this string str)  //TODO: unsigned types, more checks
        {
            if (!str.Valid())
                throw new InvalidOperationException("Input string in wrong format!");

            switch (typeof(T))
            {
                case { } sbyteType when sbyteType == typeof(sbyte): // int8
                    {
                    if (!str.IsDigitOnly() && !str.Contains("-"))
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
                        if (!str.IsDigitOnly() && !str.Contains("-"))
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
                        if (!str.IsDigitOnly() && !str.Contains("-"))
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
                        if (!str.IsDigitOnly() && !str.Contains("-"))
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
                        if (!str.IsDigitOnly() && !str.ContainsSpecialDigitChar())
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
                        if (!str.IsDigitOnly() && !str.ContainsSpecialDigitChar())
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
                        if (!str.IsDigitOnly() && !str.ContainsSpecialDigitChar())
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