using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simpu.Backend
{

    public class LabelInstruction : Instruction
    {

        public LabelInstruction(Executable executable, string name)
            : base(executable)
        {
            Name = name;
        }

        public string Name { get; }

        public override int Size => 0;

        public override void Write(Stream s)
        {

        }

        public override string ToString()
        {
            return $"{Name}:";
        }
    }
}
