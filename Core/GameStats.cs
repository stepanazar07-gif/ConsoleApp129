using System;

namespace ConsoleApp129
{
    internal class GameStats
    {
        public int HP;
        public int MaxHP;
        public int Armor;
        public int Level;

        public GameStats()
        {
            MaxHP = 40;
            HP = 40;
            Armor = 10;
            Level = 1;
        }

        public void TakeDamage(int damage)
        {
            int actualDamage = damage - Armor;
            if (actualDamage < 1)
            {
                actualDamage = 1;
            }

            HP = HP - actualDamage;

            if (HP < 0)
            {
                HP = 0;
            }

            Console.SetCursorPosition(0, 28);
            Console.WriteLine("⚔ Получено " + actualDamage + " урона (броня " + Armor + ")   ");
        }

        public void ShowStats(bool hasAmulet = false)
        {
            string amuletStatus = hasAmulet ? " | 🔮 АМУЛЕТ" : "";
            Console.WriteLine("❤ HP: " + HP + "/" + MaxHP + " | 🛡 Броня: " + Armor + " | 🎯 Уровень: " + Level + amuletStatus);
        }
    }
}