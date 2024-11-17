using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Data;

namespace BookingManagementSystem.Helpers;
public class InvertedBoolConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is bool boolValue)
        {
            return !boolValue;
        }
        throw new ArgumentException("Value must be of type bool.");
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language) => Convert(value, targetType, parameter, language);
}
