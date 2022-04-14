using System.Collections.Generic;

namespace Simpu.Compiler
{

    public class SymbolTable
    {
        public record SymbolEntry(int Address, SymbolTypes Type);

        public Dictionary<string, SymbolEntry> Entries { get; } = new();

        public Dictionary<string, List<SymbolEntry>> References { get; } = new();

        public void Entry(string label, int address, SymbolTypes type)
        {
            Entries.Add(label, new SymbolEntry(address, type));
        }

        public void Reference(string label, int address, SymbolTypes type)
        {
            if (!References.ContainsKey(label))
            {
                References.Add(label, new List<SymbolEntry>());
            }

            References[label].Add(new SymbolEntry(address, type));
        }
    }
}
