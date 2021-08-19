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

                if (DefinitionParser.TryParse(input, ref copy, true, out var definitionToken))
                {
                    tokens.Add(definitionToken);
                }
                else if (IfParser.TryParse(input, ref copy, out var ifToken))
                {
                    tokens.Add(ifToken);
                }
                else if (WhileParser.TryParse(input, ref copy, out var whileToken))
                {
                    tokens.Add(whileToken);
                }
                else if (MethodCallParser.TryParse(input, ref copy, true, out var methodCallToken))
                {
                    tokens.Add(methodCallToken);
                }
                else
                {
                    throw new Exception();
                }
            };
        }
    }
}
