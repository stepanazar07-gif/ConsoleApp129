using System;
namespace ConsoleApp129
{
    internal class Scepter : MapObject
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsPicked { get; set; }
        public bool IsVisible { get; set; }

        public Scepter(int x, int y)
        {
            X = x;
            Y = y;
            IsPicked = false;
            IsVisible = false;
        }

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