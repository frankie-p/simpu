using Simpu.Compiler;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simpu.Linker
{

    public class Executable
    {
        private Executable(Stream s)
        {
            Stream = s;
        }

        public static Executable Link(ObjectFile obj, Stream s)
        {
            var exe = new Executable(s);

#warning here we would add some instructions for preparing the stack
            exe.HandleInstructions(obj);
            exe.HandleGlobals(obj);
            exe.Symbols.Distinct();
            exe.ResolveReferences();
            return exe;
        }

        public Stream Stream { get; }

        public SymbolTable Symbols { get; } = new();

        public int GlobalsSize { get; private set; }

        private void HandleGlobals(ObjectFile obj)
        {
            foreach (var global in obj.Globals)
            {
                Symbols.Entry(global.Key, GlobalsSize + (int)Stream.Length, SymbolTypes.Global);
                GlobalsSize += global.Value;
            }
        }

        private void HandleInstructions(ObjectFile obj)
        {
            foreach (var instruction in obj.Instructions)
            {
                instruction.Write(Stream, Symbols);
            }
        }

        private void ResolveReferences()
        {
            foreach (var reference in Symbols.References)
            {
                var entry = Symbols.Entries[reference.Key];

                foreach (var value in reference.Value)
                {
                    if (value.Type == SymbolTypes.Absolute)
                    {
                        Stream.Seek(value.Address, SeekOrigin.Begin);
                        Stream.Write(BitConverter.GetBytes((ushort)entry.Address), 0, 2);
                    }
                    else
                    {
                        ushort diff = (ushort)(value.Address - entry.Address);

                        Stream.Seek(value.Address, SeekOrigin.Begin);
                        Stream.Write(BitConverter.GetBytes(diff), 0, 2);
                    }
                }
            }
        }
    }
}
