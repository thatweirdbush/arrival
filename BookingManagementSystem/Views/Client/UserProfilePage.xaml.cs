using BookingManagementSystem.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace BookingManagementSystem.Views.Client;

public sealed partial class UserProfilePage : Page
{
    public UserProfileViewModel ViewModel
    {
        get;
    }

    public UserProfilePage()
    {
        ViewModel = App.GetService<UserProfileViewModel>();
        InitializeComponent();
    }
}
