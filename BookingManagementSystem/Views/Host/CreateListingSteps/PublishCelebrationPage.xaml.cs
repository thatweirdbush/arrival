using BookingManagementSystem.ViewModels.Host.CreateListingSteps;

using Microsoft.UI.Xaml.Controls;

namespace BookingManagementSystem.Views.Host.CreateListingSteps;

public sealed partial class PublishCelebrationPage : Page
{
    public PublishCelebrationViewModel ViewModel
    {
        get;
    }

    public PublishCelebrationPage()
    {
        ViewModel = App.GetService<PublishCelebrationViewModel>();
        InitializeComponent();
    }
}
