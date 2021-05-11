using Spectre.Console;
using Utils.String;

namespace Utils.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            const string digitStr = "316574541894516";
            const string digitStrfalse = "31s65745g41894s516";

            Console.Extensions.Log($"digitonly? {Console.Extensions.YesNo(digitStr.IsDigitOnly())}", "info", false);
            Console.Extensions.Log($"digitonlyFalse? {Console.Extensions.YesNo(digitStrfalse.IsDigitOnly())}", "info", false);

            AnsiConsole.Render(Console.Extensions.DirectorySummary("C:\\Games\\RimWorld"));

            var choices = new[] {"qwerty", "fooobaar", "123467", "@^@#^&"};
            AnsiConsole.Prompt(Console.Extensions.GenerateChoiceMenu(choices));

            var numbers = "12345";
            var numbers2 = "13264.97";
            var intNumb = numbers.To<int>();
            var longNum = numbers.To<long>();
            var deci = numbers2.To<decimal>();
            var floatNum = numbers2.To<float>();

            Console.Extensions.Log($"Type: {numbers.GetType()} Value: {numbers}", "info");
            Console.Extensions.Log($"Type: {intNumb.GetType()} Value: {intNumb}", "info");
            Console.Extensions.Log($"Type: {longNum.GetType()} Value: {longNum}", "info");
            Console.Extensions.Log($"Type: {deci.GetType()} Value: {deci}", "info");
            Console.Extensions.Log($"Type: {floatNum.GetType()} Value: {floatNum}", "info");
        }
    }
}
