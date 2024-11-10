using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BookingManagementSystem.Core.Models;

public enum AmenityType
{
    GuestFavorite,
    Standout,
    Safety,
    Other
}

public class Amenity : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    // Define XAML Image's Path Declaration
    public static string MS_APPX = "ms-appx:///Assets/amenity-icons/";
    private string _imagePath;
    public override string ToString() => $"{Name} - {Description}";
    public int Id
    {
        get; set;
    }
    public string Name
    {
        get; set;
    }
    public AmenityType Type
    {
        get; set;
    } = AmenityType.Other;
    public string Description
    {
        get; set;
    }
    public string ImagePath
    {
        get
        {   // Default image path is "default-icon.svg"
            if (string.IsNullOrEmpty(_imagePath))
            {
                return $"{MS_APPX}cube.svg";
            }
            return $"{MS_APPX}{_imagePath}";
        }
        set => _imagePath = value;
    }
}
