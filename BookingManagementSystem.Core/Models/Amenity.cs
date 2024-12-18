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
    Household,
    RoomEssentials,
    Other
}

public partial class Amenity
{
    public int Id { get; set; }

    public string Name { get; set; }

    public AmenityType Type { get; set; } = AmenityType.Other;

    public string Description { get; set; }

    public int Quantity { get; set; } = 1;

    private string _imagePath;

    public string ImagePath
    {
        get => $"{MS_APPX}{_imagePath ?? DEFAULT_IMAGE}";
        set => _imagePath = value;
    }
    public virtual ICollection<PropertyAmenity> PropertyAmenities { get; set; } = [];

    // XAML Image's Path Declaration
    public const string MS_APPX = "ms-appx:///Assets/amenity-icons/";
    public const string DEFAULT_IMAGE = "cube.svg";
}
