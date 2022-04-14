﻿using System.IO;

namespace Simpu.Compiler
{

    public class NopInstruction : Instruction
    {

        public NopInstruction(ObjectFile obj)
            : base(obj)
        {

        }

        public override int Size => 1;

        public override void Write(Stream s, SymbolTable symbols)
        {
            s.WriteByte(0x00);
        }
    }
}
