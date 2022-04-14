using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simpu.Compiler
{

    public class JumpAbsoluteInstruction : Instruction
    {

        public JumpAbsoluteInstruction(ObjectFile obj, string label)
            : base(obj)
        {
            Label = label;
        }

        public string Label { get; }

        public override int Size => 5;

        public override void Write(Stream s, SymbolTable symbols)
        {
            s.WriteByte(0x01);
            symbols.Reference(Label, (int)s.Position);
            s.Write(BitConverter.GetBytes(0), 0, 4);
        }

        public override string ToOpCode()
        {
            return $"JMP {Label}";
        }
    }
}
