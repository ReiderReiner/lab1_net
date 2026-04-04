using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Core
{
    // Клас-контейнер з Dictionary та ітератором
    public class EventManager : IEnumerable<Event>
    {
        private List<Event> _events = new List<Event>();
        private Dictionary<string, Event> _eventDict = new Dictionary<string, Event>();

        // Додавання об'єкта (одразу в List і в Dictionary)
        public void Add(Event ev)
        {
            if (ev != null && !_eventDict.ContainsKey(ev.EventCode))
            {
                _events.Add(ev);
                _eventDict[ev.EventCode] = ev;
            }
        }

        // Реалізація ітератора через yield return
        // Примітка: C# підтримує duck typing ("качину типізацію") - цикл foreach працював би 
        // навіть без успадкування IEnumerable<Event>, якби був просто публічний метод GetEnumerator().
        // Але для роботи методів розширення LINQ (наприклад, .Count(), .Where()) успадкування IEnumerable<T> є обов'язковим.
        public IEnumerator<Event> GetEnumerator()
        {
            foreach (var ev in _events)
            {
                yield return ev;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // Швидкий пошук через Dictionary (О(1) складність)
        public Event FindByCode(string code)
        {
            return _eventDict.TryGetValue(code, out var ev) ? ev : null;
        }

        // LINQ по словнику (фільтрація за умовою)
        public IEnumerable<Event> GetExpensiveEvents(double minPrice)
        {
            // Беремо лише значення словника (.Values) і фільтруємо їх
            return _eventDict.Values.Where(e => e.TicketPrice >= minPrice);
        }
    }
}
