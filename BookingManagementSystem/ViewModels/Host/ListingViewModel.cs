using BookingManagementSystem.Core.Contracts.Repositories;
using BookingManagementSystem.Core.Models;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using BookingManagementSystem.Contracts.ViewModels;
using Microsoft.UI.Dispatching;
using BookingManagementSystem.Core.Commons.Enums;

namespace BookingManagementSystem.ViewModels.Host;

public partial class ListingViewModel : ObservableRecipient, INavigationAware
{
    private readonly IRepository<Property> _propertyRepository;

    // List of content items
    public ObservableCollection<Property> Properties { get; set; } = [];
    public List<Property> CachedProperties { get; set; } = [];

    // List of Property's Name & Location for searching
    public List<string> PropertyNameAndLocationList { get; set; } = [];

    [ObservableProperty]
    private bool isPropertyListEmpty;

    [ObservableProperty]
    private bool isLoading;

    [ObservableProperty]
    private LoadingState currentLoadingState;

    // Other properties
    public int PropertyCountTotal;
    private int _currentPage = 1;
    public int CurrentPage => _currentPage;
    private const int PageSize = 5; // Default page size

    public ListingViewModel(IRepository<Property> propertyRepository)
    {
        _propertyRepository = propertyRepository;
    }

    public async void OnNavigatedTo(object parameter)
    {
        await InitializeCacheAsync();
        await LoadPropertyListAsync();

        // Initial check
        CheckPropertyListCount();
        Properties.CollectionChanged += (s, e) => CheckPropertyListCount();

        // Set the default loading state
        CurrentLoadingState = LoadingState.Default;
    }

    public void OnNavigatedFrom()
    {
        Properties.CollectionChanged -= (s, e) => CheckPropertyListCount();
    }

    public async Task InitializeCacheAsync()
    {
        // Load all properties from the database
        CachedProperties = (List<Property>)await _propertyRepository.GetAllAsync();

        // Load Property Name and Location string data list
        PropertyNameAndLocationList = CachedProperties.Select(p => p.Name)
                                                        .Concat(CachedProperties.Select(p => p.Location))
                                                        .ToList();
    }

    public async Task LoadPropertyListAsync()
    {
        // Avoid calling multiple times at the same time
        if (IsLoading) return;

        try
        {
            IsLoading = true;
            var paginatedProperties = CachedProperties
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
            await Task.CompletedTask;
        }
    }

    public async Task RemoveBookingAsync(Property property)
    {
        await _propertyRepository.DeleteAsync(property.Id);
        await _propertyRepository.SaveChangesAsync();

        // Update cache and UI
        CachedProperties.Remove(property);
        Properties.Remove(property);
    }

    public async Task RemoveAllBookingsAsync()
    {
        foreach (var property in Properties)
        {
            await _propertyRepository.DeleteAsync(property.Id);
        }
        await _propertyRepository.SaveChangesAsync();

        // Update cache and UI
        CachedProperties.Clear();
        Properties.Clear();
    }

    private void CheckPropertyListCount()
    {
        IsPropertyListEmpty = Properties.Count == 0;
        PropertyCountTotal = Properties.Count;
    }

    public async Task SearchPropertiesAsync(string query)
    {
        // Set the current loading state
        CurrentLoadingState = LoadingState.Search;

        // Reset current pagination index & clear the list
        ResetPaginationIndex();

        // Start loading the search data
        await LoadSearchedPropertiesAsync(query);
    }

    public async Task LoadSearchedPropertiesAsync(string query)
    {
        var filteredProperties = CachedProperties.Where(p => p.Name.Contains(query) || p.Location.Contains(query));
        foreach (var item in filteredProperties)
        {
            Properties.Add(item);
        }
        await Task.CompletedTask;
    }

    public int GetSingleSearchedProperty(string query)
    {
        var filteredProperties = CachedProperties.Where(p => p.Name.Contains(query) || p.Location.Contains(query));
        Properties.Clear();
        foreach (var item in filteredProperties)
        {
            Properties.Add(item);
        }
        return Properties.FirstOrDefault()!.Id;
    }

    public void ResetPaginationIndex()
    {
        _currentPage = 1;
    }

    public async Task RefreshPropertiesAsync()
    {
        CurrentLoadingState = LoadingState.Default;
        ResetPaginationIndex();
        await InitializeCacheAsync();
        Properties.Clear();
        await LoadPropertyListAsync();
    }
}
