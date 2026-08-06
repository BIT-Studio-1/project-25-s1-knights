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
using static gameproject.keyboard;
using System.Numerics;

namespace gameproject
{

    public static class playerInfo
    {
        public static Vector2 playerPosition = new Vector2(WindowWidth / 2, WindowHeight - 8);
        public static int shootCooldown = 0;
        public static List<Bullet> PlayerBullets = new List<Bullet>();
        public static List<Rocket> playerRocket = new List<Rocket>();
    }
    public class Bullet
    {
        public Vector2 bullet { get; set; }
        public void Move() => bullet = new Vector2(bullet.X, bullet.Y - 1);
    }
    public class Rocket
    {
        public int x { get; set; }
        public int y { get; set; }
        public void Move() => y--;
    }

    internal class Character
    {
        // Small helpers to avoid repeated Convert.ToInt32 calls
        private static int ToX(Vector2 v) => Convert.ToInt32(v.X);
        private static int ToY(Vector2 v) => Convert.ToInt32(v.Y);

        public static void movement() //James
        {

            int oldx = Convert.ToInt32(playerPosition.X);

            moved = false; //sets the move bool to false at the start of each loop so the movement isnt continuous

            if ((IsKeyDown(RightArrow) || IsKeyDown(D)) && (playerPosition.X + 6 < consoleWidth))
            {
                playerPosition.X++;
                moved = true;
            }
            if ((IsKeyDown(LeftArrow) || IsKeyDown(A)) && (playerPosition.X > 3))
            {
                playerPosition.X--;
                moved = true;
            }
            if (IsKeyDown(Spacebar) && shootCooldown == 0)
            {
                PlayerBullets.Add(new Bullet { bullet = new Vector2(playerPosition.X - 3, playerPosition.Y - 1) });
                PlayerBullets.Add(new Bullet { bullet = new Vector2(playerPosition.X + 4, playerPosition.Y - 1) });
                shootCooldown = 5;
            }
            if (IsKeyDown(R) && shootCooldown == 0)
            {
                // Use playerPosition (converted to ints) instead of undefined playerX/playerY
                int px = ToX(playerPosition);
                int py = ToY(playerPosition);
                playerRocket.Add(new Rocket { x = px - 3, y = py - 1 });
                playerRocket.Add(new Rocket { x = px + 4, y = py - 1 });
                shootCooldown = 35;
            }

            if (shootCooldown > 0) shootCooldown--;// adds a cool down for the bullets

            if (playerPosition.Y != WindowHeight - 8) playerPosition.Y = WindowHeight - 8;

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
                if (PlayerBullets[i].bullet.Y >= 0 && PlayerBullets[i].bullet.Y < WindowHeight && PlayerBullets[i].bullet.X < WindowWidth) //check if the bullet is still within the window
                {

                    SetCursorPosition(Convert.ToInt32(PlayerBullets[i].bullet.X), Convert.ToInt32(PlayerBullets[i].bullet.Y));
                    Write(' '); // clear the old position
                }

                PlayerBullets[i].Move();

                bool hitSomething = false;
                for (int e = invaderInfo.Invaders.Count - 1; e >= 0 && !hitSomething; e--) // loop through every invader
                {
                    // Use invaderPos for position checks
                    var invPos = invaderInfo.Invaders[e].invaderPos;
                    if ((PlayerBullets[i].bullet.X == invPos.X + 1 || PlayerBullets[i].bullet.X == invPos.X - 1 || PlayerBullets[i].bullet.X == invPos.X) && PlayerBullets[i].bullet.Y == invPos.Y) // check if bullet is on same spot as this invader
                    {
                        SetCursorPosition(ToX(invPos), ToY(invPos));
                        Write(' '); // erase invader from screen

                        Vector2 invaderDropPos = invPos; //save position before removing

                        invaderInfo.Invaders.RemoveAt(e); //removes invaders from list

                        enemiesKilled++; // Increase kill count for level progression

                        PlayerBullets.RemoveAt(i); // remove the bullet
                        hitSomething = true; // stops the loop since this bullet is used up

                        //1 in 10 chance to spawn a life booster drop
                        if (rand.Next(10) == 0)
                        {
                            lifeInfo.LifeDrops.Add(new LifeDrop { lifeDropPos = invaderDropPos });
                        }
                    }
                }
                if (hitSomething) continue; // skip to next bullet since this one is gone

                if (PlayerBullets[i].bullet.Y < 0 || PlayerBullets[i].bullet.Y > WindowHeight || PlayerBullets[i].bullet.X > WindowWidth)
                {
                    PlayerBullets.RemoveAt(i); //remove if off screen otherwise draw
                }
                else
                {
                    SetCursorPosition(Convert.ToInt32(PlayerBullets[i].bullet.X), Convert.ToInt32(PlayerBullets[i].bullet.Y));
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
                    int invX = ToX(Invaders[e].invaderPos);
                    int invY = ToY(Invaders[e].invaderPos);
                    if (Abs(impactX - invX) <= 3 && impactY == invY)
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
                        int invX = ToX(Invaders[e].invaderPos);
                        int invY = ToY(Invaders[e].invaderPos);

                        // Check if the invader is within the Y plane and the horizontal blast radius
                        if (invY == impactY && Math.Abs(invX - impactX) <= aoeradius)
                        {
                            if (invY >= 0 && invY < WindowHeight && invX >= 0 && invX < WindowWidth)
                            {
                                SetCursorPosition(invX, invY);
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

            if (playerPosition.X >= 3 && playerPosition.X + 3 < consoleWidth)
            {
                SetCursorPosition(Convert.ToInt32(playerPosition.X - 3), Convert.ToInt32(playerPosition.Y));
                ForegroundColor = ConsoleColor.DarkGreen;
                Write("I      I");
                ResetColor();
                SetCursorPosition(Convert.ToInt32(playerPosition.X - 3), Convert.ToInt32(playerPosition.Y + 1));
                Write("| _  _ |");
                SetCursorPosition(Convert.ToInt32(playerPosition.X - 3), Convert.ToInt32(playerPosition.Y + 2));
                Write("|/    \\|");
                SetCursorPosition(Convert.ToInt32(playerPosition.X - 2), Convert.ToInt32(playerPosition.Y + 3));
                Write("\\____/");
                SetCursorPosition(Convert.ToInt32(playerPosition.X - 1), Convert.ToInt32(playerPosition.Y + 4));
                ForegroundColor = ConsoleColor.DarkYellow;
                Write("Y  Y");
                ResetColor();
            }

        }
        public static void ClearShip(int x)//clears the ship when moved
        {

            if (x >= 3 && x + 3 < consoleWidth)
            {
                SetCursorPosition(x - 3, Convert.ToInt32(playerPosition.Y));
                Write("         ");
                SetCursorPosition(x - 3, Convert.ToInt32(playerPosition.Y + 1));
                Write("         ");
                SetCursorPosition(x - 3, Convert.ToInt32(playerPosition.Y + 2));
                Write("         ");
                SetCursorPosition(x - 2, Convert.ToInt32(playerPosition.Y + 3));
                Write("       ");
                SetCursorPosition(x - 1, Convert.ToInt32(playerPosition.Y + 4));
                Write("     ");
            }

        }
    }
}
