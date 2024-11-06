using BookingManagementSystem.Core.Models;
using BookingManagementSystem.ViewModels.Payment;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

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

                DiscountTextBlock.Visibility = Visibility.Visible;
                DiscountTextBlock.Text = $"{Math.Floor((decimal)discountPercentage)}%";

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
}
