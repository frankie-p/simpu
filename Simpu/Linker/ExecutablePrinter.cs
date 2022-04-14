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
                PrintLabel(exe, demangle);
                PrintAddress(exe);

                var instruction = exe.Stream.ReadByte();

                switch (instruction)
                {
                    case 0x00:
                        Nop();
                        break;
                    case 0x01:
                        Jump(exe, demangle);
                        break;
                    case 0x10:
                        MoveValueToAddress(exe, demangle);
                        break;
                    case 0x11:
                        MoveAddressToRegister(exe, demangle);
                        break;
                    case 0x12:
                        MoveRegisterToAddress(exe, demangle);
                        break;
                    case 0x20:
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
            Console.WriteLine($"  ADD {GetRegister(exe)},{GetValue(exe)}");
        }

        private static void MoveRegisterToAddress(Executable exe, bool demangle)
        {
            var register = GetRegister(exe);
            var address = GetAddress(exe, demangle);
            Console.WriteLine($"  MOV {register},{address}");
        }

        private static void MoveAddressToRegister(Executable exe, bool demangle)
        {
            Console.WriteLine($"  MOV {GetAddress(exe, demangle)},{GetRegister(exe)}");
        }

        private static void MoveValueToAddress(Executable exe, bool demangle)
        {
            Console.WriteLine($"  MOV {GetAddress(exe, demangle)},{GetValue(exe)}");
        }

        private static void Jump(Executable exe, bool demangle)
        {
            Console.WriteLine($"  JMP {GetAddress(exe, demangle)}");
        }

        private static void Nop()
        {
            Console.WriteLine($"  NOP");
        }

        private static void PrintAddress(Executable exe)
        {
            ConsoleHelper.WriteColored($"0x{exe.Stream.Position.ToString("X").PadLeft(4, '0')}  ", ConsoleColor.White);
        }

        private static string GetAddress(Executable exe, bool demangle)
        {
            var buffer = new byte[4];
            exe.Stream.Read(buffer, 0, 4);
            var address = BitConverter.ToInt32(buffer, 0);

            return demangle
                ? Demangle(exe, address)
                : $"0x{address.ToString("X").PadLeft(4, '0')}";
        }

        private static string GetRegister(Executable exe)
        {
            var buffer = new byte[2];
            exe.Stream.Read(buffer, 0, 2);
            var register = (Registers)BitConverter.ToInt16(buffer, 0);
            return $"%{register}";
        }

        private static string GetValue(Executable exe)
        {
            var buffer = new byte[4];
            exe.Stream.Read(buffer, 0, 4);
            return BitConverter.ToInt32(buffer, 0).ToString();
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
