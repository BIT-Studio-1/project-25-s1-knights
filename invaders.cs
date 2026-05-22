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
using static gameproject.Levels;
using static gameproject.Menu;
using static gameproject.Invader;

namespace gameproject
{
    internal class invaders
    {
        public static async Task newInvader()
        {


            Random rand = new Random();

            //Arjun - moving this variables to global. in global it declared as a int only. if need to use all invanders i need to declare it in global
            //int[] invaderX = new int[1000];
            //int[] invaderY = new int[1000];
            //int spawned = 0;
            //int spawnTimer = 0;
            int finished = 0;
            int[] meteorX = new int[1000];
            int[] meteorY = new int[1000];
            int meteorSpawn = 0;
            int meteorSpawnTimer = 0;

            while (true)
            {
                int consoleWidth = Console.WindowWidth;
                int consoleHeight = Console.WindowHeight;
                Write(' ');
                

                


                spawnTimer++;
                if (spawnTimer >= spawnRate && Invaders.Count < maxInvaders)
                {
                    Invaders.Add(new Invader{ x = rand.Next(Console.WindowWidth), y = 0}); // Spaawning randomly along x axis at 0 y position

                    spawnTimer = 0;
                }

                if ((meteorSpawnTimer >=5) && (meteorSpawn < 2))
                {
                    meteorX[meteorSpawn] = rand.Next(consoleWidth);
                    meteorY[meteorSpawn] = 0;
                    meteorSpawn++;
                    meteorSpawnTimer = 0;
                }



                 

                
                foreach (Invader invader in Invaders)
                {
                    invader.Move();
                    
                    if (invader.y >= Console.WindowHeight)
                    {
                        invader.y = 0;
                    }
                    SetCursorPosition(invader.x, invader.y);
                    Write("X");
                }

                

                for (int j = 0; j < meteorSpawn; j++)
                {
                    meteorY[j]++;
                    if (meteorY[j] >= Console.WindowHeight)
                    {
                        meteorY[j] = 0;
                    }
                    Console.SetCursorPosition(meteorX[j], meteorY[j]);
                    Console.Write("O");
                }






                    await Task.Delay(invaderSpeed);
            }
        }
    }
}
