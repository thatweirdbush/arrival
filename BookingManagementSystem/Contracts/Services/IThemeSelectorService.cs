using Microsoft.UI.Xaml;

namespace BookingManagementSystem.Contracts.Services;
public interface IThemeSelectorService
{
    ElementTheme Theme { get; }

    Task InitializeAsync();

    Task SetThemeAsync(ElementTheme theme);

    Task SetRequestedThemeAsync();
}
