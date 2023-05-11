using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarcodeManager.command
{
    public class HelpCommand : Command
    {
        public HelpCommand() : base("help", null)
        {
            MinProceeding = MaxProceeding = 0;
            
        }

        public override bool Execute(string[] input, TerminalWindow window)
        {
            foreach(Command c in window.Processor.ContextCommands)
            {
                if (c.HelpMessage == null)
                    continue;

                window.Write("{0} - {1}", c.Name, c.HelpMessage).EndLine();
            }

            return true;
        }
    }
}
