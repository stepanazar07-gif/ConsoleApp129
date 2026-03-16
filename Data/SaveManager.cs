using ConsoleApp129;
using ConsoleApp129.Exceptions;
using System;
using System.IO;
using System.Text.Json;
using ConsoleApp129.Exceptions;

namespace ConsoleApp129.Save
{
    /// <summary>
    /// Менеджер сохранений игры. Обрабатывает запись и загрузку данных в JSON
    /// </summary>
    public static class SaveManager
    {
        /// <summary>
        /// Путь к файлу сохранения
        /// </summary>
        private const string path = "save.json";

        /// <summary>
        /// Сохраняет данные игры в JSON-файл
        /// </summary>
        /// <param name="data">Объект GameData с текущим состоянием игры</param>
        /// <exception cref="IOException">При ошибке записи файла</exception>
        /// <exception cref="UnauthorizedAccessException">При отсутствии прав доступа</exception>
        public static void Save(GameData data)
        {
            try
            {
                if (data == null)  // ← Добавлена проверка
                {
                    throw new SaveGameException("Данные для сохранения пустые");
                }
                string json = JsonSerializer.Serialize(data, new JsonSerializerOptions
                {
                    WriteIndented = true
                });
                File.WriteAllText(path, json);
                Console.SetCursorPosition(0, 30);
                Console.WriteLine(" 💾   Игра   сохранена !          ");
            }
            catch (IOException ioEx)
            {
                Console.SetCursorPosition(0, 30);
                Console.WriteLine(" ❌  Ошибка записи файла сохранения.");
            }
            catch (UnauthorizedAccessException uaEx)
            {
                Console.SetCursorPosition(0, 30);
                Console.WriteLine(" ❌   Доступ   к   файлу   запрещён .");
            }
            catch (Exception ex)
            {
                Console.SetCursorPosition(0, 30);
                Console.WriteLine(" ❌  Ошибка при сохранении.");
            }
        }

        /// <summary>
        /// Загружает сохранённое состояние игры из файла
        /// </summary>
        /// <returns>Объект GameData или null при ошибке загрузки</returns>
        /// <exception cref="JsonException">При повреждении JSON-файла</exception>
        /// <exception cref="FileNotFoundException">Если файл сохранения не найден</exception>
        public static GameData Load()
        {
            try
            {
                if (!File.Exists(path))
                {
                    Console.SetCursorPosition(0, 30);
                    Console.WriteLine(" ❌  Сохранение не найдено!");
                    return null;
                }
                string json = File.ReadAllText(path);
                var data = JsonSerializer.Deserialize<GameData>(json);
                Console.SetCursorPosition(0, 30);
                Console.WriteLine(" ✅   Игра   загружена !          ");
                return data;
            }
            catch (JsonException jEx)
            {
                Console.SetCursorPosition(0, 30);
                Console.WriteLine(" ❌   Файл   сохранения   повреждён .");
                return null;
            }
            catch (IOException ioEx)
            {
                Console.SetCursorPosition(0, 30);
                Console.WriteLine(" ❌   Ошибка   чтения   файла .");
                return null;
            }
            catch (Exception ex)
            {
                Console.SetCursorPosition(0, 30);
                Console.WriteLine(" ❌   Ошибка   при   загрузке .");
                return null;
            }
        }
    }
}