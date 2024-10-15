using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingManagementSystem.Core.Models;
public class Smartphone : INotifyPropertyChanged
{
    // Define XAML Image's Path Declaration
    public static string MS_APPX = "ms-appx:///Assets/smartphone-images/";
    private string _imagePath;

    public event PropertyChangedEventHandler PropertyChanged;

    public int Id
    {
        get; set;
    }
    public string Name
    {
        get; set;
    }
    public string Manufacturer
    {
        get; set;
    }
    public int Price
    {
        get; set;
    }
    public string ImagePath
    {
        get => $"{MS_APPX}{_imagePath}";
        set => _imagePath = value;
    }
    public override string ToString()
    {
        return $"{Name} by {Manufacturer}, starts from ${Price}";
    }
}
