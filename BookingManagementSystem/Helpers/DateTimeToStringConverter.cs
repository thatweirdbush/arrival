using Microsoft.UI.Xaml.Data;

namespace BookingManagementSystem.Helpers;

public  class DateTimeToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is DateTime dateTime)
        {
            // Định dạng thời gian, ví dụ "HH:mm" cho 24 giờ hoặc "hh:mm tt" cho 12 giờ
            return dateTime.ToString("HH:mm");
        }
        return string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
