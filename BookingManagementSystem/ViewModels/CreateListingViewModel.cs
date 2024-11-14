using System.Collections.ObjectModel;
using System.Windows.Input;
using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.Core.Contracts.Repositories;
using BookingManagementSystem.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BookingManagementSystem.ViewModels;

public partial class CreateListingViewModel : ObservableRecipient
{
    // Stored property for the Property model through each step
    public Property PropertyOnCreating = new()
    {
        Id = new Random().Next(1000, 9999),
        Status = PropertyStatus.InProgress,
    };

    private readonly IRepository<Property> _propertyRepository;

    [ObservableProperty]
    private int currentStageIndex;
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
        get => Stages[CurrentStageIndex];
        set
        {
            if (Stages[CurrentStageIndex] != value)
            {
                Stages[CurrentStageIndex] = value;
                OnPropertyChanged(nameof(CurrentStage));
            }
        }
    }
    public CreateListingViewModel(IRepository<Property> propertyRepository)
    {
        _propertyRepository = propertyRepository;

        CurrentStageIndex = 0;
        CurrentStage = Stages[CurrentStageIndex];

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

    public async void GoForward()
    {
        if (CurrentStageIndex == Stages.Count - 1)
        {
            // Save listing
            await SaveListingAsync();

            // Return to Listings page using BackTrack
            App.GetService<INavigationService>()?.Frame?.GoBack();
            return;
        }
        CurrentStageIndex++;
    }

    public void GoBackward()
    {
        if (CurrentStageIndex == 0)
        {
            // Return to Listings page using BackTrack
            App.GetService<INavigationService>()?.Frame?.GoBack();
            return;
        }
        CurrentStageIndex--;
    }

    public async Task SaveListingAsync()
    {
        await _propertyRepository.AddAsync(PropertyOnCreating);
    }
}
