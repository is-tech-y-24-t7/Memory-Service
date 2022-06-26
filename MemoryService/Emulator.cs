using System;
using System.Windows.Forms;

namespace MemoryService
{
    public class Emulator
    {
        [STAThread]
        public static void Main()
        {
            Application.Run(new Ui());
        }
    }
}
