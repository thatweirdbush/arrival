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
using System.Collections.ObjectModel;
using BookingManagementSystem.Core.Contracts.Facades;
using CommunityToolkit.Mvvm.Input;
using BookingManagementSystem.Core.Services;
using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.ViewModels.Payment;

namespace BookingManagementSystem.ViewModels.Client;

public partial class RentalDetailViewModel : ObservableRecipient, INavigationAware
{
    private readonly IRentalDetailFacade _rentalDetailFacade;
    private readonly INavigationService _navigationService;

    [ObservableProperty]
    private Property? item;
    public ObservableCollection<Review> Reviews { get; set; } = [];
    public ObservableCollection<QnA> QnAs { get; set; } = [];
    public IEnumerable<DestinationTypeSymbol> DestinationTypeSymbols { get; set; } = Enumerable.Empty<DestinationTypeSymbol>();
    public IEnumerable<PropertyPolicy> PropertyPolicies { get; set; } = Enumerable.Empty<PropertyPolicy>();
    public IAsyncRelayCommand ProceedPaymentCommand
    {
        get;
    }

    public RentalDetailViewModel(IRentalDetailFacade rentalDetailFacade, INavigationService navigationService)
    {
        _rentalDetailFacade = rentalDetailFacade;
        _navigationService = navigationService;
        ProceedPaymentCommand = new AsyncRelayCommand(ProceedPaymentAsync);
    }

    public async void OnNavigatedTo(object parameter)
    {
        if (parameter is int Id)
        {
            Item = await _rentalDetailFacade.GetPropertyByIdAsync(Id);
            var reviews = await _rentalDetailFacade.GetReviewsAsync();
            foreach (var review in reviews)
            {
                Reviews.Add(review);
            }

            var qnas = await _rentalDetailFacade.GetQnAsAsync();
            foreach (var qna in qnas)
            {
                QnAs.Add(qna);
            }

            DestinationTypeSymbols = await _rentalDetailFacade.GetDestinationTypeSymbolsAsync();
            PropertyPolicies = await _rentalDetailFacade.GetPropertyPoliciesAsync();
        }
    }

    public void OnNavigatedFrom()
    {
    }

    public async Task AddReviewAsync(Review review)
    {
        await _rentalDetailFacade.AddReviewAsync(review);
    }

    public async Task AddQnAAsync(QnA qna)
    {
        await _rentalDetailFacade.AddQnAAsync(qna);
    }

    public async Task AddBadReportAsync(BadReport badReport)
    {
        await _rentalDetailFacade.AddBadReportAsync(badReport);
    }

    public async Task ProceedPaymentAsync()
    {
        // Simulate network delay
        await Task.Delay(400);

        // Navigate to Payment Page
        _navigationService.NavigateTo(typeof(PaymentViewModel).FullName!, Item?.Id);
    }
}
