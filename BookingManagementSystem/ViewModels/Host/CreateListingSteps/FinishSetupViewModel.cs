using BookingManagementSystem.Contracts.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BookingManagementSystem.ViewModels.Host.CreateListingSteps;

public partial class FinishSetupViewModel : BaseStepViewModel
{
    public FinishSetupViewModel()
    {
        IsStepCompleted = true;
    }

    public override void ValidateStep()
    {
    }
}
