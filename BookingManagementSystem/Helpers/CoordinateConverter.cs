using Microsoft.UI.Xaml.Data;

namespace BookingManagementSystem.Helpers;
public class CoordinateConverter : IValueConverter
{
    public object? Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is double coordinate)
        {
            // Check if view is latitude or longitude (parameter is "latitude" or "longitude")
            var isLatitude = parameter != null && parameter.ToString() == "latitude";
            return ConvertToDms(coordinate, isLatitude);
        }
        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }

    private string ConvertToDms(double degree, bool isLatitude)
    {
        var d = (int)degree;  // Degree
        var m = (int)((Math.Abs(degree) - Math.Abs(d)) * 60);  // Minute
        var s = (Math.Abs(degree) - Math.Abs(d) - m / 60.0) * 3600;  // Second

        // Round seconds to 1 decimal place
        s = Math.Round(s, 1);

        // Check seconds again if over 60
        if (s >= 60)
        {
            m += (int)(s / 60);  // Add to minute
            s %= 60;          // Update second
        }

        string direction;
        if (isLatitude)
        {
            direction = degree >= 0 ? "N" : "S";  // Latitude: North (N) or South (S)
        }
        else
        {
            direction = degree >= 0 ? "E" : "W";  // Longitude: East (E) or West (W)
        }

        // Return result in DMS format
        return $"{Math.Abs(d)}°{m}'{s}\"{direction}";
    }
}
