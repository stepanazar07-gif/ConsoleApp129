using System.Collections.Generic;

namespace ConsoleApp129.Save
{
    /// <summary>
    /// Класс для хранения данных сохранения игры
    /// </summary>
    public class GameData
    {
        /// <summary>
        /// Номер текущего уровня карты
        /// </summary>
        public int MapLevel { get; set; } = 1;

        /// <summary>
        /// Координата X героя на карте
        /// </summary>
        public int HeroX { get; set; }

        /// <summary>
        /// Координата Y героя на карте
        /// </summary>
        public int HeroY { get; set; }

        /// <summary>
        /// Текущее здоровье героя
        /// </summary>
        public int HeroHP { get; set; }

        /// <summary>
        /// Максимальное здоровье героя
        /// </summary>
        public int HeroMaxHP { get; set; }

        /// <summary>
        /// Уровень брони героя
        /// </summary>
        public int HeroArmor { get; set; }

        /// <summary>
        /// Уровень героя
        /// </summary>
        public int HeroLevel { get; set; }

        /// <summary>
        /// Флаг наличия амулета у героя
        /// </summary>
        public bool HasAmulet { get; set; }

        /// <summary>
        /// Флаг наличия короны у героя
        /// </summary>
        public bool HasCrown { get; set; }

        /// <summary>
        /// Флаг наличия скипетра у героя
        /// </summary>
        public bool HasScepter { get; set; }

        /// <summary>
        /// Количество ходов заморозки
        /// </summary>
        public int FreezeTurns { get; set; }

        /// <summary>
        /// Количество ходов ослепления песком
        /// </summary>
        public int SandTurns { get; set; }

        /// <summary>
        /// Количество ходов отравления
        /// </summary>
        public int PoisonTurns { get; set; }

        /// <summary>
        /// Список объектов на карте для сохранения
        /// </summary>
        public List<MapItem> Items { get; set; } = new List<MapItem>();
    }

    /// <summary>
    /// Модель объекта карты для сохранения
    /// </summary>
    public class MapItem
    {
        /// <summary>
        /// Тип объекта (строковое представление)
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Координата X объекта
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Координата Y объекта
        /// </summary>
        public int Y { get; set; }
    }
}