using BookingManagementSystem.Contracts.ViewModels;
using BookingManagementSystem.Core.Contracts.Repositories;
using BookingManagementSystem.Core.Contracts.Services;
using BookingManagementSystem.Core.Models;

namespace BookingManagementSystem.ViewModels.Host.CreateListingSteps;

public partial class AmenitiesViewModel : BaseStepViewModel
{
    private readonly IRepository<Amenity> _amenityRepository;
    private readonly IPropertyService _propertyService;
    public Property PropertyOnCreating => _propertyService.PropertyOnCreating;

    // List of content items for UI
    public IEnumerable<Amenity> GuestFavoriteAmenities { get; set; } = [];
    public IEnumerable<Amenity> StandoutAmenities { get; set; } = [];
    public IEnumerable<Amenity> SafetyAmenities { get; set; } = [];

    // List of selected amenities by user
    public IEnumerable<Amenity> SelectedGuestFavoriteAmenities { get; set; } = [];
    public IEnumerable<Amenity> SelectedStandoutAmenities { get; set; } = [];
    public IEnumerable<Amenity> SelectedSafetyAmenities { get; set; } = [];

    public AmenitiesViewModel(IPropertyService propertyService, IRepository<Amenity> amenityRepository)
    {
        _propertyService = propertyService;
        _amenityRepository = amenityRepository;

        // Initialize UI amenities for selection
        _ = LoadUIAmenities();

        // Initialize core properties
        TryLoadSelectedAmenities();

        // User can skip this step too
        IsStepCompleted = true;
    }

    public async Task LoadUIAmenities()
    {
        // Load content data list
        var data = await _amenityRepository.GetAllAsync();

        GuestFavoriteAmenities = data.Where(x => x.Type == AmenityType.GuestFavorite);
        StandoutAmenities = data.Where(x => x.Type == AmenityType.Standout);
        SafetyAmenities = data.Where(x => x.Type == AmenityType.Safety);
    }

    public void TryLoadSelectedAmenities()
    {
        // Load selected amenities
        SelectedGuestFavoriteAmenities = PropertyOnCreating.PropertyAmenities
            .Where(x => x.Amenity.Type == AmenityType.GuestFavorite)
            .Select(x => x.Amenity);
        SelectedStandoutAmenities = PropertyOnCreating.PropertyAmenities
            .Where(x => x.Amenity.Type == AmenityType.Standout)
            .Select(x => x.Amenity);
        SelectedSafetyAmenities = PropertyOnCreating.PropertyAmenities
            .Where(x => x.Amenity.Type == AmenityType.Safety)
            .Select(x => x.Amenity);
    }

    public void ResetPropertyAmenities()
    {
        foreach (var amenity in PropertyOnCreating.PropertyAmenities
            .Where(x => x.Amenity.Type == AmenityType.GuestFavorite)
            .ToList())
        {
            PropertyOnCreating.PropertyAmenities.Remove(amenity);
        }
        foreach (var amenity in PropertyOnCreating.PropertyAmenities
            .Where(x => x.Amenity.Type == AmenityType.Standout)
            .ToList())
        {
            PropertyOnCreating.PropertyAmenities.Remove(amenity);
        }
        foreach (var amenity in PropertyOnCreating.PropertyAmenities
            .Where(x => x.Amenity.Type == AmenityType.Safety)
            .ToList())
        {
            PropertyOnCreating.PropertyAmenities.Remove(amenity);
        }
    }

    public override void SaveProcess()
    {
        // Reset all amenities first
        ResetPropertyAmenities();

        // Add selected amenities to the property
        foreach (var amenity in SelectedGuestFavoriteAmenities)
        {
            PropertyOnCreating.PropertyAmenities
                .Add(new PropertyAmenity { Property = PropertyOnCreating, Amenity = amenity });
        }
        foreach (var amenity in SelectedStandoutAmenities)
        {
            PropertyOnCreating.PropertyAmenities
                .Add(new PropertyAmenity { Property = PropertyOnCreating, Amenity = amenity });
        }
        foreach (var amenity in SelectedSafetyAmenities)
        {
            PropertyOnCreating.PropertyAmenities
                .Add(new PropertyAmenity { Property = PropertyOnCreating, Amenity = amenity });
        }
    }

    public override void ValidateProcess()
    {
    }
}
