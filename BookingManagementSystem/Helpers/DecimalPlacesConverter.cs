using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Data;

namespace BookingManagementSystem.Helpers;
public class DecimalPlacesConverter : IValueConverter
{
    public int DecimalPlaces { get; set; } = 2; // Default is 2 decimal places

    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is double doubleValue)
        {
            // Reduce double value to x decimal places
            return doubleValue.ToString($"F{DecimalPlaces}");
        }
        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
