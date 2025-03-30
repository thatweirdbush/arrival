using BookingManagementSystem.Contracts.ViewModels;

namespace BookingManagementSystem.ViewModels.Host.CreateListingSteps;

public partial class FinishSetupViewModel : BaseStepViewModel
{
    public FinishSetupViewModel()
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
