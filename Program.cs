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

        public static int invader;

        public static int invaderX = 0;
        public static int invaderY = 0;

    }
    public class Bullet
    {
        public int x {  get; set; }
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
            newInvader();
            
            while (start == true)
            {
                limits();
                movement(); //calls on the movement method while the start bool is true so it is continuous.
                Luke();


                
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
            if (IsKeyDown(Spacebar))
            {
                PlayerBullets.Add(new Bullet { x = playerX, y = playerY - 1});
            }
            
                
            
                
                    
                
                

                
            
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

                if (PlayerBullets[i].x == invaderX && PlayerBullets[i].y == invaderY)
                {
                    SetCursorPosition(invaderX,invaderY);
                    Write(' '); // removes invader if it hits

                    PlayerBullets.RemoveAt(i); // removes bullets after hit

                    Random rand = new Random();
                    invaderX = rand.Next(WindowWidth);
                    invaderY = 0;

                    continue; // bullet is gone, skip to next one
                }

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


             

             invaderX = 0;
             invaderY = 0;

            for (int i = 0; i < 15; i++)
            {
                await Task.Delay(1000);
                invaderX = rand.Next(Console.WindowWidth);
                invaderY = 0;
                Console.SetCursorPosition((int)invaderX, (int)invaderY);
                Console.WriteLine("x");
            }


            while (invaderY != 1000f)
            {
                invaderY += 1;
            }
        }


        public static void Arjun()
        {

        }

        public static void Stephanie()
        {

        }

    }
}
