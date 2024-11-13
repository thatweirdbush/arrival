using System.Collections.ObjectModel;
using System.Windows.Input;
using BookingManagementSystem.Contracts.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BookingManagementSystem.ViewModels;

public partial class CreateListingViewModel : ObservableRecipient
{
    private int _currentStageIndex;
    public readonly ObservableCollection<string> Stages =
    [
        "AboutYourPlacePage",
        "PlaceStructurePage",
        "PlaceLocationPage",
        "FloorPlanPage",
        "StandOutPage",
        "AmenitiesPage",
        "PlacePhotosPage",
        "PlaceDescriptionPage",
        "FinishSetupPage",
        "SetPricePage",
        "ReviewListingPage",
        "PublishCelebrationPage",
    ];
    public string CurrentStage
    {
        get => Stages[_currentStageIndex];
        set
        {
            if (Stages[_currentStageIndex] != value)
            {
                Stages[_currentStageIndex] = value;
                OnPropertyChanged(nameof(CurrentStage));
            }
        }
    }
    public CreateListingViewModel()
    {
        _currentStageIndex = 0;
        CurrentStage = Stages[_currentStageIndex];

        GoForwardCommand = new RelayCommand(GoForward);
        GoBackwardCommand = new RelayCommand(GoBackward);
    }

    public ICommand GoForwardCommand
    {
        get;
    }
    public ICommand GoBackwardCommand
    {
        get;
    }

    public void GoForward()
    {
        if (_currentStageIndex == Stages.Count - 1)
        {
            // Save listing
            //await SaveListingAsync();

            // Return to Listings page using BackTrack
            App.GetService<INavigationService>()?.Frame?.GoBack();
            return;
        }
        _currentStageIndex++;
        OnPropertyChanged(nameof(CurrentStage));
    }

    public void GoBackward()
    {
        if (_currentStageIndex == 0)
        {
            // Return to Listings page using BackTrack
            App.GetService<INavigationService>()?.Frame?.GoBack();
            return;
        }
        _currentStageIndex--;
        OnPropertyChanged(nameof(CurrentStage));
    }
}
