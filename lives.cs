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

        //public static async Task lives()
        //{
        //    for (int i = 0; i < spawned; i++)
        //    {
        //        if (invader[i].x == -1) continue; // skip destroyed invaders
        //        if (invader[i].x == playerX && invaderY[i] == playerY)
        //        {
        //            lives--;
        //            // Arjun - setting this because of need to skip or destroy the invander from screen after hitting
        //            await Task.Delay(1000);
        //        }
        //    }

        //    if (lives <= 0) start = false;

        //    string livesText = $"Lives: {lives}";
        //    SetCursorPosition(WindowWidth - livesText.Length, 0);
        //    Write(livesText);
        //}

    }
}
