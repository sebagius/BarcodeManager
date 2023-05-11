using BarcodeManager.command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarcodeManager.context
{
    public class RegistryContext : AppContext
    {
        public RegistryContext() : base("Registry Application Context", "Manage Barcode Registry")
        {
            //Commands.Add();
        }

        public override AppContext SwitchTo(TerminalWindow terminalWindow)
        {
            base.SwitchTo(terminalWindow);
            terminalWindow.Color(ConsoleColor.Yellow)
                .Write("Barcode Registry Interface")
                .EndLine();

            return this;
        }
    }
}
