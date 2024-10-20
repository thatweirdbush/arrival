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
    }
    public List<Amenity> Amenities
    {
        get; set;
    } // E.g., WiFi, Pool, Parking
    public List<PropertyPolicy> HouseRules
    {
        get; set;
    } // E.g., No smoking, No pets, No parties

    public bool IsAvailable
    {
        get; set;
    }
    public DateTime CreatedAt
    {
        get; set;
    }
    public DateTime UpdatedAt
    {
        get; set;
    }
    // Add properties for geographic coordinates using GeoPoint
    public double Latitude
    {
        get; set;
    }
    public double Longitude
    {
        get; set;
    }
    public bool IsPriority
    {
        get; set;
    } = false;
}
