using Microsoft.UI.Xaml.Data;

namespace BookingManagementSystem.Helpers;

public class DateToShortDateFormatConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        var format = parameter as string ?? "MMM dd";
        var localTimeZone = TimeZoneInfo.Local;
        return value switch
        {
            DateTime date => date.ToString(format),
            DateTimeOffset dateTimeOffset => TimeZoneInfo.ConvertTime(dateTimeOffset, localTimeZone).ToString(format),
            _ => string.Empty
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
