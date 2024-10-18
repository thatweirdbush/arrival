using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingManagementSystem.Contracts.ViewModels;
using BookingManagementSystem.Core.Contracts.Services;
using BookingManagementSystem.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BookingManagementSystem.ViewModels;

public partial class RentalDetailViewModel : ObservableRecipient, INavigationAware
{
    [ObservableProperty]
    private Smartphone? item;

    public RentalDetailViewModel()
    {
    }

    public void OnNavigatedTo(object parameter)
    {
        if (parameter is int)
        {
            //var data = await _sampleDataService.GetContentGridDataAsync();
            //Item = data.First(i => i.OrderID == orderID);
            Item = new Smartphone()
            {
                Id = 0,
                Name = "iPhone 16 Pro Max",
                Manufacturer = "Apple",
                Price = 2099,
                ImagePath = "iphone-16-pro-max.png"
            };
        }

    }

    public void OnNavigatedFrom()
    {
    }
}
