using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Core;

static void TryModifyStruct(TicketPrice p)
{
    p.Amount += 500;
    p.Currency = "USD";
    Console.WriteLine($"[в середині] {p}");
}

TicketPrice originalPrice = new TicketPrice(150, "UAH");
Console.WriteLine($"Оригінал до: {originalPrice}");
TryModifyStruct(originalPrice);
Console.WriteLine($"Оригінал після: {originalPrice}");
Console.WriteLine("----------------------------------");

// boxing/unboxing
object boxed = (object)2026;
int unboxed = (int)boxed;

const int iterations = 1_000_000;
var sw = new Stopwatch();

var arrayList = new ArrayList();
sw.Start();
for (int i = 0; i < iterations; i++) arrayList.Add(i);
sw.Stop();
var tArray = sw.ElapsedMilliseconds;

sw.Restart();
var genericList = new List<int>();
for (int i = 0; i < iterations; i++) genericList.Add(i);
sw.Stop();
var tGeneric = sw.ElapsedMilliseconds;

Console.WriteLine($"ArrayList: {tArray} ms, List<int>: {tGeneric} ms, diff: {tArray - tGeneric} ms");
Console.WriteLine("----------------------------------");

// 10 об’єктів (Event з Core)
var eventList = new List<Event>
{
    new Event { Title="Cyberpunk Night", Date=new DateTime(2026,5,10), TicketPrice=450 },
    new Event { Title="Tech Summit", Date=new DateTime(2026,6,12), TicketPrice=1200 },
    new Event { Title="C# Meetup", Date=new DateTime(2026,5,10), TicketPrice=0 },
    new Event { Title="Hardware Expo", Date=new DateTime(2026,8,20), TicketPrice=800 },
    new Event { Title="AI Workshop", Date=new DateTime(2026,6,12), TicketPrice=2500 },
    new Event { Title="GameDev Party", Date=new DateTime(2026,5,10), TicketPrice=300 },
    new Event { Title="Security Conf", Date=new DateTime(2026,11,5), TicketPrice=1500 },
    new Event { Title="Cloud Day", Date=new DateTime(2026,6,12), TicketPrice=0 },
    new Event { Title="Retro Gaming", Date=new DateTime(2026,4,15), TicketPrice=200 },
    new Event { Title="Networking Hub", Date=new DateTime(2026,5,10), TicketPrice=550 }
};

var expensive = eventList.Where(e => e.TicketPrice > 1000).ToList();
var sorted = eventList.OrderBy(e => e.Date).ThenBy(e => e.TicketPrice).ToList();
var titles = eventList.Select(e => e.Title).ToList();
var freeEvent = eventList.FirstOrDefault(e => e.TicketPrice == 0);

Console.WriteLine("=== Всі події (відсортовані) ===");
foreach (var e in sorted)
    Console.WriteLine(e);

Console.WriteLine("\n=== Події > 1000 грн ===");
foreach (var e in expensive)
    Console.WriteLine(e);

Console.WriteLine("\n=== Назви подій ===");
foreach (var t in titles)
    Console.WriteLine(t);

if (freeEvent != null)
    Console.WriteLine($"\nПерша безкоштовна подія: {freeEvent.Title}");
else
    Console.WriteLine("\nБезкоштовних подій нема.");