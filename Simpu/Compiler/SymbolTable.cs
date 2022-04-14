using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simpu.Compiler
{

    public class SymbolTable
    {
        public record SymbolEntry(int Address, SymbolTypes Type);

        public Dictionary<string, SymbolEntry> Entries { get; } = new();

        public Dictionary<string, List<int>> References { get; } = new();

        public void Entry(string label, int address, SymbolTypes type)
        {
            Entries.Add(label, new SymbolEntry(address, type));
        }

        public void Reference(string address, int offset)
        {
            if (!References.ContainsKey(address))
            {
                References.Add(address, new List<int>());
            }

            References[address].Add(offset);
        }
    }
}
