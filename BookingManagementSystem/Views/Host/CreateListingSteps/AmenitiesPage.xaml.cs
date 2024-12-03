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
        // Initialize the selected amenities
        TryInitializeSelectedAmenities();

        base.OnNavigatedTo(e);
    }

    private async void TryInitializeSelectedAmenities()
    {
        // Wait for UI to finish rendering the GridViews
        // Considering replace with GridView's Loaded/LayoutUpdated events
        await Task.Delay(50);

        foreach (var amenity in ViewModel!.SelectedGuestFavoriteAmenities)
        {
            GuestFavoriteAmenitiesGridView.SelectedItems.Add(amenity);
        }
        foreach (var amenity in ViewModel.SelectedStandoutAmenities)
        {
            StandoutAmenitiesGridView.SelectedItems.Add(amenity);
        }
        foreach (var amenity in ViewModel.SelectedSafetyAmenities)
        {
            SafetyAmenitiesGridView.SelectedItems.Add(amenity);
        }
    }


    private void GuestFavoriteAmenitiesGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // Update the selected Guest Favorite amenities
        ViewModel!.SelectedGuestFavoriteAmenities = GuestFavoriteAmenitiesGridView.SelectedItems.Cast<Amenity>();
    }

    private void StandoutAmenitiesGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // Update the selected Standout amenities
        ViewModel!.SelectedStandoutAmenities = StandoutAmenitiesGridView.SelectedItems.Cast<Amenity>();
    }

    private void SafetyAmenitiesGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // Update the selected Safety amenities
        ViewModel!.SelectedSafetyAmenities = SafetyAmenitiesGridView.SelectedItems.Cast<Amenity>();
    }
}
