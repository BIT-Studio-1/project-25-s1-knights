using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static gameproject.Bigger_Threats.BigShipsInfo;
using static gameproject.Bullet;
using static gameproject.Program;
using static gameproject.Character;
using static gameproject.Levels;
using static gameproject.Menu;
using static gameproject.Globals;
using static System.Console;

namespace gameproject
{
    internal class Bigger_Threats
    {

        public static class BigShipsInfo   //sets up things live movement speed and the max bigships
        {
            public static int bigShipSpawnTimer = 0, bigShipMoveRate = 10, maxBigShips = 2, bigShipSpeed = 10, bigShipSpawnRate = 10, bigShipMoveTimer = 0;
            public static List<BigShip> BiggerShips = new List<BigShip>();
        }

        public class BigShip
        {
            public int x { get; set; }
            public int y { get; set; }

            public void Move() => y++;
        }

        public static class bigShip
        {
            public static void CreateBigShip()
            {

                string drawBigShip = "|-V-|";

                int shipLength = drawBigShip.Length;  // turns drawBigShip into an int that can be used later in code for boundaries

                bigShipSpawnTimer++;
                bigShipMoveTimer++;

                if (level == 1)        //sets max ships and the move speed of the ships per level
                {
                    maxBigShips = 1;
                    bigShipMoveRate = 10;
                }

                if (level == 2)
                {
                    maxBigShips = 1;
                    bigShipMoveRate = 9;
                }

                if (level == 3)
                {
                    maxBigShips = 2;
                    bigShipMoveRate = 8;
                }

                if (level == 4)
                {
                    maxBigShips = 2;
                    bigShipMoveRate = 7;
                }

                if (level == 5)
                {
                    maxBigShips = 2;
                    bigShipMoveRate = 6;
                }

                if (bigShipSpawnTimer >= bigShipSpawnRate && BiggerShips.Count < maxBigShips)
                {
                    BiggerShips.Add(new BigShip { x = rand.Next(consoleWidth), y = 0 }); // Spaawning randomly along x axis at 0 y position

                    bigShipSpawnTimer = 0;
                }

                if (bigShipMoveTimer >= bigShipMoveRate)   //everytime the move timer meets the moverate specified, move it back to zero
                {
                    bigShipMoveTimer = 0;

                    for (int i = BiggerShips.Count - 1; i >= 0; i--)
                    {
                        if (BiggerShips[i].y >= consoleHeight)
                        {
                            BiggerShips[i].y = rand.Next(consoleHeight);
                        }

                        if (BiggerShips[i].x >= consoleWidth)
                        {
                            BiggerShips[i].x = rand.Next(consoleWidth);
                        }

                        if (BiggerShips[i].x >= 0 && BiggerShips[i].y >= 0 && BiggerShips[i].x + shipLength < consoleWidth && BiggerShips[i].y < consoleHeight)  //writes over the old position against the variable shipLength
                        {
                            SetCursorPosition(BiggerShips[i].x, BiggerShips[i].y);

                            Write(new string(' ', shipLength));
                        }

                        BiggerShips[i].Move();             //calls the method to move the threat ship downwards

                        if (BiggerShips[i].y >= consoleHeight)
                        {
                            BiggerShips[i].y = 0;
                            BiggerShips[i].x = rand.Next(consoleWidth);
                        }

                        if (BiggerShips[i].x >= 0 && BiggerShips[i].y >= 0 && BiggerShips[i].x + shipLength < consoleWidth && BiggerShips[i].y < consoleHeight)
                        //writes in clear space the threat ship against the length of the variable shipLength 
                        // and then prints in the clear space the variable drawBigShip. 

                        {
                            SetCursorPosition(BiggerShips[i].x, BiggerShips[i].y);
                            ForegroundColor = ConsoleColor.White;

                            Write(drawBigShip);
                            ResetColor();
                        }
                    }
                }

            }

       

        }

        
    }
}
