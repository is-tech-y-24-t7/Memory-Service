using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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

        public Action<byte[]> DrawAction { get; set; }
        public Mapper Mapper { get; private set; }
        public bool Stop { get; set; }


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

        public void Start()
        {
            Stop = false;
            byte[] bitmapData = Ppu.BitmapData;

            while (!Stop)
            {
                Stopwatch watch = Stopwatch.StartNew();
                GoUntilFrame();
                watch.Stop();
                long timeTaken = watch.ElapsedMilliseconds;
                int sleepTime = (int)(1000.0 / 60 - timeTaken);
                Thread.Sleep(Math.Max(sleepTime, 0));
            }
        }


        public void DrawFrame()
        {
            DrawAction(Ppu.BitmapData);
            _frameEvenOdd = !_frameEvenOdd;
        }

        private void GoUntilFrame()
        {
            var original = _frameEvenOdd;
            while (original == _frameEvenOdd)
            {
                var cycles = Cpu.Step() * 3;

                for (var i = 0; i < cycles; i++)
                {
                    Ppu.Step();
                    Mapper.Step();
                }
            }
        }
    }
}
