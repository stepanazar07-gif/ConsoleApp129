using System;
using System.IO;
using System.Text.Json;
using ConsoleApp129;

namespace ConsoleApp129.Save
{
    public static class SaveManager
    {
        private const string path = "save.json";

        public static void Save(GameData data)
        {
            try
            {
                string json = JsonSerializer.Serialize(data, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                File.WriteAllText(path, json);
                Console.SetCursorPosition(0, 30);
                Console.WriteLine("💾 Игра сохранена!          ");
            }
            catch (IOException ioEx)
            {
                Console.SetCursorPosition(0, 30);
                Console.WriteLine("❌ Ошибка записи файла сохранения.");
            }
            catch (UnauthorizedAccessException uaEx)
            {
                Console.SetCursorPosition(0, 30);
                Console.WriteLine("❌ Доступ к файлу запрещён.");
            }
            catch (Exception ex)
            {
                Console.SetCursorPosition(0, 30);
                Console.WriteLine("❌ Ошибка при сохранении.");
            }
        }

        public static GameData Load()
        {
            try
            {
                if (!File.Exists(path))
                {
                    Console.SetCursorPosition(0, 30);
                    Console.WriteLine("❌ Сохранение не найдено!");
                    return null;
                }

                string json = File.ReadAllText(path);
                var data = JsonSerializer.Deserialize<GameData>(json);
                Console.SetCursorPosition(0, 30);
                Console.WriteLine("✅ Игра загружена!          ");
                return data;
            }
            catch (JsonException jEx)
            {
                Console.SetCursorPosition(0, 30);
                Console.WriteLine("❌ Файл сохранения повреждён.");
                return null;
            }
            catch (IOException ioEx)
            {
                Console.SetCursorPosition(0, 30);
                Console.WriteLine("❌ Ошибка чтения файла.");
                return null;
            }
            catch (Exception ex)
            {
                Console.SetCursorPosition(0, 30);
                Console.WriteLine("❌ Ошибка при загрузке.");
                return null;
            }
        }
    }
}