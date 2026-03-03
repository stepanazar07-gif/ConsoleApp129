using System;
using System.Threading;

namespace ConsoleApp129
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.CursorVisible = false;
            Console.OutputEncoding = System.Text.Encoding.UTF8;


            Map gameMap = new Map();
            gameMap.Map_generation();


            bool gameRunning = true;

            while (gameRunning)
            {

                Console.SetCursorPosition(0, 0);
                gameMap.Drawing_the_map();


                ConsoleKeyInfo keyInfo = Console.ReadKey(true);


                if (keyInfo.Key == ConsoleKey.Escape)
                {
                    gameRunning = false;
                    break;
                }


                if (keyInfo.Key == ConsoleKey.UpArrow ||
                    keyInfo.Key == ConsoleKey.DownArrow ||
                    keyInfo.Key == ConsoleKey.LeftArrow ||
                    keyInfo.Key == ConsoleKey.RightArrow)
                {
                    gameMap.MovePersons(keyInfo.Key);


                    gameMap.MovePersons();
                }
            }
            Console.Clear();
            Console.WriteLine("Игра завершена...!");
            
        }
    }
}
