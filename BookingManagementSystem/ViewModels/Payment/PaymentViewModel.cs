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
    public IEnumerable<Voucher> Vouchers { get; private set; } = [];

    [ObservableProperty]
    public Voucher? voucher;

    [ObservableProperty]
    private Property? item;
    
    [ObservableProperty]
    public int totalNights;
    public decimal TotalAmount { get; set; }
    public decimal TotalAmountBeforeFees { get; set; }
    public decimal TotalAmountAfterFees { get; set; }
    public decimal DiscountAmount { get; set; }

    public decimal Tax = 9.90m;
    public decimal PayPartLaterPrice => TotalAmount / 2;
    public DateTime PayLaterDate => DateTime.Now.AddDays(2);

    public PaymentViewModel(IPaymentFacade paymentFacade)
    {
        _paymentFacade = paymentFacade;
    }

    public async void OnNavigatedTo(object parameter)
    {
        if (parameter is int Id)
        {
            Item = await _paymentFacade.GetPropertyByIdAsync(Id);
            Vouchers = await _paymentFacade.GetVouchersAsync();

            // Initialize core properties
            TotalNights = 5;
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
