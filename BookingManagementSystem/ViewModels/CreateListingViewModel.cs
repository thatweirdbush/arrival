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

public partial class CreateListingViewModel : ObservableRecipient
{
    // Stored property for the Property model through each step
    private readonly IPropertyService _propertyService;

    // List of content items for each step's ViewModel
    private readonly IRepository<PropertyTypeIcon> _propertyTypeIconRepository;

    [ObservableProperty]
    private int currentStepIndex;
    public BaseStepViewModel CurrentStep => Stages[CurrentStepIndex];

    public readonly ObservableCollection<BaseStepViewModel> Stages = [];

    public readonly Dictionary<string, Type> ViewModelToPageDictionary;
    public Property PropertyOnCreating => _propertyService.PropertyOnCreating;

    public CreateListingViewModel(IPropertyService propertyService, IRepository<PropertyTypeIcon> propertyTypeIconRepository)
    {
        // Property Service is set to Singleton instance
        _propertyService = propertyService;

        // So new Property instance must be created by this ViewModel, which is Transient
        _propertyService.PropertyOnCreating = new()
        {
            Id = new Random().Next(1000, 9999),
            Status = PropertyStatus.InProgress,
        };
        _propertyTypeIconRepository = propertyTypeIconRepository;

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

        CurrentStepIndex = 0;
        GoForwardCommand = new RelayCommand(GoForward, CanGoForward);
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

    public bool CanGoForward()
    {
        return CurrentStep.IsStepCompleted == true;
    }

    public async void GoForward()
    {
        //CurrentStep.ValidateStep();
        //if (!CurrentStep.IsStepCompleted)
        //{
        //    return;
        //}

        if (CurrentStepIndex == Stages.Count - 1)
        {
            // Save the current listing in each step
            await SaveCurrentStepAsync();

            // Update new listing status
            PropertyOnCreating.Status = PropertyStatus.Listed;

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

    public async Task SaveCurrentStepAsync()
    {
        await _propertyService.SavePropertyAsync(_propertyService.PropertyOnCreating);
    }

    public async Task LoadPropertyInProgressAsync(int propertyId)
    {
        _propertyService.PropertyOnCreating = await _propertyService.GetPropertyInProgressAsync(propertyId)
            ?? new Property()
            {
                Id = new Random().Next(1000, 9999),
                Status = PropertyStatus.InProgress,
            };
        OnPropertyChanged(nameof(_propertyService.PropertyOnCreating));
    }
}
