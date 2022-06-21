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
      
        public void WriteToRegister(ushort address, byte data)
        {
            throw new NotImplementedException();
        }

        public byte ReadFromRegister(ushort address)

        public Console Console { get; }

        internal void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
