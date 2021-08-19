using Simpu.Tokenizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Simpu.Parser
{

    public class VariableParser : ParserBase
    {
        private static readonly Regex m_nameRegex = new Regex("^[a-zA-Z_][a-zA-Z0-9_]*");

        public static bool TryParse(string input, ref int index, out VariableToken token)
        {
            var match = m_nameRegex.Match(input.Substring(index));

            if (match.Success)
            {
                token = new VariableToken
                {
                    Name = match.Value
                };

                index += match.Value.Length;
                return true;
            }

            token = null;
            return false;
        }
    }
}
