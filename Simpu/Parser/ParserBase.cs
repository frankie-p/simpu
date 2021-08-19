using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Simpu.Parser
{

    public abstract class ParserBase
    {
        protected static readonly char[] TrimmingCharacters = { ' ', '\t', '\n' };

        protected static bool IsTrimmableChar(string input, int index) => TrimmingCharacters.Contains(input[index]);

        protected static void Trim(string input, ref int index)
        {
            while (IsTrimmableChar(input, index))
            {
                index++;
            }
        }

        protected static bool TryChar(string input, ref int index, char c)
        {
            if (input[index] != c)
                return false;   

            index++;
            return true;
        }

        protected static bool TryChar(string input, ref int index, string chars)
        {
            for (var i = 0; i < chars.Length; i++)
            {
                if (input[index] == chars[i])
                {
                    index++;
                    return true;
                }
            }

            return false;           
        }

        protected static bool TryString(string input, ref int index, string str)
        {
            var copy = index;

            for (var i = 0; i < str.Length; i++, copy++)
            {
                if (copy >= input.Length)
                    return false;

                if (input[copy] != str[i])
                    return false;
            }

            index = copy;
            return true;
        }

        protected static bool TryTrimSemikolons(string input, ref int index)
        {
            var copy = index;

            Trim(input, ref copy);

            if (!TryChar(input, ref index, ';'))
                return false;

            do
            {
                Trim(input, ref index);
            }
            while (TryChar(input, ref index, ';'));

            return true;
        }
    }
}
