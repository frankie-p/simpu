using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simpu.Tokenizer
{

    public abstract class TokenBase
    {

        public bool RequiresStack { get; private set; }

        public int UsedRegisters { get; private set; }

        public int TotalUsedRegisters { get; private set; }
    }
}
