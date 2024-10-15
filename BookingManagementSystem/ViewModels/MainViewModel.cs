using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using BookingManagementSystem.Core.Models;

namespace BookingManagementSystem.ViewModels;

public partial class MainViewModel : ObservableRecipient
{
    // Prepare sample data
    private ObservableCollection<Smartphone> _smartphones { get; } = new()
    {
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

    public MainViewModel()
    {
    }
}
