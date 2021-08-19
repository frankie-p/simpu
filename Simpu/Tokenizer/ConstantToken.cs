using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simpu.Tokenizer
{

    public class ConstantToken : TokenBase
    {

        public string String { get; set; }

        public int? Integer { get; set; }

        public float? Float { get; set; }

        public bool IsString => String != null;

        public bool IsInteger => Integer != null;

        public bool IsFloat => Float != null;

        public bool IsFalseCondition => (IsInteger && Integer.Value == 0) || (IsFloat && Float.Value == 0);
    }
}
