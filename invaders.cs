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
using static gameproject.Character;
using static gameproject.Levels;
using static gameproject.Menu;
using static gameproject.invaderInfo;


namespace gameproject
{
    public static class invaderInfo
    {
        public static int spawnTimer = 0, moveRate = 5, maxInvaders = 5, invaderSpeed = 10, spawnRate = 10;
        public static List<Invader> Invaders = new List<Invader>();
    }
    public class Invader
    {
        public int x { get; set; }
        public int y { get; set; }

        public void Move() => y++;
    }

    internal class invaders
    {


            
        
        
        public static void updateinvaders()
        {

            string drawInvaders = "V";


            moveRate = invaderSpeed;// added this because it was overwriting what Stephanie had wrote in levels
            spawnTimer++;
            moveTimer++;

            if (level == 1)
            {
                moveRate = 5;
            }

            if (level == 2)
            {
                moveRate = 4;
            }

            if (level == 3)
            {
                moveRate = 3;
            }

            if (level == 4)
            {
                moveRate = 2;
            }

            if (level == 5)
            {
                moveRate = 1;
            }



            if (spawnTimer >= spawnRate && Invaders.Count < maxInvaders)
            {
                Invaders.Add(new Invader{ x = rand.Next(consoleWidth), y = 0}); // Spaawning randomly along x axis at 0 y position

                spawnTimer = 0;
            }

            if (moveTimer >= moveRate) //moves invaders down each time moveTimer matches moveRate
                                      //(levels also doesn't seem to be moving them faster each level progression)
            {
                moveTimer = 0;


                for (int i = Invaders.Count - 1; i >= 0; i--)
                {
                    

                    if (Invaders[i].x >= consoleWidth)
                    {
                        Invaders[i].x = rand.Next(consoleWidth);
                    }

                    if (Invaders[i].y >= consoleHeight)
                    {
                        Invaders[i].y = rand.Next(consoleHeight);
                    }
                   

                    if (Invaders[i].x >= 0 && Invaders[i].y >= 0 && Invaders[i].x < consoleWidth && Invaders[i].y < consoleHeight)
                    {

                        SetCursorPosition(Invaders[i].x, Invaders[i].y);

                        Write(" ");


                    }


                    Invaders[i].Move();


                    if (Invaders[i].y >= consoleHeight)
                    {
                        Invaders[i].y = 0;
                        Invaders[i].x = rand.Next(consoleWidth);
                    }

                    if (Invaders[i].x >= 0 && Invaders[i].y >= 0 && Invaders[i].x < consoleWidth && Invaders[i].y < consoleHeight)
                    {
                        SetCursorPosition(Invaders[i].x, Invaders[i].y);
                        ForegroundColor = ConsoleColor.Magenta;

                        Write(drawInvaders);
                        ResetColor();
                    }



                }

            }


                
            
        }
    }
}
