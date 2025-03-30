using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.Contracts.ViewModels;
using BookingManagementSystem.Core.Contracts.Facades;
using BookingManagementSystem.Core.Commons.Filters;
using BookingManagementSystem.Core.Models;
using BookingManagementSystem.ViewModels.Account;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Microsoft.UI.Xaml.Controls;

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
    public DateTime? CancelBeforeDate => ScheduleInformation?.CheckInDate?.AddDays(-1).DateTime;
    public bool IsVoucherApplied => Voucher != null;
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
            await ShowErrorDialogAsync("Property Not Found", "No property selected for booking.");
            return;
        }
        if (LoginViewModel.CurrentUser == null)
        {
            await ShowErrorDialogAsync("Login Required", "Please log in to continue.");
            return;
        }

        // Create new booking object
        var booking = new Booking
        {
            PropertyId = Item.Id,
            UserId = LoginViewModel.CurrentUser.Id,
            CheckInDate = ScheduleInformation?.CheckInDate?.DateTime.ToUniversalTime() ?? DateTime.MinValue.ToUniversalTime(),
            CheckOutDate = ScheduleInformation?.CheckOutDate?.DateTime.ToUniversalTime() ?? DateTime.MinValue.ToUniversalTime(),
            TotalPrice = TotalAmount,
            Status = BookingStatus.Confirmed
        };

        // Add the booking to the database
        await _paymentFacade.AddBookingAsync(booking);

        // Add the payment to the database
        await _paymentFacade.AddPaymentAsync(new Core.Models.Payment
        {
            UserId = LoginViewModel.CurrentUser.Id,
            BookingId = booking.Id,
            Amount = TotalAmount,
            PaymentDate = DateTime.Now.ToUniversalTime(),
            Status = PaymentStatus.Paid
        });

        // Add confirmed notification to the database
        await _paymentFacade.AddNotificationAsync(new Notification
        {
            UserId = LoginViewModel.CurrentUser.Id,
            Title = "Booking Confirmed",
            Message = $"Your booking to {Item.Name} has been confirmed, we are looking forward to welcoming you!",
            ImagePath = Item.ImageThumbnail
        });

        // Update voucher since it's availability has been checked before
        if (Voucher != null)
        {
            await UpdateVoucherAsync(Voucher);
        }

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
            $"Booking Date: {TimeZoneInfo.ConvertTimeFromUtc(booking.CreatedAt, TimeZoneInfo.Local):f}\n\n" +
            "We will review your booking and keep you updated. Thank you for choosing our service!",
            CloseButtonText = "OK",
            DefaultButton = ContentDialogButton.Close
        };

        await dialog.ShowAsync();
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

    partial void OnVoucherChanged(Voucher? value)
    {
        CalculateTotalAmount();
    }

    public async Task GetVoucherByCodeAsync(string code)
    {
        Voucher = await _paymentFacade.GetVoucherByCodeAsync(code);
    }

    public async Task UpdateVoucherAsync(Voucher voucher)
    {
        voucher.Quantity -= 1;
        await _paymentFacade.UpdateVoucherAsync(voucher);
    }
}
