using BookingManagementSystem.ViewModels.Host.CreateListingSteps;

using Microsoft.UI.Xaml.Controls;

namespace BookingManagementSystem.Views.Host.CreateListingSteps;

public sealed partial class PlaceDescriptionPage : Page
{
    public PlaceDescriptionViewModel ViewModel
    {
        get;
    }

    public PlaceDescriptionPage()
    {
        ViewModel = App.GetService<PlaceDescriptionViewModel>();
        InitializeComponent();
    }
}
