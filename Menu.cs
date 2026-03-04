using System;

namespace ConsoleApp129
{
    internal class Menu
    {
        private string[] items;
        private int selected = 0;

        public Menu(string[] menuItems)
        {
            items = menuItems;
        }

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

                Console.WriteLine("\n↑/↓ - выбор, Enter - подтвердить");

                key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.UpArrow)
                    selected = (selected - 1 + items.Length) % items.Length;
                else if (key == ConsoleKey.DownArrow)
                    selected = (selected + 1) % items.Length;

            } while (key != ConsoleKey.Enter);

            return selected;
        }
    }
}