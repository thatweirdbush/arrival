using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.Contracts.ViewModels;
using BookingManagementSystem.Core.Contracts.Repositories;
using BookingManagementSystem.Core.Models;
using CommunityToolkit.Mvvm.Input;
using BookingManagementSystem.Core.Contracts.Services;

namespace BookingManagementSystem.ViewModels.Client;

public partial class HomeViewModel : ObservableRecipient, INavigationAware
{
    private readonly INavigationService _navigationService;
    private readonly IRepository<Property> _propertyRepository;
    private readonly IRepository<DestinationTypeSymbol> _destinationTypeSymbolRepository;

    // Filtered destination data
    public IEnumerable<DestinationTypeSymbol> DestinationTypeSymbols { get; set; } = [];

    // List of items for the AdaptiveGridView
    public ObservableCollection<Property> Properties { get; set; } = [];

    [ObservableProperty]
    private bool isPropertyListEmpty;

    // Properties nessesary for Schedule searching
    private readonly IRoomService _roomService;
    public DateTimeOffset CheckInDate { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset CheckOutDate { get; set; } = DateTimeOffset.Now.AddDays(1);

    public HomeViewModel(INavigationService navigationService, 
                        IRepository<Property> propertyRepository, 
                        IRepository<DestinationTypeSymbol> destinationTypeSymbolRepository,
                        IRoomService roomService)
    {
        _navigationService = navigationService;
        _propertyRepository = propertyRepository;
        _destinationTypeSymbolRepository = destinationTypeSymbolRepository;
        _roomService = roomService;

        // Subscribe to CollectionChanged event
        Properties.CollectionChanged += (s, e) => CheckPropertyListCount();

        // Initial check
        CheckPropertyListCount();

        SearchAvailableRoomsCommand = new AsyncRelayCommand(SearchRoomsAsync);
    }

    private void CheckPropertyListCount()
    {
        IsPropertyListEmpty = Properties.Count == 0;
    }

    public async void OnNavigatedTo(object parameter)
    {
        // Load DestinationTypeSymbols data
        DestinationTypeSymbols = await _destinationTypeSymbolRepository.GetAllAsync();

        // Load Properties data
        LoadAllProperties();
    }

    public void OnNavigatedFrom()
    {
    }

    public async void LoadAllProperties()
    {
        Properties.Clear();
        var data = await _propertyRepository.GetAllAsync();
        var listedProperties = data.Where(x => x.Status == PropertyStatus.Listed);

        foreach (var item in listedProperties)
        {
            Properties.Add(item);
        }
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

    public IAsyncRelayCommand SearchAvailableRoomsCommand
    {
        get;
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
        var data = await _propertyRepository.GetAllAsync();

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
        if (CheckInDate >= CheckOutDate)
        {
            return;
        }
        var results = await _roomService.GetAvailableRoomsAsync(CheckInDate, CheckOutDate);

        // Simulate network delay
        await Task.Delay(600);

        Properties.Clear();
        foreach (var room in results)
        {
            Properties.Add(room);
        }
    }
}
