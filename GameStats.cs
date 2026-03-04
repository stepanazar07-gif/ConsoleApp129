using System;

namespace ConsoleApp129
{
    internal class GameStats
    {
        public int HP { get; set; }
        public int MaxHP { get; set; }
        public int Armor { get; set; }
        public int XP { get; set; }
        public int Level { get; set; }

        public GameStats()
        {
            MaxHP = 40;
            HP = MaxHP;
            Armor = 10;
            XP = 0;
            Level = 1;
        }

        public void TakeDamage(int damage)
        {
            int actualDamage = damage - Armor;
            if (actualDamage < 1) actualDamage = 1;
            HP -= actualDamage;
            if (HP < 0) HP = 0;
        }

        public void AddXP(int amount)
        {
            XP += amount;
            if (XP >= Level * 10)
            {
                Level++;
                MaxHP += 10;
                HP = MaxHP;
                Armor += 2;
                Console.WriteLine($"*** УРОВЕНЬ {Level}! ***");
                Console.WriteLine($"HP: {MaxHP}, Броня: {Armor}");
            }
        }

        public void Heal(int amount)
        {
            HP += amount;
            if (HP > MaxHP) HP = MaxHP;
        }

        public void ShowStats()
        {
            Console.WriteLine($"❤ HP: {HP}/{MaxHP} | 🛡 Броня: {Armor} | ⭐ XP: {XP}/{Level * 10} | 🎯 Уровень: {Level}");
        }
    }
}