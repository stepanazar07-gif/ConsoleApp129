using System;
using System.Threading;
using ConsoleApp129.Core;

namespace ConsoleApp129
{
    class Program
    {
        static Hero player;
        static Map gameMap;
        static LevelManager levelManager;

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WindowWidth = 60;
            Console.WindowHeight = 40;

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
            LevelManager levelManager = new LevelManager();
            gameMap.LevelManager = levelManager;

            gameMap.GenerateNewLevel(1);

            bool gameRunning = true;

            player = gameMap.FindHero();

            while (gameRunning)
            {
                Console.SetCursorPosition(0, 0);
                gameMap.Drawing_the_map();
                Console.SetCursorPosition(0, 26);
                Console.WriteLine("Этаж: " + levelManager.CurrentLevel);
                player.Stats.ShowStats();
                Console.WriteLine("ESC - меню | Стрелки - движение");

                levelManager.CheckEnemies(gameMap);

                if (player.Stats.HP <= 0)
                {
                    Console.SetCursorPosition(0, 29);
                    Console.WriteLine("💀 ТЫ УМЕР! Возрождение...");
                    Thread.Sleep(1500);

                    player.Stats.HP = player.Stats.MaxHP;
                    gameMap.GenerateNewLevel(levelManager.CurrentLevel);
                    continue;
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
            Console.WriteLine("Дверь: 🚪 - выход на уровень");
            Console.WriteLine("\n❤ HP - здоровье");
            Console.WriteLine("🛡 Броня - уменьшает урон");
            Console.WriteLine("\nУбей всех врагов - откроется дверь!");
            Console.WriteLine("\nУправление: стрелки, ESC - меню");
            Console.WriteLine("\nНажми любую клавишу...");
            Console.ReadKey();
        }
    }
}