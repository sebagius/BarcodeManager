using BarcodeManager.context;
using BarcodeManager.context.barcode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarcodeManager.command
{
    public class ExitCommand : Command
    {
        public ExitCommand() : base("exit", "Exits a context or the application")
        {
            MinProceeding = MaxProceeding = 0;
        }

        public override bool Execute(string[] input, TerminalWindow window)
        {
            if(input.Length > 0)
            {
                return false;
            }

            if(window.AppContext.GetType() == typeof(context.MainContext)) 
            {
                Environment.Exit(0);
                return true;
            }

            window.AppContext.HandleExit(window);

            window.AppContext = new MainContext();

            return true;
        }
    
    }
}
