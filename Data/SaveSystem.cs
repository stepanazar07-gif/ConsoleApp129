using System;
using System.IO;

namespace ConsoleApp129
{
    /// <summary>
    /// Альтернативная система сохранений в текстовом формате
    /// Используется для простого сохранения статистики героя
    /// </summary>
    internal class SaveSystem
    {
        /// <summary>
        /// Путь к файлу сохранения (текстовый формат)
        /// </summary>
        private static string savePath = "savegame.txt";

        /// <summary>
        /// Сохранение данных героя в текстовый файл
        /// Записывает: HP, MaxHP, Armor, Level (по строке на параметр)
        /// </summary>
        /// <param name="hero">Объект героя для сохранения</param>
        /// <param name="map">Объект карты (не используется в текущей версии)</param>
        public static void SaveGame(Hero hero, Map map)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(savePath))
                {
                    writer.WriteLine(hero.Stats.HP);
                    writer.WriteLine(hero.Stats.MaxHP);
                    writer.WriteLine(hero.Stats.Armor);
                    writer.WriteLine(hero.Stats.Level);
                }
                Console.SetCursorPosition(0, 30);
                Console.Write(new string(' ', 50));
                Console.SetCursorPosition(0, 30);
                Console.WriteLine(" 💾   Игра   сохранена !          ");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка сохранения:" + ex.Message);
            }
        }

        /// <summary>
        /// Загрузка данных героя из текстового файла
        /// Читает 4 строки: HP, MaxHP, Armor, Level
        /// </summary>
        /// <param name="hero">Объект героя для заполнения данными</param>
        /// <returns>True если загрузка успешна, иначе False</returns>
        public static bool LoadGame(Hero hero)
        {
            try
            {
                if (!File.Exists(savePath))
                {
                    Console.WriteLine("Сохранение не найдено!");
                    return false;
                }
                using (StreamReader reader = new StreamReader(savePath))
                {
                    hero.Stats.HP = int.Parse(reader.ReadLine());
                    hero.Stats.MaxHP = int.Parse(reader.ReadLine());
                    hero.Stats.Armor = int.Parse(reader.ReadLine());
                    hero.Stats.Level = int.Parse(reader.ReadLine());
                }
                Console.WriteLine(" Игра   загружена !");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(" Ошибка   загрузки : " + ex.Message);
                return false;
            }
        }
    }
}