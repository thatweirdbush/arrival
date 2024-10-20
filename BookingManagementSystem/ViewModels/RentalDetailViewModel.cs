using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingManagementSystem.Contracts.ViewModels;
using BookingManagementSystem.Core.Contracts.Services;
using BookingManagementSystem.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BookingManagementSystem.ViewModels;

public partial class RentalDetailViewModel : ObservableRecipient, INavigationAware
{
    private readonly IDao _dao;

    [ObservableProperty]
    private Property? item;
    public IEnumerable<Review> Reviews { get; set; } = Enumerable.Empty<Review>();
    public IEnumerable<DestinationTypeSymbol> DestinationTypeSymbols { get; set; } = Enumerable.Empty<DestinationTypeSymbol>();

    public RentalDetailViewModel(IDao dao)
    {
        _dao = dao;
    }

    public async void OnNavigatedTo(object parameter)
    {
        if (parameter is int Id)
        {
            var data = await _dao.GetPropertyListDataAsync();
            Item = data.First(i => i.Id == Id);
        }

        // Load Review data
        var reviews = await _dao.GetReviewListDataAsync();
        Reviews = reviews;

        // Load DestinationTypeSymbols data
        var destinationTypeSymbols = await _dao.GetDestinationTypeSymbolDataAsync();
        DestinationTypeSymbols = destinationTypeSymbols;
    }

    public void OnNavigatedFrom()
    {
    }
}
