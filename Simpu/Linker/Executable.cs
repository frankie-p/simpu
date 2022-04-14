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

            exe.HandleGlobals(obj);
            exe.HandleInstructions(obj);
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
                Symbols.Entry(global.Key, GlobalsSize, SymbolTypes.Global);
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
                    Stream.Seek(value, SeekOrigin.Begin);
                    Stream.Write(BitConverter.GetBytes(entry.Address), 0, 4);
                }
            }
        }
    }
}
