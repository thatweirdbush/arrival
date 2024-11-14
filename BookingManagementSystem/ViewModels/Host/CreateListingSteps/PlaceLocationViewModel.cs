using BookingManagementSystem.Contracts.ViewModels;
using BookingManagementSystem.Core.Contracts.Services;
using BookingManagementSystem.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BookingManagementSystem.ViewModels.Host.CreateListingSteps;

public partial class PlaceLocationViewModel : BaseStepViewModel
{
    private readonly IPropertyService _propertyService;
    public Property PropertyOnCreating => _propertyService.PropertyOnCreating;
    public PlaceLocationViewModel(IPropertyService propertyService)
    {
        _propertyService = propertyService;
    }

    public void SetPropertyCoordinates(double latitude, double longitude)
    {
        PropertyOnCreating.Latitude = latitude;
        PropertyOnCreating.Longitude = longitude;
    }

    public override void ValidateStep() 
        => IsStepCompleted = PropertyOnCreating.Latitude != 0 
        && PropertyOnCreating.Longitude != 0
        && !string.IsNullOrEmpty(PropertyOnCreating.Location);
}
