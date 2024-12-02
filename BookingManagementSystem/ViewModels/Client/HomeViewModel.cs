using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.Contracts.ViewModels;
using BookingManagementSystem.Core.Models;
using CommunityToolkit.Mvvm.Input;
using BookingManagementSystem.Core.Contracts.Facades;
using BookingManagementSystem.Core.Contracts.Services;

namespace BookingManagementSystem.ViewModels.Client;

public partial class HomeViewModel : ObservableRecipient, INavigationAware
{
    private readonly INavigationService _navigationService;
    private readonly IRoomService _roomService;

    [ObservableProperty]
    private bool isPropertyListEmpty;

    [ObservableProperty]
    private string? destination;

    [ObservableProperty]
    private int numberOfAdults;

    [ObservableProperty]
    private int numberOfChildren;

    [ObservableProperty]
    private int numberOfPets;
    public DateTimeOffset? CheckInDate { get; set; }
    public DateTimeOffset? CheckOutDate { get; set; }

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

        // Subscribe to CollectionChanged event
        Properties.CollectionChanged += (s, e) => CheckPropertyListCount();

        // Initial check
        CheckPropertyListCount();

        // Async relay command for searching available rooms
        SearchAvailableRoomsCommand = new AsyncRelayCommand(SearchRoomsAsync);
    }

    public async void OnNavigatedTo(object parameter)
    {
        // Load DestinationTypeSymbols data
        DestinationTypeSymbols = await _roomService.GetAllDestinationTypeSymbolsAsync();

        // Load Properties data
        LoadAllProperties();

        // Initialize observable properties
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

    public async void LoadAllProperties()
    {
        Properties.Clear();
        var data = await _roomService.GetAllPropertiesAsync();
        var listedProperties = data.Where(x => x.Status == PropertyStatus.Listed);

        foreach (var item in listedProperties)
        {
            Properties.Add(item);
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

    public async void FilterProperties(DestinationTypeSymbol destinationTypeSymbol)
    {
        if (destinationTypeSymbol.DestinationType.Equals(DestinationType.All))
        {
            LoadAllProperties();
            return;
        }
        // Reload all properties before filtering
        Properties.Clear();
        var data = await _roomService.GetAllPropertiesAsync();

        // Prepare Trending properties data by filtering based on IsPriority or IsFavourtie
        if (destinationTypeSymbol.DestinationType.Equals(DestinationType.Trending))
        {
            data = data.Where(p => p.IsPriority || p.IsFavourite).ToList();
        }
        else
        {
            data = data.Where(p => p.DestinationTypes.Contains(destinationTypeSymbol.DestinationType)).ToList();
        }
        foreach (var item in data)
        {
            Properties.Add(item);
        }
    }

    public async Task SearchRoomsAsync()
    {
        if (CheckInDate == null || CheckOutDate == null)
        {
            return; // Check-in and check-out dates are required
        }

        var results = await _roomService.GetAvailableRoomsAsync(CheckInDate, CheckOutDate, Destination, NumberOfAdults + NumberOfChildren, NumberOfPets);
        var listedProperties = results.Where(x => x.Status == PropertyStatus.Listed);

        // Simulate network delay
        await Task.Delay(500);

        Properties.Clear();
        foreach (var room in listedProperties)
        {
            Properties.Add((Property)room);
        }
    }

    public Task<List<string>> SearchLocationsToStringAsync(string query)
    {
        return _roomService.SearchLocationsToStringAsync(query);
    }
}
