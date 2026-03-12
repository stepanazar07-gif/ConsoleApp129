using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp129.Core
{
    internal class NewLevel2 : Map
    {
        public NewLevel2() : base()
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

            // Генерация кактусов (вместо деревьев)
            int cactusCount = rand.Next(12, 18);
            for (int c = 0; c < cactusCount; c++)
            {
                int x = rand.Next(0, 25);
                int y = rand.Next(0, 25);

                if ((x != 12 || y != 12) && map[x, y] is Field)
                {
                    map[x, y] = new Cactus(); // Новый класс для кактусов
                }
            }

            // Генерация скал (вместо гор)
            int rockCount = rand.Next(20, 30);
            for (int r = 0; r < rockCount; r++)
            {
                int x = rand.Next(0, 25);
                int y = rand.Next(0, 25);

                if (x != 12 || y != 12)
                {
                    map[x, y] = new Rock(); // Новый класс для скал
                }
            }

            // Генерация песчаных дюн (новый тип препятствия)
            int duneCount = rand.Next(8, 12);
            for (int d = 0; d < duneCount; d++)
            {
                int x = rand.Next(0, 25);
                int y = rand.Next(0, 25);

                if ((x != 12 || y != 12) && map[x, y] is Field)
                {
                    map[x, y] = new SandDune(); // Новый класс для песчаных дюн
                }
            }

            // Генерация врагов (пустынные враги)
            int enemyCount = 15; // Чуть больше врагов для сложности
            int enemiesPlaced = 0;
            while (enemiesPlaced < enemyCount)
            {
                int x = rand.Next(0, 25);
                int y = rand.Next(0, 25);

                if (map[x, y] is Field && (x != 12 || y != 12))
                {
                    DesertEnemy enemy = new DesertEnemy(x, y);
                    enemy.Damage = 8; // Увеличенный урон для пустынных врагов
                    enemy.Health = 15; // Больше здоровья
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