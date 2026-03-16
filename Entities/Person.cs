using System;
using System.Threading;

namespace ConsoleApp129
{
    /// <summary>
    /// Абстрактный базовый класс для всех персонажей на карте
    /// Наследуется от MapObject, добавляет координаты и общие свойства
    /// </summary>
    internal abstract class Person : MapObject
    {
        /// <summary>
        /// Координата X персонажа на карте
        /// </summary>
        protected int pointX;

        /// <summary>
        /// Координата Y персонажа на карте
        /// </summary>
        protected int pointY;

        /// <summary>
        /// Флаг наличия амулета у персонажа (для расчёта урона)
        /// </summary>
        public bool HasAmulet { get; set; }

        /// <summary>
        /// Конструктор персонажа с начальными координатами
        /// </summary>
        /// <param name="X">Координата X</param>
        /// <param name="Y">Координата Y</param>
        public Person(int X, int Y)
        {
            pointX = X;
            pointY = Y;
            HasAmulet = false;
        }

        /// <summary>
        /// Базовая отрисовка персонажа: символ '☺'
        /// Переопределяется в наследниках для специфичного отображения
        /// </summary>
        /// <returns>Символ '☺'</returns>
        public override char Rendering_on_the_map()
        {
            return '☺';
        }
    }

    /// <summary>
    /// Класс героя. Управляемый игроком персонаж
    /// Наследуется от Person, добавляет статистику и статусные эффекты
    /// </summary>
    internal class Hero : Person
    {
        /// <summary>
        /// Объект статистики героя (HP, броня, уровень)
        /// </summary>
        public GameStats Stats { get; private set; }

        /// <summary>
        /// Координата X героя на карте
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Координата Y героя на карте
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Количество ходов заморозки (пропуск хода)
        /// </summary>
        public int FreezeTurns { get; set; }

        /// <summary>
        /// Количество ходов ослепления песком (пропуск хода)
        /// </summary>
        public int SandTurns { get; set; }

        /// <summary>
        /// Количество ходов отравления (периодический урон)
        /// </summary>
        public int PoisonTurns { get; set; }

        /// <summary>
        /// Счётчик ходов для расчёта урона от яда (каждые 3 хода)
        /// </summary>
        public int PoisonCounter { get; set; }

        /// <summary>
        /// Флаг наличия амулета (бонус +5 урона против босса)
        /// Скрывает свойство HasAmulet из базового класса Person
        /// </summary>
        public new bool HasAmulet { get; set; }

        /// <summary>
        /// Флаг наличия короны (бонус +8 урона против босса)
        /// </summary>
        public bool HasCrown { get; set; }

        /// <summary>
        /// Флаг наличия скипетра (бонус +12 урона против босса)
        /// </summary>
        public bool HasScepter { get; set; }

        /// <summary>
        /// Суммарный бонус урона от подобранных артефактов
        /// </summary>
        public int ArtifactBonus { get; set; } = 0;

        /// <summary>
        /// Конструктор героя с начальными координатами и статистикой
        /// </summary>
        /// <param name="X">Координата X спавна</param>
        /// <param name="Y">Координата Y спавна</param>
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

        /// <summary>
        /// Отрисовка героя с учётом статусных эффектов
        /// Яд: '☣' тёмно-зелёный | Песок: '≅' тёмно-жёлтый | Заморозка: '■' голубой
        /// Нормальное состояние: '☻' жёлтый
        /// </summary>
        /// <returns>Символ героя в зависимости от статуса</returns>
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

    /// <summary>
    /// Класс обычного врага. Базовый противник игрока
    /// Наследуется от Person, добавляет урон
    /// </summary>
    internal class Enemy : Person
    {
        /// <summary>
        /// Урон, наносимый врагом при столкновении
        /// </summary>
        public int Damage { get; set; }

