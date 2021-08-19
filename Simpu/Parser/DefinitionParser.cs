﻿using Simpu.Tokenizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simpu.Parser
{

    public class DefinitionParser : ParserBase
    {

        public static bool TryParse(string input, ref int index, out DefinitionToken token)
        {
            token = null;
            var copy = index;

            ValueToken initToken = null;

            Trim(input, ref copy);

            if (!VariableParser.TryParse(input, ref copy, out var typeToken))
                return false;

            Trim(input, ref copy);

            if (!VariableParser.TryParse(input, ref copy, out var nameToken))
                return false;

            Trim(input, ref copy);

            if (TryChar(input, ref copy, '='))
            {
                Trim(input, ref copy);

                if (!ValueParser.TryParse(input, ref copy, out initToken))
                    return false;
            }

            if (!TryTrimSemikolons(input, ref copy))
                return false;

            token = new DefinitionToken
            {
                Type = typeToken.Name,
                Name = nameToken.Name,
                InitialValue = initToken
            };

            index = copy;
            return true;
        }
    }
}
