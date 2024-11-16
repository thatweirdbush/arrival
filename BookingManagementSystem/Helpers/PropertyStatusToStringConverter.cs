using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingManagementSystem.Core.Models;
using Microsoft.UI.Xaml.Data;

namespace BookingManagementSystem.Helpers;
public class PropertyStatusToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is PropertyStatus status)
        {
            return status switch
            {
                PropertyStatus.Listed => "Listed",
                PropertyStatus.Unlisted => "Unlisted",
                PropertyStatus.InProgress => "In Progress",
                _ => "Unknown"
            };
        }
        return "Invalid";
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}

