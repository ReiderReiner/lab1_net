using System;

namespace Core
{
    public class ConcertEvent : EventBase
    {
        public bool IsVipZoneAvailable { get; set; }

        public ConcertEvent(string title, DateTime date, double basePrice, bool isVip) 
            : base(title, date, basePrice)
        {
            IsVipZoneAvailable = isVip;
        }

        public override double CalculateFinalPrice()
        {
            // Націнка за VIP-зону 50%
            return IsVipZoneAvailable ? BasePrice * 1.5 : BasePrice;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"[Концерт] {Title} | VIP-зона: {(IsVipZoneAvailable ? "Так" : "Ні")}");
        }
    }
}