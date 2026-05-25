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






namespace gameproject
{



    public static class Globals // variables that any class or function can access
    {

     
        // Level System Added
        public static int level = 1;
        public static int maxInvaders = 5;
        public static int invaderSpeed = 10;
        public static int spawnRate = 10;
        public static int enemiesKilled = 0;
        public static Random rand = new Random();

        public static int bottomRow = WindowHeight - 1, farRow = WindowWidth - 1, playerX = WindowWidth / 2, playerY = WindowHeight - 5;
        public static HashSet<ConsoleKey> PressedKeys = new HashSet<ConsoleKey>();
        public static bool start = false, moved = false, menuStart = true;

        public static List<Bullet> PlayerBullets = new List<Bullet>(); //creates the list to hold the bullets - saw this on reddit

        

        //Arjun - Variables declared in Invanders moved to here.
        public static List<Invader> Invaders = new List<Invader>(); //creates list to hold invaders
        public static int spawnTimer = 0, shootCooldown = 0,moveTimer = 0, Life = 5, consoleWidth = Console.WindowWidth, consoleHeight = Console.WindowHeight;
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

            OutroAndDeath.ShowWin();

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
                _=lives(); // Calls the function to calculate the lives.

                movement(); //calls on the movement method while the start bool is true so it is continuous.
                shoot();
                //newInvader(); // removed because of async
                updateinvaders();






                SetCursorPosition(playerX, playerY);
                Write('^');
                await Task.Delay(15);
                // When the move bool is set to true, it clears the current screen and rewrites the player at the new postition.

                if (IsKeyDown(Escape))
                {
                    start = false;
                    menuStart = true;
                    startmenu();
                } //pauses if you press escape
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
        

    }
}
