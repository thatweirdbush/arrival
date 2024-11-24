using BookingManagementSystem.Contracts.ViewModels;
using BookingManagementSystem.Core.Contracts.Repositories;
using BookingManagementSystem.Core.Contracts.Services;
using BookingManagementSystem.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BookingManagementSystem.ViewModels.Host.CreateListingSteps;

public partial class PlaceStructureViewModel : BaseStepViewModel
{
    private readonly IRepository<PropertyTypeIcon> _propertyTypeIconRepository;
    private readonly IPropertyService _propertyService;

    // List of content items
    public IEnumerable<PropertyTypeIcon> PropertyTypeIcons { get; set; } = [];

    [ObservableProperty]
    private PropertyTypeIcon? selectedTypeIcon;
    public Property PropertyOnCreating => _propertyService.PropertyOnCreating;

    public PlaceStructureViewModel(IPropertyService propertyService, IRepository<PropertyTypeIcon> propertyTypeIconRepository)
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
