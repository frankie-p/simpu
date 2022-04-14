using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simpu.Compiler
{
    public class MoveRegisterToAddressInstruction : MoveInstruction
    {

        public MoveRegisterToAddressInstruction(ObjectFile obj, string moveAddress, Registers register)
            : base(obj, moveAddress)
        {
            Register = register;
        }

        public Registers Register { get; }

        public override int Size => 7;

        public override void Write(Stream s, SymbolTable symbols)
        {
            s.WriteByte(0x12);
            symbols.Reference(MoveAddress, (int)s.Position);
            s.Write(BitConverter.GetBytes(0), 0, 4);
            s.Write(BitConverter.GetBytes((short)Register), 0, 2);
        }

        public override string ToOpCode()
        {
            return $"MOV %{Register},{MoveAddress}";
        }
    }
}
