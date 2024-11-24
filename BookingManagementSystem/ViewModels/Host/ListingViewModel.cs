using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.Core.Contracts.Repositories;
using BookingManagementSystem.Core.DTOs;
using BookingManagementSystem.Core.Models;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Windows.ApplicationModel.Contacts;
using BookingManagementSystem.ViewModels.Client;
using CommunityToolkit.Mvvm.Input;

namespace BookingManagementSystem.ViewModels.Host;

public partial class ListingViewModel : ObservableRecipient
{
    private readonly INavigationService _navigationService;
    private readonly IRepository<Property> _propertyRepository;

    // List of content items
    public ObservableCollection<Property> Properties { get; set; } = [];

    [ObservableProperty]
    private bool isPropertyListEmpty;

    public int PropertyCountTotal;

    // List of Property's Name & Location
    public List<string> PropertyNameAndLocationList
    {
        get;
        set;
    } = [];

    public ListingViewModel(INavigationService navigationService, IRepository<Property> propertyRepository)
    {
        _navigationService = navigationService;
        _propertyRepository = propertyRepository;
        CheckPropertyListCount();

        // Subscribe to CollectionChanged event
        Properties.CollectionChanged += (s, e) => CheckPropertyListCount();

        // Initial check
        _ = LoadPropertyList();

        // Load Property Name and Location string data list
        PropertyNameAndLocationList = Properties.Select(p => p.Name)
                                                .Concat(Properties.Select(p => p.Location))
                                                .ToList();
        PropertyCountTotal = Properties.Count;
    }

    public async Task LoadPropertyList()
    {
        // Load Property data list filtered by User/Host Id
        var properties = await _propertyRepository.GetAllAsync();
        foreach (var item in properties)
        {
            Properties.Add(item);
        }
    }

    public void OnNavigatedFrom()
    {
    }

    public async Task RemoveBookingAsync(Property property)
    {
        await _propertyRepository.DeleteAsync(property.Id);
        Properties.Remove(property);
    }

    public async Task RemoveAllBookingsAsync()
    {
        foreach (var property in Properties)
        {
            await _propertyRepository.DeleteAsync(property.Id);
        }
        Properties.Clear();
    }

    private void CheckPropertyListCount()
    {
        IsPropertyListEmpty = Properties.Count == 0;
    }

    public async void AddFilterProperties(string query)
    {
        var data = await _propertyRepository.GetAllAsync();
        var filteredProperties = data.Where(p => p.Name.Contains(query) || p.Location.Contains(query)).ToList();
        foreach (var item in filteredProperties)
        {
            Properties.Add(item);
        }
    }
}
