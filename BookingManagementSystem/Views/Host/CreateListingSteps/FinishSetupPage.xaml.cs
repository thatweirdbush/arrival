using BookingManagementSystem.ViewModels.Host.CreateListingSteps;

using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace BookingManagementSystem.Views.Host.CreateListingSteps;

public sealed partial class FinishSetupPage : Page
{
    public FinishSetupViewModel? ViewModel
    {
        get; set;
    }

    public FinishSetupPage()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        if (e.Parameter is FinishSetupViewModel viewModel)
        {
            ViewModel = viewModel;
        }
        base.OnNavigatedTo(e);
    }
}
