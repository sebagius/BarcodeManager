﻿using BarcodeManager.command;
using BarcodeManager.context;

namespace BarcodeManager
{
    /// <summary>
    /// This class is for handling of console input and output
    /// </summary>
    public class TerminalWindow
    {
        /// <summary>
        /// The current app context
        /// </summary>
        private context.AppContext _context;

        /// <summary>
        /// The command processor
        /// </summary>
        private CommandProcessor _processor;

        /// <summary>
        /// The saved cursor position for writing
        /// </summary>
        private int _cursorX, _cursorY;

        /// <summary>
        /// The buffer for reading input
        /// </summary>
        private String _readBuffer = "";

        /// <summary>
        /// The saved cursor position for typing
        /// </summary>
        private int _typeCursor, _endInline;

        /// <summary>
        /// Creates a handler for the terminal window
        /// </summary>
        public TerminalWindow()
        {
            this._context = new MainContext();
            this._processor = new CommandProcessor(this);
        }

        /// <summary>
        /// Returns the current app context or updates the context and runs the SwitchTo function
        /// </summary>
        public context.AppContext AppContext { get { return _context; } set { _context = value.SwitchTo(this); } }

        public CommandProcessor Processor { get { return _processor; } }

        /// <summary>
        /// Prepares the terminal - usually used to prepare main context
        /// </summary>
        public void Prepare()
        {
            _context.SwitchTo(this);
        }

        public TerminalWindow ResetContext()
        {
            _context.SwitchTo(this);
            return this;
        }

        /// <summary>
        /// Changes the terminal colour
        /// </summary>
        /// <param name="color">ConsoleColor to change to</param>
        /// <returns>this</returns>
        public TerminalWindow Color(ConsoleColor color)
        {
            Console.ForegroundColor = color;
            return this;
        }

        /// <summary>
        /// Writes a message to the console with a potential format
        /// </summary>
        /// <param name="message">the string message</param>
        /// <param name="format">the param objects to format</param>
        /// <returns>this</returns>
        public TerminalWindow Write(string message, params object[] format) 
        {
            RestoreCursor();
            Console.Write(message, format);
            StoreCursor();
            UpdateCursor();

            return this;
        }

        /// <summary>
        /// Writes a message to the console with a potential format AT the cursor position
        /// </summary>
        /// <param name="message">the string message</param>
        /// <param name="format">the param objects to format</param>
        /// <returns>this</returns>
        public TerminalWindow WriteInline(string message, params object[] format)
        {
            Console.Write(message, format);
            _endInline = Console.CursorLeft;

            return this;
        }

        /// <summary>
        /// Prints the end of the line to the console
        /// </summary>
        /// <returns>this</returns>
        public TerminalWindow EndLine()
        {
            RestoreCursor();
            Console.ForegroundColor= ConsoleColor.White;
            Console.WriteLine();
            StoreCursor();
            UpdateCursor();

            return this;
        }

        /// <summary>
        /// Waits for a key to be typed before proceeding
        /// </summary>
        public void WaitKey()
        {
            if (Program.TEST_ENVIRONMENT)
                return;
            Console.ReadKey();
        }

        private void ClearInputLine(bool clear = false)
        {
            UpdateCursor(0, 5);
            Color(ConsoleColor.White);
            for (int i = 0; i < Console.WindowWidth * 6; i++)
            {
                Console.Write(" ");
            }
            
            if(!clear)
            {
                UpdateCursor();
                Console.Write(_readBuffer);
            }
                
        }

        /// <summary>
        /// Reads input and produces autocompletes
        /// </summary>
        public void AwaitInput()
        {
            char c;
            while ((c = Console.ReadKey().KeyChar) != '\r')
            {
                if(c == '\b' && _readBuffer.Length > 0)
                {
                    _readBuffer = _readBuffer.Substring(0, _readBuffer.Length - 1);
                    
                } else if(c == '\t')
                {
                    String[]? auto = _processor.AutoComplete(_readBuffer);
                    if (auto != null && auto.Length > 0)
                    {
                        _readBuffer += auto[0];
                    }
                    ClearInputLine();
                    continue;
                } else
                {
                    _readBuffer += c;
                }

                ClearInputLine();

                String[]? autoCompletes = _processor.AutoComplete(_readBuffer);

                if (autoCompletes == null)
                {
                    if (Console.CursorLeft >= _endInline)
                        continue;

                    ClearInputLine();
                    _endInline = -1;
                    continue;
                }
                    

                _typeCursor = Console.CursorLeft;
                Color(ConsoleColor.DarkYellow);
                for (int i = 0; i < autoCompletes.Length; i++)
                {
                    UpdateCursor(_typeCursor, i);
                    WriteInline(autoCompletes[i]);
                }
                UpdateCursor(_typeCursor);
                Color(ConsoleColor.White);
            }
            ClearInputLine(true);
            _processor.ProcessCommand(_readBuffer);
            _readBuffer = "";
        }

        /// <summary>
        /// Updates the cursor to the bottom of the terminal for input
        /// </summary>
        private void UpdateCursor(int left = 0, int fromBottom = 0)
        {
            if (Program.TEST_ENVIRONMENT)
                return;
            Console.SetCursorPosition(left, Console.WindowHeight - 1 - fromBottom);
        }

        /// <summary>
        /// Saves the current cursor position for WRITING to the console
        /// </summary>
        private void StoreCursor()
        {
            if (Program.TEST_ENVIRONMENT)
                return;
            _cursorX = Console.CursorLeft;
            _cursorY = Console.CursorTop;
        }

        /// <summary>
        /// Restores the current cursor position for WRITING to the console
        /// </summary>
        private void RestoreCursor()
        {
            if (Program.TEST_ENVIRONMENT)
                return;
            Console.SetCursorPosition(_cursorX, _cursorY);
        }

        public void Clear()
        {
            Console.Clear();
            _readBuffer = "";
            _endInline = 0;
            _cursorX = 0;
            _cursorY = 0;
        }
    }
}
