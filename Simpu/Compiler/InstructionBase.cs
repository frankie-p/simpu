using System.IO;
namespace Simpu.Compiler
{

    public abstract class InstructionBase
    {

        public InstructionBase(ObjectFile obj, Instructions instruction)
        {
            Obj = obj;
            Instruction = instruction;
        }

        public ObjectFile Obj { get; }

        public Instructions Instruction { get; }

        public virtual void Write(Stream s, SymbolTable symbols)
        {
            s.WriteByte((byte)Instruction);
        }

        protected void WritePad(Stream s)
        {
            s.WriteByte(0);
        }

        protected void WriteRegister(Stream s, Registers register)
        {
            s.WriteByte((byte)register);
        }

        protected void WriteAddressPlaceholder(Stream s, SymbolTable symbols, string label, SymbolTypes type)
        {
            symbols.Reference(label, (int)s.Position, type);
            s.WriteByte(0);
            s.WriteByte(0);
        }
    }
}
