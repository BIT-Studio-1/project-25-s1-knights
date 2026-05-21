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
using static gameproject.Lives;

namespace gameproject
{
    internal class Character
    {

        public static void movement() //James
        {



            SetCursorPosition(playerX, playerY);
            Write(' ');
            //clear old position before moving




            moved = false; //sets the move bool to false at the start of each loop so the movement isnt continuous



            if ((IsKeyDown(RightArrow) || IsKeyDown(D)) && (playerX < farRow)) // if the key pressed is the right arrow key or the D key, it sets the move bool to true and adds one to the playerX variable if it isnt too close to the edge
            {
                playerX++;
                moved = true;
            }
            if ((IsKeyDown(LeftArrow) || IsKeyDown(A)) && (playerX > 0))  // if the key pressed is the left arrow key or the A key, it sets the move bool to true and removes one from the playerX variable if it isnt too close to the edge
            {
                playerX--;
                moved = true;
            }
            if (IsKeyDown(Spacebar) && shootCooldown == 0)
            {
                PlayerBullets.Add(new Bullet { x = playerX, y = playerY - 1 });
                shootCooldown = 5;
            }
            if (shootCooldown > 0) shootCooldown--;// adds a cool down for the bullets


        }


        public static void shoot()
        {

            for (int i = PlayerBullets.Count - 1; i >= 0; i--) //update the players bullets by looping backwards
            {
                if (PlayerBullets[i].y >= 0 && PlayerBullets[i].y < WindowHeight) //check if the bullet is still within the window
                {
                    SetCursorPosition(PlayerBullets[i].x, PlayerBullets[i].y);
                    Write(' '); // clear the old position
                }


                PlayerBullets[i].Move();


                //Arjun - now the variables invanderX and InvanderY are array, thats why this code is breaking.
                bool hitSomething = false;
                for (int e = 0; e < spawned && !hitSomething; e++) // loop through every invader
                {
                    if (invaderX[e] == -1) continue; // skip already destroyed ones

                    if (PlayerBullets[i].x == invaderX[e] && PlayerBullets[i].y == invaderY[e]) // check if bullet is on same spot as this invader
                    {
                        SetCursorPosition(invaderX[e], invaderY[e]);
                        Write(' '); // erase invader from screen

                        invaderX[e] = -1; // mark as destroyed
                        invaderY[e] = -1;

                        PlayerBullets.RemoveAt(i); // remove the bullet
                        hitSomething = true; // stops the loop since this bullet is used up
                    }
                }
                if (hitSomething) continue; // skip to next bullet since this one is gone


                if (PlayerBullets[i].y < 0)
                {
                    PlayerBullets.RemoveAt(i); //remove if off screen otherwise draw
                }

                else
                {
                    SetCursorPosition(PlayerBullets[i].x, PlayerBullets[i].y);
                    Write('|');
                }
            }
        }
    }
}
