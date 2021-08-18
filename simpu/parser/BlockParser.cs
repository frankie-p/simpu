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
                else
                {
                    throw new Exception();
                }
            };
        }
    }
}
