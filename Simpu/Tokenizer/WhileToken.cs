using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simpu.Tokenizer
{

    public class WhileToken : TokenBase
    {

        public TokenBase Condition { get; set; }

        public BlockToken Block { get; set; }
    }
}
