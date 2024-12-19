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

    [ObservableProperty]
    private bool isReviewListEmpty;

    [ObservableProperty]
    private bool isQnAListEmpty;

    [ObservableProperty]
    private bool isPropertyPoliciesEmpty;

    [ObservableProperty]
    private bool isAmenitiesEmpty;

    public ObservableCollection<Review> Reviews { get; set; } = [];
    public ObservableCollection<QnA> QnAs { get; set; } = [];
    public IEnumerable<PropertyAmenity> PropertyAmenities { get; set; } = [];
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

            PropertyPolicies = await _rentalDetailFacade.GetPropertyPoliciesAsync();
            PropertyAmenities = await _rentalDetailFacade.GetPropertyAmenitiesAsync();

            UpdateObservableProperties();
        }
    }

    public void OnNavigatedFrom()
    {
    }

    public void UpdateObservableProperties()
    {
        IsReviewListEmpty = !Reviews.Any();
        IsQnAListEmpty = !QnAs.Any();
        IsPropertyPoliciesEmpty = !PropertyPolicies.Any();
        IsAmenitiesEmpty = !PropertyAmenities.Any();
    }

    public async Task AddReviewAsync(Review review)
    {
        await _rentalDetailFacade.AddReviewAsync(review);
        Reviews.Insert(0, review);
        UpdateObservableProperties();
    }

    public async Task AddQnAAsync(QnA qna)
    {
        await _rentalDetailFacade.AddQnAAsync(qna);
        QnAs.Insert(0, qna);
        UpdateObservableProperties();
    }

    public Task AddBadReportAsync(BadReport badReport)
    {
        return _rentalDetailFacade.AddBadReportAsync(badReport);
    }

    public Task UpdateAsync(Property property)
    {
        return _rentalDetailFacade.UpdateAsync(property);
    }

    public async Task ProceedPaymentAsync()
    {
        // Simulate network delay
        await Task.Delay(400);

        // Navigate to Payment Page
        _navigationService.NavigateTo(typeof(PaymentViewModel).FullName!, Item?.Id);
    }
}
