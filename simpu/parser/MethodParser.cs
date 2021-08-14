using simpu.tokenizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simpu.parser
{

    public class MethodParser : ParserBase
    {


        public bool TryParse(string input, ref int index, out MethodToken token)
        {
            token = null;

            var copy = index;

            Trim(input, ref copy);

            if (!ValueParser.TryParse(input, ref copy, out var returnValueToken) || returnValueToken.IsNumber)
                return false;

            Trim(input, ref copy);

            if (!ValueParser.TryParse(input, ref copy, out var nameValueToken) || nameValueToken.IsNumber)
                return false;

            Trim(input, ref copy);

            if (!TryChar(input, ref copy, '('))
                return false;

#warning add parameter parsing here

            if (!TryChar(input, ref copy, ')'))
                return false;

            if (!BlockParser.TryParse(input, ref copy, out var blockToken))
                return false;            

            token = new MethodToken
            {
                Name = nameValueToken.Value,
                ReturnType = returnValueToken.Value,
                Block = blockToken
            };

            index = copy;
            return true;
        }
    }
}
