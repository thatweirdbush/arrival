using BookingManagementSystem.Core.Models;
using BookingManagementSystem.ViewModels.Host.CreateListingSteps;

using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace BookingManagementSystem.Views.Host.CreateListingSteps;

public sealed partial class AmenitiesPage : Page
{
    public AmenitiesViewModel? ViewModel
    {
        get; set;
    }

    public AmenitiesPage()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        if (e.Parameter is AmenitiesViewModel viewModel)
        {
            ViewModel = viewModel;
        }
        base.OnNavigatedTo(e);
    }

    private void GuestFavoriteAmenitiesGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (ViewModel != null)
        {
            // Update the selected Guest Favorite amenities
            ViewModel.SelectedGuestFavoriteAmenities = GuestFavoriteAmenitiesGridView.SelectedItems.Cast<Amenity>();
        }
    }

    private void StandoutAmenitiesGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (ViewModel != null)
        {
            // Update the selected Standout amenities
            ViewModel.SelectedStandoutAmenities = StandoutAmenitiesGridView.SelectedItems.Cast<Amenity>();
        }
    }

    private void SafetyAmenitiesGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (ViewModel != null)
        {
            // Update the selected Safety amenities
            ViewModel.SelectedSafetyAmenities = SafetyAmenitiesGridView.SelectedItems.Cast<Amenity>();
        }
    }
}
