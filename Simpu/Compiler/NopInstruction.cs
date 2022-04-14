using System.IO;

namespace Simpu.Compiler
{

    public class NopInstruction : InstructionBase
    {

        public NopInstruction(ObjectFile obj)
            : base(obj, Instructions.NOP)
        {

        }

        public override void Write(Stream s, SymbolTable symbols)
        {
            base.Write(s, symbols);

            base.WritePad(s);
            base.WritePad(s);
            base.WritePad(s);
        }
    }
}
