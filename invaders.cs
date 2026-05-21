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
                if (spawnTimer >= 10 && spawned < 15)
                {
                    invaderX[spawned] = rand.Next(consoleWidth);
                    invaderY[spawned] = 0;
                    spawned++;
                    spawnTimer = 0;
                }

                if ((meteorSpawnTimer >=5) && (meteorSpawn < 2))
                {
                    meteorX[meteorSpawn] = rand.Next(consoleWidth);
                    meteorY[meteorSpawn] = 0;
                    meteorSpawn++;
                    meteorSpawnTimer = 0;
                }


                for (int i = 0; i < spawned; i++)


                {
                    invaderY[i]++;
                    if (invaderY[i] >= Console.WindowHeight)

                    {
                        invaderY[i] = 0;
                    }
                    if (invaderY[i] == -1)
                    {
                        continue;
                    }
                    Console.SetCursorPosition(invaderX[i], invaderY[i]);
                    Console.Write("X");


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






                    await Task.Delay(300);
            }
        }
    }
}
