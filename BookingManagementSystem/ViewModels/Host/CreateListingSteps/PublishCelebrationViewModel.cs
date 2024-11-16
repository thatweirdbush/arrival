using BookingManagementSystem.Contracts.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BookingManagementSystem.ViewModels.Host.CreateListingSteps;

public partial class PublishCelebrationViewModel : BaseStepViewModel
{
    public PublishCelebrationViewModel()
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
