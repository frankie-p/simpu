using Simpu.Tokenizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Simpu.Parser
{

    public class ConstantParser : ParserBase
    {
        private readonly static Regex m_integerRegex = new Regex(@"^(-?\d+)");
        private readonly static Regex m_floatRegex = new Regex(@"^-?\d+\.\d+");
        private readonly static Regex m_stringRegex = new Regex("^\\\"([^\"]|\\\")*\\\"");

        public static bool TryParse(string input, ref int index, out ConstantToken token)
        {
            var copy = index;

            var match = m_integerRegex.Match(input.Substring(copy));

            if (match.Success)
            {
                token = new ConstantToken
                {
                    Integer = int.Parse(match.Value)
                };

                index += match.Value.Length;
                return true;
            }

            match = m_floatRegex.Match(input.Substring(copy));

            if (match.Success)
            {
                token = new ConstantToken
                {
                    Float = float.Parse(match.Value)
                };

                index += match.Value.Length;
                return true;
            }

            match = m_stringRegex.Match(input.Substring(copy));

            if (match.Success)
            {
                token = new ConstantToken
                {
                    String = match.Value.Substring(1, match.Value.Length - 2)
                };

                index += match.Value.Length;
                return true;
            }

            token = null;
            return false;
        }
    }
}
