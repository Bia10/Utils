using Spectre.Console;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Utils.Console;
using Utils.Enumeration;
using Utils.Types.Enum;
using Utils.Types.String;

namespace Utils.TestConsole
{
    static class Program
    {
        private enum Lang
        {
            En = 0,
            De = 1,
            Fr = 2,
            Ru = 3,
        }

        private enum describedEnum
        {
            [Description("fooo")]
            fuxx = 0,
            [Description("bar")]
            bar = 1,
            [Description("fxxxx")]
            fxxxx = 2
        }

        private static void Main()
        {
            const string digitStr = "316574541894516";
            const string digitStrfalse = "31s65745g41894s516";

            ConsoleExtensions.Log($"digitonly? {ConsoleExtensions.YesNo(digitStr.IsDigitsOnly())}", "info");
            ConsoleExtensions.Log($"digitonlyFalse? {ConsoleExtensions.YesNo(digitStrfalse.IsDigitsOnly())}", "info");

            AnsiConsole.Render(ConsoleExtensions.DirectorySummary("C:\\Games\\RimWorld"));

            var choices = new[] { "qwerty", "fooobaar", "123467", "@^@#^&" };

            foreach (var (choice, index) in choices.WithIndex())
                ConsoleExtensions.Log($"Choice: {choice} Index: {index}", "info");

            AnsiConsole.Prompt(ConsoleExtensions.GenerateChoiceMenu(choices));

            const string numbers = "12345";
            const string numbers2 = "13264.97";
            var intNumb = numbers.To<int>();
            var longNumb = numbers.To<long>();
            var deciNumb = numbers2.To<decimal>();
            var floatNum = numbers2.To<float>();

            ConsoleExtensions.Log($"Type: {numbers.GetType()} Value: {numbers}", "info");
            ConsoleExtensions.Log($"Type: {intNumb.GetType()} Value: {intNumb}", "info");
            ConsoleExtensions.Log($"Type: {longNumb.GetType()} Value: {longNumb}", "info");
            ConsoleExtensions.Log($"Type: {deciNumb.GetType()} Value: {deciNumb}", "info");
            ConsoleExtensions.Log($"Type: {floatNum.GetType()} Value: {floatNum}", "info");

            const Lang lang = Lang.Fr;
            var langInt = lang.ToInt();
            ConsoleExtensions.Log($"langInt: {langInt}", "info");
            var enumCount = lang.CountMembers();
            ConsoleExtensions.Log($"enumCount: {enumCount}", "info");
            var enumDesc = lang.GetDescription();
            ConsoleExtensions.Log($"enumDesc: {enumDesc}", "info");

            const string endsWithDigits = "b54891stastassgfdahblab545lafgijfsdgisdogi ... 12 ... 1234 dfhgsdffhgsdf 14 56";
            var endsDigits = endsWithDigits.EndsWithDigits(4);
            ConsoleExtensions.Log($"endsDigits: {ConsoleExtensions.YesNo(endsDigits)}", "info");
            var endDigitStr = endsWithDigits.GetDigitsFromEnd(4);
            ConsoleExtensions.Log($"endDigitStr: {endDigitStr}", "info");
            ConsoleExtensions.Log($"head: {endsWithDigits.Head(5)} tail: {endsWithDigits.Tail(5)}", "info");

            const string rn = "blblbl" + "\r\n" + "56748946";
            ConsoleExtensions.Log($"rn: {rn}", "info");
            ConsoleExtensions.Log($"rn.RemoveLineBreaks(): {rn.RemoveLineBreaks()}", "info");

            const string firsToUpper = "red green";
            ConsoleExtensions.Log($"firsToUpper: {firsToUpper}", "info");
            ConsoleExtensions.Log($"firsToUpper.FirstCharToUpper(): {firsToUpper.FirstCharToUpper()}", "info");

            const string url = "https://www.youtube.com/";
            ConsoleExtensions.Log($"url: {url.ReplaceForbiddenFilenameChars("_")}", "info");

            var x = "fooo".GetValueFromEnumDescription<describedEnum>();
            ConsoleExtensions.Log($"enum value: {x}", "info");

            var indicesSTr = "ZArray<ZPair<long,long> > aPotionDiscountRate;";
            const string pattern = " >";
            List<int> indices = new();
            indices.AddRange(indicesSTr.AllIndicesOf(pattern));

            foreach (var n in indices)
            {
                ConsoleExtensions.Log($"Pattern: {pattern} found at: {n}", "info");
                indicesSTr = indicesSTr.ReplaceAt(n, 1);
            }

            ConsoleExtensions.Log($"Replaced: {indicesSTr}", "info");

            const string testStr1 = "short nir";
            const string testStr2 = "short int";
            const string testStr3 = "short n";
            var testList1 = new List<string> { testStr1 };
            var testList2 = new List<string> { testStr2 };

            ConsoleExtensions.Log($"CharHashEqual: {testStr1.CharHashEqualTo(testStr2)}", "info");
            ConsoleExtensions.Log($"StrHashEqual: {testList1.StrHashEqualTo(testList2)}", "info");
            ConsoleExtensions.Log($"StringsEqual: {testStr1.StringEqualTo(testStr2)}", "info");
            ConsoleExtensions.Log($"StrictlyCharEqual: {testStr1.StrictlyCharEqualTo(testStr2)}", "info");
            ConsoleExtensions.Log($"HeadSizeOfStrB: {testStr1.HeadSizeOfStrB(testStr3)}", "info");

            const string testStr4 = "ZArray<ZPair<long, long>> aPotionDiscountRate;";
            var split = testStr4.SplitIfNotPrecededByChar(" ", ',');
            ConsoleExtensions.Log($"Split0 {split[0]} Split1 {split[1]}", "info");

            const string testStr5 = "ščřěžěřžžč";
            var encoded = testStr5.EncodeToBytes();
            var decoder = testStr5.DecodeToBytes();

            var xx = string.Concat(encoded.Select(b => b.ToString("X2")));
            var nn = string.Concat(decoder.Select(b => b.ToString("X2")));

            ConsoleExtensions.Log($"encoded: {xx} decoded: {nn}", "info", true);
        }
    }
}
