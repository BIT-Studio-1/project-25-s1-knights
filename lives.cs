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

            //adding hit cooldown
            if(hitCooldown > 0)
            {
                hitCooldown--;

                string hud = $"Lives: {Life}";
                SetCursorPosition(WindowWidth - hud.Length, 0);
                Write(hud);
                return; // skipping teh collision in this frame
            }
            //player hitbox
            int hitboxLeft = playerX - 3;
            int hitboxRight = playerX + 4;
            int hitboxTop = playerY;
            int hitboxBottom = playerY + 3;

            //loop backwards so removal is safe
            for (int i = Invaders.Count - 1; i >= 0; i--) //chnaged teh logic to backward safe to remove
            {
                bool withinX = Invaders[i].x >= hitboxLeft && Invaders[i].x <= hitboxRight;
                bool withinY = Invaders[i].y >= hitboxTop && Invaders[i].y <= hitboxBottom;
                

                if (Invaders[i].x >= hitboxLeft && Invaders[i].x <= hitboxRight && Invaders[i].y >= hitboxTop && Invaders[i].y >= hitboxBottom)//removes live if hit box of the ship is hit
                {
                    SetCursorPosition(Invaders[i].x, Invaders[i].y);
                    Write(' ');
                    Invaders.RemoveAt(i);//remove from the list
                    Life--;
                    hitCooldown = 30; //30-frame invincibility
                    break; //stop checking after one hit
                    // Arjun - setting this because of need to skip or destroy the invander from screen after hitting
                    // Explosion + destroy invader
                    //await ExplosionAnimation(playerX, playerY);
                    //await Task.Delay(1000);
                }
            }

            string livesText = $"Lives: {Life}";
            SetCursorPosition(WindowWidth - livesText.Length, 0);
            Write(livesText);

            if (Life <= 0) { 
            start=false;
                OutroAndDeath.ShowLose();
                //next fuction goes here.
            }

            
        }

    }
}
