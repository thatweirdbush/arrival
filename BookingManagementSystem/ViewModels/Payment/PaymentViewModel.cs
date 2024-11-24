using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.Core.Contracts.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using BookingManagementSystem.Core.Models;
using Windows.System;
using System.Security.Cryptography.X509Certificates;
using System.Collections.ObjectModel;

namespace BookingManagementSystem.ViewModels.Payment;

public partial class PaymentViewModel : ObservableRecipient
{
    private readonly INavigationService _navigationService;
    private readonly IDao _dao;

    [ObservableProperty]
    private Property? item;
    public ObservableCollection<Review> Reviews { get; set; } = [];
    public IEnumerable<DestinationTypeSymbol> DestinationTypeSymbols { get; set; } = Enumerable.Empty<DestinationTypeSymbol>();
    public IEnumerable<Voucher> Vouchers { get; private set; } = Enumerable.Empty<Voucher>();

    public PaymentViewModel(INavigationService navigationService, IDao dao)
    {
        _navigationService = navigationService;
        _dao = dao;
        OnNavigatedTo(_dao);
    }

    public async void OnNavigatedTo(object parameter)
    {
        var vouchers = await _dao.GetVoucherListDataAsync();
        Vouchers = vouchers;
    }

    public void OnNavigatedFrom()
    {

    }

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
}
