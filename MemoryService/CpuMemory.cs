using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryService
{
    public class CpuMemory : Memory
    {
        private readonly byte[] _internalRam = new byte[2048];
        private readonly Console _console;

        private static ushort HandleInternalRamMirror(ushort address)
        {
            return (ushort)(address % 0x800);
        }

        private byte ReadPpuRegister(ushort address)
        {
            return _console.Ppu.ReadFromRegister(GetPpuRegisterFromAddress(address));
        }

        private byte ReadApuIoRegister(ushort address)
        {
            return address switch
            {
                0x4016 =>
                    _console.Controller.ReadControllerOutput(),
                _ => 0
            };
        }

        private void WritePpuRegister(ushort address, byte data)
        {
            _console.Ppu.WriteToRegister(GetPpuRegisterFromAddress(address), data);
        }

        private static ushort GetPpuRegisterFromAddress(ushort address)
        {
            if (address == 0x4014)
            {
                return address;
            }
            else
            {
                return (ushort)(0x2000 + ((address - 0x2000) % 8));
            }
        }

        private void WriteApuIoRegister(ushort address, byte data)
        {
            switch (address)
            {
                case 0x4016:
                    _console.Controller.WriteControllerInput(data);
                    break;
                default:
                    data = 0;
                    break;
            }
        }

        public override byte Read(ushort address)
        {
            byte data;

            switch (address)
            {
                case < 0x2000:
                {
                    var addressIndex = HandleInternalRamMirror(address);
                    data = _internalRam[addressIndex];
                    break;
                }
                case <= 0x3FFF:
                    data = ReadPpuRegister(address);
                    break;
                case <= 0x4017:
                    data = ReadApuIoRegister(address);
                    break;
                case <= 0x401F:
                    data = 0;
                    break;
                case >= 0x4020:
                    data = _console.Mapper.Read(address);
                    break;
                default:
                    throw new Exception("Read failed");
            }

            return data;
        }

        public override void Write(ushort address, byte data)
        {
            switch (address)
            {
                case < 0x2000:
                {
                    var addressIndex = HandleInternalRamMirror(address);
                    _internalRam[addressIndex] = data;
                    break;
                }
                case <= 0x3FFF:
                case 0x4014:
                    WritePpuRegister(address, data);
                    break;
                case <= 0x4017:
                    WriteApuIoRegister(address, data);
                    break;
                case <= 0x401F:
                    break;
                case >= 0x4020:
                    _console.Mapper.Write(address, data);
                    break;
                default:
                    throw new Exception("Write failed");
            }
        }
    }
}
