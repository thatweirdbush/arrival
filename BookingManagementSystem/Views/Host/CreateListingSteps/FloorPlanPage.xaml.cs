using BookingManagementSystem.ViewModels.Host.CreateListingSteps;

using Microsoft.UI.Xaml.Controls;

namespace BookingManagementSystem.Views.Host.CreateListingSteps;

public sealed partial class FloorPlanPage : Page
{
    public FloorPlanViewModel ViewModel
    {
        get;
    }

    public FloorPlanPage()
    {
        ViewModel = App.GetService<FloorPlanViewModel>();
        InitializeComponent();
    }
}
