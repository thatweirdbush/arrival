using BookingManagementSystem.ViewModels.Host.CreateListingSteps;

using Microsoft.UI.Xaml.Controls;

namespace BookingManagementSystem.Views.Host.CreateListingSteps;

public sealed partial class StandOutPage : Page
{
    public StandOutViewModel ViewModel
    {
        get;
    }

    public StandOutPage()
    {
        ViewModel = App.GetService<StandOutViewModel>();
        InitializeComponent();
    }
}
