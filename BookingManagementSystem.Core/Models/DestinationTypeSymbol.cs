using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingManagementSystem.Core.Models;
public class DestinationTypeSymbol
{
    // Define XAML Image's Path Declaration
    public static string MS_APPX = "ms-appx:///Assets/destination-type-symbol-icons/";
    private string _imagePath;

    public string Name
    {
        get; set;
    }
    public string ImagePath
    {
        get => $"{MS_APPX}{_imagePath}";
        set => _imagePath = value;
    }
}
