using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.Core.Contracts.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using BookingManagementSystem.Core.Models;
using Windows.System;
using System.Security.Cryptography.X509Certificates;

namespace BookingManagementSystem.ViewModels;

public partial class PaymentViewModel : ObservableRecipient
{
    private readonly INavigationService _navigationService;
    private readonly IDao _dao;

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
