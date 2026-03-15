using ConsoleApp129.Core;
using ConsoleApp129.Save;
using System;
using System.Threading;

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
                string[] menuItems = { "Новая игра", "Загрузить игру", "Информация", "Выход" };
                Menu mainMenu = new Menu(menuItems);
                int choice = mainMenu.Show();

                if (choice == 0)
                    StartGame();
                else if (choice == 1)
                {
                    var savedData = SaveManager.Load();
                    if (savedData != null)
                        ResumeGame(savedData);
                }
                else if (choice == 2)
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
            levelManager = new LevelManager();
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
                Console.WriteLine("ESC - меню | Стрелки - движение | S - сохранение");

                levelManager.CheckEnemies(gameMap);
                gameMap.CheckAndSpawnArtifact(player);

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

              
                if (keyInfo.Key == ConsoleKey.S)
                {
                    SaveGame();
                    Thread.Sleep(1000);

                   
                    continue;
                }

                if (keyInfo.Key == ConsoleKey.Escape)
                {
                    Console.SetCursorPosition(0, 30);
                    Console.Write("💾 Сохранить перед выходом? (Y/N): ");
                    var saveKey = Console.ReadKey(true).Key;

                    if (saveKey == ConsoleKey.Y || saveKey == ConsoleKey.N) 
                    {
                        SaveGame();
                        Thread.Sleep(1000);
                    }

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
        }

       
        static void ResumeGame(GameData savedData)
        {
            levelManager = new LevelManager();
            levelManager.CurrentLevel = savedData.MapLevel;

            gameMap = new Map();
            gameMap.LevelManager = levelManager;

            if (savedData.MapLevel == 1)
                gameMap.GenerateNewLevel(1);
            else if (savedData.MapLevel == 2)
            {
                var level2 = new NewLevel1();
                var tempHero = new Hero(12, 12);
                level2.Map_generation(tempHero);
                gameMap = level2;
                gameMap.LevelManager = levelManager;
            }
            else if (savedData.MapLevel == 3)
            {
                var level3 = new NewLevel2();
                var tempHero = new Hero(12, 12);
                level3.Map_generation(tempHero);
                gameMap = level3;
                gameMap.LevelManager = levelManager;
            }
            else if (savedData.MapLevel == 4)
            {
                var level4 = new NewLevel3();
                var tempHero = new Hero(12, 12);
                level4.Map_generation(tempHero);
                gameMap = level4;
                gameMap.LevelManager = levelManager;
            }

            player = gameMap.FindHero();
            if (player != null)
            {
                player.Stats.HP = savedData.HeroHP;
                player.Stats.MaxHP = savedData.HeroMaxHP;
                player.Stats.Armor = savedData.HeroArmor;
                player.Stats.Level = savedData.HeroLevel;
                player.HasAmulet = savedData.HasAmulet;
                player.HasCrown = savedData.HasCrown;
                player.HasScepter = savedData.HasScepter;
                player.FreezeTurns = savedData.FreezeTurns;
                player.SandTurns = savedData.SandTurns;
                player.PoisonTurns = savedData.PoisonTurns;
            }

            bool gameRunning = true;
            while (gameRunning)
            {
                Console.SetCursorPosition(0, 0);
                gameMap.Drawing_the_map();
                Console.SetCursorPosition(0, 26);
                Console.WriteLine("Этаж: " + levelManager.CurrentLevel);
                player.Stats.ShowStats();
                Console.WriteLine("ESC - меню | Стрелки - движение | S - сохранение");

                levelManager.CheckEnemies(gameMap);
                gameMap.CheckAndSpawnArtifact(player);

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

                if (keyInfo.Key == ConsoleKey.S)
                {
                    SaveGame();
                    Thread.Sleep(1000);
                    continue;
                }

                if (keyInfo.Key == ConsoleKey.Escape)
                {
                    Console.SetCursorPosition(0, 30);
                    Console.Write("💾 Сохранить перед выходом? (Y/N): ");
                    var saveKey = Console.ReadKey(true).Key;

                    if (saveKey == ConsoleKey.Y || saveKey == ConsoleKey.N)  
                    {
                        SaveGame();
                        Thread.Sleep(1000);
                    }

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
        }

      
        static void SaveGame()
        {
            var saveData = new GameData
            {
                MapLevel = levelManager.CurrentLevel,
                HeroHP = player.Stats.HP,
                HeroMaxHP = player.Stats.MaxHP,
                HeroArmor = player.Stats.Armor,
                HeroLevel = player.Stats.Level,
                HasAmulet = player.HasAmulet,
                HasCrown = player.HasCrown,
                HasScepter = player.HasScepter,
                FreezeTurns = player.FreezeTurns,
                SandTurns = player.SandTurns,
                PoisonTurns = player.PoisonTurns
            };
            SaveManager.Save(saveData);
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
            Console.WriteLine("\nУправление: стрелки, ESC - меню, S - сохранение");
            Console.WriteLine("\nНажми любую клавишу...");
            Console.ReadKey();
        }
    }
}