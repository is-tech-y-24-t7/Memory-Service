using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryService
{
    public class Ppu
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
        {
            throw new NotSupportedException();
        }

        public int Step()
        {
            throw new NotImplementedException();
        }

        public Console Console { get; }
        public byte[] BitmapData { get; internal set; }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
