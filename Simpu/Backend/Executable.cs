using Simpu.Tokenizer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simpu.Backend
{

    public class Executable
    {
        private readonly List<Instruction> m_instructions = new();

        private int m_whileCount;

        private Executable()
        {

        }

        public static Executable Create(FileToken token)
        {
            var exe = new Executable();

            // for now, the very first instruction is the jump to main entry
            // added main entry ret jump address and args later

            exe.m_instructions.Add(new JumpInstruction(exe, "main"));

            foreach (var method in token.Methods)
            {
                exe.HandleMethod(method);
            }

            // apply addresses

            var address = 0;

            foreach (var instruction in exe.m_instructions)
            {
                instruction.Address = address;
                address += instruction.Size;
            }

            return exe;
        }

        public void Write(Stream stream)
        {
            foreach (var instruction in m_instructions)
            {
                instruction.Write(stream);
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            foreach (var instruction in m_instructions)
            {
                if (instruction.Size != 0)
                {
                    sb.Append($"0x{instruction.Address:X4}  ");
                }

                if (instruction == m_instructions.Last())
                {
                    sb.Append(instruction.ToString());
                }
                else
                {
                    sb.AppendLine(instruction.ToString());
                }
            }

            return sb.ToString();
        }

        private void HandleMethod(MethodToken method)
        {
            m_instructions.Add(new LabelInstruction(this, method.Name));
            HandleBlock(method.Block);
            // add jump ret
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
                    default:
                        throw new Exception($"Unknown block token child: {token}");
                }
            }
        }

        private void HandleMethodCall(MethodCallToken call)
        {
            // this is going to be the fun part
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

            m_instructions.Add(new JumpInstruction(this, labelStart));
            m_instructions.Add(new LabelInstruction(this, labelEnd));
        }

        public int GetAddressOffset(Instruction from, string label)
        {
            var labelFound = false;
            var selfFound = false;
            var offset = 0;

            foreach (var instruction in m_instructions)
            {
                if (instruction is LabelInstruction li && li.Name == label)
                {
                    if (selfFound)
                        break;

                    labelFound = true;
                }
                else if (instruction == from)
                {
                    if (labelFound)
                    {
                        offset *= -1;
                        break;
                    }

                    selfFound = true;
                    offset += instruction.Size;
                }
                else if (labelFound && selfFound)
                {
                    offset += instruction.Size;
                }
            }

            return offset;
        }

        public bool TryGetAddressOfLabel(string label, out int address)
        {
            var li = m_instructions.FirstOrDefault(i => i is LabelInstruction l && l.Name == label);
            address = li.Address;
            return li != null;
        }
    }
}
