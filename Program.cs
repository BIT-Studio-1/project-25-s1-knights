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
using System.Collections.Concurrent;
using static gameproject.keyboard;
using System.Threading.Tasks;

namespace gameproject
{
    public static class Globals // variables that any class or function can access
    {
        // Level System Added
        public static int level = 1, enemiesKilled = 0, bottomRow = WindowHeight - 1,
                          farRow = WindowWidth - 1, moveTimer = 0, consoleWidth = WindowWidth, consoleHeight = WindowHeight; 
        public static Random rand = new Random();
        public static HashSet<ConsoleKey> PressedKeys = new HashSet<ConsoleKey>();
        public static bool start = false, moved = false, menuStart = false;
    }

    public static class keyboard
    {
        [DllImport("user32.dll")] // imports a library for to make the movement smoother
        private static extern short GetAsyncKeyState(int vKey);

        private static readonly ConcurrentDictionary<ConsoleKey, DateTime> _linuxKeyTimestamps = new();
        private static bool _isInitialized = false;
        private static readonly object _lock = new();
        private const int keyReleaseTimeMs = 30;
        public static bool IsKeyDown(ConsoleKey key) //method that registers while a key is pressed
        {
            if (OperatingSystem.IsLinux() && !_isInitialized) 
            {
                lock (_lock)
                {
                    if (!_isInitialized)
                    {
                        Task.Run(() => otherInputProcessor());
                        _isInitialized = true;
                    }
                }
            }
            
            if (OperatingSystem.IsWindows()) return (GetAsyncKeyState((int)key) & 0x8000) != 0;
            else if (OperatingSystem.IsLinux())
            {
                if (_linuxKeyTimestamps.TryGetValue(key, out DateTime lastKey))
                {
                    return (DateTime.UtcNow - lastKey).TotalMilliseconds < keyReleaseTimeMs;
                }
            }
            return false;
        }

        private static void otherInputProcessor()
        {
            while (true)
            {
                TreatControlCAsInput = true;
                if (KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = ReadKey(true);
                    _linuxKeyTimestamps[keyInfo.Key] = DateTime.UtcNow;
                }
            }
        } 

    }

    internal class Program
    {
        static async Task Main()
        {
            CursorVisible = false;

            start = false;
            menuStart = false;
            initialScreen();

            startmenu();

            while (true)
            {
                Clear();
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
                    if (lifeInfo.Life <= 0)
                    {
                        start = false; //Stops game loop first 

                        await Task.Delay(500);
                        while (KeyAvailable)
                            ReadKey(true);

                        bool playAgain = OutroAndDeath.ShowLose();
                        if (playAgain == false)
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
                    DrawShip();
                    //newInvader(); // removed because of async
                    updateinvaders();
                    newAsteroids();

                    UpdateDrops(); //add update drops function into the gameloop


                    await Task.Delay(20);

                    // When the move bool is set to true, it clears the current screen and rewrites the player at the new postition.

                    if (IsKeyDown(Escape))
                    {
                        start = false;
                        menuStart = true;

                    }

                    //Win Condition
                    //if (level == 5 && enemiesKilled == maxInvaders)
                    //{
                    //    start = false; //stops game loop first

                    //    OutroAndDeath.ShowWin();

                    //}

                    //Win Condition
                    if (level == 5 && enemiesKilled == invaderInfo.maxInvaders)
                    {
                        start = false; //stops game loop first

                        await Task.Delay(500);
                        while (KeyAvailable)
                            ReadKey(true);

                        bool playAgain = OutroAndDeath.ShowWin();

                        if (!playAgain)
                            Environment.Exit(0);

                        ResetGame();

                        await Task.Delay(100);
                        Clear();
                        start = true;
                        break;
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
            lifeInfo.Life = 5;
            level = 1;
            enemiesKilled = 0;
            

            invaderInfo.Invaders.Clear();
            playerInfo.PlayerBullets.Clear();
            lifeInfo.LifeDrops.Clear();

            playerInfo.playerPosition.X = WindowWidth / 2;
            playerInfo.playerPosition.Y = WindowHeight - 8;

            //isDead = false;
            //Clear();

        }

        public static void limits()
        {
            bottomRow = WindowHeight - 1;
            farRow = WindowWidth - 1;
            playerInfo.playerPosition.X = Clamp(playerInfo.playerPosition.X, 3, farRow - 5);
            playerInfo.playerPosition.Y = Clamp(playerInfo.playerPosition.Y, 0, bottomRow - 4);



            // sets the player position every time it loops and makes it so that if the window maximizes and the minimizes it doesn't crash form out of bounds
        }


    }
}
