using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.Core.Models;
using BookingManagementSystem.ViewModels.Payment;
using CommunityToolkit.WinUI.UI.Animations;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using BookingManagementSystem.ViewModels.Client;

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

    private void Apply_click(object sender, RoutedEventArgs e)
    {
        // Lấy giá trị từ AmountTextBox và TaxTextBox
        var amount = decimal.Parse(AmountTextBox.Text.Trim('$'));
        var tax = decimal.Parse(TaxTextBox.Text.Trim('$'));

        var voucherCode = VoucherInputTextBox.Text.Trim();

        decimal? discountPercentage = 0m;

        if (string.IsNullOrWhiteSpace(VoucherInputTextBox.Text))
        {
            VoucherWarning.Text = "Please enter a voucher!";
            VoucherWarning.Visibility = Visibility.Visible;
        }
        else
        {
            if (ViewModel.CheckVoucher(voucherCode, ref discountPercentage))
            {
                VoucherWarning.Visibility = Visibility.Collapsed;

                DiscountTextBlock.Text = $"{Math.Floor(discountPercentage.GetValueOrDefault())}%";
                DiscountTextBlock.Visibility = Visibility.Visible;

                var discountAmount = amount * discountPercentage / 100;

                // Cập nhật giá trị vào VoucherTextBox
                VoucherTextBox.Text = $"-${discountAmount:F2}";

                // Tính tổng tiền sau khi áp dụng voucher
                var totalAmount = (amount + tax) - discountAmount;
                TotalAmountTextBox.Text = $"${totalAmount:F2}";
            }
            else
            {
                VoucherWarning.Text = "Voucher is non - existent!";
                VoucherWarning.Visibility = Visibility.Visible;
            }
        }
    }

    private void btnConfirmAndPay_Click(object sender, RoutedEventArgs e)
    {

    }
}
