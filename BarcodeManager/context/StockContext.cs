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

        public override AppContext SwitchTo(TerminalWindow terminalWindow)
        {
            base.SwitchTo(terminalWindow);
            terminalWindow.Color(ConsoleColor.Yellow)
                .Write("Stock Management Interface")
                .EndLine();

            return this;
        }
    }
}
