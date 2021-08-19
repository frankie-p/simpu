using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simpu.Tokenizer
{

    public class FileToken : TokenBase
    {

        public List<DefinitionToken> Definitions { get; set; }

        public List<MethodToken> Methods { get; set; }
    }
}
