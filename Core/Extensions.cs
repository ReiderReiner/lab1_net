using System;
using System.Collections.Generic;
using System.Linq;

namespace Core
{
    // Статичний клас для методів розширення
    public static class Extensions
    {
        // Метод розширення для double (форматування ціни)
        public static string ToCurrencyString(this double amount, string currency = "UAH")
        {
            return $"{amount:N2} {currency}";
        }

        // Метод розширення для EventManager
        public static int CountFreeEvents(this EventManager manager)
        {
            if (manager == null) return 0;
            return manager.Count(e => e.TicketPrice == 0); // Працює завдяки IEnumerable<Event>
        }
    }
}
