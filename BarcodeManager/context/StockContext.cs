using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarcodeManager.context
{
    public class StockContext : AppContext
    {
        public StockContext() : base("Stock Application Context", "Stock Management")
        {
        }

        public override void HandleExit(TerminalWindow terminalWindow)
        {
            
        }

        public override void PrintIntro(TerminalWindow terminalWindow)
        {
            terminalWindow.Color(ConsoleColor.Yellow)
                .Write("Stock Management Interface")
                .EndLine()
                .Color(ConsoleColor.DarkRed)
                .Write("Unfortunately due to time constraints I was unable to finish this section of the program. The current program still demonstrates OOP concepts and design patterns.")
                .EndLine();
        }

        public override AppContext SwitchTo(TerminalWindow terminalWindow)
        {
            base.SwitchTo(terminalWindow);
            PrintIntro(terminalWindow);

            return this;
        }
    }
}
