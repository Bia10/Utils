using System;
using System.Collections.Generic;
using Spectre.Console;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
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

            Console.ConsoleExtensions.Log($"digitonly? {Console.ConsoleExtensions.YesNo(digitStr.IsDigitsOnly())}", "info");
            Console.ConsoleExtensions.Log($"digitonlyFalse? {Console.ConsoleExtensions.YesNo(digitStrfalse.IsDigitsOnly())}", "info");

            AnsiConsole.Render(Console.ConsoleExtensions.DirectorySummary("C:\\Games\\RimWorld"));

            var choices = new[] { "qwerty", "fooobaar", "123467", "@^@#^&" };

            foreach (var (choice, index) in choices.WithIndex())
                Console.ConsoleExtensions.Log($"Choice: {choice} Index: {index}", "info");

            AnsiConsole.Prompt(Console.ConsoleExtensions.GenerateChoiceMenu(choices));

            const string numbers = "12345";
            const string numbers2 = "13264.97";
            var intNumb = numbers.To<int>();
            var longNumb = numbers.To<long>();
            var deciNumb = numbers2.To<decimal>();
            var floatNum = numbers2.To<float>();

            Console.ConsoleExtensions.Log($"Type: {numbers.GetType()} Value: {numbers}", "info");
            Console.ConsoleExtensions.Log($"Type: {intNumb.GetType()} Value: {intNumb}", "info");
            Console.ConsoleExtensions.Log($"Type: {longNumb.GetType()} Value: {longNumb}", "info");
            Console.ConsoleExtensions.Log($"Type: {deciNumb.GetType()} Value: {deciNumb}", "info");
            Console.ConsoleExtensions.Log($"Type: {floatNum.GetType()} Value: {floatNum}", "info");

            const Lang lang = Lang.Fr;
            var langInt = lang.ToInt();
            Console.ConsoleExtensions.Log($"langInt: {langInt}", "info");
            var enumCount = lang.CountMembers();
            Console.ConsoleExtensions.Log($"enumCount: {enumCount}", "info");
            var enumDesc = lang.GetDescription();
            Console.ConsoleExtensions.Log($"enumDesc: {enumDesc}", "info");

            const string endsWithDigits = "b54891stastassgfdahblab545lafgijfsdgisdogi ... 12 ... 1234 dfhgsdffhgsdf 14 56";
            var endsDigits = endsWithDigits.EndsWithDigits(4);
            Console.ConsoleExtensions.Log($"endsDigits: {Console.ConsoleExtensions.YesNo(endsDigits)}", "info");
            var endDigitStr = endsWithDigits.GetDigitsFromEnd(4);
            Console.ConsoleExtensions.Log($"endDigitStr: {endDigitStr}", "info");
            Console.ConsoleExtensions.Log($"head: {endsWithDigits.Head(5)} tail: {endsWithDigits.Tail(5)}", "info");

            const string rn = "blblbl" + "\r\n" + "56748946";
            Console.ConsoleExtensions.Log($"rn: {rn}", "info");
            Console.ConsoleExtensions.Log($"rn.RemoveLineBreaks(): {rn.RemoveLineBreaks()}", "info");

            const string firsToUpper = "red green";
            Console.ConsoleExtensions.Log($"firsToUpper: {firsToUpper}", "info");
            Console.ConsoleExtensions.Log($"firsToUpper.FirstCharToUpper(): {firsToUpper.FirstCharToUpper()}", "info");

            const string url = "https://www.youtube.com/";
            Console.ConsoleExtensions.Log($"url: {url.ReplaceForbiddenFilenameChars("_")}", "info");

            var x = "fooo".GetValueFromEnumDescription<describedEnum>();
            Console.ConsoleExtensions.Log($"enum value: {x}", "info");

            var indicesSTr = "ZArray<ZPair<long,long> > aPotionDiscountRate;";
            const string pattern = " >";
            List<int> indices = new();
            indices.AddRange(indicesSTr.AllIndicesOf(pattern));

            foreach (var n in indices)
            {
                Console.ConsoleExtensions.Log($"Pattern: {pattern} found at: {n}", "info");
                indicesSTr = indicesSTr.ReplaceAt(n, 1);
            }

            Console.ConsoleExtensions.Log($"Replaced: {indicesSTr}", "info");

            const string testStr1 = "short nir";
            const string testStr2 = "short int";
            const string testStr3 = "short n";
            var testList1 = new List<string>() { testStr1 };
            var testList2 = new List<string>() { testStr2 };

            Console.ConsoleExtensions.Log($"CharHashEqual: {testStr1.CharHashEqualTo(testStr2)}", "info");
            Console.ConsoleExtensions.Log($"StrHashEqual: {testList1.StrHashEqualTo(testList2)}", "info");
            Console.ConsoleExtensions.Log($"StringsEqual: {testStr1.StringEqualTo(testStr2)}", "info");
            Console.ConsoleExtensions.Log($"StrictlyCharEqual: {testStr1.StrictlyCharEqualTo(testStr2)}", "info");
            Console.ConsoleExtensions.Log($"HeadSizeOfStrB: {testStr1.HeadSizeOfStrB(testStr3)}", "info");

            const string testStr4 = "ZArray<ZPair<long, long>> aPotionDiscountRate;";
            var split = testStr4.SplitIfNotPrecededByChar(" ", ',');
            Console.ConsoleExtensions.Log($"Split0 {split[0]} Split1 {split[1]}", "info");
        }
    }
}
