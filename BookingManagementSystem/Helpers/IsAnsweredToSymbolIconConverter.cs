using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;

namespace BookingManagementSystem.Helpers;
public class IsAnsweredToSymbolIconConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is bool isAnswered)
        {
            return isAnswered ? Symbol.Accept : Symbol.Cancel;
        }

        return Symbol.Cancel;

    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
