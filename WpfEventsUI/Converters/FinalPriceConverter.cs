using System.Globalization;
using System.Windows.Data;
using Core;

namespace WpfEventsUI.Converters;

public sealed class FinalPriceConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is EventBase ev)
            return $"{ev.CalculateFinalPrice():N2} UAH";
        return "";
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
