using BookingManagementSystem.Contracts.ViewModels;
using BookingManagementSystem.Core.Contracts.Services;
using BookingManagementSystem.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BookingManagementSystem.ViewModels.Host.CreateListingSteps;

public partial class SetPriceViewModel : BaseStepViewModel
{
    private readonly IPropertyService _propertyService;

    [ObservableProperty]
    private decimal basePrice = DefaultPrice;

    public Property PropertyOnCreating => _propertyService.PropertyOnCreating!;

    public const decimal DefaultPrice = 10m;
    public const decimal GuestServiceFeeRate = 0.14m;
    public const decimal HostServiceFeeRate = 0.03m;

    partial void OnBasePriceChanged(decimal value)
    {
        // Update dependent properties when basePrice changes
        GuestServiceFee = Math.Round(value * GuestServiceFeeRate, 0);
        GuestPriceBeforeTax = value + GuestServiceFee;
        HostServiceFee = -1m * Math.Round(value * HostServiceFeeRate, 0);
        YouEarn = value - Math.Round(value * HostServiceFeeRate, 0);

        // Validate the step right after this property changes
        ValidateProcess();
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

        // Initialize core properties
        TryInitializePrice();
        OnBasePriceChanged(basePrice);

        // Initial check because we are current using Lost Focus on Price Text Box
        IsStepCompleted = BasePrice > 0.0m;
    }

    public void TryInitializePrice()
    {
        if (PropertyOnCreating.PricePerNight > 0.0m)
        {
            BasePrice = PropertyOnCreating.PricePerNight;
        }
    }

    public override void ValidateProcess()
    {
        IsStepCompleted = BasePrice > 0.0m;
    }

    public override void SaveProcess()
    {
        PropertyOnCreating.PricePerNight = BasePrice;
    }
}

