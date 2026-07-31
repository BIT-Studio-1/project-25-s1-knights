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
using static gameproject.Asteroid;
using static gameproject.asteroidInfo;
using System.Collections;
using System.ComponentModel;
using System.Numerics;

namespace gameproject
{

    public class Asteroid
    {
        public Vector2 asteroidPos {  get; set; }

        public int asteroidDirection;
        public void MoveDown() => asteroidPos = new Vector2(asteroidPos.X, asteroidPos.Y + 1);
        public void MoveRight() => asteroidPos = new Vector2(asteroidPos.X + 1, asteroidPos.Y);
        public void MoveLeft() => asteroidPos = new Vector2(asteroidPos.X -1, asteroidPos.Y);
    }

    public class asteroidInfo
    {
        public static int asteroidSpawnTimer = 0, maxAsteroids = 4, asteroidMoveRate = 6, asteroidMoveTimer = 0, asteroidSpawnRate = 10;
        public static List<Asteroid> Asteroids = new List<Asteroid>();

        
    }
    internal class asteroids

    {
        public static void newAsteroids()
        {
            
            asteroidMoveTimer++;  
            asteroidSpawnTimer++;

            if (level == 1)  //handles speed per level for asteroids, increasing move rate per level, need to do rest of code so it works properly
            {
                maxAsteroids = 1;
                asteroidMoveRate = 5;
            }

            if (level == 2)
            {
                maxAsteroids = 2;
                asteroidMoveRate = 4;
            }

            if (level == 3)
            {
                maxAsteroids = 3;
                asteroidMoveRate = 3;
            }

            if (level == 4)
            {
                maxAsteroids = 3;
                asteroidMoveRate = 2;
            }

            if (level == 5)
            {
                maxAsteroids = 3;
                asteroidMoveRate = 1;
            }

            if ((asteroidSpawnTimer >= asteroidSpawnRate) && (Asteroids.Count < maxAsteroids))
            {
                Asteroids.Add(new Asteroid { asteroidPos = new Vector2(rand.Next(consoleWidth), 0), asteroidDirection = rand.Next(1, 2) }); //spawn rate is 20, should be low enough to not have them spawn so frequently, also spawns asteroid in corner
                asteroidSpawnTimer = 0;
            }

            if (asteroidMoveTimer >= asteroidMoveRate)
            {
                asteroidMoveTimer = 0;

                
                for (int i = Asteroids.Count - 1; i >= 0; i--)
                {
                   

                    if ((Asteroids[i].asteroidPos.X >= 0) && (Asteroids[i].asteroidPos.Y >= 0) && (Asteroids[i].asteroidPos.X < consoleWidth) && (Asteroids[i].asteroidPos.Y < consoleHeight))
                    {
                        SetCursorPosition(Convert.ToInt32(Asteroids[i].asteroidPos.X), Convert.ToInt32(Asteroids[i].asteroidPos.Y));
                        Write(" ");
                         
                    }


                    if ((Asteroids[i].asteroidPos.X >= consoleWidth) || (Asteroids[i].asteroidPos.X < 0))
                    {
                        Asteroids[i].asteroidPos = new Vector2(rand.Next(1, 15), Asteroids[i].asteroidPos.Y);
                    }

                    if (Asteroids[i].asteroidPos.Y >= consoleHeight)
                    {
                        Asteroids[i].asteroidPos = new Vector2(Asteroids[i].asteroidPos.X, rand.Next(consoleHeight));
                    }


                    Asteroids[i].MoveDown();

                    if (Asteroids[i].asteroidDirection == 1)
                    {
                        Asteroids[i].MoveRight();
                    }

                    else
                    {
                        Asteroids[i].MoveLeft();
                    }
                    


                    if (((Asteroids[i].asteroidPos.Y >= consoleHeight) || (Asteroids[i].asteroidPos.X >= consoleWidth)) || (Asteroids[i].asteroidPos.X <= 0))
                    {
                        Asteroids[i].asteroidPos = new Vector2(Asteroids[i].asteroidPos.X, 0);
                        Asteroids[i].asteroidPos =new Vector2(rand.Next(consoleWidth), Asteroids[i].asteroidPos.Y);
                        Asteroids[i].asteroidDirection = rand.Next(2);
                    }

                    if ((Asteroids[i].asteroidPos.X >= 0) && (Asteroids[i].asteroidPos.Y >= 0) && (Asteroids[i].asteroidPos.X < consoleWidth) && (Asteroids[i].asteroidPos.Y < consoleHeight))
                    {
                        SetCursorPosition(Convert.ToInt32(Asteroids[i].asteroidPos.X), Convert.ToInt32(Asteroids[i].asteroidPos.Y));
                        ForegroundColor = ConsoleColor.Red;
                        Write("O");
                        ResetColor();  

                    }
                }
            }
        }
    }
}


