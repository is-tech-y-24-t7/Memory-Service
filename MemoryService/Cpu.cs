using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryService
{
    public class Cpu
    {
        public Cpu(Console console)
        {
            Console = console;
        }

        public int Step()
        {
            throw new NotImplementedException();
        }

        public Console Console { get; }
        internal void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
