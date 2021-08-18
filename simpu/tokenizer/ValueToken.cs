using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simpu.tokenizer
{

    public class ValueToken : TokenBase
    {

        public ConstantToken Constant { get; set; }

        public VariableToken Variable { get; set; }

        public MethodCallToken MethodCall { get; set; }

        public bool IsConstant => Constant != null;

        public bool IsVariable => Variable != null;

        public bool IsMethodCall => MethodCall != null;
    }
}
