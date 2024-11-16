using BookingManagementSystem.Contracts.ViewModels;
using BookingManagementSystem.Core.Contracts.Services;
using BookingManagementSystem.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BookingManagementSystem.ViewModels.Host.CreateListingSteps;

public partial class PlaceLocationViewModel : BaseStepViewModel
{
    private readonly IPropertyService _propertyService;
    public Property PropertyOnCreating => _propertyService.PropertyOnCreating;
    public double CurrentLatitude { get; set; } = 0.0;
    public double CurrentLongitude { get; set; } = 0.0;
    public string CurrentLocation { get; set; } = Property.DEFAULT_PROPERTY_LOCATION;
    public PlaceLocationViewModel(IPropertyService propertyService)
    {
        _propertyService = propertyService;
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
