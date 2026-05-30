using System;
using System.IO;
using System.Linq;
using Core;
using Xunit;

namespace Core.Tests;

public class EventTests
{
    [Fact]
    public void ConcertEvent_CalculateFinalPrice_AddsVipMarkup()
    {
        var concert = new ConcertEvent("Rock", new DateTime(2026, 8, 15), 1000, true);
        Assert.Equal(1500, concert.CalculateFinalPrice());
    }

    [Fact]
    public void ConferenceEvent_CalculateFinalPrice_AddsCoffeeBreakCost()
    {
        var conference = new ConferenceEvent("Tech", new DateTime(2026, 10, 1), 1000, 2);
        Assert.Equal(1300, conference.CalculateFinalPrice());
    }

    [Fact]
    public void EventValidator_DetectsInvalidTitle()
    {
        var eventBase = new ConcertEvent(string.Empty, DateTime.Today, 200, false);
        var valid = EventValidator.IsValid(eventBase, out var errorMessage);

        Assert.False(valid);
        Assert.Contains("Назва події", errorMessage);
    }

    [Fact]
    public void EventManager_GetExpensiveEvents_FiltersByThreshold()
    {
        var manager = new EventManager();
        manager.Add(new Event { EventCode = "A", Title = "Cheap", Date = DateTime.Today, TicketPrice = 100 });
        manager.Add(new Event { EventCode = "B", Title = "Expensive", Date = DateTime.Today, TicketPrice = 500 });

        var filtered = manager.GetExpensiveEvents(300).ToList();

        Assert.Single(filtered);
        Assert.Equal("Expensive", filtered[0].Title);
    }

    [Fact]
    public void EventStorage_SaveAndLoad_PersistsDerivedEventData()
    {
        var tempFile = Path.GetTempFileName();
        try
        {
            var sourceEvents = new EventBase[]
            {
                new ConcertEvent("Live", new DateTime(2026, 7, 20), 700, true),
                new ConferenceEvent("Business", new DateTime(2026, 9, 4), 900, 1)
            };

            EventStorage.Save(tempFile, sourceEvents);
            var loaded = EventStorage.Load(tempFile);

            Assert.Equal(2, loaded.Count);
            Assert.IsType<ConcertEvent>(loaded[0]);
            Assert.IsType<ConferenceEvent>(loaded[1]);
            Assert.Equal(1050, loaded[0].CalculateFinalPrice());
            Assert.Equal(1050, loaded[1].CalculateFinalPrice());
        }
        finally
        {
            File.Delete(tempFile);
        }
    }
}
