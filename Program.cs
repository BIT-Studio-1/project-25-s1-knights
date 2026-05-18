using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using static gameproject.Globals;
using static System.Console;
using static System.ConsoleKey;
using static System.Math;





namespace gameproject
{
    
    
    
    public static class Globals // variables that any class or function can access
    {
        public static int bottomRow = WindowHeight - 1, farRow = WindowWidth - 1, playerX = WindowWidth/2, playerY = WindowHeight/2;
        public static HashSet<ConsoleKey> PressedKeys = new HashSet<ConsoleKey>();
        public static bool start = true, moved = false;
        public static List<Bullet> PlayerBullets = new List<Bullet>(); //creates the list to hold the bullets - saw this on reddit
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
            Tim();
            
            while (start == true)
            {
                limits();
                movement(); //calls on the movement method while the start bool is true so it is continuous.
                
                
                if (moved)
                {
                    Clear();
                    SetCursorPosition(playerX, playerY);
                    Write('^');
                    await Task.Delay(15);
                } // When the move bool is set to true, it clears the current screen and rewrites the player at the new postition.
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
            Write('^');
            //writes the player at the location that is set at each loop


            
            
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
                

                
            
        }

        public static void Luke()
        {
            for (int i = PlayerBullets.count -1 ; i >= 0; i--) //update the players bullets
            {
                if (PlayerBullets[i].Y >=0 && PlayerBullets[i].y < WindowHeight)
                {
                    SetCursorPosition(PlayerBullets[i].X, PlayerBullets[i].Y);
                    Write(' ');
                }
                playerBullets[i].Move();
                
            }
        }

        public static void Tim()
        {
            Random rand = new Random();

            int x = rand.Next(Console.WindowWidth);
            int y = 0;
            Console.SetCursorPosition(x, y);
            Console.WriteLine("X");
            

            
        }

        public static void Arjun()
        {

        }

        public static void Stephanie()
        {

        }

    }
}
