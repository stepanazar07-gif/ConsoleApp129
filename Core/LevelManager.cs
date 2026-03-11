using System;

namespace ConsoleApp129
{
    internal class LevelManager
    {
        public int CurrentLevel { get; set; }
        public bool DoorOpen { get; private set; }
        private int doorX;
        private int doorY;

        public LevelManager()
        {
            CurrentLevel = 1;
            DoorOpen = false;
        }

        public void CheckEnemies(Map map)
        {
            int enemyCount = 0;
            for (int i = 0; i < 25; i++)
            {
                for (int j = 0; j < 25; j++)
                {
                    if (map.GetObjectAt(i, j) is Enemy)
                    {
                        enemyCount++;
                    }
                }
            }

            if (enemyCount == 0 && !DoorOpen)
            {
                FindAndOpenDoor(map);
            }
        }

        private void FindAndOpenDoor(Map map)
        {
            for (int i = 0; i < 25; i++)
            {
                for (int j = 0; j < 25; j++)
                {
                    if (map.GetObjectAt(i, j) is Door door)
                    {
                        door.IsOpen = true;
                        doorX = i;
                        doorY = j;
                        DoorOpen = true;
                        Console.SetCursorPosition(0, 28);
                        Console.WriteLine("🚪 ДВЕРЬ ОТКРЫТА!          ");
                        return;
                    }
                }
            }
        }

        public bool HeroOnDoor(int heroX, int heroY)
        {
            if (heroX == doorX && heroY == doorY && DoorOpen)
            {
                return true;
            }
            return false;
        }

        public void NextLevel(Map map, Hero hero)
        {
            CurrentLevel++;
            DoorOpen = false;
            hero.Stats.HP = hero.Stats.MaxHP;
            map.GenerateNewLevel(CurrentLevel);
            Console.SetCursorPosition(0, 28);
            Console.WriteLine("✨ УРОВЕНЬ " + CurrentLevel + "!          ");
        }
    }
}