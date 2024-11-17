using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;

using BookingManagementSystem.Contracts.ViewModels;
using BookingManagementSystem.Core.Contracts.Services;
using BookingManagementSystem.Core.Models;
using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.Core.Services;
using CommunityToolkit.Mvvm.Input;
using System.Collections;
using static System.Runtime.InteropServices.JavaScript.JSType;
using BookingManagementSystem.Core.Contracts.Repositories;

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

    public HomeViewModel(INavigationService navigationService, 
                        IRepository<Property> propertyRepository, 
                        IRepository<DestinationTypeSymbol> destinationTypeSymbolRepository)
    {
        _navigationService = navigationService;
        _propertyRepository = propertyRepository;
        _destinationTypeSymbolRepository = destinationTypeSymbolRepository;

        // Subscribe to CollectionChanged event
        Properties.CollectionChanged += (s, e) => CheckPropertyListCount();

        // Initial check
        CheckPropertyListCount();
    }

    private void CheckPropertyListCount()
    {
        IsPropertyListEmpty = Properties.Count == 0;
    }

    public async void OnNavigatedTo(object parameter)
    {
        // Load DestinationTypeSymbols data
        var destinationTypeSymbols = await _destinationTypeSymbolRepository.GetAllAsync();
        DestinationTypeSymbols = destinationTypeSymbols;

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

        foreach (var item in data)
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

    public async void FilterProperties(DestinationTypeSymbol? destinationTypeSymbol)
    {
        if (destinationTypeSymbol == null)
        {
            return;
        }
        if (destinationTypeSymbol.DestinationType.Equals(DestinationType.All))
        {
            // Reload all properties before filtering
            LoadAllProperties();
            return;
        }

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
            Properties.Clear();

        }
        foreach (var item in data)
        {
            Properties.Add(item);
        }
    }
}
