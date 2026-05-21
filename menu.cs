using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using static gameproject.Globals;
using static System.Console;
using static System.ConsoleKey;
using static System.Math;
using System.Diagnostics;
using static gameproject.Bullet;
using static gameproject.Program;
using static gameproject.Character;

namespace gameproject
{
    internal class Menu
    {
        static int option = 1;

        public static void startmenu()
        {
            while (menustart)
            {
                Clear();

                if (option == 1)
                {
                    BackgroundColor = ConsoleColor.White;
                    ForegroundColor = ConsoleColor.Black;
                    WriteLine("> PLAY    ");
                    ResetColor();
                    WriteLine("  CONTROLS");
                    WriteLine("  EXIT    ");
                }
                else if (option == 2)
                {
                    WriteLine("  PLAY    ");
                    BackgroundColor = ConsoleColor.White;
                    ForegroundColor = ConsoleColor.Black;
                    WriteLine("> CONTROLS");
                    ResetColor();
                    WriteLine("  EXIT    ");
                }
                else if (option == 3)
                {
                    WriteLine("  PLAY    ");
                    WriteLine("  CONTROLS");
                    BackgroundColor = ConsoleColor.White;
                    ForegroundColor = ConsoleColor.Black;
                    WriteLine("> EXIT    ");
                    ResetColor();
                }
                ReadKey(true);
            }
        }
    }
}
