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
using static gameproject.invaders;
using static gameproject.Levels;
using static gameproject.Menu;

namespace gameproject
{
    internal class Lives
    {

        public static void CheckLives()
        {
            for (int i = 0; i < Invaders.Count; i++)
            {
                if (Invaders[i].x == -1) continue; // skip destroyed Invaders
                if (Invaders[i].x == playerX && Invaders[i].y == playerY)
                {
                    Life--;
                    // Arjun - setting this because of need to skip or destroy the invander from screen after hitting
                    // Explosion + destroy invader
                    //await ExplosionAnimation(playerX, playerY);
                    //await Task.Delay(1000);
                }
            }

            if (Life <= 0) start = false;

            string livesText = $"Lives: {Life}";
            SetCursorPosition(WindowWidth - livesText.Length, 0);
            Write(livesText);
        }

    }
}
