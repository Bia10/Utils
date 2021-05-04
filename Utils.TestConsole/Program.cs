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
        }
    }
}
