using BarcodeManager.context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarcodeManager.command
{
    public class SwitchCommand : Command
    {
        public SwitchCommand() : base("switch", "Switches between a context", new string[] { "stock", "registry" })
        {
            MinProceeding = MaxProceeding = 1;
        }

        public override bool Execute(string[] input, TerminalWindow window)
        {
            switch(input[0].ToLower())
            {
                case "registry":
                case "reg":
                    window.AppContext = new BarcodeContext();
                    break;
                case "stock":
                    window.AppContext = new StockContext();
                    break;
                default:
                    return false;
            }

            return true;
        }
    }
}
