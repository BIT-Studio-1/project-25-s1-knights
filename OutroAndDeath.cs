using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gameproject
{
    internal class OutroAndDeath
    {
        public static void ShowWin()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine(@"____    ____  ______    __    __     ____    __    ____  __  .__   __. 
\   \  /   / /  __  \  |  |  |  |    \   \  /  \  /   / |  | |  \ |  | 
 \   \/   / |  |  |  | |  |  |  |     \   \/    \/   /  |  | |   \|  | 
  \_    _/  |  |  |  | |  |  |  |      \            /   |  | |  . `  | 
    |  |    |  `--'  | |  `--'  |       \    /\    /    |  | |  |\   | 
    |__|     \______/   \______/         \__/  \__/     |__| |__| \__| 
                                                                       ");

            Console.ResetColor();

            Console.WriteLine();
            Console.WriteLine("YOU WIN!");
            Console.WriteLine("Press any key to continue....");

            Console.ReadKey(true);
        }

        public static void ShowLose()
        {
            Console.Clear();
            Console.WriteLine("YOU LOSE!");
            Console.WriteLine("Press any key to continue....");
            Console.ReadLine();//added so it doesnt close when you die
        }

    }
}
