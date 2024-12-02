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
    public List<Property> Properties { get; set; } = [];
    public IEnumerable<DestinationTypeSymbol> DestinationTypeSymbols { get; set; } = [];
    public IEnumerable<Review> Reviews { get; set; } = [];

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

    public MainViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    public void OnNavigatedTo(object parameter)
    {
    }

    public void OnNavigatedFrom()
    {
    }
}
