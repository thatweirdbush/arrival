using BookingManagementSystem.Contracts.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BookingManagementSystem.ViewModels.Host.CreateListingSteps;

public partial class AboutYourPlaceViewModel : BaseStepViewModel
{
    public AboutYourPlaceViewModel()
    {
        IsStepCompleted = true;
    }

    public override void ValidateProcess()
    {
    }

    public override void SaveProcess()
    {
    }
}
