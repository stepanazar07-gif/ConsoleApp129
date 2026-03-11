using System;

namespace ConsoleApp129
{
    internal class Map
    {
        Random rand = new Random();
        protected MapObject[,] map = new MapObject[25, 25];
        protected int heroX;
        protected int heroY;
        private char[,] backBuffer = new char[25, 25];

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

                for (int i = 0; i < 25; i++)
                {
                    for (int j = 0; j < 25; j++)
                    {
                        if (map[i, j] is Enemy)
                        {
                            enemyCount++;
                        }
                        if (map[i, j] is Tree)
                        {
                            treeCount++;
                        }
                        if (map[i, j] is Mountain)
                        {
                            mountainCount++;
                        }
                    }
                }
             
             Console.WriteLine($"Врагов: {enemyCount} | Деревьев: {treeCount} | Гор: {mountainCount}");
             
           
         
        }

        public void CheckCombat(Hero hero)
        {
            for (int i = 0; i < 25; i++)
            {
                for (int j = 0; j < 25; j++)
                {
                    if (map[i, j] is Enemy enemy)
                    {
                        if (Math.Abs(i - heroX) + Math.Abs(j - heroY) == 1)
                        {
                            hero.Stats.TakeDamage(enemy.Damage);
                            map[i, j] = new Field();

                            Console.SetCursorPosition(0, 27);
                            Console.Write("⚔ Бой! Получено " + enemy.Damage + " урона        ");
                        }
                    }
                }
            }
        }

        public void MovePersons()
        {
            MapObject[,] newMap = new MapObject[25, 25];
            Array.Copy(map, newMap, map.Length);

            for (int i = 0; i < 25; i++)
            {
                for (int j = 0; j < 25; j++)
                {
                    if (map[i, j] is Enemy)
                    {
                        int direction = rand.Next(4);
                        int newX = i;
                        int newY = j;

                        if (direction == 0)
                        {
                            newX = (i - 1 + 25) % 25;
                        }
                        else if (direction == 1)
                        {
                            newX = (i + 1) % 25;
                        }
                        else if (direction == 2)
                        {
                            newY = (j - 1 + 25) % 25;
                        }
                        else if (direction == 3)
                        {
                            newY = (j + 1) % 25;
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
            MapObject[,] newMap = new MapObject[25, 25];
            Array.Copy(map, newMap, map.Length);

            for (int i = 0; i < 25; i++)
            {
                for (int j = 0; j < 25; j++)
                {
                    if (map[i, j] is Hero)
                    {
                        int newX = i;
                        int newY = j;

                        if (key == ConsoleKey.UpArrow)
                        {
                            newX = (i - 1 + 25) % 25;
                        }
                        else if (key == ConsoleKey.DownArrow)
                        {
                            newX = (i + 1) % 25;
                        }
                        else if (key == ConsoleKey.LeftArrow)
                        {
                            newY = (j - 1 + 25) % 25;
                        }
                        else if (key == ConsoleKey.RightArrow)
                        {
                            newY = (j + 1) % 25;
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
            if (x >= 0 && x < 25 && y >= 0 && y < 25)
            {
                return map[x, y];
            }
            return null;
        }

        public void PlaceObject(int x, int y, MapObject obj)
        {
            if (x >= 0 && x < 25 && y >= 0 && y < 25)
            {
                map[x, y] = obj;
            }
        }

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

        protected void PlaceDoor()
        {
            int doorX = 22;
            int doorY = 22;

            while (doorX > 0 && doorY > 0 && !(map[doorX, doorY] is Field))
            {
                doorX--;
                if (doorX < 0)
                {
                    doorX = 22;
                    doorY--;
                }
            }

            if (doorX >= 0 && doorY >= 0)
            {
                Door door = new Door();
                door.X = doorX;
                door.Y = doorY;
                map[doorX, doorY] = door;
            }
        }
    }
}