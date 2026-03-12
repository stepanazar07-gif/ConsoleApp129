using System;

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
    internal class Cactus : MapObject
    {
        public override char Rendering_on_the_map()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            return '♣';
        }
        
    }
    internal class SandDune : MapObject
    {
        public override char Rendering_on_the_map()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            return '~';
        }
    }
    internal class Rock : MapObject
    {
        public override char Rendering_on_the_map()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            return '▲';
        }
    }
}

