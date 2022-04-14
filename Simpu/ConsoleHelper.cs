using System;

namespace Simpu
{

    public static class ConsoleHelper
    {

        public static void WriteLineColored(string value, ConsoleColor foreground)
        {
            WriteColored(value, foreground);
            Console.WriteLine();
        }

        public static void WriteColored(string value, ConsoleColor foreground)
        {
            var restore = Console.ForegroundColor;
            Console.ForegroundColor = foreground;
            Console.Write(value);
            Console.ForegroundColor = restore;
        }
    }
}
