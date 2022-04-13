using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simpu.Compiler
{

    public class LabelInstruction : Instruction
    {

        public LabelInstruction(ObjectFile obj, string name)
            : base(obj)
        {
            Name = name;
        }

        public string Name { get; }

        public override int Size => 0;

        public override void Write(Stream s)
        {

        }

        public override string ToOpCode()
        {
            return $"{Name}:";
        }
    }
}
