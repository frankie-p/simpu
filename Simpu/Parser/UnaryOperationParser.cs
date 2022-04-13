using Simpu.Tokenizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simpu.Parser
{

    public class UnaryOperationParser : ParserBase
    {

        public static bool TryParse(string input, ref int index, out UnaryOperationToken token)
        {
            token = null;
            var copy = index;

            Trim(input, ref copy);

            if (TryString(input, ref copy, "++"))
            {
                if (TryNameEnd(input, ref copy, out var nameToken))
                {
                    token = new UnaryOperationToken
                    {
                        IsPreIncrement = true,
                        Name = nameToken.Name
                    };
                }
                else
                {
                    return false;
                }
            }
            else if (TryString(input, ref copy, "--"))
            {
                if (TryNameEnd(input, ref copy, out var nameToken))
                {
                    token = new UnaryOperationToken
                    {
                        IsPreDecrement = true,
                        Name = nameToken.Name
                    };
                }
                else
                {
                    return false;
                }
            }
            else if (VariableParser.TryParse(input, ref copy, out var nameToken))
            {
                if (TryString(input, ref copy, "++"))
                {
                    Trim(input, ref copy);

                    if (TryChar(input, ref copy, ';'))
                    {
                        token = new UnaryOperationToken
                        {
                            IsPostIncrement = true,
                            Name = nameToken.Name
                        };
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (TryString(input, ref copy, "--"))
                {
                    Trim(input, ref copy);

                    if (TryChar(input, ref copy, ';'))
                    {
                        token = new UnaryOperationToken
                        {
                            IsPostDecrement = true,
                            Name = nameToken.Name
                        };
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    throw new NotImplementedException();
                }
            }

            index = copy;
            return true;
        }

        private static bool TryNameEnd(string input, ref int index, out VariableToken nameToken)
        {
            if (!VariableParser.TryParse(input, ref index, out nameToken))
                return false;

            Trim(input, ref index);

            if (!TryChar(input, ref index, ';'))
                return false;

            return true;
        }
    }
}
