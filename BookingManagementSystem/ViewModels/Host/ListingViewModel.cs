using BookingManagementSystem.Core.Contracts.Repositories;
using BookingManagementSystem.Core.Models;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using BookingManagementSystem.Contracts.ViewModels;
using Microsoft.UI.Dispatching;

namespace BookingManagementSystem.ViewModels.Host;

public partial class ListingViewModel : ObservableRecipient, INavigationAware
{
    private readonly IRepository<Property> _propertyRepository;

    [ObservableProperty]
    private bool isPropertyListEmpty;

    [ObservableProperty]
    private bool isLoading;

    // Other properties
    public int PropertyCountTotal;
    private int _currentPage = 1;
    private const int PageSize = 5; // Default page size

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
        await LoadPropertyListAsync();

        // Load Property Name and Location string data list
        PropertyNameAndLocationList = Properties.Select(p => p.Name)
                                                .Concat(Properties.Select(p => p.Location))
                                                .ToList();
        // Initial check
        CheckPropertyListCount();
        Properties.CollectionChanged += (s, e) => CheckPropertyListCount();
    }

    public void OnNavigatedFrom()
    {
    }

    public async Task LoadPropertyListAsync()
    {
        // Avoid calling multiple times at the same time
        if (IsLoading) return;

        try
        {
            IsLoading = true;
            var allProperties = await _propertyRepository.GetAllAsync();
            var paginatedProperties = allProperties
                .OrderByDescending(p => p.CreatedAt) // Sort by CreatedAt time
                .Skip((_currentPage - 1) * PageSize) // Skip those already loaded
                .Take(PageSize); // Get next items

            // Add the new items to the list
            DispatcherQueue.GetForCurrentThread().TryEnqueue(() =>
            {
                foreach (var property in paginatedProperties)
                {
                    Properties.Add(property);
                }
            });

            _currentPage++; // Increment the current page
        }
        finally
        {
            IsLoading = false;
        }
    }

    public async Task RemoveBookingAsync(Property property)
    {
        await _propertyRepository.DeleteAsync(property.Id);
        await _propertyRepository.SaveChangesAsync();
        Properties.Remove(property);
    }

    public async Task RemoveAllBookingsAsync()
    {
        foreach (var property in Properties)
        {
            await _propertyRepository.DeleteAsync(property.Id);
        }
        await _propertyRepository.SaveChangesAsync();
        Properties.Clear();
    }

    private void CheckPropertyListCount()
    {
        IsPropertyListEmpty = Properties.Count == 0;
        PropertyCountTotal = Properties.Count;
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
