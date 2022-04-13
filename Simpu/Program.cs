using Simpu.Compiler;
using Simpu.Parser;
using Simpu.Simulation;
using Simpu.Tokenizer;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace simpu
{

    public class Program
    {

        public static void Main(string[] args)
        {
            var kernel = File.ReadAllText("kernel.c").Replace(Environment.NewLine, "\n");

            // tokenize kernel
            var token = FileParser.Parse(kernel);

            // compile tokens
            var obj = ObjectFile.Compile(token);

            obj.PrintOpCodes();


            //using var memory = new MemoryStream(new byte[10000000]);
            //exe.Write(memory);

            //// write compiled kernel to memory

            //var core = new Core(memory.ToArray(), 0);
            //core.Run();
        }
    }
}
