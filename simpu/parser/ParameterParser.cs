using Simpu.Tokenizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simpu.Parser
{

    public class ParameterParser : ParserBase
    {

        public static bool TryParse(string input, ref int index, out List<ValueToken> tokens)
        {
            tokens = null;
            var copy = index;

            if (!TryChar(input, ref copy, '('))
                return false;

            tokens = new List<ValueToken>();

            if (CheckClosingBracket(input, ref index, copy, ref tokens))
                return true;

            while (true)
            {
                if (!ValueParser.TryParse(input, ref copy, out var parameterToken))
                    throw new Exception($"Excpected parameter but got '{input.Substring(copy).Take(10)}'");

                tokens.Add(parameterToken);

                if (CheckClosingBracket(input, ref index, copy, ref tokens))
                    return true;

                if (TryChar(input, ref copy, ','))
                    continue;

                throw new Exception($"Excpected parameter but got '{new string(input.Substring(copy).Take(10).ToArray())}'");
            }
        }

        private static bool CheckClosingBracket(string input, ref int index, int copy, ref List<ValueToken> tokens)
        {
            if (TryChar(input, ref copy, ')'))
            {
                index = copy;
                return true;
            }

            return false;
        }
    }
}
