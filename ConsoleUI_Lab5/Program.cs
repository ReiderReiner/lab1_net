using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Xml.Linq;
using Core;

namespace ConsoleUI
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("=========================================================");
            Console.WriteLine("         СИСТЕМА УПРАВЛІННЯ ПОДІЯМИ (LAB 5)");
            Console.WriteLine("         СЕРІАЛІЗАЦІЯ, XML, IDisposable");
            Console.WriteLine("=========================================================\n");

            // Створюємо тестові дані
            var events = new List<EventBase>
            {
                new ConcertEvent("Rock Festival", new DateTime(2026, 8, 15), 800, true),
                new ConcertEvent("Jazz Night", new DateTime(2026, 9, 20), 500, false),
                new ConferenceEvent("Tech Summit", new DateTime(2026, 10, 5), 1200, 3),
                new ConferenceEvent("Business Forum", new DateTime(2026, 11, 12), 900, 2)
            };

            Console.WriteLine(">>> СТВОРЕНО ТЕСТОВІ ДАНІ");
            foreach (var ev in events)
            {
                ev.DisplayInfo();
            }
            Console.WriteLine();

            // --------------------------------------------------------- 
            // ПУНКТ 2. JSON-СЕРІАЛІЗАЦІЯ
            // ---------------------------------------------------------
            Console.WriteLine(">>> ПУНКТ 2. JSON-СЕРІАЛІЗАЦІЯ");
            string jsonFile = "events.json";
            
            // Збереження у JSON
            var options = new JsonSerializerOptions { WriteIndented = true };
            using (var stream = File.Create(jsonFile))
            {
                JsonSerializer.Serialize(stream, events, options);
            }
            Console.WriteLine($"Дані збережено у файл {jsonFile}");

            // Завантаження з JSON
            if (File.Exists(jsonFile))
            {
                try
                {
                    using (var stream = File.OpenRead(jsonFile))
                    {
                        var loadedEvents = JsonSerializer.Deserialize<List<EventBase>>(stream);
                        Console.WriteLine("Дані успішно завантажено з JSON:");
                        foreach (var ev in loadedEvents ?? new List<EventBase>())
                        {
                            ev.DisplayInfo();
                        }
                    }
                }
                catch (JsonException ex)
                {
                    Console.WriteLine($"Помилка десеріалізації JSON: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine($"Файл {jsonFile} не існує");
            }
            Console.WriteLine();

            // --------------------------------------------------------- 
            // ПУНКТ 3. XML ЕКСПОРТ ЧЕРЕЗ XDocument
            // ---------------------------------------------------------
            Console.WriteLine(">>> ПУНКТ 3. XML ЕКСПОРТ (лише VIP-концерти)");
            
            // LINQ-запит: відбираємо лише концерти з VIP-зоною
            var vipConcerts = events.OfType<ConcertEvent>().Where(c => c.IsVipZoneAvailable);
            
            var xmlDoc = new XDocument(
                new XElement("Events",
                    from concert in vipConcerts
                    select new XElement("Concert",
                        new XElement("Title", concert.Title),
                        new XElement("Date", concert.Date.ToShortDateString()),
                        new XElement("Price", concert.CalculateFinalPrice()),
                        new XElement("VipZone", concert.IsVipZoneAvailable)
                    )
                )
            );

            string xmlFile = "vip_concerts.xml";
            using (var writer = new StreamWriter(xmlFile))
            {
                xmlDoc.Save(writer);
            }
            Console.WriteLine($"XML експортовано у файл {xmlFile}");
            Console.WriteLine("Зміст XML:");
            Console.WriteLine(xmlDoc.ToString());
            Console.WriteLine();

            // --------------------------------------------------------- 
            // ПУНКТ 4 та 5. IDisposable та USING
            // ---------------------------------------------------------
            Console.WriteLine(">>> ПУНКТ 4 та 5. IDisposable та USING");
            
            using (var logger = new ResourceManager("lab5_log.txt"))
            {
                logger.Log("Початок роботи Lab 5");
                logger.Log($"Оброблено {events.Count} подій");
                logger.Log("JSON серіалізація виконана");
                logger.Log("XML експорт виконаний");
                logger.Log("Кінець роботи Lab 5");
                Console.WriteLine("Лог записано через ResourceManager");
            }
            Console.WriteLine();

            // --------------------------------------------------------- 
            // ПУНКТ 6. ВАЛІДАЦІЯ ТА ОБРОБКА ПОМИЛОК
            // ---------------------------------------------------------
            Console.WriteLine(">>> ПУНКТ 6. ВАЛІДАЦІЯ ТА ОБРОБКА ПОМИЛОК");
            
            string corruptedFile = "corrupted.json";
            // Створюємо пошкоджений файл для демонстрації
            File.WriteAllText(corruptedFile, "{ invalid json content }");
            
            if (File.Exists(corruptedFile))
            {
                try
                {
                    using (var stream = File.OpenRead(corruptedFile))
                    {
                        var corruptedEvents = JsonSerializer.Deserialize<List<EventBase>>(stream);
                        Console.WriteLine("Несподівано, файл прочитано");
                    }
                }
                catch (JsonException ex)
                {
                    Console.WriteLine($"Оброблено помилку десеріалізації пошкодженого файлу: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Інша помилка: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Файл для тестування не існує");
            }

            Console.WriteLine("\n=========================================================");
            Console.WriteLine("                РОБОТУ ПРОГРАМИ ЗАВЕРШЕНО");
            Console.WriteLine("=========================================================");
        }
    }

    // ПУНКТ 4. Клас ResourceManager з IDisposable
    public class ResourceManager : IDisposable
    {
        private StreamWriter? _writer;
        private bool _disposed = false;

        public ResourceManager(string filePath)
        {
            _writer = new StreamWriter(filePath, append: true);
        }

        public void Log(string message)
        {
            if (_writer != null)
            {
                _writer.WriteLine($"{DateTime.Now}: {message}");
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _writer?.Dispose();
                }
                _writer = null;
                _disposed = true;
            }
        }
    }
}