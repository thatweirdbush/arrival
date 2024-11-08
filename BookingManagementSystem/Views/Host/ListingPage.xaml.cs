using BookingManagementSystem.ViewModels.Host;
using Microsoft.UI.Xaml.Controls;

namespace BookingManagementSystem.Views.Host;

public sealed partial class ListingPage : Page
{
    public ListingViewModel ViewModel
    {
        get;
    }

    public ListingPage()
    {
        ViewModel = App.GetService<ListingViewModel>();
        InitializeComponent();
    }
}
