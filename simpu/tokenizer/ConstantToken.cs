using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simpu.tokenizer
{

    public class ConstantToken : TokenBase
    {

        public string String { get; set; }

        public int? Integer { get; set; }

        public float? Float { get; set; }

        public bool IsString => String != null;

        public bool IsFloat => Float != null;
    }
}
