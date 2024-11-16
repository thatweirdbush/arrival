using BookingManagementSystem.Contracts.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BookingManagementSystem.ViewModels.Host.CreateListingSteps;

public partial class StandOutViewModel : BaseStepViewModel
{
    public StandOutViewModel()
    {
        IsStepCompleted = true;
    }
    public override void ValidateStep()
    {
    }
}
