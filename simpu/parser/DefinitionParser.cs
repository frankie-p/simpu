using simpu.tokenizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simpu.parser
{

    public class DefinitionParser : ParserBase
    {

        public static bool TryParse(string input, ref int index, out DefinitionToken token)
        {
            token = null;
            var copy = index;

            ValueToken initToken = null;

            Trim(input, ref copy);

            if (!ValueParser.TryParse(input, ref copy, out var typeToken))
                return false;

            Trim(input, ref copy);

            if (!ValueParser.TryParse(input, ref copy, out var nameToken))
                return false;

            Trim(input, ref copy);

            if (TryChar(input, ref copy, '='))
            {
                Trim(input, ref copy);

                if (!ValueParser.TryParse(input, ref copy, out initToken))
                    return false;

                Trim(input, ref copy);
            }

            if (TryChar(input, ref copy, ';'))
            {
                token = new DefinitionToken
                {
                    Type = typeToken.Value,
                    Name = nameToken.Value,
                    InitialValue = initToken
                };

                index = copy;
                return true;
            }

            return false;
        }
    }
}
