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
            private int heroX;
            private int heroY;

      
            private char[,] backBuffer = new char[25, 25];
            private ConsoleColor[,] colorBuffer = new ConsoleColor[25, 25];

            public void Map_generation()
            {
         
                for (int i = 0; i < map.GetLength(0); i++)
                {
                    for (int j = 0; j < map.GetLength(1); j++)
                    {
                        map[i, j] = new Field();
                    }
                }

       
                int treeCount = rand.Next(30, 50); 
                for (int t = 0; t < treeCount; t++)
                {
                    int x = rand.Next(0, map.GetLength(0));
                    int y = rand.Next(0, map.GetLength(1));

                    if (!(x == map.GetLength(0) / 2 && y == map.GetLength(1) / 2))
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

          
                heroX = map.GetLength(0) / 2;
                heroY = map.GetLength(1) / 2;
                map[heroX, heroY] = new Hero(heroX, heroY);

          
                for (int i = 0; i < map.GetLength(0); i++)
                {
                    for (int j = 0; j < map.GetLength(1); j++)
                    {
                        backBuffer[i, j] = ' ';
                        colorBuffer[i, j] = ConsoleColor.White;
                    }
                }
            }

            public void Drawing_the_map()
            {
           
                for (int i = 0; i < map.GetLength(0); i++)
                {
                    for (int j = 0; j < map.GetLength(1); j++)
                    {
                   
                        char currentChar = map[i, j].Rendering_on_the_map();

                  
                        backBuffer[i, j] = currentChar;
                    }
                }

          
                Console.SetCursorPosition(0, 0);

                for (int i = 0; i < map.GetLength(0); i++)
                {
                    for (int j = 0; j < map.GetLength(1); j++)
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

                for (int i = 0; i < map.GetLength(0); i++)
                {
                    for (int j = 0; j < map.GetLength(1); j++)
                    {
                        if (map[i, j] is Enemy) enemyCount++;
                        if (map[i, j] is Tree) treeCount++;
                        if (map[i, j] is Mountain) mountainCount++;
                    }
                }

                Console.WriteLine($"Врагов: {enemyCount} | Деревьев: {treeCount} | Гор: {mountainCount}");
            }

        public void CheckCombat(Hero hero)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] is Enemy enemy)
                    {
                        if (Math.Abs(i - heroX) + Math.Abs(j - heroY) == 1)
                        {
                            hero.Stats.TakeDamage(enemy.Damage);

                            Console.SetCursorPosition(0, map.GetLength(0) + 2);
                            Console.Write($"⚔ Бой! Получено {enemy.Damage} урона      ");

                           
                            map[i, j] = new Field();

                            
                            hero.Stats.AddXP(enemy.XPReward);
                        }
                    }
                }
            }
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

                            if (newMap[newX, newY] is Field)
                            {
                                newMap[newX, newY] = map[i, j];
                                newMap[i, j] = new Field();

                                heroX = newX;
                                heroY = newY;
                            }
                        }
                    }
                }

                Array.Copy(newMap, map, map.Length);
            }

  
            public MapObject GetObjectAt(int x, int y)
            {
                if (x >= 0 && x < map.GetLength(0) && y >= 0 && y < map.GetLength(1))
                    return map[x, y];
                return null;
            }

            public void PlaceObject(int x, int y, MapObject obj)
            {
                if (x >= 0 && x < map.GetLength(0) && y >= 0 && y < map.GetLength(1))
                    map[x, y] = obj;
            }

            public void GenerateNewLevel(int level)
            {
           
                for (int i = 0; i < map.GetLength(0); i++)
                {
                    for (int j = 0; j < map.GetLength(1); j++)
                    {
                        map[i, j] = new Field();
                    }
                }

       
                int enemyCount = 8 + level * 2;
                int treeCount = rand.Next(30, 50);
                int mountainCount = rand.Next(15, 25);

         
                for (int t = 0; t < treeCount; t++)
                {
                    int x = rand.Next(0, map.GetLength(0));
                    int y = rand.Next(0, map.GetLength(1));
                    if (!(x == map.GetLength(0) / 2 && y == map.GetLength(1) / 2) && map[x, y] is Field)
                        map[x, y] = new Tree();
                }

         
                for (int m = 0; m < mountainCount; m++)
                {
                    int x = rand.Next(0, map.GetLength(0));
                    int y = rand.Next(0, map.GetLength(1));
                    if (!(x == map.GetLength(0) / 2 && y == map.GetLength(1) / 2) && map[x, y] is Field)
                        map[x, y] = new Mountain();
                }

         
                int enemyDamage = 3 + level;
                int enemiesPlaced = 0;
                while (enemiesPlaced < enemyCount)
                {
                    int x = rand.Next(0, map.GetLength(0));
                    int y = rand.Next(0, map.GetLength(1));

                    if (map[x, y] is Field && !(x == map.GetLength(0) / 2 && y == map.GetLength(1) / 2))
                    {
                        var enemy = new Enemy(x, y);
                        enemy.Damage = enemyDamage;
                        enemy.XPReward = 5 + level;
                        map[x, y] = enemy;
                        enemiesPlaced++;
                    }
                }

           
                heroX = map.GetLength(0) / 2;
                heroY = map.GetLength(1) / 2;
                map[heroX, heroY] = new Hero(heroX, heroY);

            
                PlaceDoor();
    
                for (int i = 0; i < map.GetLength(0); i++)
                {
                    for (int j = 0; j < map.GetLength(1); j++)
                    {
                        backBuffer[i, j] = ' ';
                    }
                }
            }

            private void PlaceDoor()
            {
            
                int doorX = map.GetLength(0) - 2;
                int doorY = map.GetLength(1) - 2;

        
                while (doorX > 0 && doorY > 0 && !(map[doorX, doorY] is Field))
                {
                    doorX--;
                    if (doorX < 0)
                    {
                        doorX = map.GetLength(0) - 2;
                        doorY--;
                    }
                }

                if (doorX >= 0 && doorY >= 0)
                    map[doorX, doorY] = new Door();
            }
        }
    }