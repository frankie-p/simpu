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

        public void Distinct()
        {
            var removes = new List<string>();

            foreach (var entry in Entries.Keys)
            {
                if (!References.ContainsKey(entry))
                {
                    removes.Add(entry);
                }
            }

            foreach (var remove in removes)
            {
                Entries.Remove(remove);
            }
        }
    }
}
