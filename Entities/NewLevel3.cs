using System;
using ConsoleApp129;
using ConsoleApp129.Core;

namespace ConsoleApp129.Core
{
    /// <summary>
    /// Генератор карты для уровня 4 (Финальный босс)
    /// </summary>
    internal class NewLevel3 : Map
    {
        /// <summary>
        /// Конструктор уровня 4
        /// </summary>
        public NewLevel3() : base()
        {
        }

        /// <summary>
        /// Генерация карты финального уровня с боссом
        /// </summary>
        /// <param name="existingHero">Существующий объект героя для переноса</param>
        /// <returns>Двумерный массив объектов карты MapObject[,]</returns>
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
            for (int i = 0; i < 25; i++)
            {
                map[i, 0] = new Wall();
                map[i, 24] = new Wall();
                map[0, i] = new Wall();
                map[24, i] = new Wall();
            }
            int internalWalls = rand.Next(10, 18);
            for (int w = 0; w < internalWalls; w++)
            {
                int x = rand.Next(2, 23);
                int y = rand.Next(2, 23);
                if ((x != 12 || y != 12) && map[x, y] is Field)
                {
                    map[x, y] = new Wall();
                }
            }
            int bossX, bossY;
            do
            {
                bossX = rand.Next(2, 23);
                bossY = rand.Next(2, 23);
            } while (map[bossX, bossY] is Field == false || (bossX == 12 && bossY == 12));
            map[bossX, bossY] = new Boss(bossX, bossY);
            int healsCount = rand.Next(6, 10);
            for (int i = 0; i < healsCount; i++)
            {
                int x = rand.Next(2, 23);
                int y = rand.Next(2, 23);
                if ((x != 12 || y != 12) && map[x, y] is Field)
                {
                    map[x, y] = new Heal();
                }
            }
            heroX = 12;
            heroY = 12;
            map[12, 12] = existingHero;
            return map;
        }
    }
}