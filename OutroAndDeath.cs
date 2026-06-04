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
using static gameproject.Intro;
using static gameproject.asteroids;
using static gameproject.Program;

namespace gameproject
{
    internal class OutroAndDeath
    {
        public static bool ShowWin()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine(@"                                                                                          
▄▄▄    ▄▄▄   ▄▄▄▄    ▄▄    ▄▄           ▄▄      ▄▄  ▄▄▄▄▄▄   ▄▄▄   ▄▄     ▄▄        ▄▄    
 ██▄  ▄██   ██▀▀██   ██    ██           ██      ██  ▀▀██▀▀   ███   ██     ██        ██    
  ██▄▄██   ██    ██  ██    ██           ▀█▄ ██ ▄█▀    ██     ██▀█  ██     ██        ██    
   ▀██▀    ██    ██  ██    ██            ██ ██ ██     ██     ██ ██ ██     ██        ██    
    ██     ██    ██  ██    ██            ███▀▀███     ██     ██  █▄██     ▀▀        ▀▀    
    ██      ██▄▄██   ▀██▄▄██▀            ███  ███   ▄▄██▄▄   ██   ███     ▄▄        ▄▄    
    ▀▀       ▀▀▀▀      ▀▀▀▀              ▀▀▀  ▀▀▀   ▀▀▀▀▀▀   ▀▀   ▀▀▀     ▀▀        ▀▀    
                                                                                          
                                                                                          
                                                                       ");

            Console.ResetColor();

            Console.WriteLine();
            Console.WriteLine("YOU WIN!");
            Console.WriteLine();

            Console.WriteLine("[Y] Play Again");
            Console.WriteLine("[N] Exit Game");
            while (true)
            {

                if (IsKeyDown(Y))
                {
                    return true;

                }
                else if (IsKeyDown(N))
                {
                    return false;
                }


            }



        }

        public static bool ShowLose()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(@"                                                                                                     
▄▄▄    ▄▄▄   ▄▄▄▄    ▄▄    ▄▄            ▄▄          ▄▄▄▄      ▄▄▄▄    ▄▄▄▄▄▄▄▄     ▄▄        ▄▄    
 ██▄  ▄██   ██▀▀██   ██    ██            ██         ██▀▀██   ▄█▀▀▀▀█   ██▀▀▀▀▀▀     ██        ██    
  ██▄▄██   ██    ██  ██    ██            ██        ██    ██  ██▄       ██           ██        ██    
   ▀██▀    ██    ██  ██    ██            ██        ██    ██   ▀████▄   ███████      ██        ██    
    ██     ██    ██  ██    ██            ██        ██    ██       ▀██  ██           ▀▀        ▀▀    
    ██      ██▄▄██   ▀██▄▄██▀            ██▄▄▄▄▄▄   ██▄▄██   █▄▄▄▄▄█▀  ██▄▄▄▄▄▄     ▄▄        ▄▄    
    ▀▀       ▀▀▀▀      ▀▀▀▀              ▀▀▀▀▀▀▀▀    ▀▀▀▀     ▀▀▀▀▀    ▀▀▀▀▀▀▀▀     ▀▀        ▀▀    
                                                                                                    
                                                                                                     ");

            Console.ResetColor();

            Console.WriteLine("YOU LOSE!");
            Console.WriteLine();

            Console.WriteLine("[Y] Play Again");
            Console.WriteLine("[N] Exit Game");
            while (true)
            {
                
                if (IsKeyDown(Y))
                {
                    return true;
                    
                }
                else if (IsKeyDown(N))
                {
                    return false;
                }
                
                
            }


            //Console.ReadKey(true);
            //Console.ReadLine();//added so it doesnt close when you die
        }

    }
}
