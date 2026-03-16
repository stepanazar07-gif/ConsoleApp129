using ConsoleApp129.Core;
using ConsoleApp129.Exceptions;
using System;
using System.Threading;
using ConsoleApp129.Exceptions;

namespace ConsoleApp129
{
    /// <summary>
    /// Базовый класс карты. Управляет генерацией, отрисовкой и движением объектов
    /// </summary>
    internal class Map
    {
        /// <summary>
        /// Генератор случайных чисел для размещения объектов
        /// </summary>
        Random rand = new Random();

        /// <summary>
        /// Менеджер уровней для отслеживания прогресса
        /// </summary>
        public LevelManager LevelManager { get; set; }

        /// <summary>
        /// Двумерный массив объектов карты размером 25x25
        /// </summary>
        protected MapObject[,] map = new MapObject[25, 25];

        /// <summary>
        /// Координата X героя на карте
        /// </summary>
        protected int heroX;

        /// <summary>
        /// Координата Y героя на карте
        /// </summary>
        protected int heroY;

        /// <summary>
        /// Буфер для отрисовки карты (оптимизация вывода)
        /// </summary>
        private char[,] backBuffer = new char[25, 25];

        /// <summary>
        /// Генерация базового уровня карты (Уровень 1)
        /// Заполняет карту полями, деревьями, горами, врагами и зельями лечения
        /// </summary>
        public void Map_generation()
        {
            for (int i = 0; i < 25; i++)
            {
                for (int j = 0; j < 25; j++)
                {
                    map[i, j] = new Field();
                }
            }
            int treeCount = rand.Next(30, 50);
            for (int t = 0; t < treeCount; t++)
            {
                int x = rand.Next(0, 25);
                int y = rand.Next(0, 25);
                if (x != 12 || y != 12)
                {
                    if (map[x, y] is Field)
                    {
                        map[x, y] = new Tree();
                    }
                }
            }
            int mountainCount = rand.Next(15, 25);
            for (int m = 0; m < mountainCount; m++)
            {
                int x = rand.Next(0, 25);
                int y = rand.Next(0, 25);
                if (x != 12 || y != 12)
                {
                    if (map[x, y] is Field)
                    {
                        map[x, y] = new Mountain();
                    }
                }
            }
            int enemyCount = 0;
            while (enemyCount < 8)
            {
                int x = rand.Next(0, 25);
                int y = rand.Next(0, 25);
                if (map[x, y] is Field && (x != 12 || y != 12))
                {
                    map[x, y] = new Enemy(x, y);
                    enemyCount++;
                }
            }
            int HealsCount = rand.Next(9, 13);
            for (int i = 0; i < HealsCount; i++)
            {
                int x = rand.Next(0, 25);
                int y = rand.Next(0, 25);
                if (x != 12 || y != 12)
                {
                    if (map[x, y] is Field)
                    {
                        map[x, y] = new Heal();
                    }
                }
            }
            heroX = 12;
            heroY = 12;
            map[12, 12] = new Hero(12, 12);
            PlaceDoor();
            for (int i = 0; i < 25; i++)
            {
                for (int j = 0; j < 25; j++)
                {
                    backBuffer[i, j] = ' ';
                }
            }
        }

