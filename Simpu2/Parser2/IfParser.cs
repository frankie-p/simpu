using Simpu.Tokenizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simpu.Parser
{

    public class IfParser : ParserBase
    {

        public static bool TryParse(string input, ref int index, out IfToken token)
        {
            token = null;
            var copy = index;

            if (!TryParseIf(input, ref copy, "if", out var ifToken))
                return false;

            var elseIfTokens = new List<IfBlockToken>();

            while (TryParseIf(input, ref copy, "else if", out var elseIfToken))
            {
                elseIfTokens.Add(elseIfToken);
            }

            Trim(input, ref copy);

            if (!BlockParser.TryParse(input, ref copy, out var elseToken))
            {
                elseToken = null;
            }

            token = new IfToken
            {
                If = ifToken,
                ElseIfs = elseIfTokens.Any() ? elseIfTokens : null,
                Else = elseToken
            };

            index = copy;
            return true;
        }


        private static bool TryParseIf(string input, ref int index, string key, out IfBlockToken token)
        {
            token = null;
            var copy = index;

            Trim(input, ref copy);

            if (!TryString(input, ref copy, key))
                return false;

            Trim(input, ref copy);

            if (!TryChar(input, ref copy, '('))
                return false;

            Trim(input, ref copy);

            if (!ValueParser.TryParse(input, ref copy, out var condition))
                return false;

            Trim(input, ref copy);

            if (!TryChar(input, ref copy, ')'))
                return false;

            Trim(input, ref copy);

            if (!BlockParser.TryParse(input, ref copy, out var block))
                return false;

            token = new IfBlockToken
            {
                Condition = condition,
                Block = block,
            };

            index = copy;
            return true;
        }
    }
}
