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
using static gameproject.lifeInfo;
using System.Numerics;

namespace gameproject
{
    public static class lifeInfo
    {
        public static int Life = 5, hitCooldown = 0, dropMoveTimer = 0, dropMoveRate = 3;
        public static List<LifeDrop> LifeDrops = new List<LifeDrop>();
    }

    public class LifeDrop
    {
        public Vector2 lifeDropPos { get; set; }
        public void Move() => lifeDropPos = new Vector2(lifeDropPos.X, lifeDropPos.Y + 1); //falls down like invaders
    }

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
            int hitboxLeft = Convert.ToInt32(playerInfo.playerPosition.X - 3);
            int hitboxRight = Convert.ToInt32(playerInfo.playerPosition.X + 4);
            int hitboxTop = Convert.ToInt32(playerInfo.playerPosition.Y);
            int hitboxBottom = Convert.ToInt32(playerInfo.playerPosition.Y + 3);

            //loop backwards so removal is safe
            for (int i = invaderInfo.Invaders.Count - 1; i >= 0; i-- ) //chnaged teh logic to backward safe to remove
            {
                bool withinX = invaderInfo.Invaders[i].invaderPos.X >= hitboxLeft && invaderInfo.Invaders[i].invaderPos.X <= hitboxRight;
                bool withinY = invaderInfo.Invaders[i].invaderPos.Y >= hitboxTop && invaderInfo.Invaders[i].invaderPos.Y <= hitboxBottom;
                

                if (invaderInfo.Invaders[i].invaderPos.X >= hitboxLeft && invaderInfo.Invaders[i].invaderPos.X <= hitboxRight && invaderInfo.Invaders[i].invaderPos.Y >= hitboxTop && invaderInfo.Invaders[i].invaderPos.Y >= hitboxBottom)//removes live if hit box of the ship is hit
                {
                    SetCursorPosition(Convert.ToInt32(invaderInfo.Invaders[i].invaderPos.X), Convert.ToInt32(invaderInfo.Invaders[i].invaderPos.Y));
                    Write(' ');
                    invaderInfo.Invaders.RemoveAt(i);//remove from the list
                    Life--;
                    hitCooldown = 15; //30-frame invincibility
                     //stop checking after one hit
                    // Arjun - setting this because of need to skip or destroy the invander from screen after hitting
                    // Explosion + destroy invader
                    //await ExplosionAnimation(playerX, playerY);
                    //await Task.Delay(1000);
                }

                
            }

            for (int i = asteroidInfo.Asteroids.Count - 1; i >= 0; i--)
            {
                bool withinX = asteroidInfo.Asteroids[i].asteroidPos.X >= hitboxLeft && asteroidInfo.Asteroids[i].asteroidPos.X <= hitboxRight;
                bool withinY = asteroidInfo.Asteroids[i].asteroidPos.Y >= hitboxTop && asteroidInfo.Asteroids[i].asteroidPos.Y <= hitboxBottom;

                if (asteroidInfo.Asteroids[i].asteroidPos.X >= hitboxLeft && asteroidInfo.Asteroids[i].asteroidPos.X <= hitboxRight && asteroidInfo.Asteroids[i].asteroidPos.Y >= hitboxTop && asteroidInfo.Asteroids[i].asteroidPos.Y <= hitboxBottom)
                {
                    SetCursorPosition(Convert.ToInt32(asteroidInfo.Asteroids[i].asteroidPos.X), Convert.ToInt32(asteroidInfo.Asteroids[i].asteroidPos.Y));
                    Write(' ');
                    asteroidInfo.Asteroids.RemoveAt(i);//remove from the list
                    Life--;
                    hitCooldown = 15; //15-frame invincibility
                    
                }
            }

            string livesText = $"Lives: {Life}";
            SetCursorPosition(WindowWidth - livesText.Length, 0);
            Write(livesText);

            //if (Life <= 0) { 
            //start=false;
            //    OutroAndDeath.ShowLose();
            //    //next function goes here.
            //}

            
        }

        public static void UpdateDrops()
        {
            dropMoveTimer++;
            //TODO: move drops, draw drops, check player collection
           for(int i= LifeDrops.Count - 1; i >= 0;i--)
            {
                // check if ships collects the drop
                int hitboxLeft = Convert.ToInt32(playerInfo.playerPosition.X - 3);
                int hitboxRight = Convert.ToInt32(playerInfo.playerPosition.X + 4);
                int hitboxTop = Convert.ToInt32(playerInfo.playerPosition.Y);
                int hitboxBottom = Convert.ToInt32(playerInfo.playerPosition.Y + 4);

                bool inX = LifeDrops[i].lifeDropPos.X >= hitboxLeft && LifeDrops[i].lifeDropPos.X <= hitboxRight;
                bool inY = LifeDrops[i].lifeDropPos.Y >= hitboxTop && LifeDrops[i].lifeDropPos.Y <= hitboxBottom;

                if (level == 1)
                {
                    dropMoveRate = 5;
                }

                if (level == 2)
                {
                    dropMoveRate = 4;
                }

                if (level == 3)
                {
                    dropMoveRate = 3;
                }

                if (level == 4)
                {
                    dropMoveRate = 2;
                }

                if (level == 5)
                {
                    dropMoveRate = 2;
                }


                if (LifeDrops[i].lifeDropPos.X >= hitboxLeft && LifeDrops[i].lifeDropPos.X <= hitboxRight && LifeDrops[i].lifeDropPos.Y >= hitboxTop && LifeDrops[i].lifeDropPos.Y <= hitboxBottom)
                {
                    SetCursorPosition(Convert.ToInt32(LifeDrops[i].lifeDropPos.X), Convert.ToInt32(LifeDrops[i].lifeDropPos.Y));
                    Write(' ');// erase from screen
                    LifeDrops.RemoveAt(i);
                    Life++;  //give player an extra life
                    string livesText =  $"Lives: {Life}";
                    SetCursorPosition(WindowWidth - livesText.Length, 0);
                    Write(livesText); // update  HUD immediately
                    continue;
                    
                }
                if (dropMoveTimer >= dropMoveRate)
                {
                    //erase old position
                    if (LifeDrops[i].lifeDropPos.X >=0 && LifeDrops[i].lifeDropPos.Y >=0 &&
                        LifeDrops[i].lifeDropPos.X < consoleWidth && LifeDrops[i].lifeDropPos.Y < consoleHeight)
                    {
                        SetCursorPosition(Convert.ToInt32(LifeDrops[i].lifeDropPos.X), Convert.ToInt32(LifeDrops[i].lifeDropPos.Y));
                        Write(" ");

                    }

                    LifeDrops[i].Move(); //fall one row down

                    //remove if off screen
                    if (LifeDrops[i].lifeDropPos.Y >= consoleHeight)
                    {
                        LifeDrops.RemoveAt(i);
                        continue;
                    }
                }

                //draw + at current position
                if (LifeDrops[i].lifeDropPos.X >=0 && LifeDrops[i].lifeDropPos.Y >0 &&
                    LifeDrops[i].lifeDropPos.X < consoleWidth && LifeDrops[i].lifeDropPos.Y <= consoleHeight)
                {
                    SetCursorPosition(Convert.ToInt32(LifeDrops[i].lifeDropPos.X), Convert.ToInt32(LifeDrops[i].lifeDropPos.Y));
                    ForegroundColor = ConsoleColor.Cyan;
                    Write('+');
                    ResetColor();
                }
            }
            if (dropMoveTimer >= dropMoveRate) dropMoveTimer = 0;

        }
         

    }
}
