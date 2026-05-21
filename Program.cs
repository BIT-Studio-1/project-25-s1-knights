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
        public static int invaderSpeed = 300;
        public static int spawnRate = 10;
        public static int enemiesKilled = 0;
        

        public static int bottomRow = WindowHeight - 1, farRow = WindowWidth - 1, playerX = WindowWidth / 2, playerY = WindowHeight - 5;
        public static HashSet<ConsoleKey> PressedKeys = new HashSet<ConsoleKey>();
        public static bool start = false, moved = false, menuStart = true;

        public static List<Bullet> PlayerBullets = new List<Bullet>(); //creates the list to hold the bullets - saw this on reddit

        public static int invader;

        //Arjun - Variables declared in Invanders moved to here.
        public static int[] invaderX = new int[1000];
        public static int[] invaderY = new int[1000];
        public static int spawned = 0;
        public static int spawnTimer = 0;

        public static int shootCooldown = 0; //stops bullet spam

        public static int lives = 5; // set lives to 5 by default

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
            startmenu();
            if (start) { _ = newInvader(); }
            

            while (start == true)
            {

                Level(); //calls on the level method while the start bool is true so it is continuous.

                limits();
                _= Arjun(); // Calls the function to calculate the lives.

                movement(); //calls on the movement method while the start bool is true so it is continuous.
                shoot();
                



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
        

    }
}
