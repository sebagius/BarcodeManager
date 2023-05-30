using BarcodeManager.context.barcode;
using BarcodeManager.context;
using BarcodeManager.exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BarcodeManager.command
{
    public class BarcodeDeleteCommand : Command
    {
        public BarcodeDeleteCommand() : base("delete", "Deleted a barcode")
        {
        }

        public override bool Execute(string[] input, TerminalWindow window)
        {
            String? barcodeNumber = readCommandString(String.Format("Please enter the barcode number to delete (or \"exit\" to exit)\n"), window);

            if (String.IsNullOrEmpty(barcodeNumber))
            {
                window.Color(ConsoleColor.Red).Write("Please enter the barcode value!\n");
                return Execute(input, window);
            }

            if (barcodeNumber.Equals("exit", StringComparison.OrdinalIgnoreCase))
            {
                window.Color(ConsoleColor.Red).Write("Exited Command!\n");
                return true;
            }

            Regex regex = new Regex("-\\s+", RegexOptions.IgnoreCase); //Replace dashes and whitespaces with nothing
            barcodeNumber = regex.Replace(barcodeNumber, "");

            if (window.AppContext is not DataContext<Barcode>)
            {
                throw new IllegalContextException(); // For the command to be processed, we MUST be in the context of the barcode registry
            }

            DataContext<Barcode> context = (DataContext<Barcode>)window.AppContext;
            if(!context.Registry.Has(barcodeNumber))
            {
                window.Color(ConsoleColor.Red).Write("That barcode does not exist in the registry!\n");
                return Execute(input, window);
            }

            context.Registry.Remove(barcodeNumber);
            window.Color(ConsoleColor.Yellow)
                    .Write("Barcode removed successfully!");

            return true;
        }
    }
}
