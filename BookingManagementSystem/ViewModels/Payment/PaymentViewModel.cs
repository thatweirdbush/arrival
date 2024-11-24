using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.Core.Contracts.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using BookingManagementSystem.Core.Models;
using Windows.System;
using System.Security.Cryptography.X509Certificates;
using System.Collections.ObjectModel;
using BookingManagementSystem.Core.Contracts.Facades;
using BookingManagementSystem.Core.Facades;

namespace BookingManagementSystem.ViewModels.Payment;

public partial class PaymentViewModel : ObservableRecipient
{
    //private readonly INavigationService _navigationService;
    //private readonly IDao _dao;

    private readonly IPaymentFacade _paymentFacade;

    [ObservableProperty]
    private Property? item;
    public ObservableCollection<Review> Reviews { get; set; } = [];
    public IEnumerable<DestinationTypeSymbol> DestinationTypeSymbols { get; set; } = Enumerable.Empty<DestinationTypeSymbol>();
    public IEnumerable<Voucher> Vouchers { get; private set; } = Enumerable.Empty<Voucher>();

    public PaymentViewModel(IPaymentFacade paymentFacade, IDao dao)
    {
        //_navigationService = navigationService;
        //_dao = dao;
        //OnNavigatedTo(_dao);

        _paymentFacade = paymentFacade;
    }

    public async void OnNavigatedTo(object parameter)
    {
        //var vouchers = await _dao.GetVoucherListDataAsync();
        //Vouchers = vouchers;
        if (parameter is int Id)
        {
            Item = await _paymentFacade.GetPropertyByIdAsync(Id);
            var reviews = await _paymentFacade.GetReviewsAsync();
            foreach (var review in reviews)
            {
                Reviews.Add(review);
            }

            DestinationTypeSymbols = await _paymentFacade.GetDestinationTypeSymbolsAsync();
        }
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
