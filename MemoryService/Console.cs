﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryService
{
    public class Console
    {
        public Cpu Cpu { get; }

        public Ppu Ppu { get; }

        public CpuMemory CpuMemory { get; }

        public PpuMemory PpuMemory { get; }

        public Controller Controller { get; }

        public Cartridge Cartridge { get; private set; }

        
        public Mapper Mapper { get; private set; }


        private bool _frameEvenOdd;

        public Console()
        {
            Controller = new Controller();

            CpuMemory = new CpuMemory(this);
            PpuMemory = new PpuMemory(this);

            Cpu = new Cpu(this);
            Ppu = new Ppu(this);
        }
        public bool LoadCartridge(string path)
        {
            System.Console.WriteLine("Loading cartridge from: " + path);

            Cartridge = new Cartridge(path);

            if (Cartridge.Invalid)
            {
                System.Console.WriteLine("Cartridge is invalid!");
                return false;
            }


            System.Console.Write("iNES Mapper Number: " + Cartridge.MapperNumber.ToString());

            switch (Cartridge.MapperNumber)
            {
                case 0:
                    Mapper = new NromMapper(this);
                    System.Console.WriteLine(" NROM ");
                    break;
                case 1:
                    Mapper = new Mmc1Mapper(this);
                    System.Console.WriteLine(" MMC1 ");
                    break;
                case 2:
                    Mapper = new UxRomMapper(this);
                    System.Console.WriteLine(" UxROM ");
                    break;
                case 4:
                    Mapper = new Mmc3Mapper(this);
                    System.Console.WriteLine(" MMC3 ");
                    break;
                default:
                    System.Console.WriteLine(" Mapper is not maintained");
                    return false;
            }

            Cpu.Reset();
            Ppu.Reset();

            CpuMemory.Reset();
            PpuMemory.Reset();

            _frameEvenOdd = false;
            return true;
        }
    }
}