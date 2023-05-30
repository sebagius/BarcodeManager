using BarcodeManager.command;
using BarcodeManager.registry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarcodeManager.context
{
    /// <summary>
    /// Defines the base class for different App Contexts of the program. 
    /// This class allows for commands to belong to a single context
    /// </summary>
    public abstract class AppContext
    {
        private String _name;
        private String _description;

        private List<Command> _commands;

        public String Name { get { return _name; } }
        public String Description { get { return _description; } }

        public List<Command> Commands { get { return _commands; } }

        public AppContext(String name, String description) 
        { 
            this._name = name;
            this._description = description;
            this._commands = new List<Command>();
        }

        /// <summary>
        /// This method is overridden to handle the preparation of a new AppContext
        /// </summary>
        /// <param name="terminalWindow">The Terminal Window Handler object</param>
        /// <returns>this</returns>
        public virtual AppContext SwitchTo(TerminalWindow terminalWindow)
        {
            if (Program.TEST_ENVIRONMENT)
                return this;
            Console.Title = String.Format("{0} - {1}", _name, _description);
            terminalWindow.Processor.ClearCommandCache();
            terminalWindow.Clear();
            return this;
        }

        public abstract void PrintIntro(TerminalWindow terminalWindow);

        public abstract void HandleExit(TerminalWindow terminalWindow);
    }
}
