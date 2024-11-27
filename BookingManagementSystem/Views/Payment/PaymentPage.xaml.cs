using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.Core.Models;
using BookingManagementSystem.ViewModels.Payment;
using CommunityToolkit.WinUI.UI.Animations;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using BookingManagementSystem.ViewModels.Client;
using Microsoft.UI.Xaml.Controls.Primitives;

namespace BookingManagementSystem.Views.Payment;

public sealed partial class PaymentPage : Page
{
    public PaymentViewModel ViewModel
    {
        get;
    }

    public PaymentPage()
    {
        ViewModel = App.GetService<PaymentViewModel>();
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);

        // Scroll to top when navigating to this page
        ContentScrollView.ScrollTo(0, 0);

    }

    protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
    {
        base.OnNavigatingFrom(e);
        if (e.NavigationMode == NavigationMode.Back)
        {
            var navigationService = App.GetService<INavigationService>();

            if (ViewModel.Item != null)
            {
                navigationService.SetListDataItemForNextConnectedAnimation(ViewModel.Item);
            }
        }
    }

    private void ApplyVoucherButton_click(object sender, RoutedEventArgs e)
    {
        var pricePerNight = ViewModel.Item?.PricePerNight;
        var tax = ViewModel.Tax;

        // Try to find the voucher with the given code
        var voucherCode = VoucherInputTextBox.Text.Trim();
        ViewModel.Voucher = ViewModel.Vouchers.FirstOrDefault(v => v.Code == voucherCode);

        if (string.IsNullOrWhiteSpace(VoucherInputTextBox.Text))
        {
            VoucherWarning.Text = "Please enter the voucher code.";
            VoucherWarning.Visibility = Visibility.Visible;
        }
        else if (ViewModel.Voucher == null)
        {
            VoucherWarning.Text = "This voucher does not exist.";
            VoucherWarning.Visibility = Visibility.Visible;
        }
        else if (ViewModel.Voucher.Quantity <= 0)
        {
            VoucherWarning.Text = "The number of voucher uses has exceeded the limit.";
            VoucherWarning.Visibility = Visibility.Visible;
        }
        else
        {
            // All checks passed, hide the warning
            VoucherWarning.Visibility = Visibility.Collapsed;

            // Update the discount text blocks
            DiscountPercentageTextBlock.Visibility = Visibility.Visible;
            DiscountAmountTextBlock.Visibility = Visibility.Visible;
        }
    }

    private async void btnConfirmAndPay_Click(object sender, RoutedEventArgs e)
    {
        if (ViewModel.Item != null)
        {
            var voucherCode = VoucherInputTextBox.Text.Trim();
            var booking = new Booking()
            {
                Id = new Random().Next(1000, 9999),
                PropertyId = ViewModel.Item.Id,
                TotalPrice = ViewModel.TotalAmount,
                Status = BookingStatus.Confirmed
            };
            // Voucher availability has been checked before
            ViewModel.UpdateVoucherUsage();

            // Add the booking to the database
            await ViewModel.AddBookingAsync(booking);

            // Show the successful dialog and wait for it to close
            _ = new ContentDialog
            {
                XamlRoot = XamlRoot,
                Title = "Booking Confirmed",
                Content = "Your booking has been successfully completed! Here are the details of your reservation:\n\n" +
                $"Booking ID: {booking.Id}\n" +
                $"Property ID: {booking.PropertyId}\n" +
                $"Voucher: {voucherCode}\n" +
                $"Total Price: ${booking.TotalPrice}\n" +
                $"Status: {booking.Status}\n" +
                $"Booking Date: {booking.CreatedAt}\n\n" +
                "We will review your booking and keep you updated. Thank you for choosing our service!",
                CloseButtonText = "Ok"
            }.ShowAsync();

            Frame.Navigate(typeof(BookingHistoryPage));
        }
        else
        {
            // Thông báo lỗi nếu không có thông tin
            _ = new ContentDialog
            {
                XamlRoot = XamlRoot,
                Title = "Error",
                Content = "No property selected for booking.",
                CloseButtonText = "Ok"
            }.ShowAsync();
        }
    }

    private void DiscountPercentageTextBlock_Click(Microsoft.UI.Xaml.Documents.Hyperlink sender, Microsoft.UI.Xaml.Documents.HyperlinkClickEventArgs args)
    {
        FlyoutBase.ShowAttachedFlyout(DiscountPercentageTextBlock);
    }

    private void QuestionButton_Click(object sender, RoutedEventArgs e)
    {
        FlyoutBase.ShowAttachedFlyout(QuestionButton);
    }
}
