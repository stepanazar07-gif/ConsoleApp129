using System;

namespace ConsoleApp129
{
    internal abstract class Person : MapObject
    {
        protected int pointX;
        protected int pointY;
        public bool HasAmulet { get; set; }

        public Person(int X, int Y)
        {
            pointX = X;
            pointY = Y;
            HasAmulet = false; 
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
        public int FreezeTurns { get; set; }
        public int SandTurns { get; set; }
        public int PoisonTurns { get; set; }
        public int PoisonCounter { get; set; }





        public Hero(int X, int Y) : base(X, Y)
        {
            this.X = X;
            this.Y = Y;
            Stats = new GameStats();
            FreezeTurns = 0;
            SandTurns = 0;
            PoisonTurns = 0;
            PoisonCounter = 0;

        }

        public override char Rendering_on_the_map()
        { 
            if (PoisonTurns > 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                return '☣';
            }


            if (SandTurns > 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                return '≅';
            }

            if (FreezeTurns > 0)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                return '■';

            }
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
        public int FreezeChance { get; set; }
        public WinterEnemy(int x, int y) : base(x, y)
        {
            Damage = 5;
            FreezeChance = 50;


        }

        public override char Rendering_on_the_map()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            return '☻';
        }
    }
    internal class DesertEnemy : Enemy
    {
        public int SandChance { get; set; }
        public int PoisonChance { get; set; }

        public DesertEnemy(int x, int y) : base(x, y)
        {
            Damage = 8;
            SandChance = 50;
            PoisonChance = 70;
        }

        public override char Rendering_on_the_map()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            return 'S';
        }
    }
    internal class Boss : Enemy
    {
        public Boss(int x, int y) : base(x, y)
        {
            Damage = 100;
        }

        public override char Rendering_on_the_map()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            return '☠'; 
        }
    }
}             