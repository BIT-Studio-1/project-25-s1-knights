using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using System.Xml.XPath;
using static gameproject.Bigger_Threats.BigShipsInfo;
using static gameproject.Bullet;
using static gameproject.Character;
using static gameproject.Globals;
using static gameproject.Levels;
using static gameproject.Menu;
using static gameproject.Program;
using static System.Console;

namespace gameproject
{
    internal class Bigger_Threats
    {

        public static class BigShipsInfo   //sets up things live movement speed and the max bigships
        {
            public static int bigShipSpawnTimer = 0, bigShipMoveRate = 10, maxBigShips = 2, bigShipSpeed = 10, bigShipSpawnRate = 10, bigShipMoveTimer = 0;
            public static int threatBulletMoveRate = 2, threatBulletMoveTimer = 0;
            public static List<BigShip> BiggerShips = new List<BigShip>();
            public static List<ThreatBulletsPosition> ThreatBullets = new List<ThreatBulletsPosition>();
        }

        public class BigShip
        {
            public Vector2 BigShipPos { get; set; }

            public void Move() => BigShipPos = new Vector2(BigShipPos.X, BigShipPos.Y + 1);
        }

        public class ThreatBulletsPosition
        {
            public Vector2 ThreatBulletPos { get; set; }

            public void BulletsMove() => ThreatBulletPos = new Vector2(ThreatBulletPos.X, ThreatBulletPos.Y + 1);
        }

        public static class bigShip
        {
            public static void CreateBigShip()
            {

                string drawBigShip = "|-V-|";

                int shipLength = drawBigShip.Length;  // turns drawBigShip into an int that can be used later in code for boundaries

                bigShipSpawnTimer++;
                bigShipMoveTimer++;

                switch (level)
                {
                    case 1:
                        maxBigShips = 1;
                        bigShipMoveRate = 10;
                        break;

                    case 2:
                        maxBigShips = 1;
                        bigShipMoveRate = 8;
                        break;

                    case 3:
                        maxBigShips = 2;
                        bigShipMoveRate = 7;
                        break;

                    case 4:
                        maxBigShips = 2;
                        bigShipMoveRate = 6;
                        break;

                    case 5:
                        maxBigShips = 3;
                        bigShipMoveRate = 5;
                        break;

                    default:
                        break;


                }


                if (bigShipSpawnTimer >= bigShipSpawnRate && BiggerShips.Count < maxBigShips)
                {
                    Math.Clamp(1, 2, WindowWidth - shipLength - 1);

                    BiggerShips.Add(new BigShip { BigShipPos = new Vector2(rand.Next(WindowWidth - shipLength -1)) });  // Spaawning randomly along x axis at 0 y position

                    bigShipSpawnTimer = 0;
                }

                if (bigShipMoveTimer >= bigShipMoveRate)   //everytime the move timer meets the moverate specified, move it back to zero
                {
                    bigShipMoveTimer = 0;

                    for (int i = BiggerShips.Count - 1; i >= 0; i--)
                    {
                        if (BiggerShips[i].BigShipPos.X >= consoleWidth)
                        {
                            BiggerShips[i].BigShipPos = new Vector2(rand.Next(consoleWidth), BiggerShips[i].BigShipPos.Y);
                        }

                        if (BiggerShips[i].BigShipPos.Y >= consoleHeight)
                        {
                            BiggerShips[i].BigShipPos = new Vector2(BiggerShips[i].BigShipPos.X, rand.Next(consoleHeight));
                        }

                        if (BiggerShips[i].BigShipPos.X >= 0 && BiggerShips[i].BigShipPos.Y >= 0 && BiggerShips[i].BigShipPos.X + shipLength <= consoleWidth &&
                            BiggerShips[i].BigShipPos.Y < consoleHeight)  //writes over the old position against the variable shipLength
                        {
                            SetCursorPosition(Convert.ToInt32(BiggerShips[i].BigShipPos.X), Convert.ToInt32(BiggerShips[i].BigShipPos.Y));

                            Write(new string(' ', shipLength));
                        }

                        BiggerShips[i].Move();             //calls the method to move the threat ship downwards



                        if (BiggerShips[i].BigShipPos.Y >= consoleHeight)
                        {
                            BiggerShips[i].BigShipPos = new Vector2(rand.Next(consoleWidth), 0);
                        }

                        if (BiggerShips[i].BigShipPos.X >= 0 && BiggerShips[i].BigShipPos.Y >= 0 && BiggerShips[i].BigShipPos.X < consoleWidth && BiggerShips[i].BigShipPos.Y < consoleHeight)
                        {
                            SetCursorPosition(Convert.ToInt32(BiggerShips[i].BigShipPos.X), Convert.ToInt32(BiggerShips[i].BigShipPos.Y));
                            ForegroundColor = ConsoleColor.White;

                            Write(drawBigShip);
                            ResetColor();
                        }
                    }
                }
            }

        }

        public static void ThreatShipShoot()
        {
            int bulletSpawn = rand.Next(20);

            threatBulletMoveTimer++;
            threatBulletMoveRate = 2;


            if (threatBulletMoveTimer >= threatBulletMoveRate)
            {
                threatBulletMoveTimer = 0;

                for (int i = ThreatBullets.Count - 1; i >= 0; i--)
                {
                    if (ThreatBullets[i].ThreatBulletPos.Y >= 0 && ThreatBullets[i].ThreatBulletPos.Y < WindowHeight &&
                        (ThreatBullets[i].ThreatBulletPos.X >= 0) && (ThreatBullets[i].ThreatBulletPos.X < WindowWidth))
                    // check if bullets are still within console window
                    {
                        SetCursorPosition(Convert.ToInt32(ThreatBullets[i].ThreatBulletPos.X), Convert.ToInt32(ThreatBullets[i].ThreatBulletPos.Y));
                        Write(' ');
                    }

                    ThreatBullets[i].BulletsMove();

                    if (ThreatBullets[i].ThreatBulletPos.Y < 0 || ThreatBullets[i].ThreatBulletPos.Y >= WindowHeight || ThreatBullets[i].ThreatBulletPos.X < 0
                        || ThreatBullets[i].ThreatBulletPos.X >= WindowWidth)
                    // checks if bullets are still in console window, removes them from list if they are out of bounds
                    {
                        ThreatBullets.RemoveAt(i);
                        continue;
                    }

                    else

                    {
                        SetCursorPosition(Convert.ToInt32(ThreatBullets[i].ThreatBulletPos.X), Convert.ToInt32(ThreatBullets[i].ThreatBulletPos.Y));
                        Write('*');
                    }

                }
            }

            if (bulletSpawn == 10)

            {
                for (int j = 0; j < BiggerShips.Count; j++)
                {
                    int spawnX = Convert.ToInt32(BiggerShips[j].BigShipPos.X + 2);
                    int spawnY = Convert.ToInt32(BiggerShips[j].BigShipPos.Y + 1);

                    if ((spawnX < WindowWidth) && (spawnY < WindowHeight))
                    {
                        ThreatBullets.Add(new ThreatBulletsPosition { ThreatBulletPos = new Vector2(spawnX, spawnY) });
                    }
                }
                // this loop just checks all ships on the screen, and assigns bullets to each ship.

            }




        }



    }


}




