using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp129;

namespace ConsoleApp129.Core
{
    internal class NewLevel1 : Map
    {
        public string LevelName { get; private set; }           
        public string LevelDescription { get; private set; }    
        public ConsoleColor LevelColor { get; private set; }  
        public int LevelNumber { get; private set; }
        public NewLevel1() : base()
        {
            LevelNumber = 2;
            LevelName = "❄️ ЗИМНИЙ ЛЕС";
            LevelDescription = "Холодный зимний уровень. Всё покрыто снегом и льдом.";
            LevelColor = ConsoleColor.Cyan;
        }

        public new void Map_generation()
        {
            Random random = new Random();

           
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    map[i, j] = new Field();
                }
            }

           
            int mountainCount = random.Next(25, 35);
            for (int m = 0; m < mountainCount; m++)
            {
                int x = random.Next(0, map.GetLength(0));
                int y = random.Next(0, map.GetLength(1));

                
                if (!(x == 12 && y == 12))
                {
                    map[x, y] = new Mountain();  
                }
            }

           
            int treeCount = random.Next(10, 15);
            for (int d = 0; d < treeCount; d++)
            {
                int x = random.Next(0, map.GetLength(0));
                int y = random.Next(0, map.GetLength(1));

                
                if (!(x == 12 && y == 12) && map[x, y] is Field)
                {
                    map[x, y] = new Tree();  
                }
            }

           
            int enemyCount = 12;
            int enemiesPlaced = 0;
            while (enemiesPlaced < enemyCount)
            {
                int ex = random.Next(0, map.GetLength(0)); 
                int ey = random.Next(0, map.GetLength(1));  

              
                if (map[ex, ey] is Field && !(ex == 12 && ey == 12))
                {
                    var enemy = new Enemy(ex, ey);
                    enemy.Damage = 5;     
                  
                    map[ex, ey] = enemy;
                    enemiesPlaced++;
                }
            }

          
            heroX = 12;
            heroY = 12;
            map[12, 12] = new Hero(12, 12);

            
            PlaceDoor();
        }
    }
}