using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simpu.Compiler
{

    public abstract class MoveInstruction : Instruction
    {

        public MoveInstruction(ObjectFile obj, string moveAddress)
            : base(obj)
        {
            MoveAddress = moveAddress;
        }

        public string MoveAddress { get; }
    }
}
