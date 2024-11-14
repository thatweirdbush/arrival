using System.Collections.ObjectModel;
using System.Windows.Input;
using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.Core.Contracts.Repositories;
using BookingManagementSystem.Core.Contracts.Services;
using BookingManagementSystem.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BookingManagementSystem.ViewModels;

public partial class CreateListingViewModel : ObservableRecipient
{
    // Stored property for the Property model through each step
    private readonly IPropertyService _propertyService;
    public Property PropertyOnCreating
    {
        get; set;
    } = new()
    {
        Id = new Random().Next(1000, 9999),
        Status = PropertyStatus.InProgress,
    };

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
    public CreateListingViewModel(IPropertyService propertyService)
    {
        _propertyService = propertyService;

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
            // Save the current listing in each step
            await SaveCurrentStepAsync();

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

    public async Task SaveCurrentStepAsync()
    {
        await _propertyService.SavePropertyAsync(PropertyOnCreating);
    }

    public async Task LoadPropertyInProgressAsync(int propertyId)
    {
        PropertyOnCreating = await _propertyService.GetPropertyInProgressAsync(propertyId)
            ?? new Property()
            {
                Id = new Random().Next(1000, 9999),
                Status = PropertyStatus.InProgress,
            };
        OnPropertyChanged(nameof(PropertyOnCreating));
    }
}