        /// <summary>
        /// Отрисовка карты в консоли с подсчётом объектов на уровне
        /// Выводит статистику по врагам, деревьям, горам в зависимости от уровня
        /// </summary>
        public void Drawing_the_map()
        {
            for (int i = 0; i < 25; i++)
            {
                for (int j = 0; j < 25; j++)
                {
                    char currentChar = map[i, j].Rendering_on_the_map();
                    backBuffer[i, j] = currentChar;
                }
            }
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < 25; i++)
            {
                for (int j = 0; j < 25; j++)
                {
                    map[i, j].Rendering_on_the_map();
                    Console.Write(backBuffer[i, j] + " ");
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
            int enemyCount = 0;
            int treeCount = 0;
            int mountainCount = 0;
            int cactusCount = 0;
            int rockCount = 0;
            int duneCount = 0;
            for (int i = 0; i < 25; i++)
            {
                for (int j = 0; j < 25; j++)
                {
                    if (map[i, j] is Enemy)
                    {
                        enemyCount++;
                    }
                    if (map[i, j] is Tree || map[i, j] is SnowTree)
                    {
                        treeCount++;
                    }
                    if (map[i, j] is Mountain || map[i, j] is IceMountain)
                    {
                        mountainCount++;
                    }
                    if (map[i, j] is Cactus)
                    {
                        cactusCount++;
                    }
                    if (map[i, j] is Rock)
                    {
                        rockCount++;
                    }
                    if (map[i, j] is SandDune)
                    {
                        duneCount++;
                    }
                }
            }
            if (LevelManager.CurrentLevel == 1)
                Console.WriteLine($" Врагов : {enemyCount} |  Деревьев : {treeCount} |  Гор : {mountainCount}  ");
            else if (LevelManager.CurrentLevel == 2)
                Console.WriteLine($" Врагов : {enemyCount} |  ❄️  Снежных деревьев : {treeCount} |  ❄️  Ледяных гор : {mountainCount}  ");
            else if (LevelManager.CurrentLevel == 3)
                Console.WriteLine($" Врагов : {enemyCount} |  Кактусов : {cactusCount} |  Скал : {mountainCount} |  Дюн : {duneCount}  ");
            else if (LevelManager.CurrentLevel == 4)
                Console.WriteLine($" 🏆  ФИНАЛЬНЫЙ УРОВЕНЬ |  Босс Красава вообще |  Стен :  много  ");
        }

        /// <summary>
        /// Перемещение врагов по карте (простой ИИ)
        /// Враги случайным образом перемещаются на соседние клетки
        /// </summary>
        public void MovePersons()
        {
            MapObject[,] newMap = new MapObject[map.GetLength(0), map.GetLength(1)];
            Array.Copy(map, newMap, map.Length);
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] is Enemy)
                    {
                        int direction = rand.Next(4);
                        int newX = i, newY = j;
                        switch (direction)
                        {
                            case 0:
                                newX = (i - 1 + map.GetLength(0)) % map.GetLength(0);
                                break;
                            case 1:
                                newX = (i + 1) % map.GetLength(0);
                                break;
                            case 2:
                                newY = (j - 1 + map.GetLength(1)) % map.GetLength(1);
                                break;
                            case 3:
                                newY = (j + 1) % map.GetLength(1);
                                break;
                        }
                        if (newMap[newX, newY] is Field)
                        {
                            newMap[newX, newY] = map[i, j];
                            newMap[i, j] = new Field();
                        }
                    }
                }
            }
            Array.Copy(newMap, map, map.Length);
        }

        /// <summary>
        /// Обработка перемещения героя по нажатию клавиши
        /// Обрабатывает столкновения с врагами, зельями, артефактами, дверью и боссом
        /// </summary>
        /// <param name="key">Нажатая клавиша управления (стрелки)</param>
        /// <exception cref="InvalidOperationException">Если LevelManager равен null при попытке перехода</exception>
        public void MovePersons(ConsoleKey key)
        {
            Hero hero = FindHero();
            if (hero != null && hero.FreezeTurns > 0)
            {
                hero.FreezeTurns--;
                Console.SetCursorPosition(0, 27);
                Console.Write($" ❄️  Заморозка :  осталось ходов {hero.FreezeTurns}   ");
                Thread.Sleep(300);
                return;
            }
            if (hero != null && hero.SandTurns > 0)
            {
                hero.SandTurns--;
                Console.SetCursorPosition(0, 27);
                Console.Write($" 🏜️  Песок в глазах :  осталось ходов {hero.SandTurns}   ");
                Thread.Sleep(300);
                return;
            }
            if (hero != null && hero.PoisonTurns > 0)
            {
                hero.PoisonTurns--;
                hero.PoisonCounter++;
                if (hero.PoisonCounter >= 3)
                {
                    hero.Stats.HP -= 1;
                    hero.PoisonCounter = 0;
                    Console.SetCursorPosition(0, 27);
                    Console.Write($" 🐍  ЯД ! -1 HP ( осталось {hero.PoisonTurns}  ходов )   ");
                }
                else
                {
                    Console.SetCursorPosition(0, 27);
                    Console.Write($" 🐍  Отравлен :  ход {hero.PoisonCounter}/3   ");
                }
            }
            int rows = map.GetLength(0);
            int cols = map.GetLength(1);
            MapObject[,] newMap = new MapObject[rows, cols];
            Array.Copy(map, newMap, map.Length);
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    if (map[row, col] is Hero)
                    {
                        int newRow = row;
                        int newCol = col;
                        switch (key)
                        {
                            case ConsoleKey.UpArrow:
                                newRow = (row - 1 + rows) % rows;
                                break;
                            case ConsoleKey.DownArrow:
                                newRow = (row + 1) % rows;
                                break;
                            case ConsoleKey.LeftArrow:
                                newCol = (col - 1 + cols) % cols;
                                break;
                            case ConsoleKey.RightArrow:
                                newCol = (col + 1) % cols;
                                break;
                        }
                        if (newMap[newRow, newCol] is Field)
                        {
                            newMap[newRow, newCol] = map[row, col];
                            newMap[row, col] = new Field();
                            heroX = newCol;
                            heroY = newRow;
                        }
                        else if (newMap[newRow, newCol] is Heal)
                        {
                            hero.FreezeTurns = 0;
                            hero.SandTurns = 0;
                            hero.PoisonTurns = 0;
                            hero.Stats.HP += 2;
                            newMap[newRow, newCol] = map[row, col];
                            newMap[row, col] = new Field();
                            heroX = newCol;
                            heroY = newRow;
                        }
                        else if (newMap[newRow, newCol] is Amulet amulet)
                        {
                            hero.HasAmulet = true;
                            newMap[newRow, newCol] = map[row, col];
                            newMap[row, col] = new Field();
                            heroX = newCol;
                            heroY = newRow;
                            Console.SetCursorPosition(0, 37);
                            Console.Write(" ✨  АМУЛЕТ ПОДОБРАН ! (+5 к урону против босса)   ");
                            Thread.Sleep(500);
                        }
                        else if (newMap[newRow, newCol] is Crown crown)
                        {
                            hero.HasCrown = true;
                            newMap[newRow, newCol] = map[row, col];
                            newMap[row, col] = new Field();
                            heroX = newCol;
                            heroY = newRow;
                            Console.SetCursorPosition(0, 37);
                            Console.Write(" ✨  КОРОНА ПОДОБРАНА ! (+8 к урону против босса)   ");
                            Thread.Sleep(500);
                        }
                        else if (newMap[newRow, newCol] is Scepter scepter)
                        {
                            hero.HasScepter = true;
                            newMap[newRow, newCol] = map[row, col];
                            newMap[row, col] = new Field();
                            heroX = newCol;
                            heroY = newRow;
                            Console.SetCursorPosition(0, 37);
                            Console.Write(" ✨  СКИПЕТР ПОДОБРАН ! (+12 к урону против босса)   ");
                            Thread.Sleep(500);
                        }
                        else if (newMap[newRow, newCol] is Boss boss)
                        {
                            bool bossDied = boss.SimpleFight(hero, this);
                            if (bossDied)
                            {
                                newMap[newRow, newCol] = map[row, col];
                                newMap[row, col] = new Field();
                                heroX = newCol;
                                heroY = newRow;
                            }
                        }
                        else if (newMap[newRow, newCol] is Enemy enemy)
                        {
                            newMap[newRow, newCol] = map[row, col];
                            newMap[row, col] = new Field();
                            heroX = newCol;
                            heroY = newRow;
                            if (enemy is WinterEnemy winterEnemy)
                            {
                                int chance = rand.Next(100);
                                if (chance < winterEnemy.FreezeChance)
                                {
                                    hero.FreezeTurns = 2;
                                    Console.SetCursorPosition(0, 27);
                                    Console.Write(" ❄️  ВАС ЗАМОРОЗИЛИ ! 2 хода пропуска   ");
                                    Thread.Sleep(500);
                                }
                            }
                            if (enemy is DesertEnemy desertEnemy)
                            {
                                int chance = rand.Next(100);
                                if (chance < desertEnemy.SandChance)
                                {
                                    hero.SandTurns = 2;
                                    Console.SetCursorPosition(0, 27);
                                    Console.Write(" 🏜️  ЗМЕЯ БРОСИЛА ПЕСОК ! 2 хода пропуска   ");
                                    Thread.Sleep(500);
                                }
                            }
                            hero.Stats.TakeDamage(enemy.Damage);
                            Console.SetCursorPosition(0, 27);
                            Console.Write(" ⚔  Бой !  Получено " + enemy.Damage + " урона         ");
                        }
                        else if (newMap[newRow, newCol] is Door)
                        {
                            int enemiesAlive = 0;
                            for (int x = 0; x < rows; x++)
                            {
                                for (int y = 0; y < cols; y++)
                                {
                                    if (map[x, y] is Enemy)
                                        enemiesAlive++;
                                }
                            }
                            if (enemiesAlive > 0)
                            {
                                Console.SetCursorPosition(0, 28);
                                Console.WriteLine($" Нельзя войти !  Осталось врагов : {enemiesAlive}");
                                Thread.Sleep(1000);
                                Console.SetCursorPosition(0, 28);
                                Console.Write(new string(' ', 40));
                            }
                            else
                            {
                                if (LevelManager == null)
                                    throw new InvalidOperationException("LevelManager не должен быть null");
                                LevelManager.CurrentLevel++;
                                if (LevelManager.CurrentLevel == 4)
                                {
                                    NewLevel3 bossLevel = new NewLevel3();
                                    map = bossLevel.Map_generation(hero);
                                }
                                else if (LevelManager.CurrentLevel == 2)
                                {
                                    NewLevel1 winterlevel = new NewLevel1();
                                    map = winterlevel.Map_generation(hero);
                                }
                                else if (LevelManager.CurrentLevel == 3)
                                {
                                    NewLevel2 desertLevel = new NewLevel2();
                                    map = desertLevel.Map_generation(hero);
                                }
                                heroX = 12;
                                heroY = 12;
                                LevelManager.ResetLevel();
                                Console.Clear();
                                Drawing_the_map();
                                return;
                            }
                        }
                    }
                }
            }
            Array.Copy(newMap, map, map.Length);
        }

        /// <summary>
        /// Получение объекта по координатам на карте
        /// </summary>
        /// <param name="x">Координата X (0-24)</param>
        /// <param name="y">Координата Y (0-24)</param>
        /// <returns>Объект MapObject или null если координаты вне границ</returns>
        public MapObject GetObjectAt(int x, int y)
        {
            if (x >= 0 && x < 25 && y >= 0 && y < 25)
            {
                return map[x, y];
            }
            return null;
        }

        /// <summary>
        /// Размещение объекта на карте по указанным координатам
        /// </summary>
        /// <param name="x">Координата X (0-24)</param>
        /// <param name="y">Координата Y (0-24)</param>
        /// <param name="obj">Объект для размещения на карте</param>
        public void PlaceObject(int x, int y, MapObject obj)
        {
            if (x >= 0 && x < 25 && y >= 0 && y < 25)
            {
                map[x, y] = obj;
            }
        }

        /// <summary>
        /// Генерация нового уровня с увеличением сложности
        /// Создаёт карту с деревьями, горами, врагами и зельями лечения
        /// </summary>
        /// <param name="level">Номер уровня (1-4)</param>
        public void GenerateNewLevel(int level)
        {
            for (int i = 0; i < 25; i++)
            {
                for (int j = 0; j < 25; j++)
                {
                    map[i, j] = new Field();
                }
            }
            int enemyCount = 8 + level * 2;
            int treeCount = rand.Next(30, 50);
            int mountainCount = rand.Next(15, 25);
            for (int t = 0; t < treeCount; t++)
            {
                int x = rand.Next(0, 25);
                int y = rand.Next(0, 25);
                if ((x != 12 || y != 12) && map[x, y] is Field)
                {
                    map[x, y] = new Tree();
                }
            }
            for (int m = 0; m < mountainCount; m++)
            {
                int x = rand.Next(0, 25);
                int y = rand.Next(0, 25);
                if ((x != 12 || y != 12) && map[x, y] is Field)
                {
                    map[x, y] = new Mountain();
                }
            }
            int enemyDamage = 3 + level;
            int enemiesPlaced = 0;
            while (enemiesPlaced < enemyCount)
            {
                int x = rand.Next(0, 25);
                int y = rand.Next(0, 25);
                if (map[x, y] is Field && (x != 12 || y != 12))
                {
                    Enemy enemy = new Enemy(x, y);
                    enemy.Damage = enemyDamage;
                    map[x, y] = enemy;
                    enemiesPlaced++;
                }
            }
            int HealsCount = rand.Next(9, 13);
            for (int i = 0; i < HealsCount; i++)
            {
                int x = rand.Next(0, 25);
                int y = rand.Next(0, 25);
                if (x != 12 || y != 12)
                {
                    if (map[x, y] is Field)
                    {
                        map[x, y] = new Heal();
                    }
                }
            }
            heroX = 12;
            heroY = 12;
            map[12, 12] = new Hero(12, 12);
            PlaceDoor();
            for (int i = 0; i < 25; i++)
            {
                for (int j = 0; j < 25; j++)
                {
                    backBuffer[i, j] = ' ';
                }
            }
        }

        /// <summary>
        /// Размещение выхода (двери) на карте в свободной клетке
        /// Ищет первую свободную клетку Field и размещает там дверь
        /// </summary>
        /// <returns>Кортеж с координатами двери (X, Y)</returns>
        protected (int X, int Y) PlaceDoor()
        {
            for (int x = 24; x >= 0; x--)
            {
                for (int y = 24; y >= 0; y--)
                {
                    if (map[x, y] is Field && !(x == 12 && y == 12))
                    {
                        Door door = new Door();
                        door.X = x;
                        door.Y = y;
                        map[x, y] = door;
                        return (x, y);
                    }
                }
            }
            map[24, 24] = new Door() { X = 24, Y = 24 };
            return (24, 24);
        }

        /// <summary>
        /// Поиск объекта героя на карте путём перебора всех клеток
        /// </summary>
        /// <returns>Объект Hero или null если герой не найден</returns>
        public Hero FindHero()
        {
            for (int i = 0; i < map.GetLength(0); i++)
                for (int j = 0; j < map.GetLength(1); j++)
                    if (map[i, j] is Hero h) return h;
            return null;

            throw new ObjectNotFoundException("Герой не найден на карте");
        }

        /// <summary>
        /// Проверка условий и спавн артефактов после победы над всеми врагами
        /// Артефакты появляются только когда все враги убиты и ещё не подобраны
        /// </summary>
        /// <param name="hero">Объект героя для проверки условий подбора</param>
        public void CheckAndSpawnArtifact(Hero hero)
        {
            int enemiesAlive = 0;
            for (int x = 0; x < 25; x++)
            {
                for (int y = 0; y < 25; y++)
                {
                    if (map[x, y] is Enemy)
                        enemiesAlive++;
                }
            }
            if (enemiesAlive == 0)
            {
                bool artifactExists = false;
                for (int x = 0; x < 25; x++)
                {
                    for (int y = 0; y < 25; y++)
                    {
                        if (map[x, y] is Amulet || map[x, y] is Crown || map[x, y] is Scepter)
                        {
                            artifactExists = true;
                            break;
                        }
                    }
                    if (artifactExists) break;
                }
                if (artifactExists) return;
                int ax, ay;
                do
                {
                    ax = rand.Next(0, 25);
                    ay = rand.Next(0, 25);
                } while (map[ax, ay] is Field == false || (ax == 12 && ay == 12));
                if (LevelManager.CurrentLevel == 1 && !hero.HasAmulet)
                {
                    map[ax, ay] = new Amulet(ax, ay) { IsVisible = true };
                    Console.SetCursorPosition(0, 28);
                    Console.Write("  🔮  АМУЛЕТ ПОЯВИЛСЯ !  Ищите *        ");
                }
                else if (LevelManager.CurrentLevel == 2 && !hero.HasCrown)
                {
                    map[ax, ay] = new Crown(ax, ay) { IsVisible = true };
                    Console.SetCursorPosition(0, 28);
                    Console.Write("  👑  КОРОНА ПОЯВИЛАСЬ !  Ищите ^        ");
                }
                else if (LevelManager.CurrentLevel == 3 && !hero.HasScepter)
                {
                    map[ax, ay] = new Scepter(ax, ay) { IsVisible = true };
                    Console.SetCursorPosition(0, 28);
                    Console.Write("  🗡  СКИПЕТР ПОЯВИЛСЯ !  Ищите S        ");
                }
            }
        }
    }
}