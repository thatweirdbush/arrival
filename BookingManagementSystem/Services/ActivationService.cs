using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using BookingManagementSystem.Activation;
using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.Views;
using BookingManagementSystem.ViewModels.Account;
using BookingManagementSystem.ViewModels;

namespace BookingManagementSystem.Services;

public class ActivationService : IActivationService
{
    private readonly ActivationHandler<LaunchActivatedEventArgs> _defaultHandler;
    private readonly IEnumerable<IActivationHandler> _activationHandlers;
    private readonly IThemeSelectorService _themeSelectorService;
    private UIElement? _shell = null;

    public ActivationService(ActivationHandler<LaunchActivatedEventArgs> defaultHandler, IEnumerable<IActivationHandler> activationHandlers, IThemeSelectorService themeSelectorService)
    {
        _defaultHandler = defaultHandler;
        _activationHandlers = activationHandlers;
        _themeSelectorService = themeSelectorService;
    }

    public async Task ActivateAsync(object activationArgs)
    {
        // Execute tasks before activation.
        await InitializeAsync();

        // Initialize the MainWindow content.
        await InitializeMainWindowContent();

        // Handle activation via ActivationHandlers.
        await HandleActivationAsync(activationArgs);

        // Activate the MainWindow.
        App.MainWindow.Activate();

        // Execute tasks after activation.
        await StartupAsync();
    }

    private async Task InitializeMainWindowContent()
    {
        // Initialize ViewModels from DI container
        var shellViewModel = App.GetService<ShellViewModel>();
        var loginViewModel = App.GetService<LoginViewModel>();

        // Create ShellPage with passed ViewModels
        _shell = new ShellPage(shellViewModel, loginViewModel);

        // Set the MainWindow Content.
        App.MainWindow.Content = _shell ?? new Frame();

        await Task.CompletedTask;
    }

    private async Task HandleActivationAsync(object activationArgs)
    {
        var activationHandler = _activationHandlers.FirstOrDefault(h => h.CanHandle(activationArgs));

        if (activationHandler != null)
        {
            await activationHandler.HandleAsync(activationArgs);
        }

        if (_defaultHandler.CanHandle(activationArgs))
        {
            await _defaultHandler.HandleAsync(activationArgs);
        }
    }

    private async Task InitializeAsync()
    {
        await _themeSelectorService.InitializeAsync().ConfigureAwait(false);
        await Task.CompletedTask;
    }

    private async Task StartupAsync()
    {
        await _themeSelectorService.SetRequestedThemeAsync();
        await Task.CompletedTask;
    }
}
