using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp129
{
    internal class LevelManager
    {
        public int CurrentLevel { get; private set; }
        public bool DoorOpened { get; private set; }
        public bool JustTeleported { get; set; }  

        private int doorX;
        private int doorY;

        public LevelManager()
        {
            CurrentLevel = 1;
            DoorOpened = false;
            JustTeleported = false;
        }

        public void CheckAllEnemiesDefeated(Map map)
        {
            int enemyCount = 0;
            for (int i = 0; i < 25; i++)
            {
                for (int j = 0; j < 25; j++)
                {
                    if (map.GetObjectAt(i, j) is Enemy)
                        enemyCount++;
                }
            }

            if (enemyCount == 0 && !DoorOpened)
            {
                OpenDoor(map);
            }
        }

        private void OpenDoor(Map map)
        {
            DoorOpened = true;

            for (int i = 0; i < 25; i++)
            {
                for (int j = 0; j < 25; j++)
                {
                    if (map.GetObjectAt(i, j) is Door door)
                    {
                        door.IsOpen = true;
                        doorX = i;
                        doorY = j;
                        Console.SetCursorPosition(0, 29);
                        Console.WriteLine("🚪 ДВЕРЬ ОТКРЫТА! Иди к выходу!          ");
                        return;
                    }
                }
            }

            for (int i = 0; i < 25; i++)
            {
                for (int j = 0; j < 25; j++)
                {
                    if (map.GetObjectAt(i, j) is Field)
                    {
                        var newDoor = new Door();
                        newDoor.IsOpen = true;
                        map.PlaceObject(i, j, newDoor);
                        doorX = i;
                        doorY = j;
                        Console.SetCursorPosition(0, 29);
                        Console.WriteLine("🚪 ДВЕРЬ ПОЯВИЛАСЬ! Иди к выходу!          ");
                        return;
                    }
                }
            }
        }

        public bool CheckDoorEntered(int heroX, int heroY)
        {
            return (heroX == doorX && heroY == doorY && DoorOpened);
        }

        public void NextLevel(Map map, Hero hero)
        {
            CurrentLevel++;
            DoorOpened = false;
            JustTeleported = true;

            hero.Stats.HP = hero.Stats.MaxHP;
            map.GenerateNewLevel(CurrentLevel);

            Console.SetCursorPosition(0, 29);
            Console.WriteLine($"✨ УРОВЕНЬ {CurrentLevel}! Враги сильнее!          ");
        }
    }
}