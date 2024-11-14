using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingManagementSystem.Core.Models;
using Microsoft.UI.Xaml.Data;

namespace BookingManagementSystem.Helpers;
public class PropertyStatusToStyleConverter : IValueConverter
{
    public object? Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is PropertyStatus status)
        {
            var resourceKey = status switch
            {
                PropertyStatus.Listed => "SuccessDotInfoBadgeStyle",
                PropertyStatus.Unlisted => "InformationalDotInfoBadgeStyle",
                PropertyStatus.InProgress => "AttentionIconInfoBadgeStyle",
                _ => "DefaultDotInfoBadgeStyle"
            };

            // Return the ThemeResource using Application.Current.Resources lookup
            if (App.Current.Resources.TryGetValue(resourceKey, out var style))
            {
                return style;
            }
        }
        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
