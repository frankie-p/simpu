using simpu.tokenizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simpu.parser
{

    public class ConditionParser : ParserBase
    {

        public static bool TryParse(string input, ref int index, out ConditionToken token)
        {
            token = null;
            var copy = index;

            Conditions condition;

            Trim(input, ref copy);

            if (!ValueParser.TryParse(input, ref copy, out var leftToken))
                return false;

            Trim(input, ref copy);

            if (TryString(input, ref copy, ">="))
            {
                condition = Conditions.GreaterEqual;
            }
            else if (TryString(input, ref copy, "<="))
            {
                condition = Conditions.LesserEqual;
            }
            else if (TryString(input, ref copy, "=="))
            {
                condition = Conditions.Equal;
            }
            else if (TryString(input, ref copy, ">"))
            {
                condition = Conditions.Greater;
            }
            else if (TryString(input, ref copy, "<"))
            {
                condition = Conditions.Lesser;
            }
            else
            {
                return false;
            }

            Trim(input, ref copy);

            if (!ValueParser.TryParse(input, ref copy, out var rightToken))
                return false;

            token = new ConditionToken
            {
                Left = leftToken,
                Condition = condition,
                Right = rightToken,
            };

            index = copy;
            return true;
        }
    }
}
