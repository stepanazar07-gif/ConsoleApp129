using System;

namespace ConsoleApp129
{
    /// <summary>
    /// Класс артефакта "Амулет". Увеличивает урон против босса
    /// </summary>
    internal class Amulet : MapObject
    {
        /// <summary>
        /// Координата X амулета на карте
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Координата Y амулета на карте
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Флаг подбора артефакта
        /// </summary>
        public bool IsPicked { get; set; }

        /// <summary>
        /// Флаг видимости артефакта на карте
        /// </summary>
        public bool IsVisible { get; set; }

        /// <summary>
        /// Конструктор амулета
        /// </summary>
        /// <param name="x">Координата X</param>
        /// <param name="y">Координата Y</param>
        public Amulet(int x, int y)
        {
            X = x;
            Y = y;
            IsPicked = false;
            IsVisible = false;
        }

        /// <summary>
        /// Отрисовка амулета на карте
        /// </summary>
        /// <returns>Символ '*' если виден и не подобран, иначе пробел</returns>
        public override char Rendering_on_the_map()
        {
            if (!IsPicked && IsVisible)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                return '*';
            }
            return ' ';
        }
    }
}