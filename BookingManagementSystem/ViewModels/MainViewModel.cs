using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using BookingManagementSystem.Core.Models;
using BookingManagementSystem.Core.Contracts.Services;
using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.Services;
using BookingManagementSystem.Contracts.ViewModels;

namespace BookingManagementSystem.ViewModels;

public partial class MainViewModel : ObservableRecipient, INavigationAware
{
    private readonly INavigationService _navigationService;
    private readonly IDao _dao;
    public List<Property> Properties { get; set; } = new List<Property>();
    public IEnumerable<DestinationTypeSymbol> DestinationTypeSymbols { get; set; } = Enumerable.Empty<DestinationTypeSymbol>();
    public IEnumerable<Review> Reviews { get; set; } = Enumerable.Empty<Review>();

    public Property? SelectedItem
    {
        get; set;
    }
    private readonly Random random = new();

    public Property GetNextItem()
    {
        var index = random.Next(Properties.Count);
        SelectedItem = Properties[index];
        return SelectedItem;
    }

    public MainViewModel(INavigationService navigationService, IDao dao)
    {
        _navigationService = navigationService;
        _dao = dao;

        OnNavigatedTo(null);
    }

    public async void OnNavigatedTo(object? parameter)
    {
        // Load Properties data
        var properties = await _dao.GetPropertyListDataAsync();
        Properties = new List<Property>(properties);

        // Load DestinationTypeSymbols data
        var destinationTypeSymbols = await _dao.GetDestinationTypeSymbolDataAsync();
        DestinationTypeSymbols = destinationTypeSymbols;

        // Load Review data
        var reviews = await _dao.GetReviewListDataAsync();
        Reviews = reviews;
    }

    public void OnNavigatedFrom()
    {
    }
}
