using Simpu.Tokenizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simpu.Parser
{

    public class BlockParser : ParserBase
    {

        public static bool TryParse(string input, ref int index, out BlockToken token)
        {
            token = null;
            var copy = index;

            var tokens = new List<TokenBase>();

            Trim(input, ref copy);

            if (!TryChar(input, ref copy, '{'))
                return false;

            while (true)
            {
                var semikolonRequired = false;

                Trim(input, ref copy);

                if (TryChar(input, ref copy, '}'))
                {
                    token = new BlockToken
                    {
                        Tokens = tokens
                    };

                    index = copy;
                    return true;
                }

                if (WhileParser.TryParse(input, ref copy, out var whileToken))
                {
                    tokens.Add(whileToken);
                }
                else if (DefinitionParser.TryParse(input, ref copy, out var definitionToken))
                {
                    tokens.Add(definitionToken);
                    semikolonRequired = true;
                }
                else if (MethodCallParser.TryParse(input, ref copy, out var methodCallToken))
                {
                    tokens.Add(methodCallToken);
                    semikolonRequired = true;
                }
                else
                {
                    throw new Exception();
                }

                TrimSemikolons(input, ref copy, semikolonRequired);
            };
        }

        private static void TrimSemikolons(string input, ref int index, bool required)
        {
            Trim(input, ref index);

            if (!TryChar(input, ref index, ';'))
            {
                if (required)
                    throw new Exception("Expected semikolon");
            }

            do
            {
                Trim(input, ref index);
            }
            while (TryChar(input, ref index, ';'));
        }
    }
}
