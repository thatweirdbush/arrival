using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.Core.Contracts.Repositories;
using BookingManagementSystem.Core.DTOs;
using BookingManagementSystem.Core.Models;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BookingManagementSystem.ViewModels.Host;

public partial class ListingViewModel : ObservableRecipient
{
    private readonly INavigationService _navigationService;
    private readonly IRepository<Property> _propertyRepository;

    // List of content items
    public ObservableCollection<Property> Properties { get; set; } = [];

    [ObservableProperty]
    private bool isPropertyListEmpty;

    public ListingViewModel(INavigationService navigationService, IRepository<Property> propertyRepository)
    {
        _navigationService = navigationService;
        _propertyRepository = propertyRepository;

        // Subscribe to CollectionChanged event
        Properties.CollectionChanged += (s, e) => CheckPropertyListCount();

        // Initial check
        CheckPropertyListCount();

        OnNavigatedTo(0);
    }

    public async void OnNavigatedTo(object parameter)
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

    public void DeleteBookingAsync(Property property)
    {
        _propertyRepository.DeleteAsync(property.Id);
        Properties.Remove(property);
    }

    private void CheckPropertyListCount()
    {
        IsPropertyListEmpty= Properties.Count == 0;
    }
}
