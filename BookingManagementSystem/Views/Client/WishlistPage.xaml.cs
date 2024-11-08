using BookingManagementSystem.ViewModels.Client;
using Microsoft.UI.Xaml.Controls;

namespace BookingManagementSystem.Views.Client;

public sealed partial class WishlistPage : Page
{
    public WishlistViewModel ViewModel
    {
        get;
    }

    public WishlistPage()
    {
        ViewModel = App.GetService<WishlistViewModel>();
        InitializeComponent();
    }
}
