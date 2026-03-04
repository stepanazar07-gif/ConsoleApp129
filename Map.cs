using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp129
{
    internal class Map
    {
        Random rand = new Random();
        MapObject[,] map = new MapObject[25, 25];

        public void Map_generation()
        {
            // Сначала заполняем всё полем
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    map[i, j] = new Field();
                }
            }

            // ========== ГЕНЕРАЦИЯ ЛЕСА (100% появление) ==========
            int treeCount = rand.Next(15, 25); // 15-25 деревьев
            for (int t = 0; t < treeCount; t++)
            {
                int x = rand.Next(0, map.GetLength(0));
                int y = rand.Next(0, map.GetLength(1));

                // Не ставим на героя
                if (!(x == map.GetLength(0) / 2 && y == map.GetLength(1) / 2))
                {
                    // Ставим только на Field
                    if (map[x, y] is Field)
                    {
                        map[x, y] = new Tree();
                    }
                }
            }

            // ========== ГЕНЕРАЦИЯ ГОР (100% появление) ==========
            int mountainCount = rand.Next(8, 15); // 8-15 гор
            for (int m = 0; m < mountainCount; m++)
            {
                int x = rand.Next(0, map.GetLength(0));
                int y = rand.Next(0, map.GetLength(1));

                if (!(x == map.GetLength(0) / 2 && y == map.GetLength(1) / 2))
                {
                    if (map[x, y] is Field)
                    {
                        map[x, y] = new Mountain();
                    }
                }
            }

            // ========== СТАВИМ ВРАГОВ ==========
            int enemyCount = 0;
            while (enemyCount < 8)
            {
                int x = rand.Next(0, map.GetLength(0));
                int y = rand.Next(0, map.GetLength(1));

                if ((map[x, y] is Field) &&
                    !(x == map.GetLength(0) / 2 && y == map.GetLength(1) / 2))
                {
                    map[x, y] = new Enemy(x, y);
                    enemyCount++;
                }
            }

            // ========== СТАВИМ ГЕРОЯ ==========
            map[map.GetLength(0) / 2, map.GetLength(1) / 2] = new Hero(map.GetLength(0) / 2, map.GetLength(1) / 2);
        }

        public void Drawing_the_map()
        {
            int enemyCount = 0;
            int treeCount = 0;
            int mountainCount = 0;

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    Console.Write(map[i, j].Rendering_on_the_map() + " ");
                    Console.ResetColor();

                    if (map[i, j] is Enemy) enemyCount++;
                    if (map[i, j] is Tree) treeCount++;
                    if (map[i, j] is Mountain) mountainCount++;
                }
                Console.WriteLine();
            }
            Console.WriteLine($"Врагов: {enemyCount} | Деревьев: {treeCount} | Гор: {mountainCount}");
        }

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
                            case 0: newX = (i - 1 + map.GetLength(0)) % map.GetLength(0); break;
                            case 1: newX = (i + 1) % map.GetLength(0); break;
                            case 2: newY = (j - 1 + map.GetLength(1)) % map.GetLength(1); break;
                            case 3: newY = (j + 1) % map.GetLength(1); break;
                        }

                        // Враги могут ходить только по полю
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

        public void MovePersons(ConsoleKey key)
        {
            MapObject[,] newMap = new MapObject[map.GetLength(0), map.GetLength(1)];
            Array.Copy(map, newMap, map.Length);

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] is Hero)
                    {
                        int newX = i, newY = j;
                        switch (key)
                        {
                            case ConsoleKey.UpArrow: newX = (i - 1 + map.GetLength(0)) % map.GetLength(0); break;
                            case ConsoleKey.DownArrow: newX = (i + 1) % map.GetLength(0); break;
                            case ConsoleKey.LeftArrow: newY = (j - 1 + map.GetLength(1)) % map.GetLength(1); break;
                            case ConsoleKey.RightArrow: newY = (j + 1) % map.GetLength(1); break;
                        }

                        // Герой может ходить только по полю
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
    }
}