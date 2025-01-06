using BookingManagementSystem.Contracts.ViewModels;
using BookingManagementSystem.Core.Models;
using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.ViewModels.Payment;
using BookingManagementSystem.ViewModels.Account;
using BookingManagementSystem.Core.Commons.Filters;
using BookingManagementSystem.Core.Contracts.Facades;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using Microsoft.UI.Xaml.Controls;

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
    public PropertyFilter? ScheduleInformation { get; private set; }
    public DateTimeOffset? CheckInDate { get; set; }
    public DateTimeOffset? CheckOutDate { get; set; }

    public RentalDetailViewModel(IRentalDetailFacade rentalDetailFacade, INavigationService navigationService)
    {
        _rentalDetailFacade = rentalDetailFacade;
        _navigationService = navigationService;
        ProceedPaymentCommand = new AsyncRelayCommand(ProceedPaymentAsync);
    }

    public async void OnNavigatedTo(object parameter)
    {
        int? propertyId = null;
        PropertyFilter? filter = null;

        if (parameter is IDictionary<string, object> paramDict)
        {
            if (paramDict.TryGetValue("PropertyId", out var idObj) && idObj is int id)
            {
                propertyId = id;
            }
            if (paramDict.TryGetValue("Filter", out var filterObj) && filterObj is PropertyFilter propertyFilter)
            {
                filter = propertyFilter;
            }
        }
        else if (parameter is int id)
        {
            propertyId = id;
        }

        if (propertyId.HasValue)
        {
            if (filter != null)
            {
                ScheduleInformation = filter;
                CheckInDate = ScheduleInformation?.CheckInDate;
                CheckOutDate = ScheduleInformation?.CheckOutDate;
            }

            Item = await _rentalDetailFacade.GetPropertyByIdAsync(propertyId.Value);

            var reviews = await _rentalDetailFacade.GetReviewsAsync();
            Reviews = new ObservableCollection<Review>(reviews.OrderByDescending(r => r.CreatedAt));

            var qnas = await _rentalDetailFacade.GetQnAsAsync();
            QnAs = new ObservableCollection<QnA>(qnas.OrderByDescending(q => q.CreatedAt));

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

        // Set the current user as the customer for immediate UI update
        qna.Customer = LoginViewModel.CurrentUser;
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

    public void RebuildPropertyFilter()
    {
        ScheduleInformation ??= new PropertyFilter();

        if (CheckInDate.HasValue && CheckOutDate.HasValue)
        {
            ScheduleInformation.CheckInDate = CheckInDate.Value;
            ScheduleInformation.CheckOutDate = CheckOutDate.Value;
        }
    }

    public async Task ProceedPaymentAsync()
    {
        if (Item == null)
        {
            _ = await new ContentDialog
            {
                XamlRoot = App.MainWindow.Content.XamlRoot,
                Title = "Property Not Found",
                Content = "No property selected for booking.",
                CloseButtonText = "OK",
                DefaultButton = ContentDialogButton.Close
            }.ShowAsync();
            return;
        }

        // Simulate network delay  
        await Task.Delay(400);

        // Update the filter with the latest schedule information
        RebuildPropertyFilter();

        // Wrap both Id and PropertyFilter into an object to pass  
        IDictionary<string, object> navigationParameters = new Dictionary<string, object>
        {
           { "PropertyId", Item.Id },
           { "Filter", ScheduleInformation! }
        };

        // Navigate to Payment Page  
        _navigationService.NavigateTo(typeof(PaymentViewModel).FullName!, navigationParameters);
    }
}
