using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryService
{
    class PpuMemory : Memory
    {
        readonly Console _console;
        readonly byte[] _vRam;
        readonly byte[] _paletteRam;

        public PpuMemory(Console console) => Console = console;

        public Console Console { get; }

        public override byte Read(ushort address) => address switch
        {
            < 0x2000 => _console.Mapper.Read(address),
            <= 0x3EFF => _vRam[_console.Mapper.VramAddressToIndex(address)],
            >= 0x3F00 and <= 0x3FFF => _paletteRam[GetPaletteRamIndex(address)],
            _ => throw new Exception("Can not PPU Memory read. Invalid address: " + address.ToString("x4"))
        };

        public override void Write(ushort address, byte data)
        {
            switch (address)
            {
                case < 0x2000:
                    _console.Mapper.Write(address, data);
                    break;
                case >= 0x2000 and <= 0x3EFF:
                    _vRam[_console.Mapper.VramAddressToIndex(address)] = data;
                    break;
                case >= 0x3F00 and <= 0x3FFF:
                    _paletteRam[GetPaletteRamIndex(address)] = data;
                    break;
                default:
                    throw new Exception("Can not PPU Memory write. Invalid address: " + address.ToString("x4"));
            }
        }

        internal static void Reset()
        {
            throw new NotImplementedException();
        }

        public ushort GetPaletteRamIndex(ushort address)
        {
            var index = (ushort)((address - 0x3F00) % 32);
            if (index >= 16 && (index - 16) % 4 == 0) 
                return (ushort)(index - 16);
            return index;
        }

    }
}
