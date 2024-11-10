using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Markup;
using Microsoft.UI.Xaml.Media;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BookingManagementSystem.Helpers;
public class StringToGeometryConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is string pathData && !string.IsNullOrWhiteSpace(pathData))
        {
            return (Geometry)XamlBindingHelper.ConvertValue(typeof(Geometry), pathData);
        }
        return Geometry.Empty; // Or null
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
