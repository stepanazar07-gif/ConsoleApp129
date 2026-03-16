using System;

namespace ConsoleApp129
{
    /// <summary>
    /// Класс артефакта "Скипетр". Увеличивает урон против босса
    /// </summary>
    internal class Scepter : MapObject
    {
        /// <summary>
        /// Координата X скипетра на карте
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Координата Y скипетра на карте
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
        /// Конструктор скипетра
        /// </summary>
        /// <param name="x">Координата X</param>
        /// <param name="y">Координата Y</param>
        public Scepter(int x, int y)
        {
            X = x;
            Y = y;
            IsPicked = false;
            IsVisible = false;
        }

        /// <summary>
        /// Отрисовка скипетра на карте
        /// </summary>
        /// <returns>Символ 'I' если виден и не подобран, иначе пробел</returns>
        public override char Rendering_on_the_map()
        {
            if (!IsPicked && IsVisible)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                return 'I';
            }
            return ' ';
        }
    }
}