using BookingManagementSystem.Contracts.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BookingManagementSystem.ViewModels.Host.CreateListingSteps;

public partial class ReviewListingViewModel : BaseStepViewModel
{
    public ReviewListingViewModel()
    {
    }

    public override void ValidateStep() => IsStepCompleted = true;
}
