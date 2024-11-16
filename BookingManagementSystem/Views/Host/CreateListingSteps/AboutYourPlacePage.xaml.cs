using BookingManagementSystem.ViewModels.Host.CreateListingSteps;

using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace BookingManagementSystem.Views.Host.CreateListingSteps;

public sealed partial class AboutYourPlacePage : Page
{
    public AboutYourPlaceViewModel? ViewModel
    {
        get; set;
    }

    public AboutYourPlacePage()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        if (e.Parameter is AboutYourPlaceViewModel viewModel)
        {
            ViewModel = viewModel;
        }
        base.OnNavigatedTo(e);
    }
}
