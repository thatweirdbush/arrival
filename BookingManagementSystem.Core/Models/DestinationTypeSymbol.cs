using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingManagementSystem.Core.Models;

public enum DestinationType
{
    All,
    AmazingPools,
    AmazingViews,
    Beach,
    Countryside,
    Farm,
    City,
    Islands,
    Lakefront,
    Luxe,
    Mansions,
    Room,
    NationalParks,
    TinyHomes,
    Treehouses,
    TopCities,
    Trending,
    Tropical
}

public class DestinationTypeSymbol
{
    // Define XAML Image's Path Declaration
    public static string MS_APPX = "ms-appx:///Assets/destination-type-symbol-icons/";
    private string _imagePath;

    public string Name
    {
        get; set;
    }

    public DestinationType DestinationType
    {
        get; set;
    }

    public string ImagePath
    {
        get => $"{MS_APPX}{_imagePath}";
        set => _imagePath = value;
    }
}
