using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApp129
{
    internal class Door : MapObject
    {
        public bool IsOpen { get; set; }
        public int X { get; set; }  
        public int Y { get; set; }
        public Door()
        {
            IsOpen = false;
        }

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
