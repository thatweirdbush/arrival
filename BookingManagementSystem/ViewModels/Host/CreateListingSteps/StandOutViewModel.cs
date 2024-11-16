using BookingManagementSystem.Contracts.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BookingManagementSystem.ViewModels.Host.CreateListingSteps;

public partial class StandOutViewModel : BaseStepViewModel
{
    public StandOutViewModel()
    {
    }
    public override void ValidateStep() => IsStepCompleted = true;
}
