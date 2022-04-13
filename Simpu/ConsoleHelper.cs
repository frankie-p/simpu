using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simpu
{

    public static class ConsoleHelper
    {

        public static void WriteLineColored(string value, ConsoleColor foreground)
        {
            var restore = Console.ForegroundColor;
            Console.ForegroundColor = foreground;
            Console.WriteLine(value);
            Console.ForegroundColor = restore;
        }
    }
}
