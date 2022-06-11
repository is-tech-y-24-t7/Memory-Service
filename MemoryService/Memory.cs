using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryService
{
    public abstract class Memory
    {
        public abstract byte Read(ushort address);

        public abstract void Write(ushort address, byte data);

        public void ReadBuf(byte[] buffer, ushort address, ushort size)
        {
            for (int i = 0; i < size; i++)
            {
                var readAddress = (ushort)(address + i);
                buffer[i] = Read(readAddress);
            }
        }

        public void ReadBufWrapping(byte[] buffer, int startIndex, ushort startAddress, int size)
        {
            int index = startIndex;
            int bytesRead = 0;
            ushort address = startAddress;
            while (bytesRead < size)
            {
                if (index >= buffer.Length) index = 0;
                buffer[index] = Read(address);

                address++;
                bytesRead++;
                index++;
            }
        }

        public ushort Read16(ushort address)
        {
            byte lo = Read(address);
            byte hi = Read((ushort)(address + 1));
            return (ushort)((hi << 8) | lo);
        }

        public ushort Read16WrapPage(ushort address)
        {
            ushort data;
            if ((address & 0xFF) == 0xFF)
            {
                byte lo = Read(address);
                byte hi = Read((ushort)(address & 0x00));
                data = (ushort)((hi << 8) | lo);
            }
            else
            {
                data = Read16(address);
            }
            return data;
        }
    }
}