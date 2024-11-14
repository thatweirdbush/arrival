using System.Collections.ObjectModel;
using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.Core.Contracts.Repositories;
using BookingManagementSystem.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BookingManagementSystem.ViewModels.Host.CreateListingSteps;

public partial class AmenitiesViewModel : ObservableRecipient
{
    private readonly IRepository<Amenity> _amenityRepository;
    public Property? PropertyOnCreating = null;

    // List of content items
    public IEnumerable<Amenity> GuestFavoriteAmenities { get; set; } = Enumerable.Empty<Amenity>();
    public IEnumerable<Amenity> StandoutAmenities { get; set; } = Enumerable.Empty<Amenity>();
    public IEnumerable<Amenity> SafetyAmenities { get; set; } = Enumerable.Empty<Amenity>();

    public AmenitiesViewModel(IRepository<Amenity> amenityRepository)
    {
        _amenityRepository = amenityRepository;
        LoadAmenities();
    }

    public async void LoadAmenities()
    {
        // Load content data list
        var data = await _amenityRepository.GetAllAsync();
        GuestFavoriteAmenities = data.Where(x => x.Type == AmenityType.GuestFavorite);
        StandoutAmenities = data.Where(x => x.Type == AmenityType.Standout);
        SafetyAmenities = data.Where(x => x.Type == AmenityType.Safety);
    }
}
