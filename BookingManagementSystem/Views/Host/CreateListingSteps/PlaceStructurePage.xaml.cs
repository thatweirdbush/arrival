using BookingManagementSystem.Core.Models;
using BookingManagementSystem.ViewModels.Host.CreateListingSteps;

using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace BookingManagementSystem.Views.Host.CreateListingSteps;

public sealed partial class PlaceStructurePage : Page
{
    public PlaceStructureViewModel? ViewModel
    {
        get; set;
    }

    public PlaceStructurePage()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        if (e.Parameter is PlaceStructureViewModel viewModel)
        {
            ViewModel = viewModel;
        }
        base.OnNavigatedTo(e);
    }
}
