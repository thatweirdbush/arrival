using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.Contracts.ViewModels;
using BookingManagementSystem.Core.Models;
using CommunityToolkit.Mvvm.Input;
using BookingManagementSystem.Core.Contracts.Services;
using Microsoft.UI.Dispatching;

namespace BookingManagementSystem.ViewModels.Client;

public partial class HomeViewModel : ObservableRecipient, INavigationAware
{
    private readonly INavigationService _navigationService;
    private readonly IRoomService _roomService;

    // List of items for the AdaptiveGridView
    public ObservableCollection<Property> Properties { get; set; } = [];

    // Preset filter options for UI components
    public IEnumerable<DestinationTypeSymbol> DestinationTypeSymbols { get; set; } = [];

    [ObservableProperty]
    private bool isPropertyListEmpty;

    [ObservableProperty]
    private LoadingState currentLoadingState;

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

    private int _currentPage = 1;
    private const int PageSize = 10; // Default page size
    public DateTimeOffset? CheckInDate { get; set; }
    public DateTimeOffset? CheckOutDate { get; set; }
    public DestinationType SelectedPresetFilter { get; set; } = DestinationType.All;    
    public IAsyncRelayCommand SearchAvailableRoomsCommand { get; }
    public enum LoadingState
    {
        Default,
        Filtered,
        Search
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

        // Set the default loading state
        CurrentLoadingState = LoadingState.Filtered;

        // Initialize observable properties for input fields
        NumberOfAdults = 0;
        NumberOfChildren = 0;
        NumberOfPets = 0;
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

    // Common method for loading properties data
    private async Task LoadPropertiesAsync(Func<IEnumerable<Property>, IEnumerable<Property>> filterFunction)
    {
        // Avoid calling multiple times at the same time
        if (IsLoading) return;

        try
        {
            IsLoading = true;

            // Get all properties & Filter the official listed
            var allProperties = await _roomService.GetAllPropertiesAsync();
            var filteredProperties = filterFunction(allProperties.Where(x => x.Status == PropertyStatus.Listed));

            // Get the next page of items
            var paginatedProperties = filteredProperties
                .Skip((_currentPage - 1) * PageSize)
                .Take(PageSize);

            DispatcherQueue.GetForCurrentThread().TryEnqueue(() =>
            {
                foreach (var property in paginatedProperties)
                {
                    Properties.Add(property);
                }
                CheckPropertyListCount();
            });

            _currentPage++;
        }
        finally
        {
            IsLoading = false;
        }
    }

    // Load default properties data
    public async Task LoadPropertyListAsync()
    {
        await LoadPropertiesAsync(properties => properties);
    }

    // Load properties data based on the selected filter
    public async Task LoadPropertyListFromPresetFilterAsync()
    {
        await LoadPropertiesAsync(properties =>
        {
            // Filter the data based on the preset filter
            if (SelectedPresetFilter == DestinationType.All)
            {
                // Do nothing because the steps below already return the properties with DestinationType.All
                return properties;
            }
            else if (SelectedPresetFilter == DestinationType.Trending)
            {
                // Prepare Trending properties data by filtering based on IsPriority or IsFavourtie
                return properties.Where(p => p.IsPriority || p.IsFavourite);
            }
            else
            {
                return properties.Where(p => p.DestinationTypes.Contains(SelectedPresetFilter));
            }
        });
    }

    // Load properties data based on the search query
    public async Task LoadPropertyListFromSearchAsync()
    {
        var searchResults = await _roomService.GetAvailableRoomsAsync(CheckInDate, CheckOutDate, Destination, NumberOfAdults + NumberOfChildren, NumberOfPets);

        await LoadPropertiesAsync(properties => searchResults);
    }

    public async Task FilterProperties()
    {
        // Set state to load only filter data
        CurrentLoadingState = LoadingState.Filtered;

        // Reset when selecting new filter
        _currentPage = 1;
        Properties.Clear();

        // Get filtered data
        await LoadPropertyListFromPresetFilterAsync();
    }

    public async Task SearchRoomsAsync()
    {
        if (CheckInDate == null || CheckOutDate == null)
        {
            return; // Check-in and check-out dates are required
        }

        // Set state to load only search data
        CurrentLoadingState = LoadingState.Search;

        // Reset current pagination index & clear the list
        _currentPage = 1;
        Properties.Clear();

        // Start loading the search data
        await LoadPropertyListFromSearchAsync();
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
