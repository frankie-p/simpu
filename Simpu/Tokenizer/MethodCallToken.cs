using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simpu.Tokenizer
{

    public class MethodCallToken : TokenBase
    {

        public string Name { get; set; }

        public List<ValueToken> Parameter { get; set; }

        public string Caller { get; set; }
    }
}
