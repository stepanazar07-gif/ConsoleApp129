using System;

namespace ConsoleApp129
{
    /// <summary>
    /// Класс консольного меню для навигации по игре
    /// Реализует выбор пунктов с помощью стрелок и Enter
    /// </summary>
    internal class Menu
    {
        /// <summary>
        /// Массив строк с названиями пунктов меню
        /// </summary>
        private string[] items;

        /// <summary>
        /// Индекс текущего выбранного пункта (0-based)
        /// </summary>
        private int selected = 0;

        /// <summary>
        /// Конструктор меню
        /// </summary>
        /// <param name="menuItems">Массив строк с названиями пунктов меню</param>
        public Menu(string[] menuItems)
        {
            items = menuItems;
        }

        /// <summary>
        /// Отображение меню в консоли и получение выбора пользователя
        /// Поддерживает навигацию стрелками ↑/↓ и подтверждение Enter
        /// Выбранный пункт подсвечивается жёлтым цветом
        /// </summary>
        /// <returns>Индекс выбранного пункта (0-based)</returns>
        public int Show()
        {
            ConsoleKey key;
            do
            {
                Console.Clear();
                Console.WriteLine("=== ГЛАВНОЕ МЕНЮ ===\n");
                for (int i = 0; i < items.Length; i++)
                {
                    if (i == selected)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("→ " + items[i]);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine("  " + items[i]);
                    }
                    Console.ResetColor();
                }
                Console.WriteLine("\n ↑/↓ - выбор, Enter - подтвердить");
                key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.UpArrow)
                {
                    selected = (selected - 1 + items.Length) % items.Length;
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    selected = (selected + 1) % items.Length;
                }
            } while (key != ConsoleKey.Enter);
            return selected;
        }
    }
}