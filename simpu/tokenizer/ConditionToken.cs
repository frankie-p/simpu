using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simpu.tokenizer
{

    public class ConditionToken : TokenBase
    {

        public ValueToken Left { get; set; }

        public ValueToken Right { get; set; }

        public Conditions Condition { get; set; }
    }
}
