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
                invaderInfo.maxInvaders = 5;
                invaderInfo.invaderSpeed = 10; // was 300
                invaderInfo.spawnRate = 10;
                lifeInfo.dropMoveRate = 5;
                
            }
            else if (level == 2)
            {
                invaderInfo.maxInvaders = 8;
                invaderInfo.invaderSpeed =3;
                invaderInfo.spawnRate = 8;
               
            }
            else if (level == 3)
            {
                invaderInfo.maxInvaders = 10;
                invaderInfo.invaderSpeed = 8;
                invaderInfo.spawnRate = 6;
            }
            else if (level == 4)
            {
                invaderInfo.maxInvaders = 12;
                invaderInfo.invaderSpeed = 7;
                invaderInfo.spawnRate = 5;
            }
            else if (level == 5)
            {
                invaderInfo.maxInvaders = 15;
                invaderInfo.invaderSpeed = 6;
                invaderInfo.spawnRate = 3;
            }

            // WIN GAME
            if (level == 5 && enemiesKilled >= invaderInfo.maxInvaders)
            {
                start = false;

                invaderInfo.Invaders.Clear();
                playerInfo.PlayerBullets.Clear();
                Clear();
                string winText = "YOU WIN! GAME COMPLETE!";
                SetCursorPosition(WindowWidth / 2 - winText.Length, WindowHeight / 2);
                Write(winText);

                ReadKey(true);
                return;
            }



            //Level Progression: move to the next level.
            if (enemiesKilled >= invaderInfo.maxInvaders && level <=5)
            {
                level++;
                enemiesKilled = 0;

                invaderInfo.Invaders.Clear();
                playerInfo.PlayerBullets.Clear();

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
                string levelText = $"Level: {level} | Kills: {enemiesKilled}/{invaderInfo.maxInvaders}";
                SetCursorPosition(0, 0);
                Write(levelText);

            }

        }

    }
}
