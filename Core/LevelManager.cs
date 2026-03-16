using System;

namespace ConsoleApp129
{
    /// <summary>
    /// Менеджер уровней. Управляет прогрессом игры и открытием дверей
    /// </summary>
    internal class LevelManager
    {
        /// <summary>
        /// Номер текущего уровня (1-4)
        /// </summary>
        public int CurrentLevel { get; set; }

        /// <summary>
        /// Флаг состояния двери (открыта/закрыта)
        /// </summary>
        public bool DoorOpen { get; private set; }

        /// <summary>
        /// Координата X двери на карте
        /// </summary>
        private int doorX;

        /// <summary>
        /// Координата Y двери на карте
        /// </summary>
        private int doorY;

        /// <summary>
        /// Конструктор менеджера уровней
        /// Инициализирует первый уровень и закрывает дверь
        /// </summary>
        public LevelManager()
        {
            CurrentLevel = 1;
            DoorOpen = false;
        }

        /// <summary>
        /// Проверка наличия врагов на карте и открытие двери при их уничтожении
        /// Перебирает все клетки карты 25x25 и подсчитывает живых врагов
        /// Если врагов нет и дверь ещё не открыта — вызывает FindAndOpenDoor
        /// </summary>
        /// <param name="map">Объект карты для проверки наличия врагов</param>
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

        /// <summary>
        /// Поиск и открытие двери на карте
        /// Перебирает все клетки, находит объект Door и открывает его
        /// Сохраняет координаты двери для проверки HeroOnDoor
        /// </summary>
        /// <param name="map">Объект карты для поиска двери</param>
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
                        Console.WriteLine(" 🚪   ДВЕРЬ   ОТКРЫТА !          ");
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Проверка нахождения героя на открытой двери
        /// Сравнивает координаты героя с координатами двери и статусом DoorOpen
        /// </summary>
        /// <param name="heroX">Координата X героя на карте</param>
        /// <param name="heroY">Координата Y героя на карте</param>
        /// <returns>True если герой стоит на открытой двери, иначе False</returns>
        public bool HeroOnDoor(int heroX, int heroY)
        {
            if (heroX == doorX && heroY == doorY && DoorOpen)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Переход на следующий уровень игры
        /// Увеличивает номер уровня, сбрасывает флаг двери, восстанавливает HP героя
        /// и генерирует новую карту через метод GenerateNewLevel
        /// </summary>
        /// <param name="map">Объект текущей карты для перегенерации</param>
        /// <param name="hero">Объект героя для восстановления здоровья</param>
        public void NextLevel(Map map, Hero hero)
        {
            CurrentLevel++;
            DoorOpen = false;
            hero.Stats.HP = hero.Stats.MaxHP;
            map.GenerateNewLevel(CurrentLevel);
            Console.SetCursorPosition(0, 28);
            Console.WriteLine(" ✨   УРОВЕНЬ  " + CurrentLevel + "!          ");
        }

        /// <summary>
        /// Сброс состояния уровня (для подготовки к генерации новой карты)
        /// Закрывает дверь и сбрасывает её координаты в -1
        /// </summary>
        public void ResetLevel()
        {
            DoorOpen = false;
            doorX = -1;
            doorY = -1;
        }
    }
}