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
        public bool HasAmulet { get; set; }
        public bool HasCrown { get; set; }
        public bool HasScepter { get; set; }
        public int ArtifactBonus { get; set; } = 0;

        public Hero(int X, int Y) : base(X, Y)
        {
            this.X = X;
            this.Y = Y;
            Stats = new GameStats();
            FreezeTurns = 0;
            SandTurns = 0;
            PoisonTurns = 0;
            PoisonCounter = 0;
            HasAmulet = false;
            HasCrown = false;
            HasScepter = false;
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
            Console.ForegroundColor = ConsoleColor.White;
            return '⛇';
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
        private int HP;
        public Boss(int x, int y) : base(x, y)
        {
            HP = 60;
            Damage = 12;
        }

        public override char Rendering_on_the_map()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            return '☠';
        }

        public bool SimpleFight(Hero hero, Map map)
        {
           
            int bonus = 0;
            string name = "";

            Console.Clear();
            Console.SetCursorPosition(0, 5);
            Console.WriteLine("═══ ВЫБОР АРТЕФАКТА ═══\n");

            if (hero.HasAmulet) Console.WriteLine("1. 🟡 Амулет (+5 урона)");
            if (hero.HasCrown) Console.WriteLine("2. 🟣 Корона (+8 урона)");
            if (hero.HasScepter) Console.WriteLine("3. 🔵 Скипетр (+12 урона)");

            Console.Write("\nВыберите (1-3): ");
            var key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.D1 && hero.HasAmulet) { bonus = 5; name = "Амулет"; }
            else if (key == ConsoleKey.D2 && hero.HasCrown) { bonus = 8; name = "Корона"; }
            else if (key == ConsoleKey.D3 && hero.HasScepter) { bonus = 12; name = "Скипетр"; }

            hero.ArtifactBonus = bonus;

            Console.Clear();
            Console.SetCursorPosition(0, 10);
            Console.WriteLine($"⚔ Вы выбрали: {name}! Бонус: +{bonus}");
            Thread.Sleep(1500);
            Console.Clear();
         

            Console.SetCursorPosition(0, 27);
            Console.Write("⚔ НАЧАЛСЯ БОЙ С БОССОМ!                                   ");
            Thread.Sleep(400);

            var rnd = new Random();

            while (HP > 0 && hero.Stats.HP > 0)
            {
               
                int heroDmg = 6 + rnd.Next(0, 5) + hero.ArtifactBonus;
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
                Console.Clear();
                Console.SetCursorPosition(0, 10);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("  ╔══════════════════════════════════════╗");
                Console.WriteLine("  ║      🎉  ВЫ ПРОШЛИ ИГРУ!  🎉          ║");
                Console.WriteLine("  ║      Вы победили финального босса!   ║");
                Console.WriteLine("  ╚══════════════════════════════════════╝");
                Console.ResetColor();
                Console.SetCursorPosition(0, 20);
                Console.WriteLine("  Нажмите любую клавишу для выхода...");
                Console.ReadKey();
                Environment.Exit(0);
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
}