using simpu.tokenizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simpu.parser
{

    public class WhileParser : ParserBase
    {

        public static bool TryParse(string input, ref int index, out WhileToken token)
        {
            token = null;
            var copy = index;

            Trim(input, ref copy);

            if (!TryString(input, ref copy, "while"))
                return false;

            Trim(input, ref copy);

            if (!TryChar(input, ref copy, '('))
                return false;

            Trim(input, ref copy);

            if (!TryParseWhileCondition(input, ref copy, out var conditionToken))
                return false;

            Trim(input, ref copy);

            if (!TryChar(input, ref copy, ')'))
                return false;

            Trim(input, ref copy);

            if (TryChar(input, ref copy, ';'))
            {
                token = new WhileToken
                {
                    Condition = conditionToken
                };

                index = copy;
                return true;
            }

            if (BlockParser.TryParse(input, ref copy, out var blockToken))
            {
                token = new WhileToken
                {
                    Condition = conditionToken,
                    Block = blockToken
                };

                index = copy;
                return true;
            }

            return false;
        }

        private static bool TryParseWhileCondition(string input, ref int index, out TokenBase token)
        {
            if (ValueParser.TryParse(input, ref index, out var valueToken))
            {
                token = valueToken;
                return true;
            }

            token = null;
            return false;
        }
    }
}
