using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simpu.Tokenizer
{

    public class IfBlockToken : TokenBase
    {

        public ValueToken Condition { get; set; }

        public BlockToken Block { get; set; }
    }
}
