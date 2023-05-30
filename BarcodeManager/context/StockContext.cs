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
            throw new NotImplementedException();
        }

        public override void PrintIntro(TerminalWindow terminalWindow)
        {
            terminalWindow.Color(ConsoleColor.Yellow)
                .Write("Stock Management Interface")
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
