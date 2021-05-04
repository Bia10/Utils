using System;
using Utils.String;

namespace Utils.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            const string digitStr = "316574541894516";
            const string digitStrfalse = "31s65745g41894s516";

            Console.WriteLine($"digitonly? {digitStr.IsDigitOnly()}");
            Console.WriteLine($"digitonlyFalse? {digitStrfalse.IsDigitOnly()}");
        }
    }
}
