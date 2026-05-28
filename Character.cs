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
using static gameproject.invaders;
using static gameproject.Levels;
using static gameproject.Menu;
using System.Numerics;

namespace gameproject
{
    internal class Character
    {

        public static void movement() //James
        {



            ClearShip();
            //clear old position before moving




            moved = false; //sets the move bool to false at the start of each loop so the movement isnt continuous



            if ((IsKeyDown(RightArrow) || IsKeyDown(D)) && (playerX + 6 < consoleWidth)) // if the key pressed is the right arrow key or the D key, it sets the move bool to true and adds one to the playerX variable if it isnt too close to the edge
            {
                playerX++;
                moved = true;
            }
            if ((IsKeyDown(LeftArrow) || IsKeyDown(A)) && (playerX > 3))  // if the key pressed is the left arrow key or the A key, it sets the move bool to true and removes one from the playerX variable if it isnt too close to the edge
            {
                playerX--;
                moved = true;
            }
            if (IsKeyDown(Spacebar) && shootCooldown == 0)
            {
                PlayerBullets.Add(new Bullet { x = playerX - 3, y = playerY - 1 });
                PlayerBullets.Add(new Bullet { x = playerX + 4, y = playerY - 1 });
                shootCooldown = 5;
            }
            if (shootCooldown > 0) shootCooldown--;// adds a cool down for the bullets

            if (playerY != WindowHeight - 8) playerY = WindowHeight - 8; 


        }


        public static void shoot()
        {

            for (int i = PlayerBullets.Count - 1; i >= 0; i--) //update the players bullets by looping backwards
            {
                if (PlayerBullets[i].y >= 0 && PlayerBullets[i].y < WindowHeight && PlayerBullets[i].x < WindowWidth) //check if the bullet is still within the window
                {
                    
                    SetCursorPosition(PlayerBullets[i].x, PlayerBullets[i].y);
                    Write(' '); // clear the old position
                }


                PlayerBullets[i].Move();


                //Arjun - now the variables invanderX and InvanderY are array, thats why this code is breaking.
                bool hitSomething = false;
                for (int e = Invaders.Count -1; e >= 0 && !hitSomething; e--) // loop through every invader
                {
                   

                    if ((PlayerBullets[i].x == Invaders[e].x + 1 || PlayerBullets[i].x == Invaders[e].x -1 || PlayerBullets[i].x == Invaders[e].x) && PlayerBullets[i].y == Invaders[e].y) // check if bullet is on same spot as this invader
                    {
                        SetCursorPosition(Invaders[e].x, Invaders[e].y);
                        Write(' '); // erase invader from screen

                        Invaders.RemoveAt(e); //removes invaders from list

                        enemiesKilled++; // Increase kill count for level progression

                        PlayerBullets.RemoveAt(i); // remove the bullet
                        hitSomething = true; // stops the loop since this bullet is used up
                    }
                }
                if (hitSomething) continue; // skip to next bullet since this one is gone


                if (PlayerBullets[i].y < 0 || PlayerBullets[i].y > WindowHeight || PlayerBullets[i].x > WindowWidth)
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
        public static void DrawShip()//Drawing the ship
        {

            if (playerX >= 3 && playerX + 3 < consoleWidth)
            {
                SetCursorPosition(playerX - 3, playerY);
                Write("I      I");
                SetCursorPosition(playerX - 3, playerY + 1);
                Write("| _  _ |");
                SetCursorPosition(playerX - 3, playerY + 2);
                Write("|/    \\|");
                SetCursorPosition(playerX - 2, playerY + 3);
                Write("\\____/");
            }

        }
        public static void ClearShip()//clears the ship when moved
        {

            if (playerX >= 3 && playerX + 3 < consoleWidth)
            {
                SetCursorPosition(playerX - 3, playerY);
                Write("        ");
                SetCursorPosition(playerX - 3, playerY + 1);
                Write("        ");
                SetCursorPosition(playerX - 3, playerY + 2);
                Write("        ");
                SetCursorPosition(playerX - 2, playerY + 3);
                Write("      ");
            }

        }
    }
}
