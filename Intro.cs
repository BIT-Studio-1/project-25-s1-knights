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
            StarWarsTheme();
            new Thread(StarWarsTheme) { IsBackground = true }.Start();
        }

        static void introScreen()
        {
            SetCursorPosition(0, 0);
            Write("\r\n\r\n ____  _                _   _       _     _           \r\n|  _ \\| | __ _  ___ ___| | | | ___ | | __| | ___ _ __ \r\n| |_) | |/ _` |/ __/ _ \\ |_| |/ _ \\| |/ _` |/ _ \\ '__|\r\n|  __/| | (_| | (_|  __/  _  | (_) | | (_| |  __/ |   \r\n|_|   |_|\\__,_|\\___\\___|_| |_|\\___/|_|\\__,_|\\___|_|   \r\n\r\n");
            Thread.Sleep(1200);
            menuStart = true;
        }
        public static void StarWarsTheme()
        {
            // Star Wars Main Theme
            Beep(440, 500);   // A
            Beep(440, 500);   // A
            Beep(440, 500);   // A
            Beep(349, 350);   // F
            Beep(523, 150);   // C
            Beep(440, 500);   // A
            Beep(349, 350);   // F
            Beep(523, 150);   // C
            Beep(440, 1000);  // A

            Beep(659, 500);   // E
            Beep(659, 500);   // E
            Beep(659, 500);   // E
            Beep(698, 350);   // F
            Beep(523, 150);   // C
            Beep(415, 500);   // G#
            Beep(349, 350);   // F
            Beep(523, 150);   // C
            Beep(440, 1000);  // A
        }
    }
}
