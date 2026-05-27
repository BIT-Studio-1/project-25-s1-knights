using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            while(true)
            {
                ConsoleKey key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.Y)
                {
                    return true;
                }

                if (key == ConsoleKey.N)
                {
                    return false;
                }

            }
        }

        public static bool ShowLose()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;

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
                ConsoleKey key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.Y)
                {
                    return true;
                }

                if (key == ConsoleKey.N)
                {
                    return false;
                }

            }


            //Console.ReadKey(true);
            //Console.ReadLine();//added so it doesnt close when you die
        }

    }
}
