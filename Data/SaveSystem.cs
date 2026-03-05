using System;
using System.IO;

namespace ConsoleApp129
{
    internal class SaveSystem
    {
       
        private static string savePath = "savegame.txt";

    
        public static void SaveGame(Hero hero, Map map)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(savePath))
                {
                   
                    writer.WriteLine(hero.Stats.HP);
                    writer.WriteLine(hero.Stats.MaxHP);
                    writer.WriteLine(hero.Stats.Armor);
                    writer.WriteLine(hero.Stats.XP);
                    writer.WriteLine(hero.Stats.Level);

                    

                    Console.WriteLine("Игра сохранена!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка сохранения: {ex.Message}");
            }
        }

       
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
                    hero.Stats.XP = int.Parse(reader.ReadLine());
                    hero.Stats.Level = int.Parse(reader.ReadLine());

                    Console.WriteLine("Игра загружена!");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка загрузки: {ex.Message}");
                return false;
            }
        }
    }
}