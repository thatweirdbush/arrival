using BookingManagementSystem.Core.Models;
using BookingManagementSystem.ViewModels.Host.CreateListingSteps;

using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace BookingManagementSystem.Views.Host.CreateListingSteps;

public sealed partial class AmenitiesPage : Page
{
    public AmenitiesViewModel ViewModel
    {
        get;
    }

    public AmenitiesPage()
    {
        ViewModel = App.GetService<AmenitiesViewModel>();
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        if (e.Parameter is Property property)
        {
            ViewModel.PropertyOnCreating = property;
        }
        base.OnNavigatedTo(e);
    }

    // TODO: Fix the selection changed event handlers and this method
    private void AddPropertyAmenities(IEnumerable<Amenity> amenities)
    {
        // Check if the property collection is null
        if (ViewModel.PropertyOnCreating?.Amenities == null)
        {
            throw new InvalidOperationException("Property on creating is null.");
        }

        // Add amenities to the property
        foreach (var amenity in amenities)
        {
            // Check duplicate entity
            if (!ViewModel.PropertyOnCreating.Amenities.Any(x => x.Id == amenity.Id))
            {
                ViewModel.PropertyOnCreating.Amenities.Add(amenity);
            }
        }
    }

    private void GuestFavoriteAmenitiesGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // Update the selected amenities
        var data = GuestFavoriteAmenitiesGridView.SelectedItems.Cast<Amenity>();
        AddPropertyAmenities(data);
    }

    private void StandoutAmenitiesGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // Update the selected amenities
        var data = StandoutAmenitiesGridView.SelectedItems.Cast<Amenity>();
        AddPropertyAmenities(data);
    }

    private void SafetyAmenitiesGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // Update the selected amenities
        var data = SafetyAmenitiesGridView.SelectedItems.Cast<Amenity>();
        AddPropertyAmenities(data);
    }
}
