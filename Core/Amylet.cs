using System;
using ConsoleApp129;

namespace ConsoleApp129.Core
{
    class Amulet : MapObject
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsPicked { get; set; }
        public bool IsVisible { get; set; }
        public Amulet(int x, int y)
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
                Console.ForegroundColor = ConsoleColor.Yellow;
                return '*';
            }
            return ' ';
        }
    }
}