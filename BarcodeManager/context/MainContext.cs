using BarcodeManager.command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarcodeManager.context
{
    public class MainContext : context.AppContext
    {
        public MainContext() : base("Main Application Context", "Main Menu")
        {
            Commands.Add(new SwitchCommand());
        }

        public override AppContext SwitchTo(TerminalWindow terminalWindow)
        {
            base.SwitchTo(terminalWindow);
            terminalWindow.Color(ConsoleColor.Yellow)
                .Write("Welcome to Barcode Manager")
                .Color(ConsoleColor.Blue)
                .Write(" ( HD Level :* )")
                .EndLine()
                .Color(ConsoleColor.Yellow)
                .Write("Type 'help' to view commands for the current context!")
                .EndLine();

            return this;
        }
    }
}
