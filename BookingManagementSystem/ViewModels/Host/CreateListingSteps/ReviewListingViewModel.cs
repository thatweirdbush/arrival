using BookingManagementSystem.Contracts.ViewModels;
using BookingManagementSystem.Core.Contracts.Services;
using BookingManagementSystem.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BookingManagementSystem.ViewModels.Host.CreateListingSteps;

public partial class ReviewListingViewModel : BaseStepViewModel
{
    private readonly IPropertyService _propertyService;
    public Property PropertyOnCreating => _propertyService.PropertyOnCreating!;
    public ReviewListingViewModel(IPropertyService propertyService)
    {
        _propertyService = propertyService;
        IsStepCompleted = true;
    }

    public override void ValidateProcess()
    {
    }

    public override void SaveProcess()
    {
    }
}
