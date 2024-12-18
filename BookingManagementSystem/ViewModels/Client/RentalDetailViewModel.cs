using BookingManagementSystem.Contracts.ViewModels;
using BookingManagementSystem.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using BookingManagementSystem.Core.Contracts.Facades;
using CommunityToolkit.Mvvm.Input;
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
    public IEnumerable<DestinationTypeSymbol> DestinationTypeSymbols { get; set; } = [];
    public IEnumerable<PropertyPolicy> PropertyPolicies { get; set; } = [];
    public IAsyncRelayCommand ProceedPaymentCommand { get; }

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
            Reviews = new ObservableCollection<Review>(reviews);

            var qnas = await _rentalDetailFacade.GetQnAsAsync();
            QnAs = new ObservableCollection<QnA>(qnas);

            DestinationTypeSymbols = await _rentalDetailFacade.GetDestinationTypeSymbolsAsync();
            PropertyPolicies = await _rentalDetailFacade.GetPropertyPoliciesAsync();
        }
    }

    public void OnNavigatedFrom()
    {
    }

    public Task AddReviewAsync(Review review)
    {
        return _rentalDetailFacade.AddReviewAsync(review);
    }

    public Task AddQnAAsync(QnA qna)
    {
        return _rentalDetailFacade.AddQnAAsync(qna);
    }

    public Task AddBadReportAsync(BadReport badReport)
    {
        return _rentalDetailFacade.AddBadReportAsync(badReport);
    }

    public async Task ProceedPaymentAsync()
    {
        // Simulate network delay
        await Task.Delay(400);

        // Navigate to Payment Page
        _navigationService.NavigateTo(typeof(PaymentViewModel).FullName!, Item?.Id);
    }
}
