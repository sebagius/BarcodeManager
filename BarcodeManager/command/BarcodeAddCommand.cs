using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarcodeManager.command
{
    internal class BarcodeAddCommand : Command
    {
        public BarcodeAddCommand() : base("add", "Adds a barcode onto the system", new string[] { "UPC", "EAN" })
        {
        }

        public override bool Execute(string[] input, TerminalWindow window)
        {

            return false;
        }
    }
}
