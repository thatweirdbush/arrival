using Microsoft.UI.Xaml.Data;
using BookingManagementSystem.Core.Models;

namespace BookingManagementSystem.Helpers;

public class RatingToStatusConverter : IValueConverter
{
    public RatingToStatusConverter()
    {
    }

    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is double rating)
        {
            if (rating > 0 && rating <= 1)
            {
                return PropertyRatingStatus.Poor.ToString();
            }
            else if (rating > 1 && rating <= 2)
            {
                return PropertyRatingStatus.Fair.ToString();
            }
            else if (rating > 2 && rating <= 3)
            {
                return PropertyRatingStatus.Good.ToString();
            }
            else if (rating > 3 && rating <= 4)
            {
                return "Very Good";
            }
            else if (rating > 4 && rating <= 5)
            {
                return PropertyRatingStatus.Excellent.ToString();
            }
            else
            {
                return "Not Rated";
            }
        }
        return "Not Rated"; // Ensure a return value for non-double inputs
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
