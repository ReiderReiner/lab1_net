using System;

namespace Core
{
    public class EventController
    {
        // Контролер жорстко "володіє" конфігом (Композиція)
        private SystemConfig _config;

        public EventController()
        {
            // Об'єкт створюється безпосередньо в конструкторі
            _config = new SystemConfig();
        }

        public void Start()
        {
            Console.WriteLine($"--- Запуск контролера. Версія системи: {_config.Version} ---");
        }
    }
}