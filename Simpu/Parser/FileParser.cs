using Simpu.Tokenizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simpu.Parser
{

    public class FileParser : ParserBase
    {

        public static FileToken Parse(string input)
        {
            var index = 0;

            var definitions = new List<DefinitionToken>();
            var methods = new List<MethodToken>();

            while (!IsEOF(input, ref index))
            {
                if (MethodParser.TryParse(input, ref index, out var method))
                {
                    methods.Add(method);
                }
                else if (DefinitionParser.TryParse(input, ref index, out var definition))
                {
                    definitions.Add(definition);
                }
                else
                {
                    throw new Exception($"Excpected definition or method but got '{new string(input.Substring(index))}'");
                }
            }

            var token = new FileToken
            {
                Definitions = definitions,
                Methods = methods
            };

            return token;
        }

        private static bool IsEOF(string input, ref int index)
        {
            while (true)
            {
                if (index >= input.Length)
                    return true;

                if (!IsTrimmableChar(input, index))
                    return false;

                index++;
            }
        }
    }
}
