using BookingManagementSystem.Contracts.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BookingManagementSystem.ViewModels.Host.CreateListingSteps;

public partial class PlaceDescriptionViewModel : BaseStepViewModel
{
    public PlaceDescriptionViewModel()
    {
    }

    public override void ValidateStep() => IsStepCompleted = true;
}
