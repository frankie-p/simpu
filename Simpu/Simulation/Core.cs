using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simpu.Simulation
{

    public class Core
    {
        private readonly byte[] m_memory;

        private byte[] m_instruction = new byte[10];
        private uint m_instructionPointer;

        private uint CS;
        private uint DS;

        public Core(byte[] memory, uint instructionPointer)
        {
            m_memory = memory;
            m_instructionPointer = instructionPointer;
        }

        public void Run()
        {
            while (true)
            {
                FetchInstruction();
                ExecuteInstruction();
            }
        }

        private void FetchInstruction()
        {
            m_instruction[0] = m_memory[m_instructionPointer];

            switch (m_instruction[0])
            {
                case 0x01: // absolute jmp
                    Buffer.BlockCopy(m_memory, (int)m_instructionPointer + 1, m_instruction, 1, 4);
                    return;
                case 0x02: // relative jmp
                    Buffer.BlockCopy(m_memory, (int)m_instructionPointer + 1, m_instruction, 1, 4);
                    return;
            }
        }

        private void ExecuteInstruction()
        {
            switch (m_instruction[0])
            {
                case 0x00: // nop
                    break;
                case 0x01: // absolute jmp
                    m_instructionPointer = DS + m_instructionPointer;
                    return;
                case 0x02: // relative jmp
                    m_instructionPointer = m_instructionPointer + BitConverter.ToUInt32(m_instruction, 1);
                    return;
                default:
                    throw new Exception("hardware panic");
            }

            m_instructionPointer++;
        }
    }
}
