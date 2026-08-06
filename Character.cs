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
using static gameproject.playerInfo;
using static gameproject.invaderInfo;
using System.Numerics;

namespace gameproject
{

    public static class playerInfo
    {
        public static int playerX = WindowWidth / 2, playerY = WindowHeight - 8, shootCooldown = 0;
        public static List<Bullet> PlayerBullets = new List<Bullet>();
        public static List<Rocket> playerRocket = new List<Rocket>();
    }
    public class Bullet
    {
        public int x { get; set; }
        public int y { get; set; }
        public void Move() => y--;
    }
    public class Rocket
    {
        public int x { get; set; }
        public int y { get; set; }
        public void Move() => y--;
    }

    internal class Character
    {

        public static void movement() //James
        {

            int oldx = playerX;

            
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
            if (IsKeyDown(R) && shootCooldown == 0)
            {
                playerRocket.Add(new Rocket { x = playerX - 3, y = playerY - 1 });
                playerRocket.Add(new Rocket { x = playerX + 4, y = playerY - 1 });
                shootCooldown = 35;
            }

            if (shootCooldown > 0) shootCooldown--;// adds a cool down for the bullets

            if (playerY != WindowHeight - 8) playerY = WindowHeight - 8;

            if (moved)
            {
                ClearShip(oldx);
                DrawShip();
            }

            
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
                for (int e = invaderInfo.Invaders.Count - 1; e >= 0 && !hitSomething; e--) // loop through every invader
                {


                    if ((PlayerBullets[i].x == invaderInfo.Invaders[e].x + 1 || PlayerBullets[i].x == invaderInfo.Invaders[e].x - 1 || PlayerBullets[i].x == invaderInfo.Invaders[e].x) && PlayerBullets[i].y == invaderInfo.Invaders[e].y) // check if bullet is on same spot as this invader
                    {
                        SetCursorPosition(invaderInfo.Invaders[e].x, invaderInfo.Invaders[e].y);
                        Write(' '); // erase invader from screen

                        int dropX = invaderInfo.Invaders[e].x; //save position before removing
                        int dropY= invaderInfo.Invaders[e].y;

                        invaderInfo.Invaders.RemoveAt(e); //removes invaders from list

                        enemiesKilled++; // Increase kill count for level progression

                        PlayerBullets.RemoveAt(i); // remove the bullet
                        hitSomething = true; // stops the loop since this bullet is used up

                        //1 in 3 chance to spawn a life booster drop
                        if (rand.Next(10)==0)
                        {
                            lifeInfo.LifeDrops.Add(new LifeDrop { x = dropX, y = dropY });
                        }
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
                    ForegroundColor = ConsoleColor.Red;
                    Write('|');
                    ResetColor();
                }
            }
        }
        public static void rocketshoot()
        {
            int aoeradius = 12;
            for (int i = playerRocket.Count - 1; i >= 0; i--) //update the players bullets by looping backwards
            {

                if (playerRocket[i].y >= 0 && playerRocket[i].y < WindowHeight && playerRocket[i].x < WindowWidth) //check if the bullet is still within the window
                {

                    SetCursorPosition(playerRocket[i].x, playerRocket[i].y);
                    Write(' '); // clear the old position

                }


                playerRocket[i].Move();

                if (playerRocket[i].y < 0 || playerRocket[i].y >= WindowHeight || playerRocket[i].x < 0 || playerRocket[i].x >= WindowWidth)
                {
                    playerRocket.RemoveAt(i);
                    continue;

                }

                bool hitSomething = false;
                int impactX = playerRocket[i].x;
                int impactY = playerRocket[i].y;


                for (int e = Invaders.Count - 1; e >= 0; e--)
                {
                    if (Abs(impactX - Invaders[e].x) <= 3 && impactY == Invaders[e].y)
                    {
                        hitSomething = true;
                        // Impact detected! Proceed to trigger the explosion radius
                    }
                }

                // 2. If an impact occurred, trigger the explosion to clear surrounding enemies
                if (hitSomething)
                {
                    // Remove the rocket projectile first
                    playerRocket.RemoveAt(i);

                    // Loop backwards through all invaders to safely remove everything in the blast zone
                    for (int e = Invaders.Count - 1; e >= 0; e--)
                    {
                        // Check if the invader is within the Y plane and the horizontal blast radius
                        if (Invaders[e].y == impactY && Math.Abs(Invaders[e].x - impactX) <= aoeradius)
                        {
                            if (Invaders[e].y >= 0 && Invaders[e].y < WindowHeight && Invaders[e].x  >= 0 && Invaders[e].x < WindowWidth)
                            {
                                SetCursorPosition(Invaders[e].x, Invaders[e].y);
                                Write(' '); // Erase exploded invader from screen
                            }

                            

                            Invaders.RemoveAt(e);
                            enemiesKilled++;

                            
                            
                        }
                    }
                    continue; // Skip drawing this rocket since it exploded
                }

                // Safe to draw moving rocket if no impact occurred
                SetCursorPosition(playerRocket[i].x, playerRocket[i].y);
                ForegroundColor = ConsoleColor.Blue;
                Write('^');
                ResetColor();
            }
        }

        public static void DrawShip()//Drawing the ship
        {

            if (playerX >= 3 && playerX + 3 < consoleWidth)
            {
                SetCursorPosition(playerX - 3, playerY);
                ForegroundColor = ConsoleColor.DarkGreen;
                Write("I      I");
                ResetColor();
                SetCursorPosition(playerX - 3, playerY + 1);
                Write("| _  _ |");
                SetCursorPosition(playerX - 3, playerY + 2);
                Write("|/    \\|");
                SetCursorPosition(playerX - 2, playerY + 3);
                Write("\\____/");
                SetCursorPosition(playerX - 1, playerY + 4);
                ForegroundColor = ConsoleColor.DarkYellow;
                Write("Y  Y");
                ResetColor();
            }

        }
        public static void ClearShip(int x)//clears the ship when moved
        {

            if (x >= 3 && x + 3 < consoleWidth)
            {
                SetCursorPosition(x - 3, playerY);
                Write("         ");
                SetCursorPosition(x - 3, playerY + 1);
                Write("         ");
                SetCursorPosition(x - 3, playerY + 2);
                Write("         ");
                SetCursorPosition(x - 2, playerY + 3);
                Write("       ");
                SetCursorPosition(x - 1, playerY + 4);
                Write("     ");
            }

        }
    }
}
