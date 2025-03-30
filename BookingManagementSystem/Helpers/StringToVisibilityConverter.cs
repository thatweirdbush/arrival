﻿using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml;

namespace BookingManagementSystem.Helpers;

public class StringToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is string str && !string.IsNullOrWhiteSpace(str))
        {
            return Visibility.Visible;
        }
        return Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}

