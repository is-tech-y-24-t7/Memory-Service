using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryService
{
    public abstract class Mapper
    {
        public abstract byte Read(ushort address);
        public abstract void Write(ushort address, byte data);
    }
}
