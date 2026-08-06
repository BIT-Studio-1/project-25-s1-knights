using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using static gameproject.Globals;
using static System.Console;
using static System.ConsoleKey;
using static System.Math;
using System.Diagnostics;
using static gameproject.Character;
using static gameproject.Lives;
using static gameproject.invaders;
using static gameproject.Levels;
using static gameproject.Menu;


namespace gameproject
{
    internal class Intro
    {
       public static void initialScreen()
        {
            string text = "PRESS ANY KEY TO BEGIN";
            SetCursorPosition((consoleWidth/2 - (text.Length/2)), consoleHeight/2);
            Write(text);
            ReadKey(true);
            Clear();
            introScreen();
            
            
        }

        static void introScreen()
        {
            SetCursorPosition(0,WindowHeight/2 -5);
            Write("\r\n\r\n                          _____  _____ _____ _____   _____ _   ___      __     _____  ______ _____   _____ \r\n                   /\\    / ____|/ ____|_   _|_   _| |_   _| \\ | \\ \\    / /\\   |  __ \\|  ____|  __ \\ / ____|\r\n                  /  \\  | (___ | |      | |   | |     | | |  \\| |\\ \\  / /  \\  | |  | | |__  | |__) | (___  \r\n                 / /\\ \\  \\___ \\| |      | |   | |     | | | . ` | \\ \\/ / /\\ \\ | |  | |  __| |  _  / \\___ \\ \r\n                / ____ \\ ____) | |____ _| |_ _| |_   _| |_| |\\  |  \\  / ____ \\| |__| | |____| | \\ \\ ____) |\r\n               /_/    \\_\\_____/ \\_____|_____|_____| |_____|_| \\_|   \\/_/    \\_\\_____/|______|_|  \\_\\_____/ \r\n\r\n               ");
            string text = "dodge the asteroids and shoot the invaders";
            WriteLine("\n\n\n\n");
            CursorLeft = WindowWidth/2 - text.Length/2;
            Write(text);
            Thread.Sleep(1000);
            menuStart = true;
        }
        
    }
}
