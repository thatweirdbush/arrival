using BookingManagementSystem.Contracts.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BookingManagementSystem.ViewModels.Host.CreateListingSteps;

public partial class AboutYourPlaceViewModel : BaseStepViewModel
{
    public AboutYourPlaceViewModel()
    {
    }

    public override void ValidateStep()
    {
        IsStepCompleted = true;
    }
}
