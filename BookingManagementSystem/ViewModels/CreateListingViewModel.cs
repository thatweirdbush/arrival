using System.Collections.ObjectModel;
using System.Windows.Input;
using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.ViewModels.Client;
using BookingManagementSystem.ViewModels.Host;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BookingManagementSystem.ViewModels;

public partial class CreateListingViewModel : ObservableRecipient
{
    private int _currentStageIndex;
    private readonly ObservableCollection<string> _stages = new()
    {
                "AboutYourPlacePage",
                "PlaceStructurePage",
                "AmenitiesPage",
                "StandOutPage",
                "PlacePhotosPage",
            };
    public string CurrentStage
    {
        get => _stages[_currentStageIndex];
        set
        {
            if (_stages[_currentStageIndex] != value)
            {
                _stages[_currentStageIndex] = value;
                OnPropertyChanged(nameof(CurrentStage));
            }
        }
    }
    public CreateListingViewModel()
    {
        _currentStageIndex = 0;
        CurrentStage = _stages[_currentStageIndex];

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
