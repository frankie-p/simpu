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

        public static bool TryParse(string input, ref int index, out MethodToken token)
        {
            var copy = index;

            token = new MethodToken();

            Trim(input, ref copy);

            if (TryString(input, ref copy, "inline"))
            {
                token.Inline = true;
                Trim(input, ref copy);
            }

            if (!VariableParser.TryParse(input, ref copy, out var returnToken))
                return false;

            Trim(input, ref copy);

            if (!VariableParser.TryParse(input, ref copy, out var nameToken))
                return false;

            Trim(input, ref copy);

            if (!ParameterParser.TryParse(input, ref copy, true, out var parameterTokens))
                return false;

            if (!BlockParser.TryParse(input, ref copy, out var blockToken))
                return false;

            token.Name = nameToken.Name;
            token.ReturnType = returnToken.Name;
            token.Parameters = parameterTokens.Cast<DefinitionToken>().ToList();
            token.Block = blockToken;

            index = copy;
            return true;
        }
    }
}
