using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;

using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.Helpers;
using BookingManagementSystem.ViewModels;

using Windows.System;

namespace BookingManagementSystem.Views;

// TODO: Update NavigationViewItem titles and icons in ShellPage.xaml.
public partial class ShellPage : Page
{
    public ShellViewModel ViewModel
    {
        get;
    }

    // Variable to check login status
    private bool isSignedIn = false;

    private string username = "John Doe";

    // List of MenuItems
    private List<string> MenuItems { get; } = new()
    {
        "Home",
        "Smartphones"
    };

    public ShellPage(ShellViewModel viewModel)
    {
        ViewModel = viewModel;
        InitializeComponent();

        ViewModel.NavigationService.Frame = NavigationFrame;
        ViewModel.NavigationViewService.Initialize(NavigationViewControl);

        // TODO: Set the title bar icon by updating /Assets/WindowIcon.ico.
        // A custom title bar is required for full window theme and Mica support.
        // https://docs.microsoft.com/windows/apps/develop/title-bar?tabs=winui3#full-customization
        App.MainWindow.ExtendsContentIntoTitleBar = true;
        App.MainWindow.SetTitleBar(AppTitleBar);
        App.MainWindow.Activated += MainWindow_Activated;
        AppTitleBarText.Text = "AppDisplayName".GetLocalized();
    }

    private void OnLoaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        TitleBarHelper.UpdateTitleBar(RequestedTheme);

        KeyboardAccelerators.Add(BuildKeyboardAccelerator(VirtualKey.Left, VirtualKeyModifiers.Menu));
        KeyboardAccelerators.Add(BuildKeyboardAccelerator(VirtualKey.GoBack));
    }

    private void MainWindow_Activated(object sender, WindowActivatedEventArgs args)
    {
        App.AppTitlebar = AppTitleBarText as UIElement;
    }

    private void NavigationViewControl_DisplayModeChanged(NavigationView sender, NavigationViewDisplayModeChangedEventArgs args)
    {
        AppTitleBar.Margin = new Thickness()
        {
            Left = sender.CompactPaneLength * (sender.DisplayMode == NavigationViewDisplayMode.Minimal ? 2 : 1),
            Top = AppTitleBar.Margin.Top,
            Right = AppTitleBar.Margin.Right,
            Bottom = AppTitleBar.Margin.Bottom
        };
    }

    private static KeyboardAccelerator BuildKeyboardAccelerator(VirtualKey key, VirtualKeyModifiers? modifiers = null)
    {
        var keyboardAccelerator = new KeyboardAccelerator() { Key = key };

        if (modifiers.HasValue)
        {
            keyboardAccelerator.Modifiers = modifiers.Value;
        }

        keyboardAccelerator.Invoked += OnKeyboardAcceleratorInvoked;

        return keyboardAccelerator;
    }

    private static void OnKeyboardAcceleratorInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
    {
        var navigationService = App.GetService<INavigationService>();

        var result = navigationService.GoBack();

        args.Handled = result;
    }

    // Handle text change and present suitable items
    private void MenuAutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
    {
        // Since selecting an item will also change the text,
        // only listen to changes caused by user entering text.
        if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
        {
            var suitableItems = new List<string>();
            var splitText = sender.Text.ToLower().Split(" ");
            foreach (var item in MenuItems)
            {
                var found = splitText.All((key) =>
                {
                    return item.ToLower().Contains(key);
                });
                if (found)
                {
                    suitableItems.Add(item);
                }
            }
            if (suitableItems.Count == 0)
            {
                suitableItems.Add("No results found");
            }
            sender.ItemsSource = suitableItems;
        }
    }

    // Handle user selecting an item, not implemented yet
    private async void MenuAutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
    {
        // Show ContentDialog with the selected item
        var dialog = new ContentDialog
        {
            XamlRoot = Content.XamlRoot,
            Title = "Selected item",
            Content = args.SelectedItem,
            PrimaryButtonText = "Ok",
            CloseButtonText = "Close"
        };

        await dialog.ShowAsync();
    }

    private void UserMenuFlyoutItem_Click(object sender, RoutedEventArgs e)
    {
        var clickedItem = sender as MenuFlyoutItem;
        if (clickedItem.Tag.ToString() == "signin")
        {
            SignInUser();
        }
        else if (clickedItem.Tag.ToString() == "signout")
        {
            SignOutUser();
        }
    }

    private void SignInUser()
    {
        isSignedIn = true;
        NavigationFrame.Navigate(typeof(SignupPage));
        UpdateUserMenu();
    }

    private void SignOutUser()
    {
        isSignedIn = false;
        NavigationFrame.Navigate(typeof(HomePage));
        UpdateUserMenu();
    }

    private void UpdateUserMenu()
    {
        if (isSignedIn)
        {
            txtUsername.Text = username;
            SignInMenuItem.Text = "Sign out";
            SignInMenuItem.Icon = new SymbolIcon(Symbol.Back);
            SignInMenuItem.Tag = "signout";
        }
        else
        {
            username = "Sign In";
            txtUsername.Text = username;
            SignInMenuItem.Text = "Sign in";
            SignInMenuItem.Icon = new SymbolIcon(Symbol.Forward);
            SignInMenuItem.Tag = "signin";
        }
    }
}
