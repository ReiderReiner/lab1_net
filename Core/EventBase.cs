using System;
using System.Text.Json.Serialization;

namespace Core
{
    [JsonDerivedType(typeof(ConcertEvent), "concert")]
    [JsonDerivedType(typeof(ConferenceEvent), "conference")]
    public abstract class EventBase : IShow
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public double BasePrice { get; set; }

        protected EventBase() { }

        protected EventBase(string title, DateTime date, double basePrice)
        {
            Title = title;
            Date = date;
            BasePrice = basePrice;
        }

        // Абстрактний метод: нащадки ЗОБОВ'ЯЗАНІ його реалізувати по-своєму
        public abstract double CalculateFinalPrice();

        // Віртуальний метод: базова реалізація
        public virtual void DisplayInfo()
        {
            Console.WriteLine($"Подія: {Title} | Дата: {Date.ToShortDateString()}");
        }

        // Реалізація інтерфейсу IShow
        public virtual void Show()
        {
            DisplayInfo();
            Console.WriteLine($" -> До сплати: {CalculateFinalPrice()} UAH");
        }
    }
}