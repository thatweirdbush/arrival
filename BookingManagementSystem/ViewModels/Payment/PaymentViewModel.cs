using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.Core.Contracts.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using BookingManagementSystem.Core.Models;
using Windows.System;
using System.Security.Cryptography.X509Certificates;
using System.Collections.ObjectModel;
using BookingManagementSystem.Core.Contracts.Facades;
using BookingManagementSystem.Core.Facades;
using BookingManagementSystem.Contracts.ViewModels;

namespace BookingManagementSystem.ViewModels.Payment;

public partial class PaymentViewModel : ObservableRecipient, INavigationAware
{
    private readonly IPaymentFacade _paymentFacade;

    [ObservableProperty]
    private Property? item;
    public IEnumerable<Voucher> Vouchers { get; private set; } = Enumerable.Empty<Voucher>();

    public PaymentViewModel(IPaymentFacade paymentFacade, IDao dao)
    {
        _paymentFacade = paymentFacade;
    }

    public async void OnNavigatedTo(object parameter)
    {
        if (parameter is int Id)
        {
            Item = await _paymentFacade.GetPropertyByIdAsync(Id);

            Vouchers = await _paymentFacade.GetVouchersAsync();
        }
    }

    public void OnNavigatedFrom()
    {

    }

    public string TotalAmount
    {
        get
        {
            var amount = Item.PricePerNight * 5; // Lấy giá trị từ AmountTextBox
            var tax = 9.90m; // Giá trị của TaxTextBox
            return $"${amount - tax:F2}";
        }
    }

    public string PriceWithMultiplier => $"{Item.PricePerNight:C} x5";

    public bool CheckVoucher(string code, ref decimal? discountPecentage)
    {
        var voucher = Vouchers.FirstOrDefault(u => u.Code.Equals(code));
        if (voucher != null)
        {
            discountPecentage = voucher.DiscountPercentage;
            return true;
        }
        return false;
    }

    public bool CheckVoucherExist(string code)
    {
        var voucher = Vouchers.FirstOrDefault(u => u.Code.Equals(code));
        if (voucher != null)
        {
            return true;
        }
        return false;
    }

    public bool CheckVoucherAmount(string code)
    {
        var voucher = Vouchers.FirstOrDefault(u => u.Code.Equals(code));
        if (voucher != null && voucher.Quantity != 0)
        {
            return true;
        }
        return false;
    }

    public async Task AddBookingAsync(Booking booking)
    {
        await _paymentFacade.AddBookingAsync(booking);
    }

    public void VoucherUsed(string code)
    {
        var voucher = Vouchers.FirstOrDefault(u => u.Code.Equals(code));
        if (voucher != null)
        {
            voucher.Quantity -= 1;
        }
    }
}
