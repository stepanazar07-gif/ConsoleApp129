using System;

namespace ConsoleApp129
{
    /// <summary>
    /// Класс артефакта "Корона". Увеличивает урон против босса
    /// </summary>
    internal class Crown : MapObject
    {
        /// <summary>
        /// Координата X короны на карте
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Координата Y короны на карте
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
        /// Конструктор короны
        /// </summary>
        /// <param name="x">Координата X</param>
        /// <param name="y">Координата Y</param>
        public Crown(int x, int y)
        {
            X = x;
            Y = y;
            IsPicked = false;
            IsVisible = false;
        }

        /// <summary>
        /// Отрисовка короны на карте
        /// </summary>
        /// <returns>Символ '★' если видна и не подобрана, иначе пробел</returns>
        public override char Rendering_on_the_map()
        {
            if (!IsPicked && IsVisible)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                return '★';
            }
            return ' ';
        }
    }
}