using BookingManagementSystem.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace BookingManagementSystem.Views.Administrator;

public sealed partial class ListingRequestPage : Page
{
    public ListingRequestViewModel ViewModel
    {
        get;
    }

    public ListingRequestPage()
    {
        ViewModel = App.GetService<ListingRequestViewModel>();
        InitializeComponent();
    }
}
