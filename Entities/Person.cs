using System;

namespace ConsoleApp129
{
    internal abstract class Person : MapObject
    {
        protected int pointX;
        protected int pointY;

        public Person(int X, int Y)
        {
            pointX = X;
            pointY = Y;
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
        public int Damage { get; set; }

        public Enemy(int X, int Y) : base(X, Y)
        {
            Damage = 3;
        }

        public override char Rendering_on_the_map()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            return '☺';
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

    internal class WinterEnemy : Enemy
    {
        public WinterEnemy(int x, int y) : base(x, y)
        {
            Damage = 5;
        }

        public override char Rendering_on_the_map()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            return '☻';
        }
    }
}