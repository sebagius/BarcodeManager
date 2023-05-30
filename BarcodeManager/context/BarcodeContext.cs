using BarcodeManager.command;
using BarcodeManager.context.barcode;
using BarcodeManager.registry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarcodeManager.context
{
    public class BarcodeContext : AppContext, DataContext<Barcode>
    {

        private BarcodeRegistry _registry;

        public BarcodeContext() : base("Registry Application Context", "Manage Barcode Registry")
        {
            Commands.Add(new BarcodeAddCommand());
            Commands.Add(new BarcodeViewCommand());
            Commands.Add(new BarcodeDeleteCommand());
            this._registry = new BarcodeRegistry();
        }

        public Registry<Barcode> Registry { get { return _registry; } }

        public override void HandleExit(TerminalWindow terminalWindow)
        {
            terminalWindow.Color(ConsoleColor.Cyan).Write("Saving barcode registry...\n");
            this.Registry.Save();
            Thread.Sleep(500);
        }

        public override void PrintIntro(TerminalWindow terminalWindow)
        {
            terminalWindow.Color(ConsoleColor.Yellow)
                .Write("Barcode Registry Interface")
                .EndLine();
        }

        public override AppContext SwitchTo(TerminalWindow terminalWindow)
        {
            base.SwitchTo(terminalWindow);
            PrintIntro(terminalWindow);
            Registry.Load();

            return this;
        }
    }
}
