using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simpu.Backend
{

    public class NopInstruction : Instruction
    {

        public NopInstruction(Executable executable)
            : base(executable)
        {

        }

        public override int Size => 1;

        public override void Write(Stream s)
        {
            s.WriteByte(0x00);
        }

        public override string ToString()
        {
            return "NOP";
        }
    }
}
