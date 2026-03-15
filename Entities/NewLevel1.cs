using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp129.Core
{
    internal class NewLevel1 : Map
    {
        public NewLevel1() : base()
        {
        }

        public MapObject[,] Map_generation(Hero existingHero)
        {
            Random rand = new Random();


            for (int i = 0; i < 25; i++)
            {
                for (int j = 0; j < 25; j++)
                {
                    map[i, j] = new Field();
                }
            }


            int mountainCount = rand.Next(25, 35);
            for (int m = 0; m < mountainCount; m++)
            {
                int x = rand.Next(0, 25);
                int y = rand.Next(0, 25);

                if (x != 12 || y != 12)
                {
                    map[x, y] = new IceMountain();
                }
            }


            int treeCount = rand.Next(10, 15);
            for (int t = 0; t < treeCount; t++)
            {
                int x = rand.Next(0, 25);
                int y = rand.Next(0, 25);

                if ((x != 12 || y != 12) && map[x, y] is Field)
                {
                    map[x, y] = new SnowTree();
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


            int enemyCount = 1;
            int enemiesPlaced = 0;
            while (enemiesPlaced < enemyCount)
            {
                int x = rand.Next(0, 25);
                int y = rand.Next(0, 25);

                if (map[x, y] is Field && (x != 12 || y != 12))
                {
                    WinterEnemy enemy = new WinterEnemy(x, y);
                    enemy.Damage = 5;
                    map[x, y] = enemy;
                    enemiesPlaced++;
                }
            }


            heroX = 12;
            heroY = 12;
            map[12, 12] = existingHero;


            var doorPos = PlaceDoor();
            return map;
        }
    }
}