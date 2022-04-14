using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simpu.Compiler
{

    public class JumpRelativeAddress : Instruction
    {

        public JumpRelativeAddress(ObjectFile obj, string label)
            : base(obj)
        {
            Label = label;
        }

        public string Label { get; }

        public override int Size => 5;

        public override void Write(Stream s, SymbolTable symbols)
        {
            s.WriteByte(0x02);
            symbols.Reference(Label, (int)s.Position, SymbolTypes.Relative);
            s.Write(BitConverter.GetBytes(0), 0, 4);
        }
    }
}
