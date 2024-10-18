using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;

using BookingManagementSystem.Contracts.ViewModels;
using BookingManagementSystem.Core.Contracts.Services;
using BookingManagementSystem.Core.Models;
using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.Core.Services;
using CommunityToolkit.Mvvm.Input;

namespace BookingManagementSystem.ViewModels;

public partial class HomeViewModel : ObservableRecipient, INavigationAware
{
    private readonly INavigationService _navigationService;

    // Filtered destination data
    private ObservableCollection<FilteredDestination> _filteredDestinations
    {
        get;
    } = new()
    {
        new FilteredDestination(){
            Name = "Amazing Pools",
            ImagePath = "amazing-pools.jpg"
        },
        new FilteredDestination(){
            Name = "Amazing Views",
            ImagePath = "amazing-views.jpg"
        },
        new FilteredDestination(){
            Name = "Beach",
            ImagePath = "beach.jpg"
        },
        new FilteredDestination(){
            Name = "Countryside",
            ImagePath = "countryside.jpg"
        },
        new FilteredDestination(){
            Name = "Farm",
            ImagePath = "farm.jpg"
        },
        new FilteredDestination(){
            Name = "Islands",
            ImagePath = "islands.jpg"
        },
        new FilteredDestination(){
            Name = "Lakefront",
            ImagePath = "lakefront.jpg"
        },
        new FilteredDestination(){
            Name = "Luxe",
            ImagePath = "luxe.jpg"
        },
        new FilteredDestination(){
            Name = "Mansions",
            ImagePath = "mansions.jpg"
        },
        new FilteredDestination(){
            Name = "National Parks",
            ImagePath = "national-parks.jpg"
        },
        new FilteredDestination(){
            Name = "Room",
            ImagePath = "room.jpg"
        },
        new FilteredDestination(){
            Name = "Tiny Homes",
            ImagePath = "tiny-homes.jpg"
        },
        new FilteredDestination(){
            Name = "Treehouses",
            ImagePath = "treehouses.jpg"
        },
        new FilteredDestination(){
            Name = "Top Cities",
            ImagePath = "top-cities.jpg"
        },
        new FilteredDestination(){
            Name = "Trending",
            ImagePath = "trending.jpg"
        },
        new FilteredDestination(){
            Name = "Tropical",
            ImagePath = "tropical.jpg"
        },
    };

    public ObservableCollection<FilteredDestination> FilteredDestinations => _filteredDestinations;

