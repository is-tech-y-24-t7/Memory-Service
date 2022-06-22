using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryService
{
    public class Cartridge
    {
        private string path;

        public Cartridge(string path)
        {
            this.path = path;
        }

        public bool Invalid { get; internal set; }

        public object MapperNumber { get; internal set; }
    }
}
