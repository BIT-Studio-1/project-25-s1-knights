using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gameproject
{
    internal class OutroAndDeath
    {
        public static void ShowWin()
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
            Console.WriteLine("Press any key to exit....");
            Thread.Sleep(1000);
            Console.ReadKey(true);
            Environment.Exit(0);

            

            
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

            while(Program.IsKeyDown(ConsoleKey.Y) || Program.IsKeyDown(ConsoleKey.N) || Program.IsKeyDown(ConsoleKey.LeftArrow) || Program.IsKeyDown(ConsoleKey.Spacebar) || Program.IsKeyDown(ConsoleKey.RightArrow))
            {
                Thread.Sleep(10);
            }

            Thread.Sleep(100);
            while(Console.KeyAvailable)
                Console.ReadKey(true);

            while (true)
            {
                ConsoleKey key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.Y) return true;
                if (key == ConsoleKey.N) return false;
            }

            //Thread.Sleep(1000);
            //Console.ReadKey(true);
            //Environment.Exit(0);

            //while (Console.KeyAvailable)
            //    Console.ReadKey(true);

            ////while(Program.IsKeyDown(ConsoleKey.LeftArrow) || Program.IsKeyDown(ConsoleKey.RightArrow) || Program.IsKeyDown(ConsoleKey.UpArrow) || Program.IsKeyDown(ConsoleKey.DownArrow))
            ////{
            ////    Thread.Sleep(20);
            ////}

            //Thread.Sleep(100);

            //while (true)
            //{


            //    ConsoleKey key = Console.ReadKey(true).Key;
            //    if (key == ConsoleKey.Y)
            //        return true;
            //    if (key == ConsoleKey.N)
            //        return false;
            //    //char input = Console.ReadKey(true).KeyChar;
            //    //if (input == 'y' || input == 'Y')
            //    //    return true;
            //    //if (input == 'n' || input == 'N')
            //    //    return false;
            //    //}

            //    //while (Program.IsKeyDown(ConsoleKey.Y) || Program.IsKeyDown(ConsoleKey.N))
            //    //{
            //    //    Thread.Sleep(10);
            //    //}

            //    //while (true)
            //    //{ 

            //    //if (Program.IsKeyDown(ConsoleKey.Y))
            //    //    return true;

            //    //if (Program.IsKeyDown(ConsoleKey.N))
            //    //    return false;

            //    //Thread.Sleep(10);

            //}


            //Console.ReadKey(true);
            //Console.ReadLine();//added so it doesnt close when you die
        }

    }
}
