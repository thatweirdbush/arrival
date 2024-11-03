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

namespace BookingManagementSystem.ViewModels;

public partial class HomeViewModel : ObservableRecipient, INavigationAware
{
    private readonly INavigationService _navigationService;
    private readonly IDao _dao;

    // Filtered destination data
    public IEnumerable<DestinationTypeSymbol> DestinationTypeSymbols { get; set; } = Enumerable.Empty<DestinationTypeSymbol>();

    // List of items for the AdaptiveGridView
    public ObservableCollection<Property> Properties { get; set; } = new ObservableCollection<Property>();

    public HomeViewModel(INavigationService navigationService, IDao dao)
    {
        _navigationService = navigationService;
        _dao = dao;
    }

    public async void OnNavigatedTo(object parameter)
    {
        // Load DestinationTypeSymbols data
        var destinationTypeSymbols = await _dao.GetDestinationTypeSymbolDataAsync();
        DestinationTypeSymbols = destinationTypeSymbols;

        // Load Properties data
        //var properties = await _dao.GetPropertyListDataAsync();
        //Properties = new ObservableCollection<Property>(properties);
        LoadAllProperties();
    }

    public void OnNavigatedFrom()
    {
    }

    public async void LoadAllProperties()
    {
        // Load Properties data
        Properties.Clear();

        var data = await _dao.GetPropertyListDataAsync();
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
        var data = await _dao.GetPropertyListDataAsync();

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
