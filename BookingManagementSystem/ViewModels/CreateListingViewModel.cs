using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.Contracts.ViewModels;
using BookingManagementSystem.Core.Contracts.Services;
using BookingManagementSystem.Core.Models;
using BookingManagementSystem.ViewModels.Host;
using BookingManagementSystem.ViewModels.Host.CreateListingSteps;
using BookingManagementSystem.Views.Host.CreateListingSteps;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Controls;

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

    private int previousStageIndex = 0;

    // Property to hold the Frame for steps' pages
    public Frame? ContentFrame { get; set; }

    public BaseStepViewModel CurrentStep => Stages[CurrentStepIndex];

    public ObservableCollection<BaseStepViewModel> Stages = [];

    public Dictionary<string, Type> ViewModelToPageDictionary = [];

    public Property PropertyOnCreating => _propertyService.PropertyOnCreating!;    

    public ICommand GoForwardCommand { get; }

    public ICommand GoBackwardCommand { get; }

    public IAsyncRelayCommand SaveAndExitAsyncCommand { get; }

    public CreateListingViewModel(IPropertyService propertyService, INavigationService navigationService)
    {
        // Dependency Injection workarounds
        _propertyService = propertyService;
        _navigationService = navigationService;

        // Initialize Observable Properties
        CurrentStepIndex = 0;
        IsLastStepCompleted = false;

        // Initilize Commands
        GoForwardCommand = new RelayCommand(GoForward);
        GoBackwardCommand = new RelayCommand(GoBackward);
        SaveAndExitAsyncCommand = new AsyncRelayCommand(SaveAndExitAsync);
    }

    public void OnNavigatedTo(object? parameter)
    {
        // Check if we are editing an In-progress Property
        if (parameter is int Id)
        {
            // If there is a Property Id, we are editing an In-progress Property
            Task.Run(async () =>
            {
                // Load the Property from the database
                _propertyService.PropertyOnCreating = await _propertyService.GetPropertyInProgressAsync(Id);
            }).Wait();

            // Initialize steps' ViewModel
            InitializeSteps();

            // Update the current step index to the latest step
            var lastEditedStep = PropertyOnCreating.LastEditedStep;
            if (lastEditedStep != -1)
            {
                CurrentStepIndex = lastEditedStep;
            }
        }
        else
        {
            // Property Service is set to Singleton instance
            // So new Property instance must be created in this ViewModel, which is Transient
            _propertyService.PropertyOnCreating = new()
            {
                Status = PropertyStatus.InProgress
            };

            // Initialize steps' ViewModel
            InitializeSteps();

            // Initialize the first step
            CurrentStepIndex = 0;
        }

        // Manually call the method to update the UI
        OnCurrentStepIndexChanged(CurrentStepIndex);
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

    public async void GoForward()
    {
        // If can go forward, the current step has been validated
        // The below method will only save the current step's data to the PropertyOnCreating instance
        // Not yet to database
        CurrentStep.SaveProcess();

        // If current step is the last step
        if (CurrentStepIndex == Stages.Count - 1)
        {
            // Update new listing status
            PropertyOnCreating.Status = PropertyStatus.Listed;

            // Save new listing to the database
            await SaveCurrentStepAsync();

            // Return to Listings page
            _navigationService.NavigateTo(typeof(ListingViewModel).FullName!);
            return;
        }
        CurrentStepIndex++;
    }

    public void GoBackward()
    {
        if (CurrentStepIndex == 0)
        {
            // Return to Listings page using BackTrack
            _navigationService.Frame?.GoBack();
            return;
        }
        CurrentStepIndex--;
    }

    partial void OnCurrentStepIndexChanged(int value)
    {
        // Get the page type based on the current ViewModel name
        if (ViewModelToPageDictionary.TryGetValue(CurrentStep.GetType().Name, out var pageType))
        {
            var slideNavigationTransitionEffect =
                CurrentStepIndex - previousStageIndex > 0 ?
                    SlideNavigationTransitionEffect.FromRight :
                    SlideNavigationTransitionEffect.FromLeft;

            ContentFrame?.Navigate(pageType, CurrentStep, new SlideNavigationTransitionInfo()
            {
                Effect = slideNavigationTransitionEffect
            });

            previousStageIndex = CurrentStepIndex;
        }

        // Check if the current step is the last step
        IsLastStepCompleted = value == Stages.Count - 1;
    }

    public async Task SaveCurrentStepAsync(int stepIndex = -1)
    {
        PropertyOnCreating.LastEditedStep = stepIndex;

        await _propertyService.SavePropertyAsync(PropertyOnCreating);
    }

    public async Task SaveAndExitAsync()
    {
        // Display dialog to confirm saving and exiting
        var result = await new ContentDialog
        {
            XamlRoot = App.MainWindow.Content.XamlRoot,
            Title = "Save and Exit",
            Content = "Are you sure you want to save and exit?",
            PrimaryButtonText = "Save and Exit",
            CloseButtonText = "Cancel",
            DefaultButton = ContentDialogButton.Primary
        }.ShowAsync();

        if (result == ContentDialogResult.Primary)
        {
            await SaveCurrentStepAsync(CurrentStepIndex);

            // Return to Listings page using BackTrack
            _navigationService.GoBack();
        }
    }
}
