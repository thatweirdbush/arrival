using BookingManagementSystem.ViewModels.Host.CreateListingSteps;

using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace BookingManagementSystem.Views.Host.CreateListingSteps;

public sealed partial class PlaceDescriptionPage : Page
{
    public PlaceDescriptionViewModel? ViewModel
    {
        get; set;
    }

    public PlaceDescriptionPage()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        if (e.Parameter is PlaceDescriptionViewModel viewModel)
        {
            ViewModel = viewModel;
        }
        base.OnNavigatedTo(e);
    }

    private void TitleTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        ViewModel?.ValidateStep();
    }

    private void DescriptionTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        ViewModel?.ValidateStep();
    }
}
