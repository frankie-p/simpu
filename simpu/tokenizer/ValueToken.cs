using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simpu.tokenizer
{

    public class ValueToken : TokenBase
    {

        public string Value { get; set; }

        public bool IsNumber => int.TryParse(Value, out var _);
    }
}
