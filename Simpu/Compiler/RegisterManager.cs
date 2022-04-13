using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simpu.Compiler
{
    public class RegisterManager
    {
        private readonly Dictionary<Registers, string> m_registry = new();

        public Registers Acquire(string variable)
        {
            for (var i = 0; i < (int)Registers.EOR; i++)
            {
                var reg = (Registers)i;

                if (m_registry.ContainsKey(reg))
                    continue;

                m_registry.Add(reg, variable);
                return reg;
            }

            throw new Exception("Out of registers");
        }

        public void Release(Registers reg)
        {
            m_registry.Remove(reg);
        }

        public bool GetOrAcquire(string variable, out Registers register)
        {
            foreach (var entry in m_registry)
            {
                if (entry.Value == variable)
                {
                    register = entry.Key;
                    return false;
                }
            }

            register = Acquire(variable);
            return true;
        }
    }
}
