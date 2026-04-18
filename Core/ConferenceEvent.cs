using System;

namespace Core
{
    public class ConferenceEvent : EventBase
    {
        public int CoffeeBreaksCount { get; set; }

        public ConferenceEvent(string title, DateTime date, double basePrice, int coffeeBreaks) 
            : base(title, date, basePrice)
        {
            CoffeeBreaksCount = coffeeBreaks;
        }

        public override double CalculateFinalPrice()
        {
            // Кожен кава-брейк додає 150 грн до ціни
            return BasePrice + (CoffeeBreaksCount * 150);
        }

        public override void Show()
        {
            Console.WriteLine($"[Бізнес-Конференція] {Title} | Кава-брейків: {CoffeeBreaksCount}");
            Console.WriteLine($" -> Вартість участі: {CalculateFinalPrice()} UAH");
        }
    }
}