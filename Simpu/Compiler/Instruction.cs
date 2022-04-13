using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simpu.Compiler
{

    public abstract class Instruction
    {

        public Instruction(ObjectFile obj)
        {
            Obj = obj;
        }

        public ObjectFile Obj { get; }

        public int Address { get; set; }

        public abstract int Size { get; }

        public abstract void Write(Stream s);

        public abstract string ToOpCode();
    }
}
