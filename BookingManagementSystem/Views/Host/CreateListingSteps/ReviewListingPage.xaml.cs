using BookingManagementSystem.ViewModels.Host.CreateListingSteps;

using Microsoft.UI.Xaml.Controls;

namespace BookingManagementSystem.Views.Host.CreateListingSteps;

public sealed partial class ReviewListingPage : Page
{
    public ReviewListingViewModel ViewModel
    {
        get;
    }

    public ReviewListingPage()
    {
        ViewModel = App.GetService<ReviewListingViewModel>();
        InitializeComponent();
    }
}
