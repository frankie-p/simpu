using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simpu.Tokenizer
{

    public class MethodToken
    {

        public string Name { get; set; }

        public string ReturnType { get; set; }

        public List<ValueToken> Parameters { get; set; }

        public BlockToken Block { get; set; }
    }
}
