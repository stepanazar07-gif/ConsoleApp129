using System;
using System.Threading;

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

    // Простая версия босса: весь бой — в одном методе SimpleFight.
    internal class Boss : Enemy
    {
        private int HP;
        public Boss(int x, int y) : base(x, y)
        {
            HP = 60;      // здоровье босса (правьте при желании)
            Damage = 12;  // урон босса
        }

        public override char Rendering_on_the_map()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            return '☠';
        }
        public bool SimpleFight(Hero hero, Map map)
        {
            Console.SetCursorPosition(0, 27);
            Console.Write("⚔ НАЧАЛСЯ БОЙ С БОССОМ!                                   ");
            Thread.Sleep(400);

            var rnd = new Random();

            while (HP > 0 && hero.Stats.HP > 0)
            {
                int heroDmg = 6 + rnd.Next(0, 5);
                HP -= heroDmg;
                Console.SetCursorPosition(0, 27);
                Console.Write($"⚔ Герой наносит {heroDmg} урона (Босс {Math.Max(0, HP)})    ");
                Thread.Sleep(350);

                if (HP <= 0) break;

                int bossDmg = this.Damage + rnd.Next(0, 4);
                hero.Stats.TakeDamage(bossDmg);
                Console.SetCursorPosition(0, 27);
                Console.Write($"💥 Босс наносит {bossDmg} урона (Герой {Math.Max(0, hero.Stats.HP)})   ");
                Thread.Sleep(450);
            }

            if (HP <= 0)
            {
                for (int i = 0; i < 25; i++)
                {
                    for (int j = 0; j < 25; j++)
                    {
                        if (map.GetObjectAt(i, j) == this)
                        {
                            Amulet amulet = new Amulet(i, j) { IsVisible = true };
                            map.PlaceObject(i, j, amulet);
                            break;
                        }
                    }
                }

                Console.SetCursorPosition(0, 27);
                Console.Write("🏆 Босс повержен! Появился амулет.                         ");
                Thread.Sleep(700);
                return true;
            }
            else
            {
                Console.SetCursorPosition(0, 27);
                Console.Write("💀 Герой погиб в бою с боссом...                          ");
                Thread.Sleep(800);
                return false;
            }
        }
    }

    internal class Amulet : MapObject
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsVisible { get; set; }

        public Amulet(int x, int y)
        {
            X = x;
            Y = y;
            IsVisible = false;
        }

        public override char Rendering_on_the_map()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            return '*';
        }
    }
}