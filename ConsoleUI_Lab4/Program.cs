using System;
using System.Linq;
using Core; 

namespace ConsoleUI
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("=========================================================");
            Console.WriteLine("         СИСТЕМА УПРАВЛІННЯ ПОДІЯМИ (OOP EDITION)");
            Console.WriteLine("=========================================================\n");

            // ---------------------------------------------------------
            // ПУНКТ 2. ДЕМОНСТРАЦІЯ АБСТРАКЦІЇ ТА ПЕРЕВИЗНАЧЕННЯ (override)
            // ---------------------------------------------------------
            Console.WriteLine(">>> ПУНКТ 2. АБСТРАКТНИЙ РІВЕНЬ (virtual / override)");
            
            var testConcert = new ConcertEvent("Local Indie Gig", new DateTime(2026, 7, 15), 400, true);
            
            // Виклик віртуального методу (який ми перевизначили через override)
            testConcert.DisplayInfo(); 
            
            // Виклик абстрактного методу (який розрахував +50% за VIP зону)
            Console.WriteLine($"Базова ціна: 400 UAH. Фінальна (abstract метод): {testConcert.CalculateFinalPrice()} UAH\n");

            // ---------------------------------------------------------
            // ПУНКТ 4. ДЕМОНСТРАЦІЯ КОМПОЗИЦІЇ
            // ---------------------------------------------------------
            Console.WriteLine(">>> ПУНКТ 4. ДЕМОНСТРАЦІЯ КОМПОЗИЦІЇ (Жорсткий зв'язок)");
            var controller = new EventController();
            controller.Start(); // Контролер сам створив конфіг всередині себе
            Console.WriteLine();

            // ---------------------------------------------------------
            // ПУНКТ 5. ДЕМОНСТРАЦІЯ АГРЕГАЦІЇ
            // ---------------------------------------------------------
            Console.WriteLine(">>> ПУНКТ 5. ДЕМОНСТРАЦІЯ АГРЕГАЦІЇ (Слабкий зв'язок)");
            
            // Створюємо об'єкти ЗЗОВНІ контейнера
            var concert1 = new ConcertEvent("Rock in Dnipro", new DateTime(2026, 8, 24), 1000, true);
            var concert2 = new ConcertEvent("Symphony Evening", new DateTime(2026, 9, 5), 600, false);
            var conference = new ConferenceEvent("IT Data Summit", new DateTime(2026, 9, 10), 800, 3);

            // Передаємо їх у контейнер
            var container = new EventContainer();
            container.AddEvent(concert1);
            container.AddEvent(concert2);
            container.AddEvent(conference);

            Console.WriteLine("Об'єкти успішно створені ззовні та додані до контейнера.");
            Console.WriteLine($"Поточний вміст контейнера (кількість: {container.GetAllEvents().Count()}):");
            
            foreach (var ev in container.GetAllEvents())
            {
                Console.WriteLine($"  - {ev.Title} ({ev.Date.ToShortDateString()})");
            }
            Console.WriteLine();

            // ---------------------------------------------------------
            // ПУНКТ 3 та 6. ДЕМОНСТРАЦІЯ ПОЛІМОРФІЗМУ
            // ---------------------------------------------------------
            Console.WriteLine(">>> ПУНКТ 3 та 6. ДЕМОНСТРАЦІЯ ПОЛІМОРФІЗМУ ТА ІНТЕРФЕЙСІВ");
            Console.WriteLine("Виклик методу Show() через загальний масив інтерфейсного типу:\n");

            // Масив типу інтерфейсу
            IShow[] showableItems = new IShow[] 
            { 
                concert1, 
                concert2, 
                conference 
            };

            // Поліморфний виклик у циклі
            foreach (IShow item in showableItems)
            {
                item.Show();
                Console.WriteLine(".........................................................");
            }

            Console.WriteLine("\n=========================================================");
            Console.WriteLine("                РОБОТУ ПРОГРАМИ ЗАВЕРШЕНО");
            Console.WriteLine("=========================================================");
        }
    }
}