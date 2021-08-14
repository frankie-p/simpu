﻿using simpu.parser;
using simpu.simulation;
using simpu.tokenizer;
using System;
using System.IO;

namespace simpu
{

    public class Program
    {

        public static void Main(string[] args)
        {
            var file = File.ReadAllText(@"d:\hw.c").Replace(Environment.NewLine, "\n");
            var index = 0;

            // tokenize kernel here
            var parser = new MethodParser();
            parser.TryParse(file, ref index, out var token);

            // create memory
            var memory = new byte[10000000];

            // write compiled kernel to memory

            var core = new Core(memory, 0);
            core.Run();
        }
    }
}
