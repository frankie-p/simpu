using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simpu.Compiler
{

    public class LabelInstruction : InstructionBase
    {

        public LabelInstruction(ObjectFile obj, string name)
            : base(obj, 0)
        {
            Name = name;
        }

        public string Name { get; }

        public override void Write(Stream s, SymbolTable table)
        {
            table.Entry(Name, (int)s.Position, SymbolTypes.Method);
        }
    }
}
