using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryService
{
    internal class Ppu
    {
        public Ppu(Console console)
        {
            Console = console;
        }

        public Console Console { get; }
        public byte[] BitmapData { get; internal set; }

        internal void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
