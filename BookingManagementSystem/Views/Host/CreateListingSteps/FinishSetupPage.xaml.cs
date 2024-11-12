using BookingManagementSystem.ViewModels.Host.CreateListingSteps;

using Microsoft.UI.Xaml.Controls;

namespace BookingManagementSystem.Views.Host.CreateListingSteps;

public sealed partial class FinishSetupPage : Page
{
    public FinishSetupViewModel ViewModel
    {
        get;
    }

    public FinishSetupPage()
    {
        ViewModel = App.GetService<FinishSetupViewModel>();
        InitializeComponent();
    }
}
