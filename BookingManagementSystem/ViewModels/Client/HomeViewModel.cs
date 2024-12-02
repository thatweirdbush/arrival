using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.Contracts.ViewModels;
using BookingManagementSystem.Core.Models;
using CommunityToolkit.Mvvm.Input;
using BookingManagementSystem.Core.Contracts.Facades;

namespace BookingManagementSystem.ViewModels.Client;

public partial class HomeViewModel : ObservableRecipient, INavigationAware
{
    private readonly INavigationService _navigationService;
    private readonly IHomeFacade _homeFacade;

    // Filtered destination data
    public IEnumerable<DestinationTypeSymbol> DestinationTypeSymbols { get; set; } = [];

    // List of items for the AdaptiveGridView
    public ObservableCollection<Property> Properties { get; set; } = [];

    [ObservableProperty]
    private bool isPropertyListEmpty;
    public DateTimeOffset? CheckInDate { get; set; }
    public DateTimeOffset? CheckOutDate { get; set; }

    public IAsyncRelayCommand SearchAvailableRoomsCommand
    {
        get;
    }

    public HomeViewModel(INavigationService navigationService, IHomeFacade homeFacade)
    {
        _navigationService = navigationService;
        _homeFacade = homeFacade;

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
        DestinationTypeSymbols = await _homeFacade.GetAllDestinationTypeSymbolsAsync();

        // Load Properties data
        LoadAllProperties();
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
        var data = await _homeFacade.GetAllPropertiesAsync();
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
        var data = await _homeFacade.GetAllPropertiesAsync();

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
            return;
        }
        var results = await _homeFacade.GetAvailableRoomsAsync(CheckInDate, CheckOutDate);
        var listedProperties = results.Where(x => x.Status == PropertyStatus.Listed);

        // Simulate network delay
        await Task.Delay(600);

        Properties.Clear();
        foreach (var room in listedProperties)
        {
            Properties.Add((Property)room);
        }
    }

    public Task<List<string>> SearchLocationsAsync(string query)
    {
        return _homeFacade.SearchLocationsAsync(query);
    }
}
