using Spectre.Console;
using Utils.Enumeration;
using Utils.String;
using Utils.Types.Enumeration;

namespace Utils.TestConsole
{
    class Program
    {
        public enum Lang
        {
            En = 0,
            De = 1,
            Fr = 2,
            Ru = 3,
        }

        static void Main(string[] args)
        {
            const string digitStr = "316574541894516";
            const string digitStrfalse = "31s65745g41894s516";

            Console.ConsoleExtensions.Log($"digitonly? {Console.ConsoleExtensions.YesNo(digitStr.IsDigitOnly())}", "info", false);
            Console.ConsoleExtensions.Log($"digitonlyFalse? {Console.ConsoleExtensions.YesNo(digitStrfalse.IsDigitOnly())}", "info", false);

            AnsiConsole.Render(Console.ConsoleExtensions.DirectorySummary("C:\\Games\\RimWorld"));

            var choices = new[] { "qwerty", "fooobaar", "123467", "@^@#^&" };

            foreach (var (choice, index) in choices.WithIndex())
                Console.ConsoleExtensions.Log($"Choice: {choice} Index: {index}", "info");

            AnsiConsole.Prompt(Console.ConsoleExtensions.GenerateChoiceMenu(choices));

            var numbers = "12345";
            var numbers2 = "13264.97";
            var intNumb = numbers.To<int>();
            var longNum = numbers.To<long>();
            var deci = numbers2.To<decimal>();
            var floatNum = numbers2.To<float>();

            Console.ConsoleExtensions.Log($"Type: {numbers.GetType()} Value: {numbers}", "info");
            Console.ConsoleExtensions.Log($"Type: {intNumb.GetType()} Value: {intNumb}", "info");
            Console.ConsoleExtensions.Log($"Type: {longNum.GetType()} Value: {longNum}", "info");
            Console.ConsoleExtensions.Log($"Type: {deci.GetType()} Value: {deci}", "info");
            Console.ConsoleExtensions.Log($"Type: {floatNum.GetType()} Value: {floatNum}", "info");

            var lang = Lang.Fr;
            int langInt = lang.ToInt();
            Console.ConsoleExtensions.Log($"langInt: {langInt}", "info");
            int enumCount = lang.CountMembers();
            Console.ConsoleExtensions.Log($"enumCount: {enumCount}", "info");
            var enumDesc = lang.GetDescription();
            Console.ConsoleExtensions.Log($"enumDesc: {enumDesc}", "info");
        }
    }
}
