using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryService
{
    /// <summary>
    /// Base class for all memory classes.
    /// </summary>
    public abstract class Memory
    {
        /// <summary>
        /// Read a byte of memory from the address.
        /// </summary>
        /// <returns>Byte of memory from the address.</returns>
        /// <param name="address">Read from this address.</param>
        public abstract byte Read(ushort address);

        /// <summary>
        /// Write a byte of memory to the address.
        /// </summary>
        /// <param name="address">Write bite of memory to this address.</param>
        /// <param name="data">Value byte to write.</param>
        public abstract void Write(ushort address, byte data);

        /// <summary>
        /// Reads some bytes to buffer from the address.
        /// </summary>
        /// <param name="buffer">Read to this buffer.</param>
        /// <param name="address">Start read from this address.</param>
        /// <param name="size">Number of bytes to read.</param>
        public void ReadBuf(byte[] buffer, ushort address, ushort size)
        {
            for (int readed = 0; readed < size; readed++)
            {
                ushort ReadAddr = (ushort)(address + readed);
                buffer[readed] = Read(ReadAddr);
            }
        }

        /// <summary>
        /// Reads some bytes to buffer from the address
        /// starting at the index in the buffer
        /// wrapping around to index 0 in the buffer if it ends.
        /// </summary>
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

        /// <summary>
        /// Reads two bytes from the address.
        /// </summary>
        /// <returns>A 16 bit value representing the two bytes.</returns>
        /// <param name="address">The address to read two bytes from.</param>
        public ushort Read16(ushort address)
        {
            byte lo = Read(address);
            byte hi = Read((ushort)(address + 1));
            return (ushort)((hi << 8) | lo);
        }

        /// <summary>
        /// Reads two bytes wrapping around to the start of
        /// the page if the lower byte is at beginning.
        /// </summary>
        /// <returns>A 16 bit value representing the two bytes.</returns>
        /// <param name="address">The address to read two bytes from</param>
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
