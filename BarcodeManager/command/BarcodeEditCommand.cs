using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarcodeManager.command
{
    public class BarcodeEditCommand : Command
    {
        public BarcodeEditCommand() : base("edit", "Edit a barcode in the registry")
        {
        }

        public override bool Execute(string[] input, TerminalWindow window)
        {
            window.Color(ConsoleColor.DarkRed).Write("Due to time constraints this command was not implemented. The logic flow would be as follow")
                .EndLine()
                .Write("Check if barcode exists in registry -> display build prompts but default is current information -> remove barcode and add new")
                .EndLine();
            return true;
        }
    }
}
