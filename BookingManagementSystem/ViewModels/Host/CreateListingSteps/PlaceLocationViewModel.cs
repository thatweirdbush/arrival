using BookingManagementSystem.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BookingManagementSystem.ViewModels.Host.CreateListingSteps;

public partial class PlaceLocationViewModel : ObservableRecipient
{
    public Property? PropertyOnCreating = null;
    public PlaceLocationViewModel()
    {
    }

    public void SetPropertyCoordinates(double latitude, double longitude)
    {
        if (PropertyOnCreating != null)
        {
            PropertyOnCreating.Latitude = latitude;
            PropertyOnCreating.Longitude = longitude;
        }
    }
}
