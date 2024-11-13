using BookingManagementSystem.ViewModels.Host.CreateListingSteps;

using Microsoft.UI.Xaml.Controls;

namespace BookingManagementSystem.Views.Host.CreateListingSteps;

public sealed partial class PlaceStructurePage : Page
{
    public PlaceStructureViewModel ViewModel
    {
        get;
    }

    public PlaceStructurePage()
    {
        ViewModel = App.GetService<PlaceStructureViewModel>();
        InitializeComponent();
    }
}
