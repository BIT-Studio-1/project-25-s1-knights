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


            
        
        
        public static void updateinvaders()
        {

            
            Random rand = new Random();


            spawnTimer++;
            
            
                


                
            if (spawnTimer >= spawnRate && Invaders.Count < maxInvaders)
            {
                Invaders.Add(new Invader{ x = rand.Next(consoleWidth), y = 0}); // Spaawning randomly along x axis at 0 y position

                spawnTimer = 0;
            }

            for (int i = Invaders.Count - 1; i >= 0; i--)
            {
                if (Invaders[i].y >= 0 && Invaders[i].y < WindowHeight && Invaders[i].x < WindowWidth)
                {
                    SetCursorPosition(Invaders[i].x, Invaders[i].y);

                    Write(' ');
                }
                
                Invaders[i].Move();

                if (Invaders[i].y >= WindowHeight)
                {
                    Invaders[i].y = 0;
                }

                if (Invaders[i].x < WindowWidth && Invaders[i].y < WindowHeight)
                {
                    SetCursorPosition(Invaders[i].x, Invaders[i].y);
                    Write("X");
                }
            }

                
            
        }
    }
}
