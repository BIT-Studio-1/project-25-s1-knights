using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using static gameproject.Globals;
using static System.Console;
using static System.ConsoleKey;
using static System.Math;
using System.Diagnostics;






namespace gameproject
{



    public static class Globals // variables that any class or function can access
    {
// HEAD
        public static int bottomRow = WindowHeight - 1, farRow = WindowWidth - 1, playerX = WindowWidth/2, playerY = WindowHeight/2;
        public static ConsoleKey key;
        public static bool start = true;

        // Level System Added
        public static int level = 1;
        public static int maxInvaders = 5;
        public static int invaderSpeed = 300;
        public static int spawnRate = 10;
        public static int enemiesKilled = 0;
        
//
        public static int bottomRow = WindowHeight - 1, farRow = WindowWidth - 1, playerX = WindowWidth / 2, playerY = WindowHeight - 5;
        public static HashSet<ConsoleKey> PressedKeys = new HashSet<ConsoleKey>();
        public static bool start = true, moved = false;

        public static List<Bullet> PlayerBullets = new List<Bullet>(); //creates the list to hold the bullets - saw this on reddit

        public static int invader;

        //Arjun - Variables declared in Invanders moved to here.
        public static int[] invaderX = new int[1000];
        public static int[] invaderY = new int[1000];
        public static int spawned = 0;
        public static int spawnTimer = 0;

        public static int shootCooldown = 0; //stops bullet spam

        public static int lives = 5; // set lives to 5 by default
// 1a76d734df82ee41272dee5603e1235ed87f19ee
    }
    public class Bullet
    {
        public int x { get; set; }
        public int y { get; set; }
        public void Move() => y--;
    }

    internal class Program
    {
        [DllImport("user32.dll")] // imports a library for to make the movement smoother
        private static extern short GetAsyncKeyState(int vKey);
        public static bool IsKeyDown(ConsoleKey key) //method that registers while a key is pressed
        {
            return (GetAsyncKeyState((int)key) & 0x8000) != 0;
        }

        static async Task Main()
        {
            CursorVisible = false;
            _ = newInvader();

            while (start == true)
            {
// HEAD
                Level(); //calls on the level method while the start bool is true so it is continuous.
//
                limits();
// 1a76d734df82ee41272dee5603e1235ed87f19ee
                movement(); //calls on the movement method while the start bool is true so it is continuous.
                Luke();
                Arjun(); // Calls the function to calculate the lives.




                SetCursorPosition(playerX, playerY);
                Write('^');
                await Task.Delay(15);
                // When the move bool is set to true, it clears the current screen and rewrites the player at the new postition.
            }


        }

        public static void limits()
        {
            bottomRow = WindowHeight - 1;
            farRow = WindowWidth - 1;
            playerX = Clamp(playerX, 0, farRow);
            playerY = Clamp(playerY, 0, bottomRow - 1);
            // sets the player position every time it loops and makes it so that if the window maximizes and the minimizes it doesn't crash form out of bounds
        }
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

        public static void Luke()
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

















        public static async Task newInvader()
        {


            Random rand = new Random();

            //Arjun - moving this variables to global. in global it declared as a int only. if need to use all invanders i need to declare it in global
            //int[] invaderX = new int[1000];
            //int[] invaderY = new int[1000];
            //int spawned = 0;
            //int spawnTimer = 0;
            int finished = 0;

            while (true)
            {
                int consoleWidth = Console.WindowWidth;
                int consoleHeight = Console.WindowHeight;
                Clear();


                spawnTimer++;
                if (spawnTimer >= 10 && spawned < 15)
                {
                    invaderX[spawned] = rand.Next(consoleWidth);
                    invaderY[spawned] = 0;
                    spawned++;
                    spawnTimer = 0;
                }


                for (int i = 0; i < spawned; i++)
                {
                    invaderY[i]++;
                    if (invaderY[i] >= Console.WindowHeight)

                    {
                        invaderY[i] = 0;
                    }
                    Console.SetCursorPosition(invaderX[i], invaderY[i]);
                    Console.Write("X");


                }

                await Task.Delay(300);
            }
        }


        public static void Arjun()
        {
            for (int i = 0; i < spawned; i++)
            {
                if (invaderX[i] == -1) continue; // skip destroyed invaders
                if (invaderX[i] == playerX && invaderY[i] == playerY)
                {
                    lives--;
                    // Arjun - setting this because of need to skip or destroy the invander from screen after hitting
                    invaderX[i] = -1;
                    invaderY[i] = -1;
                }
            }

            if (lives <= 0) start = false;

            string livesText = $"Lives: {lives}";
            SetCursorPosition(WindowWidth - livesText.Length, 0);
            Write(livesText);
        }

        public static void Level() //Stephanie
        {
            //LEVEL 1
            if (level == 1)
            {
                maxInvaders = 5;
                invaderSpeed = 300;
                spawnRate = 10;
            }
            else if (level == 2)
            {
                maxInvaders = 8;
                invaderSpeed = 250;
                spawnRate = 8;
            }
            else if (level == 3)
            {
                maxInvaders = 10;
                invaderSpeed = 200;
                spawnRate = 6;
            }
            else if (level == 4)
            {
                maxInvaders = 12;
                invaderSpeed = 150;
                spawnRate = 5;
            }
            else if (level == 5)
            {
                maxInvaders = 15;
                invaderSpeed = 100;
                spawnRate = 3;
            }

            // WIN GAME
            if (level > 5)
            {
                Clear();

                SetCursorPosition(WindowWidth / 2 - 5, WindowHeight / 2);
                Write("You WIN!");

                start = false;
            }

            //SHOW LEVEL
            string levelText = $"Level: {level} | Kills: {enemiesKilled}";
            SetCursorPosition(0, 0);
            Write(levelText);

            //Level Progression: move to the next level.
            if (enemiesKilled >= maxInvaders)
            {
                level++;
                enemiesKilled = 0;

                Clear();
                SetCursorPosition(WindowWidth / 2 - 5, WindowHeight / 2);
                Write($"Level {level}");
                Thread.Sleep(2000);
                Clear();
            }


        }

    }
}
