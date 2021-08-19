using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simpu.Tokenizer
{

    public class IfToken : TokenBase
    {

        public IfBlockToken If { get; set; }

        public List<IfBlockToken> ElseIfs { get; set; }

        public BlockToken Else { get; set; }
    }
}