    // Prepare sample data
    private ObservableCollection<Smartphone> _smartphones
    {
        get;
    } = new()
    {
        new Smartphone(){
            Id = 0,
            Name = "iPhone 16 Pro Max",
            Manufacturer = "Apple",
            Price = 2099,
            ImagePath = "iphone-16-pro-max.png"
        },
        new Smartphone(){
            Id = 1,
            Name = "iPhone 15 Pro Max",
            Manufacturer = "Apple",
            Price = 2999,
            ImagePath = "iphone-15-pro-max-titanium.png"
        },
        new Smartphone(){
            Id = 2,
            Name = "Galaxy S24 Ultra",
            Manufacturer = "Samsung",
            Price = 1999,
            ImagePath = "s24-ultra.png"
        },
        new Smartphone(){
            Id = 3,
            Name = "Xiaomi 14 Ultra",
            Manufacturer = "Xiaomi",
            Price = 1499,
            ImagePath = "xiaomi-14-ultra.png"
        },
        new Smartphone(){
            Id = 4,
            Name = "Pixel 9 Pro",
            Manufacturer = "Google",
            Price = 1799,
            ImagePath = "pixel-9-pro.png"
        },
        new Smartphone(){
            Id = 5,
            Name = "OnePlus 12",
            Manufacturer = "OnePlus",
            Price = 1599,
            ImagePath = "oneplus-12.png"
        },
        new Smartphone(){
            Id = 6,
            Name = "Find X7 Ultra",
            Manufacturer = "OPPO",
            Price = 1699,
            ImagePath = "oppo-find-x7-ultra.png"
        },
        new Smartphone(){
            Id = 7,
            Name = "X100 Pro",
            Manufacturer = "Vivo",
            Price = 1799,
            ImagePath = "vivo-x100-pro.png"
        },
        new Smartphone(){
            Id = 8,
            Name = "iPhone 14",
            Manufacturer = "Apple",
            Price = 1499,
            ImagePath = "iphone-14-pink.png"
        },
        new Smartphone(){
            Id = 9,
            Name = "iPhone 16 Plus",
            Manufacturer = "Apple",
            Price = 1299,
            ImagePath = "iphone-16-plus.png"
        },
        new Smartphone(){
            Id = 10,
            Name = "iPhone 16 Pro Max",
            Manufacturer = "Apple",
            Price = 2099,
            ImagePath = "iphone-16-pro-max.png"
        },
        new Smartphone(){
            Id = 6,
            Name = "Find X7 Ultra",
            Manufacturer = "OPPO",
            Price = 1699,
            ImagePath = "oppo-find-x7-ultra.png"
        },
        new Smartphone(){
            Id = 1,
            Name = "iPhone 15 Pro Max",
            Manufacturer = "Apple",
            Price = 2999,
            ImagePath = "iphone-15-pro-max-titanium.png"
        },
        new Smartphone(){
            Id = 6,
            Name = "Find X7 Ultra",
            Manufacturer = "OPPO",
            Price = 1699,
            ImagePath = "oppo-find-x7-ultra.png"
        },
        new Smartphone(){
            Id = 6,
            Name = "Find X7 Ultra",
            Manufacturer = "OPPO",
            Price = 1699,
            ImagePath = "oppo-find-x7-ultra.png"
        },
        new Smartphone(){
            Id = 1,
            Name = "iPhone 15 Pro Max",
            Manufacturer = "Apple",
            Price = 2999,
            ImagePath = "iphone-15-pro-max-titanium.png"
        },
        new Smartphone(){
            Id = 6,
            Name = "Find X7 Ultra",
            Manufacturer = "OPPO",
            Price = 1699,
            ImagePath = "oppo-find-x7-ultra.png"
        },
        new Smartphone(){
            Id = 1,
            Name = "iPhone 15 Pro Max",
            Manufacturer = "Apple",
            Price = 2999,
            ImagePath = "iphone-15-pro-max-titanium.png"
        },
        new Smartphone(){
            Id = 6,
            Name = "Find X7 Ultra",
            Manufacturer = "OPPO",
            Price = 1699,
            ImagePath = "oppo-find-x7-ultra.png"
        },
        new Smartphone(){
            Id = 6,
            Name = "Find X7 Ultra",
            Manufacturer = "OPPO",
            Price = 1699,
            ImagePath = "oppo-find-x7-ultra.png"
        },
        new Smartphone(){
            Id = 6,
            Name = "Find X7 Ultra",
            Manufacturer = "OPPO",
            Price = 1699,
            ImagePath = "oppo-find-x7-ultra.png"
        },
        new Smartphone(){
            Id = 6,
            Name = "Find X7 Ultra",
            Manufacturer = "OPPO",
            Price = 1699,
            ImagePath = "oppo-find-x7-ultra.png"
        },
        new Smartphone(){
            Id = 6,
            Name = "Find X7 Ultra",
            Manufacturer = "OPPO",
            Price = 1699,
            ImagePath = "oppo-find-x7-ultra.png"
        },
        new Smartphone(){
            Id = 6,
            Name = "Find X7 Ultra",
            Manufacturer = "OPPO",
            Price = 1699,
            ImagePath = "oppo-find-x7-ultra.png"
        },
        new Smartphone(){
            Id = 6,
            Name = "Find X7 Ultra",
            Manufacturer = "OPPO",
            Price = 1699,
            ImagePath = "oppo-find-x7-ultra.png"
        },
        new Smartphone(){
            Id = 6,
            Name = "Find X7 Ultra",
            Manufacturer = "OPPO",
            Price = 1699,
            ImagePath = "oppo-find-x7-ultra.png"
        },
        new Smartphone(){
            Id = 6,
            Name = "Find X7 Ultra",
            Manufacturer = "OPPO",
            Price = 1699,
            ImagePath = "oppo-find-x7-ultra.png"
        },
        new Smartphone(){
            Id = 6,
            Name = "Find X7 Ultra",
            Manufacturer = "OPPO",
            Price = 1699,
            ImagePath = "oppo-find-x7-ultra.png"
        },
        new Smartphone(){
            Id = 6,
            Name = "Find X7 Ultra",
            Manufacturer = "OPPO",
            Price = 1699,
            ImagePath = "oppo-find-x7-ultra.png"
        },
        new Smartphone(){
            Id = 6,
            Name = "Find X7 Ultra",
            Manufacturer = "OPPO",
            Price = 1699,
            ImagePath = "oppo-find-x7-ultra.png"
        },
        new Smartphone(){
            Id = 6,
            Name = "Find X7 Ultra",
            Manufacturer = "OPPO",
            Price = 1699,
            ImagePath = "oppo-find-x7-ultra.png"
        },
        new Smartphone(){
            Id = 6,
            Name = "Find X7 Ultra",
            Manufacturer = "OPPO",
            Price = 1699,
            ImagePath = "oppo-find-x7-ultra.png"
        },
        new Smartphone(){
            Id = 6,
            Name = "Find X7 Ultra",
            Manufacturer = "OPPO",
            Price = 1699,
            ImagePath = "oppo-find-x7-ultra.png"
        },
        new Smartphone(){
            Id = 6,
            Name = "Find X7 Ultra",
            Manufacturer = "OPPO",
            Price = 1699,
            ImagePath = "oppo-find-x7-ultra.png"
        },
        new Smartphone(){
            Id = 6,
            Name = "Find X7 Ultra",
            Manufacturer = "OPPO",
            Price = 1699,
            ImagePath = "oppo-find-x7-ultra.png"
        },
        new Smartphone(){
            Id = 6,
            Name = "Find X7 Ultra",
            Manufacturer = "OPPO",
            Price = 1699,
            ImagePath = "oppo-find-x7-ultra.png"
        },
        new Smartphone(){
            Id = 6,
            Name = "Find X7 Ultra",
            Manufacturer = "OPPO",
            Price = 1699,
            ImagePath = "oppo-find-x7-ultra.png"
        },
        new Smartphone(){
            Id = 6,
            Name = "Find X7 Ultra",
            Manufacturer = "OPPO",
            Price = 1699,
            ImagePath = "oppo-find-x7-ultra.png"
        }
    };

    public ObservableCollection<Smartphone> Smartphones => _smartphones;
    public Smartphone? SelectedSmartphone
    {
        get; set;
    }
    private readonly Random random = new();

    public Smartphone GetNextSmartphone()
    {
        var index = random.Next(Smartphones.Count);
        SelectedSmartphone = Smartphones[index];
        return SelectedSmartphone;
    }

    public HomeViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }
    public void OnNavigatedTo(object parameter)
    {
        //Source.Clear();

        //// TODO: Replace with real data.
        //var data = await _sampleDataService.GetContentGridDataAsync();
        //foreach (var item in data)
        //{
        //    Source.Add(item);
        //}
    }

    public void OnNavigatedFrom()
    {
    }

    [RelayCommand]
    private void OnItemClick(Smartphone? clickedItem)
    {
        if (clickedItem != null)
        {
            _navigationService.SetListDataItemForNextConnectedAnimation(clickedItem);
            _navigationService.NavigateTo(typeof(RentalDetailViewModel).FullName!, clickedItem.Id);
        }
    }
}
