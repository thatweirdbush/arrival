using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;

using BookingManagementSystem.Contracts.ViewModels;
using BookingManagementSystem.Core.Contracts.Services;
using BookingManagementSystem.Core.Models;
using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.Core.Services;
using CommunityToolkit.Mvvm.Input;
using System.Collections;

namespace BookingManagementSystem.ViewModels;

public partial class HomeViewModel : ObservableRecipient, INavigationAware
{
    private readonly INavigationService _navigationService;
    private readonly IDao _dao;

    // Filtered destination data
    public IEnumerable<DestinationTypeSymbol> DestinationTypeSymbols { get; set; } = Enumerable.Empty<DestinationTypeSymbol>();

    // List of items for the AdaptiveGridView
    public IEnumerable<Property> Properties { get; set; } = Enumerable.Empty<Property>();

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
        var properties = await _dao.GetPropertyListDataAsync();
        Properties = properties;
    }

    public void OnNavigatedFrom()
    {
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
}
