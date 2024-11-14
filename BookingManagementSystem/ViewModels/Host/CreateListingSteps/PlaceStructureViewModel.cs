using System.Collections.ObjectModel;
using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.Core.Contracts.Repositories;
using BookingManagementSystem.Core.DTOs;
using BookingManagementSystem.Core.Models;
using BookingManagementSystem.Core.Repositories;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BookingManagementSystem.ViewModels.Host.CreateListingSteps;

public partial class PlaceStructureViewModel : ObservableRecipient
{
    private readonly INavigationService _navigationService;
    private readonly IRepository<PropertyTypeIcon> _propertyTypeIconRepository;
    public Property? PropertyOnCreating = null;

    // List of content items
    public IEnumerable<PropertyTypeIcon> PropertyTypeIcons { get; set; } = Enumerable.Empty<PropertyTypeIcon>();
    public PropertyType SelectedType
    {
        get; set;
    }
    public PlaceStructureViewModel(INavigationService navigationService, IRepository<PropertyTypeIcon> propertyTypeIconRepository)
    {
        _navigationService = navigationService;
        _propertyTypeIconRepository = propertyTypeIconRepository;

        OnNavigatedTo(0);
    }

    public async void OnNavigatedTo(object parameter)
    {
        // Load icon data list
        PropertyTypeIcons = await _propertyTypeIconRepository.GetAllAsync();
    }

    public void OnNavigatedFrom()
    {
    }
}
