using System;

namespace ConsoleApp129
{
    /// <summary>
    /// Абстрактный базовый класс для всех объектов на карте
    /// Все объекты карты наследуются от этого класса
    /// </summary>
    internal abstract class MapObject
    {
        /// <summary>
        /// Абстрактный метод отрисовки объекта на карте
        /// Возвращает символ и устанавливает цвет консоли
        /// </summary>
        /// <returns>Символ char для отображения объекта</returns>
        public abstract char Rendering_on_the_map();
    }

    /// <summary>
    /// Класс стены. Непроходимый объект карты
    /// </summary>
    internal class Wall : MapObject
    {
        /// <summary>
        /// Отрисовка стены: символ '+' голубого цвета
        /// </summary>
        /// <returns>Символ '+'</returns>
        public override char Rendering_on_the_map()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            return '+';
        }
    }

    /// <summary>
    /// Класс поля. Проходимая территория по умолчанию
    /// </summary>
    internal class Field : MapObject
    {
        /// <summary>
        /// Отрисовка поля: символ '.' тёмно-зелёного цвета
        /// </summary>
        /// <returns>Символ '.'</returns>
        public override char Rendering_on_the_map()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            return '.';
        }
    }

    /// <summary>
    /// Класс дерева. Декоративный непроходимый объект (Уровень 1)
    /// </summary>
    internal class Tree : MapObject
    {
        /// <summary>
        /// Отрисовка дерева: символ '♣' зелёного цвета
        /// </summary>
        /// <returns>Символ '♣'</returns>
        public override char Rendering_on_the_map()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            return '♣';
        }
    }

    /// <summary>
    /// Класс горы. Непроходимый объект карты (Уровень 1)
    /// </summary>
    internal class Mountain : MapObject
    {
        /// <summary>
        /// Отрисовка горы: символ '▲' тёмно-серого цвета
        /// </summary>
        /// <returns>Символ '▲'</returns>
        public override char Rendering_on_the_map()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            return '▲';
        }
    }

    /// <summary>
    /// Класс ледяной горы. Непроходимый объект (Зимний уровень 2)
    /// </summary>
    internal class IceMountain : MapObject
    {
        /// <summary>
        /// Отрисовка ледяной горы: символ '▲' тёмно-голубого цвета
        /// </summary>
        /// <returns>Символ '▲'</returns>
        public override char Rendering_on_the_map()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            return '▲';
        }
    }

    /// <summary>
    /// Класс снежного дерева. Декоративный объект (Зимний уровень 2)
    /// </summary>
    internal class SnowTree : MapObject
    {
        /// <summary>
        /// Отрисовка снежного дерева: символ '❄' белого цвета
        /// </summary>
        /// <returns>Символ '❄'</returns>
        public override char Rendering_on_the_map()
        {
            Console.ForegroundColor = ConsoleColor.White;
            return '❄';
        }
    }

    /// <summary>
    /// Класс кактуса. Непроходимый объект (Пустынный уровень 3)
    /// </summary>
    internal class Cactus : MapObject
    {
        /// <summary>
        /// Отрисовка кактуса: символ '♣' зелёного цвета
        /// </summary>
        /// <returns>Символ '♣'</returns>
        public override char Rendering_on_the_map()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            return '♣';
        }
    }

    /// <summary>
    /// Класс дюны. Декоративный объект (Пустынный уровень 3)
    /// </summary>
    internal class SandDune : MapObject
    {
        /// <summary>
        /// Отрисовка дюны: символ '~' жёлтого цвета
        /// </summary>
        /// <returns>Символ '~'</returns>
        public override char Rendering_on_the_map()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            return '~';
        }
    }

    /// <summary>
    /// Класс скалы. Непроходимый объект (Пустынный уровень 3)
    /// </summary>
    internal class Rock : MapObject
    {
        /// <summary>
        /// Отрисовка скалы: символ '▲' тёмно-жёлтого цвета
        /// </summary>
        /// <returns>Символ '▲'</returns>
        public override char Rendering_on_the_map()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            return '▲';
        }
    }

    /// <summary>
    /// Класс зелья лечения. Восстанавливает здоровье героя при подборе
    /// </summary>
    internal class Heal : MapObject
    {
        /// <summary>
        /// Отрисовка зелья: символ '♥' красного цвета
        /// </summary>
        /// <returns>Символ '♥'</returns>
        public override char Rendering_on_the_map()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            return '♥';
        }
    }
}