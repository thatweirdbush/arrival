using BookingManagementSystem.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace BookingManagementSystem.Views;

public sealed partial class NotificationPage : Page
{
    public NotificationViewModel ViewModel
    {
        get;
    }

    public NotificationPage()
    {
        ViewModel = App.GetService<NotificationViewModel>();
        InitializeComponent();
    }
}
