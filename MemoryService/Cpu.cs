using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryService
{
    internal class Cpu
    {
        public Cpu(Console console)
        {
            Console = console;
        }

        public Console Console { get; }

        internal void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
