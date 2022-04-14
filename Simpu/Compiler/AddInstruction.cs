using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simpu.Compiler
{

    public class AddInstruction : Instruction
    {

        public AddInstruction(ObjectFile obj, Registers register, int value)
            : base(obj)
        {
            Register = register;
            Value = value;
        }

        public Registers Register { get; }

        public int Value { get; }

        public override int Size => 7;

        public override string ToOpCode()
        {
            return $"ADD %{Register},{Value}";
        }

        public override void Write(Stream s, SymbolTable symbols)
        {
            s.WriteByte(0x20);
            s.Write(BitConverter.GetBytes((short)Register), 0, 2);
            s.Write(BitConverter.GetBytes(Value), 0, 4);
            
        }
    }
}
