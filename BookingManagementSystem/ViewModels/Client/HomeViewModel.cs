using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.Contracts.ViewModels;
using BookingManagementSystem.Core.Models;
using CommunityToolkit.Mvvm.Input;
using BookingManagementSystem.Core.Contracts.Services;
using Microsoft.UI.Dispatching;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BookingManagementSystem.ViewModels.Client;

public partial class HomeViewModel : ObservableRecipient, INavigationAware
{
    private readonly INavigationService _navigationService;
    private readonly IRoomService _roomService;

    [ObservableProperty]
    private bool isPropertyListEmpty;

    [ObservableProperty]
    private bool isLoading;

    [ObservableProperty]
    private string? destination;

    [ObservableProperty]
    private int numberOfAdults;

    [ObservableProperty]
    private int numberOfChildren;

    [ObservableProperty]
    private int numberOfPets;
    public DateTimeOffset? CheckInDate
    {
        get; set;
    }
    public DateTimeOffset? CheckOutDate
    {
        get; set;
    }
    public DestinationType SelectedPresetFilter { get; set; } = DestinationType.All;

    private int _currentPage = 1;
    private const int PageSize = 10; // Default page size

    // Other data source state properties
    public bool IsDefaultLoading = true; // Default data
    public bool IsFilteredLoading = false; // Filter data
    public bool IsSearchLoading = false; // Search data

    // List of items for the AdaptiveGridView
    public ObservableCollection<Property> Properties { get; set; } = [];

    // Filtered destination data
    public IEnumerable<DestinationTypeSymbol> DestinationTypeSymbols { get; set; } = [];

    public IAsyncRelayCommand SearchAvailableRoomsCommand
    {
        get;
    }

    public HomeViewModel(INavigationService navigationService, IRoomService roomService)
    {
        _navigationService = navigationService;
        _roomService = roomService;

        // Async relay command for searching available rooms
        SearchAvailableRoomsCommand = new AsyncRelayCommand(SearchRoomsAsync);
    }

    public async void OnNavigatedTo(object parameter)
    {
        // Load DestinationTypeSymbols data
        DestinationTypeSymbols = await _roomService.GetAllDestinationTypeSymbolsAsync();

        // Load Properties data
        await LoadPropertyListAsync();

        // Initialize observable properties
        NumberOfAdults = 0;
        NumberOfChildren = 0;
        NumberOfPets = 0;

        // Subscribe to CollectionChanged event
        //Properties.CollectionChanged += (s, e) => CheckPropertyListCount();
    }

    public void OnNavigatedFrom()
    {
    }

    private void CheckPropertyListCount()
    {
        IsPropertyListEmpty = Properties.Count == 0;
    }

    [RelayCommand]
    private void OnItemClick(Property? clickedItem)
    {
        if (clickedItem != null)
        {
            _navigationService.SetListDataItemForNextConnectedAnimation(clickedItem);
            _navigationService.NavigateTo(typeof(RentalDetailViewModel).FullName!, clickedItem.Id);
        }
    }

    public async Task LoadPropertyListAsync()
    {
        // Avoid calling multiple times at the same time
        if (IsLoading) return;

        try
        {
            // Set loading state
            IsLoading = true;

            // Get all properties
            var allProperties = await _roomService.GetAllPropertiesAsync();

            // Filter the official listed properties
            var listedProperties = allProperties.Where(x => x.Status == PropertyStatus.Listed);

            // Get the next page of items
            var paginatedProperties = listedProperties
                .Skip((_currentPage - 1) * PageSize) // Skip those already loaded
                .Take(PageSize); // Get next items

            // Add the new items to the list
            DispatcherQueue.GetForCurrentThread().TryEnqueue(() =>
            {
                foreach (var property in paginatedProperties)
                {
                    Properties.Add(property);
                }
                CheckPropertyListCount();
            });

            _currentPage++; // Increment the current page
        }
        finally
        {
            IsLoading = false;
        }
    }

    public async Task FilterProperties()
    {
        // Set state to load only filter data
        IsFilteredLoading = true;
        IsDefaultLoading = false;
        IsSearchLoading = false;

        // Reset when selecting new filter
        _currentPage = 1;
        Properties.Clear();

        // Get filtered data
        await LoadPropertyListFromPresetFilterAsync();
        CheckPropertyListCount();
    }

    public async Task LoadPropertyListFromPresetFilterAsync()
    {
        // Avoid calling multiple times at the same time
        if (IsLoading) return;

        try
        {
            // Set loading state
            IsLoading = true;

            var results = await _roomService.GetAllPropertiesAsync();
            var listedProperties = results.Where(x => x.Status == PropertyStatus.Listed);

            // Filter the data based on the preset filter
            if (SelectedPresetFilter.Equals(DestinationType.All))
            {
                // Do nothing because the steps below already return the properties with DestinationType.All
            }
            else if (SelectedPresetFilter.Equals(DestinationType.Trending))
            {
                // Prepare Trending properties data by filtering based on IsPriority or IsFavourtie
                listedProperties = listedProperties.Where(p => p.IsPriority || p.IsFavourite);
            }
            else
            {
                listedProperties = listedProperties.Where(p => p.DestinationTypes.Contains(SelectedPresetFilter));
            }

            // Get the next page of items
            var paginatedProperties = listedProperties
                .Skip((_currentPage - 1) * PageSize) // Skip those already loaded
                .Take(PageSize); // Get next items

            // Add the new items to the list
            DispatcherQueue.GetForCurrentThread().TryEnqueue(() =>
            {
                foreach (var property in paginatedProperties)
                {
                    Properties.Add(property);
                }
                CheckPropertyListCount();
            });

            _currentPage++; // Increment the current page
        }
        finally
        {
            IsLoading = false;
        }
    }

    public async Task SearchRoomsAsync()
    {
        if (CheckInDate == null || CheckOutDate == null)
        {
            return; // Check-in and check-out dates are required
        }

        // Set state to load only search data
        IsSearchLoading = true;
        IsFilteredLoading = false;
        IsDefaultLoading = false;

        // Reset current pagination index & clear the list
        _currentPage = 1;
        Properties.Clear();

        // Start loading the search data
        await LoadPropertyListFromSearchAsync();
        CheckPropertyListCount();
    }

    public async Task LoadPropertyListFromSearchAsync()
    {
        // Avoid calling multiple times at the same time
        if (IsLoading) return;

        try
        {
            // Set loading state
            IsLoading = true;

            var results = await _roomService.GetAvailableRoomsAsync(CheckInDate, CheckOutDate, Destination, NumberOfAdults + NumberOfChildren, NumberOfPets);
            var listedProperties = results.Where(x => x.Status == PropertyStatus.Listed);

            // Get the next page of items
            var paginatedProperties = listedProperties
                .Skip((_currentPage - 1) * PageSize) // Skip those already loaded
                .Take(PageSize); // Get next items

            // Add the new items to the list
            DispatcherQueue.GetForCurrentThread().TryEnqueue(() =>
            {
                foreach (var property in paginatedProperties)
                {
                    Properties.Add(property);
                }
                CheckPropertyListCount();
            });

            _currentPage++; // Increment the current page
        }
        finally
        {
            IsLoading = false;
        }
    }

    public void ToggleDisplayPropertiesPriceWithTax(bool isTaxIncluded = false)
    {
        var taxAmount = 9.90m;
        foreach (var property in Properties)
        {
            property.PricePerNight += isTaxIncluded ? taxAmount : -taxAmount;
        }
    }

    public Task<List<string>> SearchLocationsToStringAsync(string query)
    {
        return _roomService.SearchLocationsToStringAsync(query);
    }
}
