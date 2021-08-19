using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simpu.Backend
{

    public class JumpInstruction : Instruction
    {
        private readonly string m_label;

        public JumpInstruction(Executable executable, string label)
            : base(executable)
        {
            m_label = label;
        }

        public override int Size => 5;

        public override void Write(Stream s)
        {
            if (!Executable.TryGetAddressOfLabel(m_label, out var address))
                throw new Exception($"Failed to get address of {m_label}");

            s.WriteByte(0x01);
            s.Write(BitConverter.GetBytes(address), 0, 4);
        }

        public override string ToString()
        {
            var labelAddress = Executable.TryGetAddressOfLabel(m_label, out var address)
                ? $"0x{address:X4}"
                : $"<NUL>";

            return $"JMP {labelAddress} # {m_label}";
        }
    }
}
