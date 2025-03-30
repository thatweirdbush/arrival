using BookingManagementSystem.ViewModels;
using Microsoft.UI.Xaml.Controls;

namespace BookingManagementSystem.Views;

public sealed partial class LoyaltyProgramPage : Page
{
    public LoyaltyProgramViewModel ViewModel
    {
        get;
    }

    public LoyaltyProgramPage()
    {
        ViewModel = App.GetService<LoyaltyProgramViewModel>();
        InitializeComponent();
    }
}
