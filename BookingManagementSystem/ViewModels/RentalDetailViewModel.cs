using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingManagementSystem.Contracts.ViewModels;
using BookingManagementSystem.Core.Contracts.Services;
using BookingManagementSystem.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;

namespace BookingManagementSystem.ViewModels;

public partial class RentalDetailViewModel : ObservableRecipient, INavigationAware
{
    private readonly IDao _dao;

    [ObservableProperty]
    private Property? item;
    public IEnumerable<Review> Reviews { get; set; } = Enumerable.Empty<Review>();
    public IEnumerable<QnA> QnAs { get; set; } = Enumerable.Empty<QnA>();
    public IEnumerable<DestinationTypeSymbol> DestinationTypeSymbols { get; set; } = Enumerable.Empty<DestinationTypeSymbol>();
    public IEnumerable<PropertyPolicy> PropertyPolicies { get; set; } = Enumerable.Empty<PropertyPolicy>();

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

        // Load Reviews data
        var reviews = await _dao.GetReviewListDataAsync();
        Reviews = reviews;

        // Load QnAs data
        var qnas = await _dao.GetQnAListDataAsync();
        QnAs = qnas;

        // Load DestinationTypeSymbols data
        var destinationTypeSymbols = await _dao.GetDestinationTypeSymbolDataAsync();
        DestinationTypeSymbols = destinationTypeSymbols;

        // Load PropertyPolicies data
        var propertyPolicies = await _dao.GetPropertyPolicyListDataAsync();
        PropertyPolicies = propertyPolicies;
    }

    public void OnNavigatedFrom()
    {
    }
}
