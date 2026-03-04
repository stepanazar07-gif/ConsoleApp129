using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp129
{
    internal class Person : MapObject
    {
        int pointX;
        int pointY;

        public Person(int X, int Y)
        {
            pointX = X; pointY = Y;
        }
        public override char Rendering_on_the_map()
        {
            return '☺';
        }
    }

    internal class Hero : Person
    {
        public GameStats Stats { get; private set; }
        public int X { get; set; } 
        public int Y { get; set; }      

        public Hero(int X, int Y) : base(X, Y)
        {
            this.X = X;
            this.Y = Y;
            Stats = new GameStats();
        }

        public override char Rendering_on_the_map()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            return '☻';
        }
    }

    internal class Enemy : Person
    {
        public int Damage { get;  set; }
        public int XPReward { get;  set; }

        public Enemy(int X, int Y) : base(X, Y)
        {
            Damage = 3;
            XPReward = 5;
        }

        public override char Rendering_on_the_map()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            return '☺';
        }
    }
}