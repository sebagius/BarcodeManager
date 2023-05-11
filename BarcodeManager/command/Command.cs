using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BarcodeManager.command
{
    public abstract class Command
    {
        private String _name;
        private String? _help;
        private String[] _autoCompletes;
        private readonly List<Command> _subCommands;
        private int _position, _minimumProceeding, _maximumProceeding;

        public Command(String name, String? help) : this(name, help, new string[] { }, new List<Command>())
        {

        }

        public Command(String name, String? help, String autoComplete) : this(name, help, new string[] { autoComplete }, new List<Command>())
        {
            
        }

        public Command(String name, String? help, String[] autoCompletes) : this(name, help, autoCompletes, new List<Command>())
        {

        }

        public Command(String name, String? help, String[] autoCompletes, List<Command> subCommands)
        {
            _name = name;
            _help = help;
            _autoCompletes = autoCompletes;
            _subCommands = subCommands;
        }

        public String Name { get { return _name; } }
        public String? HelpMessage { get { return _help; } }

        public String[] AutoCompletes { get { return _autoCompletes; } }

        public int Position { get { return _position; } }

        protected int MinProceeding { get { return _minimumProceeding; } set { _minimumProceeding = value; } }

        protected int MaxProceeding { get { return _maximumProceeding; } set { _maximumProceeding = value; } }

        /// <summary>
        /// Will forward the input onto the correct subcommand or execute this command if the right argument is reached
        /// </summary>
        /// <param name="input">the input string</param>
        /// <param name="window">the terminal window</param>
        /// <returns>successful run of command</returns>
        public bool Process(String[] input, TerminalWindow window)
        {
            foreach(Command c in _subCommands)
            {
                if (c.Name != input[0])
                    continue;

                c.Process(input.Skip(1).ToArray(), window);
            }

            //TODO check arguments

            return this.Execute(input.Skip(1).ToArray(), window); //Runs this commands execution if there are no subcommands OR a matching subcommand is not found
        }

        /// <summary>
        /// Executes command functionality from subclass
        /// </summary>
        /// <param name="input">the input string to process</param>
        /// <param name="window">the terminal window</param>
        /// <returns>successful run of command</returns>
        public abstract bool Execute(String[] input, TerminalWindow window);

        /// <summary>
        /// Returns a predictive subcommand autocomplete
        /// </summary>
        /// <param name="input">the input text</param>
        /// <param name="split">the split text</param>
        /// <returns>predicted text</returns>
        public String[]? PredictSubCommand(String input, String[] split)
        {
            if(_subCommands.Count == 0)
            {
                List<String> result = new List<String>();
                int i = 0;
                String predictiveName = _name;

                if(_autoCompletes.Length == 0)
                {
                    return new string[] { _name[input.Length..] };
                }

                foreach(String s in _autoCompletes)
                { 
                    if (i == 4)
                        break;

                    if (i > 0 && !String.IsNullOrEmpty(predictiveName))
                    {
                        predictiveName = "";
                        for(int j  = 0; j < _name.Length; j++)
                        {
                            predictiveName += " ";
                        }
                    }
                    String basicPredictive = _name + " " + s;
                    String predictiveResult = basicPredictive;
                    if (i > 0)
                        predictiveResult = predictiveResult.Replace(_name, predictiveName);
                    if (!basicPredictive.StartsWith(input))
                        continue;
                    result.Add(predictiveResult.Substring(input.Length));
                    i++;
                }

                if (result.Count == 0)
                    return null;
                
                return result.ToArray();
            }

            //TODO: ADD SUBCOMMAND PREDICTION
            return null;
        }
    }
}
