using BookingManagementSystem.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace BookingManagementSystem.Views.Host;

public sealed partial class ReservationsPage : Page
{
    public ReservationsViewModel ViewModel
    {
        get;
    }

    public ReservationsPage()
    {
        ViewModel = App.GetService<ReservationsViewModel>();
        InitializeComponent();
    }
}
