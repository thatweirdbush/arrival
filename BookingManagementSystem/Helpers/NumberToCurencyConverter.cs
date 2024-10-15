using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Data;

namespace BookingManagementSystem.Helpers;

public class NumberToCurencyConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        var number = (int)value;
        CultureInfo culture = new("en-US");
        return number.ToString("C", culture);
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
