using System.IO;

namespace Simpu.Compiler
{
    public class MoveAddressToRegisterInstruction : MoveInstruction
    {

        public MoveAddressToRegisterInstruction(ObjectFile obj, string label, Registers register)
            : base(obj, label, Instructions.MOVE_ADDRESS_REG)
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
