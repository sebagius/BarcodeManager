using System;

namespace BarcodeManager
{
    public class Program
    {
        private TerminalWindow _window;

        private Program()
        {
            if(PROGRAM != null)
            {
                throw new MethodAccessException("PROGRAM can only be initialised once!");
            }

            _window = new TerminalWindow();
        }

        /// <summary>
        /// Gets the TerminalWindow handler for the console
        /// </summary>
        public TerminalWindow TerminalWindow { get { return _window; } }

        /// <summary>
        /// Starts the application
        /// </summary>
        public void Start()
        {
            _window.Prepare();

            while(true)
            {
                _window.AwaitInput();
            }
        }

        /// <summary>
        /// Singleton instance of Program
        /// </summary>
        public static Program? PROGRAM;

        /// <summary>
        /// Some code cannot be run as a part of the NUnit Test Environment
        /// </summary>
        public static bool TEST_ENVIRONMENT = false;

        /// <summary>
        /// Program entry point
        /// </summary>
        static void Main()
        {
            PROGRAM = new Program();
            PROGRAM.Start();
        }
    }
}