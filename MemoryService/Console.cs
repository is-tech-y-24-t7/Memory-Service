﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryService
{
    public class Console
    {
        public readonly Ppu Ppu;
        public readonly Controller Controller;
        public Mapper Mapper { get; private set; }
    }
}
