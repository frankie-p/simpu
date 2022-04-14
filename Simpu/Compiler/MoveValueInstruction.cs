using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simpu.Compiler
{

    public class MoveValueInstruction : MoveInstruction
    {

        public MoveValueInstruction(ObjectFile obj, string label, byte value)
            : base(obj, label, Instructions.MOVE_ADDRESS_VALUE)
        {
            Value = value;
        }

        public byte Value { get; }

        public override void Write(Stream s, SymbolTable symbols)
        {
            base.Write(s, symbols);
            s.WriteByte(Value);
            base.WriteAddressPlaceholder(s, symbols, Label, SymbolTypes.Absolute);
        }
    }
}
