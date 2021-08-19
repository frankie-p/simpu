using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simpu.Backend
{

    public abstract class Instruction
    {

        public Instruction(Executable executable)
        {
            Executable = executable;
        }

        public Executable Executable { get; }

        public int Address { get; set; }

        public abstract int Size { get; }

        public abstract void Write(Stream s);
    }
}
