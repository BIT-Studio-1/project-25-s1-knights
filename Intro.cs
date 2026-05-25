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
            introScreen();
        }

        static void introScreen()
        {
            Thread.Sleep(100);
            menuStart = true;
        }
    }
}
