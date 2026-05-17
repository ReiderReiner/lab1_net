using System.Globalization;
using System.Windows.Data;
using Core;

namespace WpfEventsUI.Converters;

public sealed class EventTypeConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value switch
        {
            ConcertEvent => "Концерт",
            ConferenceEvent => "Конференція",
            _ => ""
        };
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
