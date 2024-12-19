using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.Contracts.ViewModels;
using BookingManagementSystem.Core.Models;
using CommunityToolkit.Mvvm.Input;
using BookingManagementSystem.Core.Contracts.Services;
using Microsoft.UI.Dispatching;
using BookingManagementSystem.Core.Commons.Enums;
using BookingManagementSystem.Core.Commons.Filters;

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
    public DateTimeOffset? CheckInDate { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? CheckOutDate { get; set; } = DateTimeOffset.UtcNow.AddDays(5);
    public DestinationType SelectedPresetFilter { get; set; } = DestinationType.All;    
    public IAsyncRelayCommand SearchAvailableRoomsCommand { get; }

    public HomeViewModel(INavigationService navigationService, IRoomService roomService)
    {
        _navigationService = navigationService;
        _roomService = roomService;

        // Async relay command for searching available rooms
        SearchAvailableRoomsCommand = new AsyncRelayCommand(RefreshAsync);
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

    public async void OnNavigatedTo(object parameter)
    {
        // Load DestinationTypeSymbols data
        DestinationTypeSymbols = await _roomService.GetAllDestinationTypeSymbolsAsync();

        // Set the default loading state
        CurrentLoadingState = LoadingState.Default;

        // Initialize observable input fields
        NumberOfAdults = 1;
        NumberOfChildren = 0;
        NumberOfPets = 0;

        // Load Properties data
        await LoadPropertiesAsync();
    }

    public void OnNavigatedFrom()
    {
    }

    private void CheckListCount()
    {
        IsPropertyListEmpty = !Properties.Any();
    }

    // General methods for loading properties data
    private PropertyFilter BuildPropertyFilter()
    {
        return new PropertyFilter
        {
            CheckInDate = CheckInDate,
            CheckOutDate = CheckOutDate,
            DestinationType = SelectedPresetFilter,
            Destination = Destination,
            MinGuests = NumberOfAdults + NumberOfChildren,
            PetsAllowed = NumberOfPets,
            PageNumber = _currentPage,
            PageSize = PageSize
        };
    }

    public async Task LoadPropertiesAsync()
    {
        // Avoid calling multiple times at the same time
        if (IsLoading) return;

        try
        {
            // Begin loading
            IsLoading = true;

            // Build the filter based on the current state
            var filter = BuildPropertyFilter();

            // Fetch paginated result
            var result = await _roomService.GetAvailableRoomsAsync(filter);

            DispatcherQueue.GetForCurrentThread().TryEnqueue(() =>
            {
                foreach (var property in result.Items)
                {
                    Properties.Add(property);
                }
                CheckListCount();
            });

            _currentPage++;
        }
        finally
        {
            // End loading
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

    public Task ToggleFavorite(Property property)
    {
        return _roomService.ToggleFavorite(property);
    }

    public async Task RefreshAsync()
    {
        _currentPage = 1;

        Properties.Clear();
        await LoadPropertiesAsync();
        CheckListCount();
    }
}
