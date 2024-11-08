using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BookingManagementSystem.Core.Models;

// These types best describe your place
public enum PropertyType
{
    Apartment,
    House,
    Villa,
    Cabin,
    Cottage,
    Bungalow,
    Chalet,
    Lodge,
    Tent,
    Yurt,
    Treehouse,
    Boat,
    Plane,
    Train,
    Bus,
    Camper,
    RV,
    Castle,
    Dorm,
    Hostel,
    Hotel,
    Resort,
    Motel,
    Inn,
    BedAndBreakfast,
    Guesthouse,
    Ryokan,
    Pousada,
    Riad,
    Aparthotel,
    Condo,
    Townhouse,
    Farmhouse,
    Houseboat,
    Lighthouse,
    Tipi,
    Cave,
    Island,
    EarthHouse,
    Hut,
    Dome,
    Igloo,
    Other
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
public class Property : INotifyPropertyChanged
{
    // Define XAML Image's Path Declaration
    public static string MS_APPX = "ms-appx:///Assets/property-images/";

    // Private field for ImagePath
    private ICollection<string> _imagePaths;

    public event PropertyChangedEventHandler PropertyChanged;
    public override string ToString() => $"{Name} ({Location}), {Description}, " +
        $"Capacity: {Capacity}, Price: {PricePerNight:C}, Is Available: {IsAvailable}, " +
        $"Created At: {CreatedAt}, Updated At: {UpdatedAt}";
    public int Id
    {
        get; set;
    }
    public string Name
    {
        get; set;
    }
    public PropertyType Type
    {
        get; set;
    }
    public string Description
    {
        get; set;
    }

    public ICollection<DestinationType> DestinationTypes
    {
        get; set;
    } = [];

    public ICollection<string> ImagePaths
    {
        // Concatenate MS_APPX with each image path
        get => _imagePaths.Select(ip => $"{MS_APPX}{ip}").ToList();
        set => _imagePaths = value;
    }

    public string ImageThumbnail => ImagePaths.FirstOrDefault() ?? $"{MS_APPX}default-thumbnail.jpg";

    public string Location
    {
        get; set;
    }
    public int Capacity
    {
        get; set;
    }
    public decimal PricePerNight
    {
        get; set;
    }
    public bool IsFavourite
    {
        get; set;
    } = false;
    public ICollection<Amenity> Amenities
    {
        get; set;
    } // E.g., WiFi, Pool, Parking
    public ICollection<PropertyPolicy> Policies
    {
        get; set;
    } // E.g., No smoking, No pets, No parties

    public bool IsAvailable
    {
        get; set;
    } = true;
    public DateTime CreatedAt
    {
        get; set;
    } = DateTime.Now;
    public DateTime? UpdatedAt
    {
        get; set;
    }
    // Add properties for geographic coordinates using GeoPoint
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
}
