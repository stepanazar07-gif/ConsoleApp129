using System;
using System.Threading;

namespace ConsoleApp129
{
    class Program
    {
        static Hero player;
        static Map gameMap;

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            while (true)
            {
                string[] menuItems = { "Новая игра", "Информация", "Выход" };
                Menu mainMenu = new Menu(menuItems);
                int choice = mainMenu.Show();

                if (choice == 0)
                    StartGame();
                else if (choice == 1)
                    ShowInfo();
                else
                    break;
            }

            Console.Clear();
            Console.WriteLine("Игра завершена!");
            Console.ReadKey();
        }

        static void StartGame()
        {
            gameMap = new Map();
            gameMap.Map_generation();
            player = new Hero(12, 12);

            bool gameRunning = true;

            while (gameRunning)
            {
              
                gameMap.Drawing_the_map();

              
                Console.SetCursorPosition(0, 26);
                player.Stats.ShowStats();

                Console.WriteLine("ESC - меню, стрелки - движение");

                if (player.Stats.HP <= 0)
                {
                    Console.WriteLine("\n💀 ТЫ УМЕР! Нажми любую клавишу...");
                    Console.ReadKey();
                    break;
                }

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.Escape)
                    break;

                if (keyInfo.Key == ConsoleKey.UpArrow ||
                    keyInfo.Key == ConsoleKey.DownArrow ||
                    keyInfo.Key == ConsoleKey.LeftArrow ||
                    keyInfo.Key == ConsoleKey.RightArrow)
                {
                
                    gameMap.MovePersons(keyInfo.Key);
                   
                    gameMap.MovePersons();
                 
                    gameMap.CheckCombat(player);
                }
            }
        }

        static void ShowInfo()
        {
            Console.Clear();
            Console.WriteLine("=== ОБ ИГРЕ ===\n");
            Console.WriteLine("Горы: ▲ - непроходимы");
            Console.WriteLine("Леса: ♣ - декор");
            Console.WriteLine("Враг: ☺ - отнимает HP");
            Console.WriteLine("Герой: ☻ - желтый");
            Console.WriteLine("\n❤ HP - здоровье");
            Console.WriteLine("🛡 Броня - уменьшает урон");
            Console.WriteLine("⭐ XP - опыт за убийства");
            Console.WriteLine("🎯 Уровень - растет с опытом");
            Console.WriteLine("\nУправление: стрелки, ESC - меню");
            Console.WriteLine("\nНажми любую клавишу...");
            Console.ReadKey();
        }
    }
}