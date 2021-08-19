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
            var offset = Executable.GetAddressOffset(this, m_label);

            s.WriteByte(0x01);
            s.Write(BitConverter.GetBytes(offset), 0, 4);
        }

        public override string ToString()
        {
            string absolute = "<NUL>";
            string relative = "<NUL>";

            if (Executable.TryGetAddressOfLabel(m_label, out var address))
            {
                absolute = $"0x{address:X4}";
                relative = Executable.GetAddressOffset(this, m_label).ToString();
            }

            return $"JMP {absolute} # {m_label} (relative: {relative})";
        }
    }
}
