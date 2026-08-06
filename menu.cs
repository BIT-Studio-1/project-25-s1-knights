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
using static gameproject.keyboard;

namespace gameproject
{
    internal class Menu
    {
        static int option = 1;
        private static bool notReset = true;

        public static void startmenu()
        {
            SetCursorPosition(0, 0);
            while (menuStart)
            {
                Clear();
                notReset = true;
                while (notReset)
                {
                    
                if (option == 1)
                {
                    BackgroundColor = ConsoleColor.White;
                    ForegroundColor = ConsoleColor.Black;
                    WriteLine("> PLAY    ");
                    ResetColor();
                    WriteLine("  CONTROLS");
                    WriteLine("  EXIT    ");
                    SetCursorPosition(0, WindowHeight / 2);
                    WriteLine("TIP:  some invaders will drop and item which gives an extra life when picked up.");

                }
                else if (option == 2)
                {
                    WriteLine("  PLAY    ");
                    BackgroundColor = ConsoleColor.White;
                    ForegroundColor = ConsoleColor.Black;
                    WriteLine("> CONTROLS");
                    ResetColor();
                    WriteLine("  EXIT    ");
                    SetCursorPosition(0, WindowHeight / 2);
                    WriteLine("TIP:  some invaders will drop and item which gives an extra life when picked up.");

                }
                else if (option == 3)
                {
                    WriteLine("  PLAY    ");
                    WriteLine("  CONTROLS");
                    BackgroundColor = ConsoleColor.White;
                    ForegroundColor = ConsoleColor.Black;
                    WriteLine("> EXIT    ");
                    ResetColor();
                    SetCursorPosition(0, WindowHeight / 2);
                    WriteLine("TIP:  some invaders will drop and item which gives an extra life when picked up.");
                    
                }
                
                menuControl();
                menuOpperation();
                }

            }

            static void menuControl()
            {

                if ((IsKeyDown(DownArrow) || IsKeyDown(S)) && (option < 3))
                {
                    Thread.Sleep(releaseRelation);
                    option++;
                    notReset = false;
                }
                else if ((IsKeyDown(UpArrow) || IsKeyDown(W)) && (option > 1))
                {
                    Thread.Sleep(releaseRelation);
                    option--;
                    notReset = false;
                }
            }

            static void menuOpperation()
            {

                if ((IsKeyDown(Spacebar) || IsKeyDown(Enter)) && (option == 1))
                {
                    start = true;
                    menuStart = false;
                    notReset = false;
                    Clear();
                }
                else if ((IsKeyDown(Spacebar) || IsKeyDown(Enter)) && (option == 2))
                {

                    menuStart = false;
                    Clear();
                    WriteLine("MOVEMENT:    A AND D        ARROW KEYS");
                    WriteLine("SHOOT:       SPACEBAR                 ");
                    WriteLine("PAUSE:       ESCAPE                   ");
                    WriteLine("SHOOT ROCKET: R ");
                    BackgroundColor = ConsoleColor.White;
                    ForegroundColor = ConsoleColor.Black;
                    WriteLine("\n\n\n> RETURN  ");
                    ResetColor();

                    bool inControls = true;
                    while (inControls)
                    {
                        
                        if (IsKeyDown(Spacebar) || IsKeyDown(Enter))
                        {
                            inControls = false;
                            menuStart = true;
                        }
                    }


                }
                else if ((IsKeyDown(Spacebar) || IsKeyDown(Enter)) && (option == 3))
                {
                    start = false;
                    menuStart = false;
                    Environment.Exit(0);


                }
            }
        }
    }
}
