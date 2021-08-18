using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simpu.tokenizer
{

    public class DefinitionToken : TokenBase
    {

        public string Type { get; set; }

        public string Name { get; set; }

        public ValueToken InitialValue { get; set; }
    }
}
