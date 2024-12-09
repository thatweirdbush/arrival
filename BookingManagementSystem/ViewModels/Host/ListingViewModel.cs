using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.Core.Contracts.Repositories;
using BookingManagementSystem.Core.DTOs;
using BookingManagementSystem.Core.Models;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Windows.ApplicationModel.Contacts;
using BookingManagementSystem.ViewModels.Client;
using CommunityToolkit.Mvvm.Input;
using BookingManagementSystem.Contracts.ViewModels;

namespace BookingManagementSystem.ViewModels.Host;

public partial class ListingViewModel : ObservableRecipient, INavigationAware
{
    private readonly IRepository<Property> _propertyRepository;

    [ObservableProperty]
    private bool isPropertyListEmpty;

    public int PropertyCountTotal;

    // List of content items
    public ObservableCollection<Property> Properties { get; set; } = [];

    // List of Property's Name & Location
    public List<string> PropertyNameAndLocationList { get; set; } = [];

    public ListingViewModel(IRepository<Property> propertyRepository)
    {
        _propertyRepository = propertyRepository;
    }

    public async void OnNavigatedTo(object parameter)
    {
        // Load Listing data list
        await LoadPropertyList();

        // Load Property Name and Location string data list
        PropertyNameAndLocationList = Properties.Select(p => p.Name)
                                                .Concat(Properties.Select(p => p.Location))
                                                .ToList();
        // Initial check
        PropertyCountTotal = Properties.Count;
        CheckPropertyListCount();
        Properties.CollectionChanged += (s, e) => CheckPropertyListCount();
    }

    public void OnNavigatedFrom()
    {
    }

    public async Task LoadPropertyList()
    {
        // Load Property data list filtered by User/Host Id
        var properties = await _propertyRepository.GetAllAsync();

        // Reorder the list by Created Date
        properties = properties.OrderByDescending(p => p.CreatedAt);
        Properties.Clear();

        foreach (var item in properties)
        {
            Properties.Add(item);
        }
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
            await _propertyRepository.SaveChangesAsync();
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
