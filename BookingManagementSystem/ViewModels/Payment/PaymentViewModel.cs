using BookingManagementSystem.Contracts.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using BookingManagementSystem.Core.Models;
using BookingManagementSystem.Core.Contracts.Facades;
using BookingManagementSystem.Contracts.ViewModels;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;
using BookingManagementSystem.Core.Commons.Filters;

namespace BookingManagementSystem.ViewModels.Payment;

public partial class PaymentViewModel : ObservableRecipient, INavigationAware
{
    private readonly IPaymentFacade _paymentFacade;
    private readonly INavigationService _navigationService;

    [ObservableProperty]
    public Voucher? voucher;

    [ObservableProperty]
    private Property? item;
    
    [ObservableProperty]
    public int totalNights;

    [ObservableProperty]
    public int totalGuests;

    public PropertyFilter? ScheduleInformation { get; private set; }
    public decimal TotalAmount { get; set; }
    public decimal TotalAmountBeforeFees { get; set; }
    public decimal TotalAmountAfterFees { get; set; }
    public decimal DiscountAmount { get; set; }

    public decimal Tax = 9.90m;
    public decimal PayPartLaterPrice => TotalAmount / 2;
    public DateTime PayLaterDate => DateTime.Now.AddDays(2);
    public bool IsVoucherApplied => Voucher != null;

    public IEnumerable<Voucher> Vouchers { get; private set; } = [];
    public AsyncRelayCommand ConfirmAndPayCommand { get; }

    public PaymentViewModel(IPaymentFacade paymentFacade, INavigationService navigationService)
    {
        _paymentFacade = paymentFacade;
        _navigationService = navigationService;
        ConfirmAndPayCommand = new AsyncRelayCommand(ConfirmAndPayAsync);
    }

    public async void OnNavigatedTo(object parameter)
    {
        if (parameter is IDictionary<string, object> paramDict &&
            paramDict.TryGetValue("PropertyId", out var idObj) && idObj is int id &&
            paramDict.TryGetValue("Filter", out var filterObj) && filterObj is PropertyFilter filter)
        {
            ScheduleInformation = filter;
            Item = await _paymentFacade.GetPropertyByIdAsync(id);
            Vouchers = await _paymentFacade.GetVouchersAsync();

            // Initialize core properties
            TotalGuests = ScheduleInformation.MinGuests ?? 1;
            TotalNights = (int)(ScheduleInformation.CheckOutDate?.Subtract(ScheduleInformation.CheckInDate ?? DateTimeOffset.MinValue).TotalDays ?? 0);
            TotalAmountBeforeFees = Item?.PricePerNight * TotalNights ?? 0.0m;
            TotalAmountAfterFees = TotalAmountBeforeFees + Tax;
            TotalAmount = TotalAmountAfterFees;
        }
    }

    public void OnNavigatedFrom()
    {
    }

    private void CalculateTotalAmount()
    {
        TotalAmountBeforeFees = Item?.PricePerNight * TotalNights ?? 0.0m;
        TotalAmountAfterFees = TotalAmountBeforeFees + Tax;
        TotalAmount = TotalAmountAfterFees;
        if (Voucher?.DiscountPercentage.HasValue == true)
        {
            DiscountAmount = Voucher.DiscountPercentage.Value * TotalAmount / 100;
            TotalAmount -= DiscountAmount;
        }
        else if (Voucher?.DiscountAmount.HasValue == true)
        {
            DiscountAmount = Voucher.DiscountAmount.Value;
            TotalAmount -= DiscountAmount;
        }
        else
        {
            DiscountAmount = 0.0m;
        }
    }

    private async Task ConfirmAndPayAsync()
    {
        if (Item == null)
        {
            await ShowErrorDialogAsync("Error", "No property selected for booking.");
            return;
        }
        var booking = new Booking
        {
            Id = new Random().Next(1000, 9999),
            PropertyId = Item.Id,
            TotalPrice = TotalAmount,
            Status = BookingStatus.Confirmed
        };

        // Voucher availability has been checked before
        UpdateVoucherUsage();

        // Add the booking to the database
        await AddBookingAsync(booking);

        // Add a notification to the user
        await _paymentFacade.AddNotificationAsync(new Notification
        {
            Title = "Booking Confirmed",
            Message = $"Your booking to {Item.Name} has been confirmed, we are looking forward to welcoming you!",
            ImagePath= Item.ImageThumbnail
        });

        // Simulate network delay
        await Task.Delay(400);

        // Navigate to the Booking History page
        _navigationService.NavigateTo(typeof(BookingHistoryViewModel).FullName!);

        // Show the success dialog
        await ShowSuccessDialogAsync(booking);

        // Show the system notification
        ShowSystemNotification(Item);
    }

    private async Task ShowSuccessDialogAsync(Booking booking)
    {
        var dialog = new ContentDialog
        {
            XamlRoot = App.MainWindow.Content.XamlRoot,
            Title = "Booking Confirmed",
            Content = "Your booking has been successfully completed! Here are the details of your reservation:\n\n" +
            $"Booking ID: {booking.Id}\n" +
            $"Property ID: {booking.PropertyId}\n" +
            $"Voucher: {Voucher?.Code}\n" +
            $"Total Price: ${booking.TotalPrice}\n" +
            $"Status: {booking.Status}\n" +
            $"Booking Date: {booking.CreatedAt}\n\n" +
            "We will review your booking and keep you updated. Thank you for choosing our service!",
            CloseButtonText = "OK",
            DefaultButton = ContentDialogButton.Close
        };

        await dialog.ShowAsync();
    }

    private void ShowSystemNotification(Property property)
    {
        // Create parameters for the notification
        var IdParameter = $"id={property.Id}";

        // Create notification channel
        App.GetService<IAppNotificationService>().ShowNotification(
            title: "Booking Confirmed",
            message: $"Your booking to {property.Name} has been confirmed, we are looking forward to welcoming you!",
            imageUri: property.ImageThumbnail,
            buttons:
            [
                ("Mark as read", "action=mark-as-read"),
                ("See details", IdParameter)
            ]
        );
    }

    private async Task ShowErrorDialogAsync(string title, string content)
    {
        var dialog = new ContentDialog
        {
            XamlRoot = App.MainWindow.Content.XamlRoot,
            Title = title,
            Content = content,
            CloseButtonText = "OK",
            DefaultButton = ContentDialogButton.Close
        };

        await dialog.ShowAsync();
    }

    partial void OnVoucherChanged(Voucher? value)
    {
        CalculateTotalAmount();
    }

    public void UpdateVoucherUsage()
    {
        if (Voucher != null)
        {
            Voucher.Quantity -= 1;
        }
    }

    public async Task AddBookingAsync(Booking booking)
    {
        await _paymentFacade.AddBookingAsync(booking);
    }
}
