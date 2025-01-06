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

    private void SetSelectedItems(GridView gridView, IEnumerable<Amenity> selectedItems)
    {
        var amenities = gridView.Items.Cast<Amenity>();
        foreach (var amenity in selectedItems)
        {
            var officialAmenity = amenities.FirstOrDefault(x => x.Id == amenity.Id);
            gridView.SelectedItems.Add(officialAmenity);
        }
    }

    private void GuestFavoriteAmenitiesGridView_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        SetSelectedItems(GuestFavoriteAmenitiesGridView, ViewModel!.SelectedGuestFavoriteAmenities);
    }

    private void StandoutAmenitiesGridView_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        SetSelectedItems(StandoutAmenitiesGridView, ViewModel!.SelectedStandoutAmenities);
    }

    private void SafetyAmenitiesGridView_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        SetSelectedItems(SafetyAmenitiesGridView, ViewModel!.SelectedSafetyAmenities);
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
        // Update the selected Standout amenities
        ViewModel!.SelectedSafetyAmenities = SafetyAmenitiesGridView.SelectedItems.Cast<Amenity>();
    }
}
