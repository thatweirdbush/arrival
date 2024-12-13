using System;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;

namespace BookingManagementSystem.Helpers;
public class BoolToBrushConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is bool isUserMessage)
            {
            return new SolidColorBrush(isUserMessage ? Microsoft.UI.Colors.LightBlue : Microsoft.UI.Colors.LightGray);
        }
        return new SolidColorBrush(Microsoft.UI.Colors.Transparent);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
}
