using BarcodeManager.context;
using BarcodeManager.context.barcode;
using BarcodeManager.exception;
using BarcodeManager.registry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BarcodeManager.command
{
    internal class BarcodeAddCommand : Command
    {
        public BarcodeAddCommand() : base("add", "Adds a barcode onto the system", new string[] { "UPC", "EAN", "ISBN" })
        {
        }



        private Barcode? readInformation(TerminalWindow window, BarcodeType type)
        {
            // Barcode Validation
            int minimum, maximum;
            switch (type)
            {
                case BarcodeType.ISBN:
                    minimum = 10;
                    maximum = 13;
                    break;
                case BarcodeType.UPC:
                    minimum = maximum = 12;
                    break;
                case BarcodeType.EAN:
                    minimum = maximum = 13;
                    break;
                default:
                    minimum = maximum = -1;
                    break;
            }


            String? barcodeNumber = readCommandString(String.Format("Please enter the barcode number with min: {0} and max: {1} (or \"exit\" to exit)\n", minimum, maximum), window);

            if (String.IsNullOrEmpty(barcodeNumber))
            {
                window.Color(ConsoleColor.Red).Write("Please enter the barcode value!\n");
                return readInformation(window, type);
            }

            if (barcodeNumber.Equals("exit", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            Regex regex = new Regex("-\\s+", RegexOptions.IgnoreCase); //Replace dashes and whitespaces with nothing
            barcodeNumber = regex.Replace(barcodeNumber, "");

            if (barcodeNumber.Length < minimum || barcodeNumber.Length > maximum)
            {
                window.Color(ConsoleColor.Red).Write("Does not meet min/max requirements (min: {0}, max: {1})\n", minimum, maximum);
                return readInformation(window, type);
            }

            window.Color(ConsoleColor.Blue).Write("Barcode Number: {0}\n", barcodeNumber);
            Barcode.Builder builder = new Barcode.Builder(barcodeNumber)
                .BarcodeType(type)
                .ItemName(readItemName(window))
                .ItemDescription(readItemDescription(window))
                .DateAdded(DateTime.Now);


            return builder.Build();
        }

        /// <summary>
        /// Read Item Name (cannot be null)
        /// </summary>
        /// <param name="window"></param>
        /// <returns></returns>
        private String readItemName(TerminalWindow window)
        {
            String? itemName = readCommandString("Please enter the item name (or \"exit\" to exit)\n", window);
            if (String.IsNullOrEmpty(itemName))
            {
                window.Color(ConsoleColor.Red).Write("Please enter the barcode value!\n");
                return readItemName(window);
            }
            return itemName;
        }

        /// <summary>
        /// Read Item Description (can be null)
        /// </summary>
        /// <param name="window"></param>
        /// <returns></returns>
        private String readItemDescription(TerminalWindow window)
        {
            String? itemDescription = readCommandString("Please enter the item description (or \"exit\" to exit)\n", window);

            return itemDescription;
        }

        public override bool Execute(string[] input, TerminalWindow window)
        {
            if (input.Length != 1)
            {
                window.Color(ConsoleColor.Red)
                    .Write("Please enter a valid barcode type with the command!\n")
                    .Color(ConsoleColor.White);
                return false;
            }

            BarcodeType type;

            bool success = Enum.TryParse<BarcodeType>(input[0], true, out type);

            if (!success)
            {
                window.Color(ConsoleColor.Red)
                    .Write("Please enter a valid barcode type with the command!\n");
                return false;
            }

            Barcode? b = readInformation(window, type);

            if (b == null)
            {
                window.ResetContext();
                return true;
            }

            window.Color(ConsoleColor.Blue)
                .Write("Item Name: {0}\nItem Description: {1}\nDate Added: {2}\n", b.ItemName, b.ItemDescription == null ? "" : b.ItemDescription, b.DateAdded)
                .Color(ConsoleColor.Yellow)
                .Write("Confirm Add? (y/n)\n");

            if(window.GetConfirm())
            {
                window.ResetContext();
                
                
                if(window.AppContext is not DataContext<Barcode>)
                {
                    throw new IllegalContextException(); // For the command to be processed, we MUST be in the context of the barcode registry
                }

                DataContext<Barcode> context = (DataContext<Barcode>)window.AppContext;
                context.Registry.Add(b.BarcodeNumber, b);

                window.Color(ConsoleColor.Yellow)
                    .Write("Barcode added!");

                return true;
            }

            window.ResetContext();
            window.Color(ConsoleColor.Yellow)
                .Write("Barcode NOT added!");
            return true;
        }
    }
}
