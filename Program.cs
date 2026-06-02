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
                          moveRate = 5, asteroidMoveRate = 6, asteroidMoveTimer = 0, asteroidSpawnRate = 10, asteroidSpawnTimer = 0, maxAsteroids = 4; //for making invaders move slower
        public static Random rand = new Random();
        public static HashSet<ConsoleKey> PressedKeys = new HashSet<ConsoleKey>();
        public static bool start = false, moved = false, menuStart = false;
        public static List<Bullet> PlayerBullets = new List<Bullet>(); //creates the list to hold the bullets
        public static List<Invader> Invaders = new List<Invader>(); //creates list to hold invaders
        public static List<Asteroid> Asteroids = new List<Asteroid>(); // creates new list for asteroids
        public static bool isDead = false;

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

            startmenu();

            while (true)
            {
                Console.Clear();
                if (menuStart)
                {
                    startmenu();
                    menuStart = false;

                }

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
                    if (Life <= 0 && !isDead)
                    {
                        isDead = true;
                        start = false; //Stops game loop first 

                        await Task.Delay(500);
                        while(Console.KeyAvailable)
                           Console.ReadKey(true);

                        bool playAgain = OutroAndDeath.ShowLose();
                        if (!playAgain)
                            Environment.Exit(0);

                        ResetGame();
                        //Clear();
                        await Task.Delay(100);
                        Clear();
                        start = true;
                        //continue;
                        break;
                        //return;
                    }
                
                    movement(); //calls on the movement method while the start bool is true so it is continuous.
                    shoot();
                    //newInvader(); // removed because of async
                    updateinvaders();
                    newAsteroids();

                   
                    DrawShip();
                    await Task.Delay(20);
                    
                    // When the move bool is set to true, it clears the current screen and rewrites the player at the new postition.

                    if (IsKeyDown(Escape))
                    {
                        start = false;
                        menuStart = true;

                    }

                    //Win Condition
                    if (level == 5 && enemiesKilled == maxInvaders)
                    {
                        start = false; //stops game loop first

                        OutroAndDeath.ShowWin();
                    
                    }
                }


                //if (isDead)
                //{
                //    await Task.Delay(500);
                //    while (Console.KeyAvailable)
                //        Console.ReadKey(true);
                //    OutroAndDeath.ShowLose();
                //    return;



                //}
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

            isDead = false;
            //Clear();

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
