using System;

namespace ConsoleApp129
{
    /// <summary>
    /// Класс статистики героя: здоровье, броня, уровень
    /// Используется для хранения и расчёта боевых характеристик
    /// </summary>
    internal class GameStats
    {
        /// <summary>
        /// Текущее количество здоровья героя
        /// </summary>
        public int HP;

        /// <summary>
        /// Максимальное количество здоровья героя
        /// </summary>
        public int MaxHP;

        /// <summary>
        /// Уровень брони (уменьшает входящий урон)
        /// </summary>
        public int Armor;

        /// <summary>
        /// Уровень героя (влияет на сложность врагов)
        /// </summary>
        public int Level;

        /// <summary>
        /// Конструктор статистики с начальными значениями
        /// Устанавливает: HP=40, MaxHP=40, Armor=10, Level=1
        /// </summary>
        public GameStats()
        {
            MaxHP = 40;
            HP = 40;
            Armor = 10;
            Level = 1;
        }

        /// <summary>
        /// Расчёт и применение урона с учётом брони
        /// Формула: actualDamage = damage - Armor (минимум 1)
        /// </summary>
        /// <param name="damage">Базовый урон от атаки врага</param>
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
            Console.WriteLine(" ⚔  Получено " + actualDamage + " урона (броня " + Armor + ")   ");
        }

        /// <summary>
        /// Отображение текущей статистики героя в консоли
        /// Формат: ❤ HP: 40/40 | 🛡 Броня: 10
        /// </summary>
        public void ShowStats()
        {
            Console.WriteLine(" ❤ HP: " + HP + "/" + MaxHP + " |  🛡 Броня: " + Armor + "");
        }
    }
}