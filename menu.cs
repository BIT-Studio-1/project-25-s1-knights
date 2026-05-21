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
            while (menuStart)
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
                menuControl();
                menuOpperation();
            }

            static void menuControl()
            {

                if ((IsKeyDown(DownArrow) || IsKeyDown(S)) && (option < 3))
                {
                    option++;
                }
                else if ((IsKeyDown(UpArrow) || IsKeyDown(W)) && (option > 1))
                {
                    option--;

                }
            }

            static void menuOpperation()
            {

                if ((IsKeyDown(Spacebar) || IsKeyDown(Enter)) && (option == 1))
                {
                    start = true;
                    menuStart = false;
                    Clear();
                }
                else if ((IsKeyDown(Spacebar) || IsKeyDown(Enter)) && (option == 2))
                {

                    menuStart = false;
                    Clear();
                    WriteLine("MOVEMENT:    A and D        ARROW KEYS");
                    WriteLine("SHOOT:       SPACEBAR                 ");
                    BackgroundColor = ConsoleColor.White;
                    ForegroundColor = ConsoleColor.Black;
                    WriteLine("\n\n\n> RETURN  ");
                    ResetColor();

                    bool inControls = true;
                    while (inControls)
                    {
                        ConsoleKey controlKey = ReadKey(true).Key;
                        if (controlKey == Spacebar || controlKey == Enter)
                        {
                            inControls = false;
                            menuStart = true;
                        }
                    }


                }
                else if ((IsKeyDown(Spacebar) || IsKeyDown(Enter)) && (option == 3))
                {

                    menuStart = false;
                }


            }
        }
    }
}
