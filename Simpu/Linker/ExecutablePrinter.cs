using Simpu.Compiler;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simpu.Linker
{

    public class ExecutableReader
    {
        public static void Print(Executable exe, bool demangle)
        {
            exe.Stream.Seek(0, SeekOrigin.Begin);

            while (exe.Stream.Position < exe.Stream.Length)
            {
                if (exe.Stream.Position % 4 != 0)
                    throw new BadImageFormatException();

                PrintLabel(exe, demangle);
                PrintAddress(exe);

                var instruction = (Instructions)exe.Stream.ReadByte();

                switch (instruction)
                {
                    case Instructions.NOP:
                        Nop(exe);
                        break;
                    case Instructions.JUMP_ABSOLUTE:
                        JumpAbsolute(exe, demangle);
                        break;
                    case Instructions.JUMP_RELATIVE_BACK:
                        JumpRelativeBack(exe, demangle);
                        break;
                    case Instructions.MOVE_ADDRESS_VALUE:
                        MoveValueToAddress(exe, demangle);
                        break;
                    case Instructions.MOVE_ADDRESS_REG:
                        MoveAddressToRegister(exe, demangle);
                        break;
                    case Instructions.MOVE_REG_ADDRESS:
                        MoveRegisterToAddress(exe, demangle);
                        break;
                    case Instructions.ARITHMETIC_ADD:
                        Add(exe);
                        break;
                    default:
                        throw new NotImplementedException(instruction.ToString());
                }
            }
        }

        private static void PrintLabel(Executable exe, bool demangle)
        {
            if (!demangle)
                return;

            var address = (int)exe.Stream.Position;

            foreach (var entry in exe.Symbols.Entries)
            {
                if (entry.Value.Type == SymbolTypes.Method && entry.Value.Address == address)
                {
                    ConsoleHelper.WriteLineColored($"{entry.Key}:", ConsoleColor.DarkGray);
                    return;
                }
            }
        }

        private static void Add(Executable exe)
        {
            Console.WriteLine($"  ADD {GetRegister(exe)},{GetShortValue(exe)}");
        }

        private static void MoveRegisterToAddress(Executable exe, bool demangle)
        {
            var reg = GetRegister(exe);
            Console.WriteLine($"  MOV {GetAbsoluteAddress(exe, demangle)},{reg}");
        }

        private static void MoveAddressToRegister(Executable exe, bool demangle)
        {
            var reg = GetRegister(exe);
            Console.WriteLine($"  MOV {GetAbsoluteAddress(exe, demangle)},{reg}");
        }

        private static void MoveValueToAddress(Executable exe, bool demangle)
        {
            var value = GetByteValue(exe);
            Console.WriteLine($"  MOV {GetAbsoluteAddress(exe, demangle)},{value}");
        }

        private static void JumpRelativeBack(Executable exe, bool demangle)
        {
            ReadPad(exe);
            Console.WriteLine($"  JMB {GetRelativeAddress(exe, demangle)}");
        }

        private static void JumpAbsolute(Executable exe, bool demangle)
        {
            ReadPad(exe);
            Console.WriteLine($"  JMP {GetAbsoluteAddress(exe, demangle)}");
        }

        private static void Nop(Executable exe)
        {
            ReadPad(exe);
            ReadPad(exe);
            ReadPad(exe);
            Console.WriteLine($"  NOP");
        }

        private static void ReadPad(Executable exe)
        {
            exe.Stream.ReadByte();   
        }

        private static void PrintAddress(Executable exe)
        {
            ConsoleHelper.WriteColored($"0x{exe.Stream.Position.ToString("X").PadLeft(4, '0')}  ", ConsoleColor.White);
        }

        private static string GetAbsoluteAddress(Executable exe, bool demangle)
        {
            var buffer = new byte[2];
            exe.Stream.Read(buffer, 0, 2);
            var address = BitConverter.ToUInt16(buffer, 0);

            return demangle
                ? Demangle(exe, address)
                : $"0x{address.ToString("X").PadLeft(4, '0')}";
        }

        private static string GetRelativeAddress(Executable exe, bool demangle)
        {
            var buffer = new byte[2];
            var position = (int)exe.Stream.Position;            
            
            exe.Stream.Read(buffer, 0, 2);

            var offset = BitConverter.ToUInt16(buffer, 0);

            return demangle
                ? Demangle(exe, position - offset)
                : $"0x{offset.ToString("X").PadLeft(4, '0')}";
        }

        private static string GetRegister(Executable exe)
        {
            var register = (Registers)exe.Stream.ReadByte();
            return $"%{register}";
        }

        private static string GetByteValue(Executable exe)
        {
            return exe.Stream.ReadByte().ToString();
        }

        private static string GetShortValue(Executable exe)
        {
            var buffer = new byte[2];
            exe.Stream.Read(buffer, 0, 2);
            return BitConverter.ToUInt16(buffer, 0).ToString();
        }

        private static string Demangle(Executable exe, int address)
        {
            foreach (var entry in exe.Symbols.Entries)
            {
                if (entry.Value.Address == address)
                    return entry.Key;
            }

            throw new Exception($"Failed to demangle address {address}");
        }
    }
}
