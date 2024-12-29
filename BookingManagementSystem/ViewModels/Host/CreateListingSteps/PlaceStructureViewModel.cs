using BookingManagementSystem.Contracts.ViewModels;
using BookingManagementSystem.Core.Contracts.Services;
using BookingManagementSystem.Core.Models;
using BookingManagementSystem.Core.Repositories;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BookingManagementSystem.ViewModels.Host.CreateListingSteps;

public partial class PlaceStructureViewModel : BaseStepViewModel
{
    private readonly PropertyTypeIconRepository _propertyTypeIconRepository;
    private readonly IPropertyService _propertyService;

    [ObservableProperty]
    private PropertyTypeIcon? selectedTypeIcon;

    // List of content items
    public IEnumerable<PropertyTypeIcon> PropertyTypeIcons { get; set; } = [];
    public Property PropertyOnCreating => _propertyService.PropertyOnCreating!;

    public PlaceStructureViewModel(IPropertyService propertyService, PropertyTypeIconRepository propertyTypeIconRepository)
    {
        _propertyService = propertyService;
        _propertyTypeIconRepository = propertyTypeIconRepository;

        // Load icon data list
        LoadPropertyTypeIcons();

        // Initialize core properties
        SelectedTypeIcon = PropertyTypeIcons.FirstOrDefault(x => x.PropertyType == PropertyOnCreating.Type);
    }

    partial void OnSelectedTypeIconChanged(PropertyTypeIcon? value)
    {
        ValidateProcess();
    }

    public async void LoadPropertyTypeIcons()
    {
        PropertyTypeIcons = await _propertyTypeIconRepository.GetAllAsync();
    }

    public override void ValidateProcess()
    {
        IsStepCompleted = SelectedTypeIcon != null;
    }

    public override void SaveProcess()
    {
        PropertyOnCreating.Type = SelectedTypeIcon?.PropertyType;
    }
}
