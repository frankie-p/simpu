using simpu.tokenizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simpu.parser
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
                }
                else if (MethodCallParser.TryParse(input, ref copy, out var methodCallToken))
                {
                    tokens.Add(methodCallToken);
                }
                else
                {
                    throw new Exception();
                }

                if (!ParseSemikolon(input, ref copy))
                    throw new Exception("Expected semikolon");

                while (ParseSemikolon(input, ref copy));
            };
        }

        private static bool ParseSemikolon(string input, ref int index)
        {
            Trim(input, ref index);

            return TryChar(input, ref index, ';');
        }
    }
}
