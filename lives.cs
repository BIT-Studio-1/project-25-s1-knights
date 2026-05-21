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

namespace gameproject
{
    internal class Lives
    {

        public static async Task Arjun()
        {
            for (int i = 0; i < spawned; i++)
            {
                if (invaderX[i] == -1) continue; // skip destroyed invaders
                if (invaderX[i] == playerX && invaderY[i] == playerY)
                {
                    lives--;
                    // Arjun - setting this because of need to skip or destroy the invander from screen after hitting
                    await Task.Delay(100);
                }
            }

            if (lives <= 0) start = false;

            string livesText = $"Lives: {lives}";
            SetCursorPosition(WindowWidth - livesText.Length, 0);
            Write(livesText);
        }

    }
}
