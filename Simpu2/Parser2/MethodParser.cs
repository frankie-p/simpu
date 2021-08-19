using Simpu.Tokenizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simpu.Parser
{

    public class MethodParser : ParserBase
    {


        public bool TryParse(string input, ref int index, out MethodToken token)
        {
            token = null;
            var copy = index;

            Trim(input, ref copy);

            if (!VariableParser.TryParse(input, ref copy, out var returnToken))
                return false;

            Trim(input, ref copy);

            if (!VariableParser.TryParse(input, ref copy, out var nameToken))
                return false;

            Trim(input, ref copy);

            if (!ParameterParser.TryParse(input, ref copy, out var parameterTokens))
                return false;

            if (!BlockParser.TryParse(input, ref copy, out var blockToken))
                return false;            

            token = new MethodToken
            {
                Name = nameToken.Name,
                ReturnType = returnToken.Name,
                Parameters = parameterTokens,
                Block = blockToken
            };

            index = copy;
            return true;
        }
    }
}
