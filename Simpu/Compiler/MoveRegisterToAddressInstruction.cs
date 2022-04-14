using System.IO;

namespace Simpu.Compiler
{
    public class MoveRegisterToAddressInstruction : MoveInstruction
    {

        public MoveRegisterToAddressInstruction(ObjectFile obj, string label, Registers register)
            : base(obj, label, Instructions.MOVE_REG_ADDRESS)
        {
            Register = register;
        }

        public Registers Register { get; }

        public override void Write(Stream s, SymbolTable symbols)
        {
            base.Write(s, symbols);
            base.WriteRegister(s, Register);
            base.WriteAddressPlaceholder(s, symbols, Label, SymbolTypes.Absolute);
        }
    }
}
