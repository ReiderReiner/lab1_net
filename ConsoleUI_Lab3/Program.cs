using System;
using System.Collections.Generic;
using Core;

class Program
{
    static void Main(string[] args)
    {
        // Ініціалізація менеджера (Клас-контейнер)
        var manager = new EventManager
        {
            new Event { EventCode = "EV-01", Title = "Cyberpunk Night", Date = new DateTime(2026, 5, 10), TicketPrice = 450 },
            new Event { EventCode = "EV-02", Title = "Tech Summit", Date = new DateTime(2026, 6, 12), TicketPrice = 1500 },
            new Event { EventCode = "EV-03", Title = "C# Meetup", Date = new DateTime(2026, 5, 15), TicketPrice = 0 }
        };

        // 4. Ітерація через foreach (працює завдяки GetEnumerator та yield return)
        Console.WriteLine("=== Всі події в менеджері ===");
        foreach (var ev in manager)
        {
            Console.WriteLine(ev);
        }

        // 2. Демонстрація методів розширення
        Console.WriteLine("\n=== Методи розширення ===");
        double somePrice = 1250.5;
        Console.WriteLine($"Форматування числа: {somePrice.ToCurrencyString()}");
        Console.WriteLine($"Кількість безкоштовних подій у менеджері: {manager.CountFreeEvents()}");

        // 5. Швидкий пошук (Dictionary)
        Console.WriteLine("\n=== Швидкий пошук (Dictionary) ===");
        var searchCode = "EV-02";
        var foundEvent = manager.FindByCode(searchCode);
        if (foundEvent != null)
            Console.WriteLine($"Знайдено за кодом {searchCode}: {foundEvent.Title} ({foundEvent.TicketPrice.ToCurrencyString()})");
        else
            Console.WriteLine($"Подію з кодом {searchCode} не знайдено.");

        // 5. LINQ по словнику
        Console.WriteLine("\n=== LINQ по словнику (Події >= 1000 UAH) ===");
        var expensiveEvents = manager.GetExpensiveEvents(1000);
        foreach (var ev in expensiveEvents)
        {
            Console.WriteLine($"{ev.Title} - {ev.TicketPrice.ToCurrencyString()}");
        }

        // 6. Робота з HashSet (унікальні теги)
        Console.WriteLine("\n=== Робота з HashSet ===");
        // Додаємо дублікат "IT" у першу колекцію, щоб показати, що він проігнорується
        var tagsCyberpunk = new HashSet<string> { "IT", "Party", "Cyber", "IT" }; 
        var tagsSummit = new HashSet<string> { "IT", "Business", "Networking" };

        Console.WriteLine($"Теги Cyberpunk (без дублікатів): {string.Join(", ", tagsCyberpunk)}");
        Console.WriteLine($"Теги Summit: {string.Join(", ", tagsSummit)}");

        // Перетин (Intersect) - шукаємо спільні категорії
        var commonTags = new HashSet<string>(tagsCyberpunk);
        commonTags.IntersectWith(tagsSummit);
        Console.WriteLine($"Спільні теги (Перетин): {string.Join(", ", commonTags)}");

        // Об'єднання (Union) - об'єднуємо всі категорії без повторень
        var allUniqueTags = new HashSet<string>(tagsCyberpunk);
        allUniqueTags.UnionWith(tagsSummit);
        Console.WriteLine($"Всі унікальні теги (Об'єднання): {string.Join(", ", allUniqueTags)}");
    }
}
