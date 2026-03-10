using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp129
{
    internal abstract class MapObject
    {
        public abstract char Rendering_on_the_map();
    }

    internal class Wall : MapObject
    {

        public override char Rendering_on_the_map()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            return '+';
        }
    }
    internal class Field : MapObject
    {
        public override char Rendering_on_the_map()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            return '.';
        }
    }
    internal class Tree : MapObject
    {
        public override char Rendering_on_the_map()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            return '♣';  
        }
    }

    internal class Mountain : MapObject
    {
        public override char Rendering_on_the_map()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            return '▲';  
        }
    }

    internal class RoomFloor : MapObject
    {
        public override char Rendering_on_the_map()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            return '·';
        }
    }

    internal class RoomWall : MapObject
    {
        public override char Rendering_on_the_map()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            return '█';
        }
       
        internal class IceMountain : MapObject
        {
            public override char Rendering_on_the_map()
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                return '▲';
            }
        }

        
        internal class SnowTree : MapObject
        {
            public override char Rendering_on_the_map()
            {
                Console.ForegroundColor = ConsoleColor.White; 
                return '❄';  
            }
        }

        
        internal class Snowdrift : MapObject
        {
            public override char Rendering_on_the_map()
            {
                Console.ForegroundColor = ConsoleColor.Cyan;  
                return '⛄';
            }
        }

        
        internal class WinterEnemy : Enemy  
        {
            public WinterEnemy(int x, int y) : base(x, y) { } 

            public override char Rendering_on_the_map()
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                return '♔';
            }
        }

       
        internal class WinterHero : Hero  
        {
            public WinterHero(int x, int y) : base(x, y)
            {
              
                Stats.MaxHP = 60;  
                Stats.HP = 60;     
                Stats.Armor = 15; 
            }

            public override char Rendering_on_the_map()
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                return '⛄'; 
            }
                
        }
    }
}


