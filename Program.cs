using System;
using static System.Console;
using static gameproject.Globals;
using static System.ConsoleKey;
using static System.Math;



namespace gameproject
{
    
    
    
    public static class Globals // variables that any class or function can access
    {
        public static int bottomRow = WindowHeight - 1, farRow = WindowWidth - 1, playerX = WindowWidth/2, playerY = WindowHeight/2;
        public static ConsoleKey key;
        public static bool start = true;
    }
    
    internal class Program
    {
        static void Main()
        {
            CursorVisible = false; 
            Tim();
            
            while (start == true)
            {
                movement(); //calls on the movement method while the start bool is true so it is continuous.
            }
            
        }
        public static void movement() //James
        {
            bottomRow = WindowHeight - 1;
            farRow = WindowWidth - 1;
            playerX = Clamp(playerX, 0, farRow);
            playerY = Clamp(playerY, 0, bottomRow - 1);
            // sets the player position every time it loops and makes it so that if the window maximizes and the minimizes it doesn't crash form out of bounds


            SetCursorPosition(playerX, playerY);
            Write('^');
            //writes the player at the location that is set at each loop


            if (KeyAvailable) // tests if a key is being pressed already
            {
                bool moved = false; //sets the move bool to false at the start of each loop so the movement isnt continuous

                while (KeyAvailable) // while other keys aren't being pressed
                {
                    key = ReadKey(true).Key; // takes the key being pressed and assigns it to the key variable (the true within the readkey just makes it so that the key doesnt display when pressed)
                }

                if ((key == RightArrow || key == D) && (playerX < farRow)) // if the key pressed is the right arrow key or the D key, it sets the move bool to true and adds one to the playerX variable if it isnt too close to the edge
                {
                    playerX++;
                    moved = true;
                }
                else if ((key == LeftArrow || key == A) && (playerX > 0))  // if the key pressed is the left arrow key or the A key, it sets the move bool to true and removes one from the playerX variable if it isnt too close to the edge
                {
                    playerX--;
                    moved = true;
                }
                else if ((key == UpArrow || key == W) && (playerY > 0))  // if the key pressed is the up arrow key or the W key, it sets the move bool to true and removes one from the playerY variable if it isnt too close to the edge
                {
                    playerY--;
                    moved = true;
                }
                else if ((key == DownArrow || key == S) && (playerY < bottomRow - 1)) // if the key pressed is the down arrow key or the S key, it sets the move bool to true and adds one to the playerY variable if it isnt too close to the edge
                {
                    playerY++;
                    moved = true;
                }

                if (moved)
                {
                    Clear();
                    SetCursorPosition(playerX, playerY);
                    Write('^');
                } // When the move bool is set to true, it clears the current screen and rewrites the player at the new postition.
            }
        }

        public static void Luke()
        {
            for (int i = PlayerBullets.count
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
