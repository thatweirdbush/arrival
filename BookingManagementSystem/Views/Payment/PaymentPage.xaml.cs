using BookingManagementSystem.ViewModels.Payment;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
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

    private void DiscountPercentageTextBlock_Click(Microsoft.UI.Xaml.Documents.Hyperlink sender, Microsoft.UI.Xaml.Documents.HyperlinkClickEventArgs args)
    {
        FlyoutBase.ShowAttachedFlyout(DiscountPercentageTextBlock);
    }

    private void QuestionButton_Click(object sender, RoutedEventArgs e)
    {
        FlyoutBase.ShowAttachedFlyout(QuestionButton);
    }
}
