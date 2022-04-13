using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simpu.Tokenizer
{

    public class UnaryOperationToken : TokenBase
    {

        public string Name { get; set; }

        public bool IsPreIncrement { get; set; }

        public bool IsPreDecrement { get; set; }

        public bool IsPostIncrement { get; set; }

        public bool IsPostDecrement{ get; set; }
    }
}
