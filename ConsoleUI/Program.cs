using System;
using Core;

// --- Завдання 6: Системна інформація ---
Console.WriteLine("=== Системні дані ===");
// Вивід версії ОС та обсягу використаної пам'яті
Console.WriteLine($"Версія ОС: {Environment.OSVersion}");
Console.WriteLine($"Пам'ять додатка: {GC.GetTotalMemory(false) / 1024} KB");
Console.WriteLine("--------------------\n");

// --- Індивідуальне завдання: Менеджер подій ---
Console.WriteLine("=== Деталі події ===");

// Створення екземплярів класів
Event techConf = new Event { 
    Title = ".NET Meetup 2026", 
    Date = new DateTime(2026, 5, 20), 
    TicketPrice = 550.0 
};

Participant user = new Participant { 
    FullName = "Євгеній Солод", 
    Age = 20, 
    HasVipStatus = true 
};

Registration reg = new Registration { 
    RegistrationId = 1023, 
    RegisteredAt = DateTime.Now, 
    IsConfirmed = true 
};

// Вивід даних у консоль
Console.WriteLine($"Подія: {techConf.Title} на дату {techConf.Date:dd.MM.yyyy}");
Console.WriteLine($"Ціна: {techConf.TicketPrice} грн");
Console.WriteLine($"Учасник: {user.FullName}, VIP: {user.HasVipStatus}");
Console.WriteLine($"Статус реєстрації №{reg.RegistrationId}: {(reg.IsConfirmed ? "Підтверджено" : "Очікує")}");