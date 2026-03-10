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
                string[] menuItems = { "Новая игра", "Загрузить", "Информация", "Выход" };
                Menu mainMenu = new Menu(menuItems);
                int choice = mainMenu.Show();

                if (choice == 0)
                    StartGame(false);
                else if (choice == 1)
                    StartGame(true);
                else if (choice == 2)
                    ShowInfo();
                else
                    break;
            }

            Console.Clear();
            Console.WriteLine("Игра завершена!");
            Console.ReadKey();
        }

        static void StartGame(bool loadSaved)
        {
            gameMap = new Map();
            player = new Hero(12, 12);
            levelManager = new LevelManager();

            if (loadSaved)
            {
                if (!SaveSystem.LoadGame(player))
                {
                    Console.WriteLine("Начинаем новую игру...");
                    gameMap.GenerateNewLevel(1);
                    Thread.Sleep(1500);
                }
                else
                {
                    Console.WriteLine("Игра загружена!");
                    gameMap.GenerateNewLevel(levelManager.CurrentLevel);
                    Thread.Sleep(1500);
                }
            }
            else
            {
                gameMap.GenerateNewLevel(1);
            }

            bool gameRunning = true;

            while (gameRunning)
            {
                Console.SetCursorPosition(0, 0);
                gameMap.Drawing_the_map();

                Console.SetCursorPosition(0, 26);
                player.Stats.ShowStats();
                Console.WriteLine($"Уровень подземелья: {levelManager.CurrentLevel}");
                Console.WriteLine("F5 - сохранить | ESC - меню | Стрелки - движение");

                levelManager.CheckAllEnemiesDefeated(gameMap);
                gameMap.CheckDoorEntry(player, levelManager);

                if (player.Stats.HP <= 0)
                {
                    Console.SetCursorPosition(0, 29);
                    Console.WriteLine("💀 ТЫ УМЕР! Возрождение...   ");
                    Thread.Sleep(1500);

                    player.Stats.HP = player.Stats.MaxHP;
                    gameMap.GenerateNewLevel(levelManager.CurrentLevel);
                    continue;
                }

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.Escape)
                    break;

                if (keyInfo.Key == ConsoleKey.F5)
                {
                    SaveSystem.SaveGame(player, gameMap);
                    Console.SetCursorPosition(0, 30);
                    Console.WriteLine("Игра сохранена!          ");
                    continue;
                }

                // БЛОК С КЛАВИШЕЙ X ПОЛНОСТЬЮ УДАЛЕН

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
            Console.WriteLine("Дверь: 🚪 - выход на уровень");
            Console.WriteLine("\n❤ HP - здоровье");
            Console.WriteLine("🛡 Броня - уменьшает урон");
            Console.WriteLine("\nУбей всех врагов - откроется дверь!");
            Console.WriteLine("\nУправление: стрелки, ESC - меню");
            Console.WriteLine("Сохранение: F5");
            Console.WriteLine("\nНажми любую клавишу...");
            Console.ReadKey();
        }
    }
}