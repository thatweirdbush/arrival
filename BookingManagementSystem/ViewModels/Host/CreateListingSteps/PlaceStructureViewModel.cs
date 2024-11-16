using BookingManagementSystem.Contracts.ViewModels;
using BookingManagementSystem.Core.Contracts.Repositories;
using BookingManagementSystem.Core.Contracts.Services;
using BookingManagementSystem.Core.Models;

namespace BookingManagementSystem.ViewModels.Host.CreateListingSteps;

public partial class PlaceStructureViewModel : BaseStepViewModel
{
    private readonly IRepository<PropertyTypeIcon> _propertyTypeIconRepository;
    private readonly IPropertyService _propertyService;

    // List of content items
    public IEnumerable<PropertyTypeIcon> PropertyTypeIcons { get; set; } = [];
    public PropertyType? SelectedType
    {
        get; set;
    }
    public Property PropertyOnCreating => _propertyService.PropertyOnCreating;

    public PlaceStructureViewModel(IPropertyService propertyService, IRepository<PropertyTypeIcon> propertyTypeIconRepository)
    {
        _propertyService = propertyService;
        _propertyTypeIconRepository = propertyTypeIconRepository;
        LoadPropertyTypeIcons();
    }

    public async void LoadPropertyTypeIcons()
    {
        // Load icon data list
        PropertyTypeIcons = await _propertyTypeIconRepository.GetAllAsync();
    }

    public override void ValidateStep()
    {
        if (SelectedType != null)
        {
            PropertyOnCreating.Type = SelectedType;
            IsStepCompleted = true;
        }
        else
        {
            IsStepCompleted = false;
        }
    }
}
