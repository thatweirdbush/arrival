using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingManagementSystem.Core.Models;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;

namespace BookingManagementSystem.Helpers;
public class PropertyStatusToSeverityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is PropertyStatus status)
        {
            return status switch
            {
                PropertyStatus.Listed => InfoBarSeverity.Success,
                PropertyStatus.Unlisted => InfoBarSeverity.Informational,
                PropertyStatus.InProgress => InfoBarSeverity.Warning,
                _ => InfoBarSeverity.Error
            };
        }
        return InfoBarSeverity.Error;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}

