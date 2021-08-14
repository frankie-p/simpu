using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simpu.tokenizer
{

    public class MethodToken
    {

        public string Name { get; set; }

        public string ReturnType { get; set; }

        public List<ParameterToken> Parameters { get; set; }

        public BlockToken Block { get; set; }
    }
}
