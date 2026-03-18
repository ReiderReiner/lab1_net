using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Core;

// --- ЗАВДАННЯ 2: Дослідження структур (Value Types) ---
static void TryModifyStruct(TicketPrice p) 
{
    p.Amount += 500; // Змінюємо локальну копію в стеку
}

TicketPrice originalPrice = new TicketPrice(150, "UAH");
TryModifyStruct(originalPrice);
// originalPrice.Amount залишиться 150, оскільки структури передаються за значенням

// --- ЗАВДАННЯ 3: Аналіз Boxing/Unboxing та продуктивності ---
object boxed = 2026; // Boxing
int unboxed = (int)boxed; // Unboxing

const int iterations = 1_000_000;
Stopwatch sw = new Stopwatch();

// Тест ArrayList (викликає Boxing для кожного int)
ArrayList arrayList = new ArrayList();
sw.Start();
for (int i = 0; i < iterations; i++) arrayList.Add(i);
sw.Stop();
long tArrayList = sw.ElapsedMilliseconds;

// Тест List<int> (узагальнена колекція, без Boxing)
sw.Restart();
List<int> genericList = new List<int>();
for (int i = 0; i < iterations; i++) genericList.Add(i);
sw.Stop();
long tGeneric = sw.ElapsedMilliseconds;

// --- ЗАВДАННЯ 4: Колекція об'єктів (10 івентів) ---
List<Event> eventList = new List<Event>
{
    new Event("Cyberpunk Night", new DateTime(2026, 05, 10), 450),
    new Event("Tech Summit", new DateTime(2026, 06, 12), 1200),
    new Event("C# Meetup", new DateTime(2026, 05, 10), 0),
    new Event("Hardware Expo", new DateTime(2026, 08, 20), 800),
    new Event("AI Workshop", new DateTime(2026, 06, 12), 2500),
    new Event("GameDev Party", new DateTime(2026, 05, 10), 300),
    new Event("Security Conf", new DateTime(2026, 11, 05), 1500),
    new Event("Cloud Day", new DateTime(2026, 06, 12), 0),
    new Event("Retro Gaming", new DateTime(2026, 04, 15), 200),
    new Event("Networking Hub", new DateTime(2026, 05, 10), 550)
};

// --- ЗАВДАННЯ 5-8: LINQ запити ---

// Фільтрація: події дорожче 1000 грн
var expensiveEvents = eventList.Where(e => e.Price > 1000).ToList();

// Сортування: спочатку за датою, потім за ціною
var sortedEvents = eventList
    .OrderBy(e => e.Date)
    .ThenBy(e => e.Price)
    .ToList();

// Проекція: список тільки назв подій
var eventTitles = eventList.Select(e => e.Title).ToList();

// Пошук: перша безкоштовна подія
var freeEvent = eventList.FirstOrDefault(e => e.Price == 0);

// --- ЗАВДАННЯ 9: Вивід результатів ---
Console.WriteLine($"Аналіз Boxing (на $1,000,000$ елементів):");
Console.WriteLine($"- ArrayList: {tArrayList} ms");
Console.WriteLine($"- List<int>: {tGeneric} ms");
Console.WriteLine($"- Різниця: {tArrayList - tGeneric} ms\n");

Console.WriteLine("----------------------------------------------------------");
Console.WriteLine("| {0,-18} | {1,-10} | {2,-10} |", "Назва події", "Дата", "Ціна");
Console.WriteLine("----------------------------------------------------------");
foreach (var ev in sortedEvents)
{
    Console.WriteLine("| {0,-18} | {1,10:dd.MM.yyyy} | {2,10} грн |", ev.Title, ev.Date, ev.Price);
}
Console.WriteLine("----------------------------------------------------------");

if (freeEvent != null)
    Console.WriteLine($"\nЗнайдено першу вільну подію: {freeEvent.Title}");
else
    Console.WriteLine("\nВільних подій не знайдено.");