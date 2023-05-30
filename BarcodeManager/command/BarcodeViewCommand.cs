using BarcodeManager.context.barcode;
using BarcodeManager.context;
using BarcodeManager.exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarcodeManager.command
{
    public class BarcodeViewCommand : Command
    {
        public BarcodeViewCommand() : base("view", "View all barcodes in the registry")
        {
        }

        public override bool Execute(string[] input, TerminalWindow window)
        {
            if (window.AppContext is not DataContext<Barcode>)
            {
                throw new IllegalContextException(); // For the command to be processed, we MUST be in the context of the barcode registry
            }

            DataContext<Barcode> context = (DataContext<Barcode>)window.AppContext;

            // Basic data view for now
            window.Color(ConsoleColor.Blue).Write("There are {0} total barcodes in the registry\n", context.Registry.InternalRegistry.Count());
            foreach(Barcode b in context.Registry.AllValues)
            {
                window.Color(ConsoleColor.Cyan)
                    .Write("[{0}] Barcode: {1} ({2}) - {3} - {4}\n", 
                        b.DateAdded, 
                        b.BarcodeNumber, 
                        b.BarcodeType.ToString(), 
                        b.ItemName, 
                        b.ItemDescription == null ? "": b.ItemDescription);
            }

            return true;
        }
    }
}
