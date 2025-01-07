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

    private async void ApplyVoucherButton_click(object sender, RoutedEventArgs e)
    {
        var voucherCode = VoucherInputTextBox.Text.Trim();

        if (string.IsNullOrWhiteSpace(voucherCode))
        {
            ShowVoucherWarning("Please enter the voucher code.");
            ViewModel.Voucher = null;
            return;
        }

        await ViewModel.GetVoucherByCodeAsync(voucherCode);

        if (ViewModel.Voucher == null)
        {
            ShowVoucherWarning("This voucher does not exist or has been used.");
        }
        else if (ViewModel.Voucher.Quantity <= 0)
        {
            ShowVoucherWarning("The number of voucher uses has exceeded the limit.");
            ViewModel.Voucher = null;
        }
        else
        {
            VoucherWarning.Visibility = Visibility.Collapsed;
            DiscountPercentageTextBlock.Visibility = Visibility.Visible;
            DiscountAmountTextBlock.Visibility = Visibility.Visible;
        }
    }

    private void ShowVoucherWarning(string message)
    {
        VoucherWarning.Text = message;
        VoucherWarning.Visibility = Visibility.Visible;
    }

    private void DiscountPercentageTextBlock_Click(Microsoft.UI.Xaml.Documents.Hyperlink sender, Microsoft.UI.Xaml.Documents.HyperlinkClickEventArgs args)
    {
        FlyoutBase.ShowAttachedFlyout(DiscountPercentageTextBlock);
    }

    private void QuestionButton_Click(object sender, RoutedEventArgs e)
    {
        FlyoutBase.ShowAttachedFlyout(QuestionButton);
    }

    private void VoucherInputTextBox_KeyDown(object sender, Microsoft.UI.Xaml.Input.KeyRoutedEventArgs e)
    {
        if (e.Key == Windows.System.VirtualKey.Enter)
        {
            ApplyVoucherButton_click(sender, e);
            e.Handled = true;
        }
    }
}
