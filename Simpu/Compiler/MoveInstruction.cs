using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simpu.Compiler
{

    public abstract class MoveInstruction : InstructionBase
    {

        public MoveInstruction(ObjectFile obj, string moveAddress, Instructions instruction)
            : base(obj, instruction)
        {
            Label = moveAddress;
        }

        public string Label { get; }
    }
}
