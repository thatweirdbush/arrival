using BookingManagementSystem.Core.Models;

using System.Globalization;
using Microsoft.UI.Xaml.Data;

namespace BookingManagementSystem.Helpers;

public class NumberToCurencyConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is decimal number)
        {
            // Check parameter to determine whether to display currency symbol or not
            var showCurrencySymbol = parameter == null || parameter.ToString() == "True";
            CultureInfo culture = new("en-US");

            // Create NumberFormatInfo to customize display
            var formatInfo = (NumberFormatInfo)culture.NumberFormat.Clone();
            if (!showCurrencySymbol)
            {
                // Remove currency symbol
                formatInfo.CurrencySymbol = string.Empty;
            }
            return number.ToString("C", formatInfo).Trim(); // Use Trim to remove excess whitespace
        }
        return value;
    }   

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        if (value is string str)
        {
            if (string.IsNullOrWhiteSpace(str) || !str.Any(char.IsDigit))
            {
                return Property.DEFAULT_PROPERTY_PRICE;
            }
            str = str.Trim();
            str = RemoveNonNumericCharacters(str);
            CultureInfo culture = new("en-US");

            if (decimal.TryParse(str, NumberStyles.Currency, culture, out var result))
            {
                return result;
            }
        }
        return Property.DEFAULT_PROPERTY_PRICE;
    }

    private string RemoveNonNumericCharacters(string input)
    {
        var allowedChars = "0123456789,.";

        // Delete non-numeric characters
        return string.Concat(input.Where(c => allowedChars.Contains(c)));
    }
}
