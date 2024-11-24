using System.Collections.ObjectModel;
using System.Windows.Input;
using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.Contracts.ViewModels;
using BookingManagementSystem.Core.Contracts.Repositories;
using BookingManagementSystem.Core.Contracts.Services;
using BookingManagementSystem.Core.Models;
using BookingManagementSystem.ViewModels.Client;
using BookingManagementSystem.ViewModels.Host;
using BookingManagementSystem.ViewModels.Host.CreateListingSteps;
using BookingManagementSystem.Views.Host.CreateListingSteps;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BookingManagementSystem.ViewModels;

public partial class CreateListingViewModel : ObservableRecipient, INavigationAware
{
    // Navigation Service DI
    private readonly INavigationService _navigationService;

    // Stored property for the Property model through each step
    private readonly IPropertyService _propertyService;

    [ObservableProperty]
    private int currentStepIndex;

    [ObservableProperty]
    private bool isLastStepCompleted;

    public BaseStepViewModel CurrentStep => Stages[CurrentStepIndex];

    public ObservableCollection<BaseStepViewModel> Stages = [];

    public Dictionary<string, Type> ViewModelToPageDictionary = [];
    public Property PropertyOnCreating => _propertyService.PropertyOnCreating;

    public CreateListingViewModel(IPropertyService propertyService, INavigationService navigationService)
    {
        // Dependency Injection workarounds
        _propertyService = propertyService;
        _navigationService = navigationService;

        // Initialize core properties
        CurrentStepIndex = 0;
        IsLastStepCompleted = false;
        GoForwardCommand = new RelayCommand(GoForward);
        GoBackwardCommand = new RelayCommand(GoBackward);
    }

    public async void OnNavigatedTo(object? parameter)
    {
        // Check if we are editing an In Progress Property
        if (parameter is int Id)
        {
            // Initialize steps' ViewModel
            InitializeSteps();

            // If there is a Property Id, this is the case of editing an In Progress Property
            _propertyService.PropertyOnCreating = await _propertyService.GetPropertyInProgressAsync(Id);

            // Update the current step index to the last step
            var lastEditedStep = PropertyOnCreating.LastEditedStep;
            if (lastEditedStep != -1)
            {
                CurrentStepIndex = lastEditedStep;
            }
        }
        else
        {
            // Property Service is set to Singleton instance
            // So new Property instance must be created by this ViewModel, which is Transient
            _propertyService.PropertyOnCreating = new()
            {
                Id = new Random().Next(1000, 9999),
                Status = PropertyStatus.InProgress,
            };
            // Initialize steps' ViewModel
            InitializeSteps();
        }
    }

    public void OnNavigatedFrom()
    {
    }

    public void InitializeSteps()
    {
        // Using Dependency Injection to get the ViewModel instances
        Stages = [
            App.GetService<AboutYourPlaceViewModel>(),
            App.GetService<PlaceStructureViewModel>(),
            App.GetService<PlaceLocationViewModel>(),
            App.GetService<FloorPlanViewModel>(),
            App.GetService<StandOutViewModel>(),
            App.GetService<AmenitiesViewModel>(),
            App.GetService<PlacePhotosViewModel>(),
            App.GetService<PlaceDescriptionViewModel>(),
            App.GetService<FinishSetupViewModel>(),
            App.GetService<SetPriceViewModel>(),
            App.GetService<ReviewListingViewModel>(),
            App.GetService<PublishCelebrationViewModel>()
            ];

        // Dictionary to map ViewModel to Page
        ViewModelToPageDictionary = new Dictionary<string, Type>
        {
            { nameof(AboutYourPlaceViewModel), typeof(AboutYourPlacePage) },
            { nameof(PlaceStructureViewModel), typeof(PlaceStructurePage) },
            { nameof(PlaceLocationViewModel), typeof(PlaceLocationPage) },
            { nameof(FloorPlanViewModel), typeof(FloorPlanPage) },
            { nameof(StandOutViewModel), typeof(StandOutPage) },
            { nameof(AmenitiesViewModel), typeof(AmenitiesPage) },
            { nameof(PlacePhotosViewModel), typeof(PlacePhotosPage) },
            { nameof(PlaceDescriptionViewModel), typeof(PlaceDescriptionPage) },
            { nameof(FinishSetupViewModel), typeof(FinishSetupPage) },
            { nameof(SetPriceViewModel), typeof(SetPricePage) },
            { nameof(ReviewListingViewModel), typeof(ReviewListingPage) },
            { nameof(PublishCelebrationViewModel), typeof(PublishCelebrationPage) },
        };
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
        // If can go forward, the current step has been validated
        // The below method will only save the current step's data to the PropertyOnCreating instance
        // Not yet to database
        CurrentStep.SaveProcess();
        if (CurrentStepIndex == Stages.Count - 1)
        {
            // Update new listing status
            PropertyOnCreating.Status = PropertyStatus.Listed;

            // Save new listing to the database
            await SaveCurrentStepAsync();   // Current step is the last step

            // Return to Listings page using BackTrack
            App.GetService<INavigationService>().NavigateTo(typeof(ListingViewModel).FullName!);
            return;
        }
        CurrentStepIndex++;
    }

    public void GoBackward()
    {
        if (CurrentStepIndex == 0)
        {
            // Return to Listings page using BackTrack
            App.GetService<INavigationService>()?.Frame?.GoBack();
            return;
        }
        CurrentStepIndex--;
    }

    partial void OnCurrentStepIndexChanged(int value)
    {
        IsLastStepCompleted = value == Stages.Count - 1;
    }

    public async Task SaveCurrentStepAsync()
    {
        await _propertyService.SavePropertyAsync(_propertyService.PropertyOnCreating);
    }
}
