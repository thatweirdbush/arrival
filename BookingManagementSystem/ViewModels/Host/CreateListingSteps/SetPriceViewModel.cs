using BookingManagementSystem.Contracts.ViewModels;
using BookingManagementSystem.Core.Contracts.Services;
using BookingManagementSystem.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BookingManagementSystem.ViewModels.Host.CreateListingSteps;

public partial class SetPriceViewModel : BaseStepViewModel
{
    public const decimal DefaultPrice = 10m;
    public const decimal GuestServiceFeeRate = 0.14m;
    public const decimal HostServiceFeeRate = 0.03m;

    [ObservableProperty]
    private decimal basePrice = DefaultPrice;
    private readonly IPropertyService _propertyService;
    public Property PropertyOnCreating => _propertyService.PropertyOnCreating;

    partial void OnBasePriceChanged(decimal value)
    {
        // Update dependent properties when basePrice changes
        GuestServiceFee = Math.Round(value * GuestServiceFeeRate, 0);
        GuestPriceBeforeTax = value + GuestServiceFee;
        HostServiceFee = -1m * Math.Round(value * HostServiceFeeRate, 0);
        YouEarn = value - Math.Round(value * HostServiceFeeRate, 0);

        // Validate the step right after this property changes
        ValidateStep();
    }

    [ObservableProperty]
    private decimal guestServiceFee;

    [ObservableProperty]
    private decimal guestPriceBeforeTax;

    [ObservableProperty]
    private decimal hostServiceFee;

    [ObservableProperty]
    private decimal youEarn;

    public SetPriceViewModel(IPropertyService propertyService)
    {
        _propertyService = propertyService;
        OnBasePriceChanged(basePrice);

        // Initial check because we are current using Lost Focus on Price Text Box
        IsStepCompleted = BasePrice > 0.0m;
    }

    public override void ValidateStep()
    {
        PropertyOnCreating.PricePerNight = BasePrice;
        IsStepCompleted = BasePrice > 0.0m;
    }
}

