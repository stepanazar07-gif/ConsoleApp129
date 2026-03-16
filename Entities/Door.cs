using System;

namespace ConsoleApp129
{
    /// <summary>
    /// Класс объекта "Дверь". Выход с текущего уровня
    /// </summary>
    internal class Door : MapObject
    {
        /// <summary>
        /// Флаг состояния двери (открыта/закрыта)
        /// </summary>
        public bool IsOpen { get; set; }

        /// <summary>
        /// Координата X двери
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Координата Y двери
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Конструктор двери
        /// </summary>
        public Door()
        {
            IsOpen = false;
        }

        /// <summary>
        /// Отрисовка двери на карте
        /// </summary>
        /// <returns>'⬚' если открыта (зелёный), '█' если закрыта (красный)</returns>
        public override char Rendering_on_the_map()
        {
            if (IsOpen)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                return '⬚';
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                return '█';
            }
        }
    }
}