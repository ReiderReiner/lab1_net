using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Core;

public static class EventStorage
{
    private static readonly JsonSerializerOptions Options = new()
    {
        WriteIndented = true
    };

    private sealed record StoredEvent(
        string Type,
        string Title,
        DateTime Date,
        double BasePrice,
        bool IsVipZoneAvailable,
        int CoffeeBreaksCount);

    public static void Save(string filePath, IEnumerable<EventBase> events)
    {
        var storedEvents = events.Select(e => e switch
        {
            ConcertEvent c => new StoredEvent("concert", c.Title, c.Date, c.BasePrice, c.IsVipZoneAvailable, 0),
            ConferenceEvent cf => new StoredEvent("conference", cf.Title, cf.Date, cf.BasePrice, false, cf.CoffeeBreaksCount),
            _ => throw new InvalidOperationException("Невідомий тип події для збереження.")
        }).ToList();

        var json = JsonSerializer.Serialize(storedEvents, Options);
        File.WriteAllText(filePath, json);
    }

    public static IReadOnlyList<EventBase> Load(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return new List<EventBase>();
        }

        var json = File.ReadAllText(filePath);
        var storedEvents = JsonSerializer.Deserialize<List<StoredEvent>>(json, Options) ?? new List<StoredEvent>();

        return storedEvents.Select(e => e.Type switch
        {
            "concert" => new ConcertEvent(e.Title, e.Date, e.BasePrice, e.IsVipZoneAvailable) as EventBase,
            "conference" => new ConferenceEvent(e.Title, e.Date, e.BasePrice, e.CoffeeBreaksCount) as EventBase,
            _ => throw new InvalidDataException("Невідомий тип події у файлі.")
        }).ToList();
    }
}
