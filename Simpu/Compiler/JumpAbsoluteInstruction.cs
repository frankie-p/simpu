using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simpu.Compiler
{

    public class JumpAbsoluteInstruction : Instruction
    {
        private readonly string m_label;

        public JumpAbsoluteInstruction(ObjectFile obj, string label)
            : base(obj)
        {
            m_label = label;
        }

        public override int Size => 5;

        public override void Write(Stream s)
        {
            var offset = Obj.GetAddressOffset(this, m_label);

            s.WriteByte(0x01);
            s.Write(BitConverter.GetBytes(offset), 0, 4);
        }

        public override string ToOpCode()
        {
            //string absolute = "<NUL>";

            //if (Obj.TryGetAddressOfLabel(m_label, out var address))
            //{
            //    absolute = $"0x{address:X4}";
            //}

            return $"JMP {m_label}";
        }
    }
}
