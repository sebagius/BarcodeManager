using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarcodeManager.command
{
    public class CommandProcessor
    {
        private TerminalWindow _window;
        private List<Command> _globalCommands;
        private List<Command> _commandCache;

        public CommandProcessor(TerminalWindow window) 
        {
            _window = window;
            _globalCommands = new List<Command>
            {
                new ExitCommand(),
                new HelpCommand()
            };

            _commandCache = new List<Command>();
        }

        public List<Command> ContextCommands { get {
                if (_commandCache.Count > 0)
                    return _commandCache;

                List<Command> commands = new List<Command>();
                commands.AddRange(_window.AppContext.Commands);
                commands.AddRange(_globalCommands);
                _commandCache = commands;
                return commands;
            } }

        public void ClearCommandCache()
        {
            this._commandCache.Clear();
        }

        public bool ProcessCommand(String command)
        {
            String[] split = command.Split(" ");
            Command? run = null;

            foreach(Command s in ContextCommands)
            {
                if (s.Name != split[0])
                    continue;
                run = s;
                break;
            }

            if (run == null)
            {
                _window.Color(ConsoleColor.Red).Write("Invalid Command!").EndLine();
                return false;
            }

            _window.ResetContext();
            return run.Process(split, _window);
        }

        public String[]? AutoComplete(String text)
        {
            if (text.Length < 2)
                return null;

            String textL = text.ToLower();
            String[] split = text.Split(" ");

            foreach(Command c in ContextCommands)
            {
                String key = c.Name;
                if(key.StartsWith(textL) || key.Equals(textL.Split(" ")[0]))
                {
                    return c.PredictSubCommand(text, textL, split);
                }
            }

            return null;
        }
    }
}
