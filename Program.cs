using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using static gameproject.Globals;
using static System.Console;
using static System.ConsoleKey;
using static System.Math;
using System.Diagnostics;
using static gameproject.Character;
using static gameproject.Lives;
using static gameproject.invaders;
using static gameproject.Levels;
using static gameproject.Menu;
using static gameproject.Intro;
using static gameproject.asteroids;






namespace gameproject
{
    


    public static class Globals // variables that any class or function can access
    {
        // Level System Added
        public static int level = 1, maxInvaders = 5, invaderSpeed = 10, spawnRate = 10, enemiesKilled = 0, bottomRow = WindowHeight - 1,
                          farRow = WindowWidth - 1, playerX = WindowWidth / 2, playerY = WindowHeight - 8, hitCooldown = 0, spawnTimer = 0,
                          shootCooldown = 0, moveTimer = 0, Life = 5, consoleWidth = WindowWidth, consoleHeight = WindowHeight,
                          moveRate = 5, asteroidMoveRate = 5, asteroidMoveTimer = 0, asteroidSpawnRate = 10, asteroidSpawnTimer = 0, maxAsteroids = 4; //for making invaders move slower
        public static Random rand = new Random();
        public static HashSet<ConsoleKey> PressedKeys = new HashSet<ConsoleKey>();
        public static bool start = false, moved = false, menuStart = false, win = false;
        public static List<Bullet> PlayerBullets = new List<Bullet>(); //creates the list to hold the bullets
        public static List<Invader> Invaders = new List<Invader>(); //creates list to hold invaders
        public static List<Asteroid> Asteroids = new List<Asteroid>(); // creates new list for asteroids


    }
    public class Bullet
    {
        public int x { get; set; }
        public int y { get; set; }
        public void Move() => y--;
    }

    public class Invader
    {
        public int x { get; set; }
        public int y { get; set; }

        public void Move() => y++;
    }

    public class Asteroid
    {
        public int x { get; set; }
        public int y { get; set; }

        public void Move() => y++;
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

            start = false;
            menuStart = false;
            initialScreen();

            while (true)
            {
                startmenu();

                while (start)
                {

                    if (WindowWidth != consoleWidth || WindowHeight != consoleHeight)
                    {
                        consoleWidth = WindowWidth;
                        consoleHeight = WindowHeight;
                        Clear();
                    }


                    Level(); //calls on the level method while the start bool is true so it is continuous.


                    limits();
                    CheckLives(); // Calls the function to calculate the lives.

                    //Lose Condition
                    if (Life <= 0)
                    {
                        start = false; //Stops game loop first 

                        bool playAgain = OutroAndDeath.ShowLose();

                        WaitForKeyRelease();

                        if (!playAgain)
                            Environment.Exit(0);

                        ResetGame();

                        Clear();
                        start = true;
                        continue;
                        
                    }

                    movement(); //calls on the movement method while the start bool is true so it is continuous.
                    shoot();
                    //newInvader(); // removed because of async
                    _ = updateinvaders();
                    newAsteroids();
                    







                    if (win == false)
                    {
                        DrawShip();
                        await Task.Delay(20);
                    }
                    // When the move bool is set to true, it clears the current screen and rewrites the player at the new postition.

                    if (IsKeyDown(Escape))
                    {
                        start = false;
                        menuStart = true;

                    }

                    if (level == 5 && enemiesKilled == maxInvaders)
                    {
                        start = false; //stops game loop first
                        win = true;
                        bool playAgain = OutroAndDeath.ShowWin();

                        WaitForKeyRelease();

                        if (!playAgain)
                            Environment.Exit(0);

                        ResetGame();

                        Clear();
                        start = true;
                        continue;
                    
                    }
                }
            }

        }

        public static void WaitForKeyRelease()
        {
            while(IsKeyDown(ConsoleKey.Y) || IsKeyDown(ConsoleKey.N))
            {
                Thread.Sleep(10);
            }
        }

        //Reset Game
        public static void ResetGame()
        {
            Life = 5;
            level = 1;
            enemiesKilled = 0;
            

            Invaders.Clear();
            PlayerBullets.Clear();

            playerX = WindowWidth / 2;
            playerY = WindowHeight - 8;

        }

        public static void limits()
        {
            bottomRow = WindowHeight - 1;
            farRow = WindowWidth - 1;
            playerX = Clamp(playerX, 3, farRow - 5);
            playerY = Clamp(playerY, 0, bottomRow - 4);



            // sets the player position every time it loops and makes it so that if the window maximizes and the minimizes it doesn't crash form out of bounds
        }


    }
}
