using simpu.tokenizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace simpu.parser
{

    public class ValueParser : ParserBase
    {

        public static bool TryParse(string input, ref int index, out ValueToken token)
        {
            var copy = index;

            if (ConstantParser.TryParse(input, ref copy, out var constantToken))
            {
                token = new ValueToken
                {
                    Constant = constantToken
                };

                index = copy;
                return true;
            }

            if (MethodCallParser.TryParse(input, ref copy, out var methodCallToken))
            {
                token = new ValueToken
                {
                    MethodCall = methodCallToken
                };

                index = copy;
                return true;
            }

            if (VariableParser.TryParse(input, ref copy, out var variableToken))
            {
                token = new ValueToken
                {
                    Variable = variableToken
                };

                index = copy;
                return true;
            }

            token = null;
            return false;
        }
    }
}
