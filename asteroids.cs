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
    internal class asteroids
    {

        public static void newAsteroids()
        {
            asteroidMoveTimer++;
            asteroidSpawnTimer++;

            if (level == 1)
            {
                asteroidMoveRate = 5;
            }

            if (level == 2)
            {
                asteroidMoveRate = 4;
            }

            if (level == 3)
            {
                asteroidMoveRate = 3;
            }

            if (level == 4)
            {
                asteroidMoveRate = 2;
            }

            if (level == 5)
            {
                asteroidMoveRate = 1;
            }

        }

    }
}


