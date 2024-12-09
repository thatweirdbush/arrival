using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using BookingManagementSystem.Core.Services;

namespace BookingManagementSystem.Core.Models;

// These types best describe your place
public enum PropertyType
{
    House,
    Apartment,
    Barn,
    BedAndBreakfast,
    Boat,
    Cabin,
    CamperRV,
    CasaParticular,
    Castle,
    Cave,
    Container,
    CycladicHome,
    Dammuso,
    Dome,
    Farm,
    Guesthouse,
    Hotel,
    Houseboat,
    Kezhan,
    Minsu,
    Riad,
    Ryokan,
    Tent,
    TinyHome,
    Tower,
    Treehouse,
    Trullo,
    Windmill,
    Yurt
}

public class PropertyTypeIcon
{
    // Define XAML Image's Path Declaration
    public static string MS_APPX = "ms-appx:///Assets/real-estate-type-icons/";
    private string _imagePath;
    public PropertyType PropertyType
    {
        get; set;
    }
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

public enum PropertyRatingStatus
{
    NotRated, // Default
    Poor, // 0.1-1 star
    Fair, // 1.1-2 stars
    Good, // 2.1-3 stars
    VeryGood, // 3.1-4 stars
    Excellent // 4.1-5 stars
}

public enum PropertyStatus
{
    Listed,
    Unlisted,
    InProgress,
}

public class Property : INotifyPropertyChanged
{
    // Define constants
    public const string DEFAULT_PROPERTY_NAME = "Your House listing";
    public const string DEFAULT_PROPERTY_LOCATION = "Your House location";
    public const string DEFAULT_PROPERTY_DESCRIPTION= "Your House description";
    public const decimal DEFAULT_PROPERTY_PRICE = 0.0M;
    public const decimal DEFAULT_GUEST_SERVICE_FEE_RATE = 0.14m;
    public const decimal DEFAULT_HOST_SERVICE_FEE_RATE = 0.03m;

    public const int PROPERTY_NAME_MAX_LENGTH = 32;
    public const int PROPERTY_LOCATION_MAX_LENGTH = 250;
    public const int PROPERTY_DESCRIPTION_MAX_LENGTH = 500;

    public event PropertyChangedEventHandler PropertyChanged;
    public override string ToString() => $"{Name} ({Location}), {Description}, " +
        $"Price: {PricePerNight:C}, Is Available: {IsAvailable}, " +
        $"Created At: {CreatedAt}, Updated At: {UpdatedAt}";
    public int Id
    {
        get; set;
    }
    public string Name
    {
        get; set;
    } = DEFAULT_PROPERTY_NAME;
    public PropertyType? Type
    {
        get; set;
    }
    public string Description
    {
        get; set;
    } = DEFAULT_PROPERTY_DESCRIPTION;
    public int HostId
    {
        get; set;
    } // Each property belongs to a host
    public ICollection<DestinationType> DestinationTypes
    {
        get; set;
    } = [];

    public ICollection<string> ImagePaths
    {
        get; set;
    } = [];

    public string ImageThumbnail => ImagePaths.FirstOrDefault() ?? "ms-appx:///Assets/modern-european-house.png";

    public string Location
    {
        get
        {
            if (Country == null || string.IsNullOrEmpty(StateOrProvince))
            {
                return DEFAULT_PROPERTY_LOCATION;
            }
            return $"{StateOrProvince}, {Country.CountryName}";
        }
    }
    public string FullLocation
    {
        get
        {
            if (Country == null || string.IsNullOrEmpty(StreetAddress) ||
                string.IsNullOrEmpty(CityOrDistrict) || string.IsNullOrEmpty(StateOrProvince))
            {
                return DEFAULT_PROPERTY_LOCATION;
            }
            return $"{StreetAddress}, {CityOrDistrict}, {StateOrProvince}, {Country.CountryName}";
        }
    }
    public virtual CountryInfo Country
    {
        get; set;
    }
    public string StateOrProvince
    {
        get; set;
    }
    public string CityOrDistrict
    {
        get; set;
    }
    public string StreetAddress
    {
        get; set;
    }
    public string PostalCode
    {
        get; set;
    }
    public decimal PricePerNight
    {
        get; set;
    } = DEFAULT_PROPERTY_PRICE;
    public bool IsFavourite
    {
        get; set;
    } = false;
    public virtual ICollection<Amenity> Amenities
    {
        get; set;
    } = []; // E.g., WiFi, Pool, Parking
    public virtual ICollection<PropertyPolicy> Policies
    {
        get; set;
    } = []; // E.g., No smoking, No pets, No parties
    public bool IsAvailable
    {
        get; set;
    } = true;
    public PropertyStatus Status
    {
        get; set;
    } = PropertyStatus.Listed;
    public DateTime CreatedAt
    {
        get; set;
    } = DateTime.Now.ToUniversalTime();
    public DateTime? UpdatedAt
    {
        get; set;
    }
    public double Latitude
    {
        get; set;
    } = 0.0;
    public double Longitude
    {
        get; set;
    } = 0.0;
    public bool IsPriority
    {
        get; set;
    } = false;
    public bool IsRequested
    {
        get; set;
    } = false;
    public bool IsPetFriendly
    {
        get; set;
    } = false;
    public int MaxGuests
    {
        get; set;
    } = 1;
    public int LastEditedStep
    {
        get; set;
    } = -1;
}
