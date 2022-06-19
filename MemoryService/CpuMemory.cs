using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryService
{
    class CpuMemory
    {
        public CpuMemory(Console console)
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
