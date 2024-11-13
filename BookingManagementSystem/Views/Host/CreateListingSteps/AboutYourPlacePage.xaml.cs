using BookingManagementSystem.ViewModels.Host.CreateListingSteps;

using Microsoft.UI.Xaml.Controls;

namespace BookingManagementSystem.Views.Host.CreateListingSteps;

public sealed partial class AboutYourPlacePage : Page
{
    public AboutYourPlaceViewModel ViewModel
    {
        get;
    }

    public AboutYourPlacePage()
    {
        ViewModel = App.GetService<AboutYourPlaceViewModel>();
        InitializeComponent();
    }
}
