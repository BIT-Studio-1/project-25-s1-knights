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
using static gameproject.Menu;

namespace gameproject
{
    internal class Levels
    {
        public static void Level() //Stephanie
        {
            //LEVEL 1
            if (level == 1)
            {
                maxInvaders = 5;
                invaderSpeed = 10; // was 300
                spawnRate = 10;
            }
            else if (level == 2)
            {
                maxInvaders = 8;
                invaderSpeed =3;
                spawnRate = 8;
            }
            else if (level == 3)
            {
                maxInvaders = 10;
                invaderSpeed = 8;
                spawnRate = 6;
            }
            else if (level == 4)
            {
                maxInvaders = 12;
                invaderSpeed = 7;
                spawnRate = 5;
            }
            else if (level == 5)
            {
                maxInvaders = 15;
                invaderSpeed = 6;
                spawnRate = 3;
            }

            // WIN GAME
            if (level == 5 && enemiesKilled >= maxInvaders)
            {
                start = false;

                Invaders.Clear();
                PlayerBullets.Clear();
                Clear();
                string winText = "YOU WIN! GAME COMPLETE!";
                SetCursorPosition(WindowWidth / 2 - winText.Length, WindowHeight / 2);
                Write(winText);

                ReadKey(true);
                return;
            }



            //Level Progression: move to the next level.
            if (enemiesKilled >= maxInvaders && level <=5)
            {
                level++;
                enemiesKilled = 0;

                Invaders.Clear();
                PlayerBullets.Clear();

                Clear();
                SetCursorPosition(WindowWidth / 2 - 5, WindowHeight / 2);
                Write($"Level {level}");
                Thread.Sleep(1000);
                Clear();

                SetCursorPosition(WindowWidth / 2 - 6, WindowHeight / 2 - 1);
                Write("GET READY!");
                Thread.Sleep(500);

                Clear();
                return;
            }

            if (level <= 5)
            {
                //SHOW LEVEL
                string levelText = $"Level: {level} | Kills: {enemiesKilled}/{maxInvaders}";
                SetCursorPosition(0, 0);
                Write(levelText);

            }

        }

    }
}