        /// <summary>
        /// Конструктор врага с начальным уроном 3
        /// </summary>
        /// <param name="X">Координата X спавна</param>
        /// <param name="Y">Координата Y спавна</param>
        public Enemy(int X, int Y) : base(X, Y)
        {
            Damage = 3;
        }

        /// <summary>
        /// Отрисовка врага: символ '☺' красного цвета
        /// </summary>
        /// <returns>Символ '☺'</returns>
        public override char Rendering_on_the_map()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            return '☺';
        }
    }

    /// <summary>
    /// Класс зимнего врага. Специальный враг уровня 2 (Зима)
    /// Имеет шанс заморозить героя на 2 хода
    /// </summary>
    internal class WinterEnemy : Enemy
    {
        /// <summary>
        /// Шанс заморозки в процентах (по умолчанию 50%)
        /// </summary>
        public int FreezeChance { get; set; }

        /// <summary>
        /// Конструктор зимнего врага с уроном 5 и шансом заморозки 50%
        /// </summary>
        /// <param name="x">Координата X спавна</param>
        /// <param name="y">Координата Y спавна</param>
        public WinterEnemy(int x, int y) : base(x, y)
        {
            Damage = 5;
            FreezeChance = 50;
        }

        /// <summary>
        /// Отрисовка зимнего врага: символ '⛇' белого цвета
        /// </summary>
        /// <returns>Символ '⛇'</returns>
        public override char Rendering_on_the_map()
        {
            Console.ForegroundColor = ConsoleColor.White;
            return '⛇';
        }
    }

    /// <summary>
    /// Класс пустынного врага. Специальный враг уровня 3 (Пустыня)
    /// Имеет шанс ослепить песком и отравить героя
    /// </summary>
    internal class DesertEnemy : Enemy
    {
        /// <summary>
        /// Шанс броска песка в процентах (по умолчанию 50%)
        /// </summary>
        public int SandChance { get; set; }

        /// <summary>
        /// Шанс отравления в процентах (по умолчанию 70%)
        /// </summary>
        public int PoisonChance { get; set; }

        /// <summary>
        /// Конструктор пустынного врага с уроном 8 и спец. способностями
        /// </summary>
        /// <param name="x">Координата X спавна</param>
        /// <param name="y">Координата Y спавна</param>
        public DesertEnemy(int x, int y) : base(x, y)
        {
            Damage = 8;
            SandChance = 50;
            PoisonChance = 70;
        }

        /// <summary>
        /// Отрисовка пустынного врага: символ 'S' жёлтого цвета
        /// </summary>
        /// <returns>Символ 'S'</returns>
        public override char Rendering_on_the_map()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            return 'S';
        }
    }

    /// <summary>
    /// Класс финального босса. Уникальный противник уровня 4
    /// Имеет отдельную боевую систему с выбором артефактов
    /// </summary>
    internal class Boss : Enemy
    {
        /// <summary>
        /// Здоровье босса (отдельное от базового класса)
        /// </summary>
        private int HP;

        /// <summary>
        /// Конструктор босса с фиксированными характеристиками
        /// </summary>
        /// <param name="x">Координата X спавна</param>
        /// <param name="y">Координата Y спавна</param>
        public Boss(int x, int y) : base(x, y)
        {
            HP = 60;
            Damage = 12;
        }

        /// <summary>
        /// Отрисовка босса: символ '☠' фиолетового цвета
        /// </summary>
        /// <returns>Символ '☠'</returns>
        public override char Rendering_on_the_map()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            return '☠';
        }

        /// <summary>
        /// Боевая система с боссом: пошаговый бой с выбором артефакта
        /// Герой выбирает один из подобранных артефактов для бонуса урона
        /// Бой продолжается до смерти босса (победа) или героя (поражение)
        /// </summary>
        /// <param name="hero">Объект героя для расчёта урона</param>
        /// <param name="map">Объект карты (для доступа к методам)</param>
        /// <returns>True если босс побеждён, иначе False</returns>
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