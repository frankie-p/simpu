using Simpu.Tokenizer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simpu.Compiler
{

    public class ObjectFile
    {
        private readonly List<Instruction> m_instructions = new();
        private readonly RegisterManager m_registers = new();
        private readonly FileToken m_token;

        private int m_whileCount;

        private ObjectFile(FileToken token)
        {
            m_token = token;
        }

        public Dictionary<string, int> Globals { get; } = new();

        public IEnumerable<Instruction> Instructions => m_instructions;

        public static ObjectFile Compile(FileToken token)
        {
            var obj = new ObjectFile(token);

            obj.PrepareGlobalVariables();

            // jump to main
            //obj.m_instructions.Add(new JumpAbsoluteInstruction(obj, "main"));

            foreach (var method in token.Methods)
            {
                obj.HandleMethod(method);
            }

            // apply addresses

            var address = 0;

            foreach (var instruction in obj.m_instructions)
            {
                instruction.Address = address;
                address += instruction.Size;
            }

            return obj;
        }

        private void HandleMethod(MethodToken method)
        {
            if (method.Inline)
                return;

            m_instructions.Add(new LabelInstruction(this, method.Name));

            foreach (var param in method.Parameters)
            {
                m_registers.Acquire(param.Name);
            }

            HandleBlock(method.Block);
        }

        private void HandleBlock(BlockToken block)
        {
            foreach (var token in block.Tokens)
            {
                switch (token)
                {
                    case WhileToken @while:
                        HandleWhile(@while);
                        break;
                    case BlockToken innerBlock:
                        HandleBlock(innerBlock);
                        break;
                    case MethodCallToken call:
                        HandleMethodCall(call);
                        break;
                    case UnaryOperationToken unary:
                        UnaryOperation(unary);
                        break;
                    default:
                        throw new Exception($"Unknown block token child: {token}");
                }
            }
        }

        private void UnaryOperation(UnaryOperationToken token)
        {
            if (m_registers.GetOrAcquire(token.Name, out var register))
            {
                m_instructions.Add(new MoveAddressToRegisterInstruction(this, token.Name, register));
                UnaryOperationInner(token, register);
                m_instructions.Add(new MoveRegisterToAddressInstruction(this, token.Name, register));
                m_registers.Release(register);
            }
            else
            {
                UnaryOperationInner(token, register);
            }
        }

        private void UnaryOperationInner(UnaryOperationToken token, Registers register)
        {
            if (token.IsPostIncrement || token.IsPreIncrement)
            {
                m_instructions.Add(new AddInstruction(this, register, 1));
            }
            else if ( token.IsPostDecrement || token.IsPreDecrement)
            {
                throw new NotImplementedException();
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        private void HandleMethodCall(MethodCallToken call)
        {
            var method = m_token.Methods.FirstOrDefault(m => m.Name == call.Name);
            if (method == null)
                throw new Exception($"Method {call.Name} not found");

            if (method.Inline)
            {
                HandleBlock(method.Block);
            }
            else
            {
                // this is going to be the fun part
                // for now we do not create a stack

                m_instructions.Add(new JumpAbsoluteInstruction(this, call.Name));
            }
        }

        private void HandleWhile(WhileToken @while)
        {
            if (@while.Condition is ConstantToken constant && constant.IsFalseCondition)
                return;

            var labelStart = $"while_start_{m_whileCount}";
            var labelEnd = $"while_end_{m_whileCount}";

            m_whileCount++;

            m_instructions.Add(new LabelInstruction(this, labelStart));

            switch (@while.Condition)
            {
                case ConstantToken:
                    // awlays true, therefor add now jmp instruction
                    break;
            }

            if (@while.Block != null)
            {
                HandleBlock(@while.Block);
            }

            m_instructions.Add(new JumpRelativeAddress(this, labelStart));
            m_instructions.Add(new LabelInstruction(this, labelEnd));
        }

        private void PrepareGlobalVariables()
        {
            foreach (var definition in m_token
                .Definitions
                .Where(d => d.InitialValue != null))
            {
                if (definition.InitialValue.IsConstant)
                {
                    if (definition.InitialValue.Constant.IsInteger)
                    {
                        Globals.Add(definition.Name, 4);

                        m_instructions.Add(
                            new MoveValueInstruction(
                                this,
                                definition.Name,
                                definition.InitialValue.Constant.Integer.Value));
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
        }
    }
}
