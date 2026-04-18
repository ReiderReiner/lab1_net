using System.Collections.Generic;

namespace Core
{
    public class EventContainer
    {
        // Контейнер зберігає об'єкти, але створюються вони ззовні (Агрегація)
        private List<EventBase> _events = new List<EventBase>();

        public void AddEvent(EventBase ev)
        {
            _events.Add(ev);
        }
        
        // Метод для доступу до списку подій, якщо знадобиться
        public IEnumerable<EventBase> GetAllEvents()
        {
            return _events;
        }
    }
}