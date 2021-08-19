using Simpu.Tokenizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simpu.Parser
{

    public class MethodCallParser : ParserBase
    {

        public static bool TryParse(string input, ref int index, bool semikolonRequired, out MethodCallToken token)
        {
            token = null;
            var copy = index;

            if (!VariableParser.TryParse(input, ref copy, out var nameToken))
                return false;

            Trim(input, ref copy);

            if (!ParameterParser.TryParse(input, ref copy, out var parameterTokens))
                return false;

            if (semikolonRequired)
            {
                if (!TryTrimSemikolons(input, ref copy))
                    return false;
            }

            token = new MethodCallToken
            {
                Name = nameToken.Name,
                Parameter = parameterTokens
            };

            index = copy;
            return true;
        }
    }
}
