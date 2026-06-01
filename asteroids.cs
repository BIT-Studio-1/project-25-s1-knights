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
using static gameproject.Asteroid;

namespace gameproject
{
    internal class asteroids
    {

        public static void newAsteroids()
        {
            asteroidMoveTimer++;  
            asteroidSpawnTimer++;

            if (level == 1)  //handles speed per level for asteroids, increasing move rate per level, need to do rest of code so it works properly
            {
                maxAsteroids = 0;
                asteroidMoveRate = 5;
            }

            if (level == 2)
            {
                maxAsteroids = 2;
                asteroidMoveRate = 4;
            }

            if (level == 3)
            {
                maxAsteroids = 3;
                asteroidMoveRate = 3;
            }

            if (level == 4)
            {
                maxAsteroids = 3;
                asteroidMoveRate = 2;
            }

            if (level == 5)
            {
                maxAsteroids = 3;
                asteroidMoveRate = 1;
            }

            if ((asteroidSpawnTimer >= asteroidSpawnRate) && (Asteroids.Count < maxAsteroids)); 
            {
                Asteroids.Add(new Asteroid { x = rand.Next(1, 5), y = 0 }); //spawn rate is 20, should be low enough to not have them spawn so frequently, also spawns asteroid in corner
                asteroidSpawnTimer = 0;
            }
            for (int i = Asteroids.Count -1; i >= 0; i--)
            {
                if (Asteroids[i].x >= consoleWidth)
                {
                    Asteroids[i].x = rand.Next(1, 5);
                }

                if (Asteroids[i].y >= consoleHeight)
                {
                    Asteroids[i].y = rand.Next(consoleHeight);
                }

                if ((Asteroids[i].x >= 0) && (Asteroids[i].y >=0) && (Asteroids[i].x < consoleWidth) && (Asteroids[i].y < consoleHeight))
                {
                    SetCursorPosition(Asteroids[i].x, Asteroids[i].y);
                    Write(" ");
                    Asteroids[i].x += 1;
                    Asteroids[i].y += 1;
                }
            }
            

        }

    }
}


