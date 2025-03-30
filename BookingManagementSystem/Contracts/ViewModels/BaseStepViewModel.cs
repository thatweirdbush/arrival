using CommunityToolkit.Mvvm.ComponentModel;

namespace BookingManagementSystem.Contracts.ViewModels;
public abstract partial class BaseStepViewModel : ObservableObject
{
    [ObservableProperty]
    public bool isStepCompleted = false;
    public abstract void ValidateProcess();
    public abstract void SaveProcess();
}
