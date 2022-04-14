using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simpu.Compiler
{

    public class JumpRelativeAddress : InstructionBase
    {

        public JumpRelativeAddress(ObjectFile obj, string label)
            : base(obj, Instructions.JUMP_RELATIVE_BACK)
        {
            Label = label;
        }

        public string Label { get; }

        public override void Write(Stream s, SymbolTable symbols)
        {
            base.Write(s, symbols);
            base.WritePad(s);
            base.WriteAddressPlaceholder(s, symbols, Label, SymbolTypes.Relative);
        }
    }
}
