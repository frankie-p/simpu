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
        private static readonly Regex m_nameRegex = new Regex("([a-zA-Z][a-zA-Z0-9]*)|([0-9]+)");

        public static bool TryParse(string input, ref int index, out ValueToken token)
        {
            var match = m_nameRegex.Match(input, index);

            if (!match.Success)
            {
                token = null;
                return false;
            }

            index += match.Value.Length;
            token = new ValueToken
            {
                Value = match.Value  
            };
            return true;
        }
    }
}
