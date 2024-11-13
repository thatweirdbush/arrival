using CommunityToolkit.Mvvm.ComponentModel;

namespace BookingManagementSystem.ViewModels.Host.CreateListingSteps;

public partial class SetPriceViewModel : ObservableRecipient
{
    public const decimal DefaultPrice = 10m;
    public const decimal GuestServiceFeeRate = 0.14m;
    public const decimal HostServiceFeeRate = 0.03m;

    [ObservableProperty]
    private decimal basePrice = DefaultPrice;

    partial void OnBasePriceChanged(decimal value)
    {
        // Update dependent properties when basePrice changes
        GuestServiceFee = Math.Round(value * GuestServiceFeeRate, 0);
        GuestPriceBeforeTax = value + GuestServiceFee;
        HostServiceFee = -1m * Math.Round(value * HostServiceFeeRate, 0);
        YouEarn = value - Math.Round(value * HostServiceFeeRate, 0);
    }

    [ObservableProperty]
    private decimal guestServiceFee;

    [ObservableProperty]
    private decimal guestPriceBeforeTax;

    [ObservableProperty]
    private decimal hostServiceFee;

    [ObservableProperty]
    private decimal youEarn;

    public SetPriceViewModel()
    {
        OnBasePriceChanged(basePrice);
    }
}

