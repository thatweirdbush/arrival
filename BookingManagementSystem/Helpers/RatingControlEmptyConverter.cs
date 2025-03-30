using Microsoft.UI.Xaml.Data;

namespace BookingManagementSystem.Helpers;
public class RatingControlEmptyConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is double doubleValue)
        {
            return doubleValue == 0.0 ? -1 : doubleValue;
        }
        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
