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

        public static bool TryParse(string input, ref int index, bool isDefinition, out List<TokenBase> tokens)
        {
            tokens = null;
            var copy = index;

            if (!TryChar(input, ref copy, '('))
                return false;

            tokens = new List<TokenBase>();

            Trim(input, ref copy);

            if (CheckClosingBracket(input, ref index, copy, ref tokens))
                return true;

            while (true)
            {
                Trim(input, ref copy);
                
                if (isDefinition)
                {
                    if (!DefinitionParser.TryParse(input, ref copy, false, out var definitionToken))
                        throw new Exception($"Excpected parameter but got '{input.Substring(copy)}'");

                    if (definitionToken.InitialValue != null)
                        throw new Exception("Definition initial value not allowed");

                    tokens.Add(definitionToken);
                }
                else
                {
                    if (!ValueParser.TryParse(input, ref copy, out var valueToken))
                        throw new Exception($"Excpected parameter but got '{input.Substring(copy)}'");

                    tokens.Add(valueToken);
                }

                Trim(input, ref copy);

                if (CheckClosingBracket(input, ref index, copy, ref tokens))
                    return true;

                Trim(input, ref copy);

                if (TryChar(input, ref copy, ','))
                    continue;

                throw new Exception($"Excpected parameter but got '{new string(input.Substring(copy).ToArray())}'");
            }
        }

        private static bool CheckClosingBracket(string input, ref int index, int copy, ref List<TokenBase> tokens)
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
