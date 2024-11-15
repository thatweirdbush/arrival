using BookingManagementSystem.Contracts.ViewModels;
using BookingManagementSystem.Core.Contracts.Services;
using BookingManagementSystem.Core.Models;

namespace BookingManagementSystem.ViewModels.Host.CreateListingSteps;

public partial class PlaceDescriptionViewModel : BaseStepViewModel
{
    private readonly IPropertyService _propertyService;
    public Property PropertyOnCreating => _propertyService.PropertyOnCreating;
    public string SelectedTitle
    {
        get; set;
    } = Property.DEFAULT_PROPERTY_NAME;
    public string SelectedDescription
    {
        get; set;
    } = string.Empty;

    public PlaceDescriptionViewModel(IPropertyService propertyService)
    {
        _propertyService = propertyService;
    }

    public override void ValidateStep()
    {
        // Add selected title and description to the property
        PropertyOnCreating.Name = SelectedTitle;
        PropertyOnCreating.Description = SelectedDescription;

        if (!string.IsNullOrEmpty(PropertyOnCreating.Name) &&
            !string.IsNullOrEmpty(PropertyOnCreating.Description) &&
            !PropertyOnCreating.Name.Equals(Property.DEFAULT_PROPERTY_NAME) &&
            PropertyOnCreating.Name.Length <= Property.PROPERTY_NAME_MAX_LENGTH &&
            PropertyOnCreating.Description.Length <= Property.PROPERTY_DESCRIPTION_MAX_LENGTH)
        {
            IsStepCompleted = true;
        }
    }
}
