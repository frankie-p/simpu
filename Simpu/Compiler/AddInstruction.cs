using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simpu.Compiler
{

    public class AddInstruction : InstructionBase
    {

        public AddInstruction(ObjectFile obj, Registers register, ushort value)
            : base(obj, Instructions.ARITHMETIC_ADD)
        {
            Register = register;
            Value = value;
        }

        public Registers Register { get; }

        public ushort Value { get; }

        public override void Write(Stream s, SymbolTable symbols)
        {
            base.Write(s, symbols);
            base.WriteRegister(s, Register);
            s.Write(BitConverter.GetBytes(Value), 0, 2);
        }
    }
}
