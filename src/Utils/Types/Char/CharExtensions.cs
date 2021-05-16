using System.Globalization;

namespace Utils.Types.Char
{
    public static class CharExtensions
    {
        public static bool IsDigitNumber(this char ch) // assumes a unicode char
        {
            return CharUnicodeInfo.GetUnicodeCategory(ch) == UnicodeCategory.DecimalDigitNumber;
        }
    }
}