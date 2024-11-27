using BookingManagementSystem.Contracts.ViewModels;
using BookingManagementSystem.Core.Contracts.Services;
using BookingManagementSystem.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BookingManagementSystem.ViewModels.Host.CreateListingSteps;

public partial class PlaceLocationViewModel : BaseStepViewModel
{
    private readonly IPropertyService _propertyService;
    public Property PropertyOnCreating => _propertyService.PropertyOnCreating;
    public double CurrentLatitude { get; set; }
    public double CurrentLongitude { get; set; }
    public string CurrentLocation { get; set; }
    public PlaceLocationViewModel(IPropertyService propertyService)
    {
        _propertyService = propertyService;

        // Initialize core properties
        CurrentLatitude = PropertyOnCreating.Latitude;
        CurrentLongitude = PropertyOnCreating.Longitude;
        CurrentLocation = PropertyOnCreating.Location;
    }

    public override void SaveProcess()
    {
        PropertyOnCreating.Latitude = CurrentLatitude;
        PropertyOnCreating.Longitude = CurrentLongitude;
        PropertyOnCreating.Location = CurrentLocation;
    }

    public override void ValidateProcess()
    {
        IsStepCompleted = (CurrentLatitude != 0.0
                        && CurrentLongitude != 0.0
                        && !string.IsNullOrWhiteSpace(CurrentLocation)
                        && CurrentLocation.Length <= Property.PROPERTY_LOCATION_MAX_LENGTH);
    }
}
