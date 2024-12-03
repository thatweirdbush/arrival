using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;

using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.Helpers;
using BookingManagementSystem.ViewModels;

using Windows.System;
using BookingManagementSystem.Views.Host;
using BookingManagementSystem.Views.Account;
using BookingManagementSystem.Views.Client;
using BookingManagementSystem.ViewModels.Account;
using BookingManagementSystem.Core.Models;
using Microsoft.UI.Xaml.Controls.Primitives;

namespace BookingManagementSystem.Views;

// TODO: Update NavigationViewItem titles and icons in ShellPage.xaml.
public partial class ShellPage : Page
{
    public ShellViewModel ViewModel
    {
        get;
    }

    public LoginViewModel LoginViewModel
    {
        get;
    }

    // Variable to check login status
    private bool isLoggedIn = false;

    // List of MenuItems
    private List<string> MenuItems
    {
        get;
    } = new()
    {
        "Home - Hotels & Apartments",
        "Rental Details",
        "Map Services",
        "Hosting"
    };

    public ShellPage(ShellViewModel viewModel, LoginViewModel loginViewModel)
    {
        ViewModel = viewModel;
        LoginViewModel = loginViewModel;
        InitializeComponent();

        // Subscribe to the UserLoggedIn event
        loginViewModel.UserLoggedIn += OnUserLoggedIn;

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
                    return item.Contains(key, StringComparison.CurrentCultureIgnoreCase);
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
        var itemTag = clickedItem?.Tag?.ToString();
        if (itemTag != null)
        {
            switch (itemTag)
            {
                case "signup":
                    NavigationFrame.Navigate(typeof(SignupPage));
                    break;
                case "login":
                    NavigationFrame.Navigate(typeof(LoginPage));
                    break;
                case "logout":
                    OnUserLoggedOut();
                    break;
                case "host":
                    NavigationFrame.Navigate(typeof(ListingPage));
                    break;
                case "setting":
                    NavigationFrame.Navigate(typeof(SettingsPage));
                    break;
                case "account":
                    NavigationFrame.Navigate(typeof(LoyaltyProgramPage));
                    break;
                case "trips":
                    NavigationFrame.Navigate(typeof(BookingHistoryPage));
                    break;
                case "wishlists":
                    NavigationFrame.Navigate(typeof(WishlistPage));
                    break;
                default:
                    break;
            }
        }
    }

    private void OnUserLoggedIn(BookingManagementSystem.Core.Models.User user)
    {
        isLoggedIn = true;
        txtUsername.Text = user.FullName;
        UserProfilePicture.DisplayName = user.FullName;
        NavigationFrame.Navigate(typeof(LoginPage));
        UserLoginStatusChanged();
    }

    private void OnUserLoggedOut()
    {
        isLoggedIn = false;
        txtUsername.Text = "Sign In";
        UserProfilePicture.DisplayName = "";
        NavigationFrame.Navigate(typeof(HomePage));
        UserLoginStatusChanged(false);
    }

    private void UpdateMenuFlyoutVisibility()
    {
        // Display items for logged in users
        HostMenuItem.Visibility = isLoggedIn ? Visibility.Visible : Visibility.Collapsed;
        TripsMenuItem.Visibility = isLoggedIn ? Visibility.Visible : Visibility.Collapsed;
        WishlistsMenuItem.Visibility = isLoggedIn ? Visibility.Visible : Visibility.Collapsed;
        NoAccountSeparator.Visibility = !isLoggedIn ? Visibility.Visible : Visibility.Collapsed;
        LoggedInSeparator.Visibility = isLoggedIn ? Visibility.Visible : Visibility.Collapsed;
        //AccountMenuItem.Visibility = isLoggedIn ? Visibility.Visible : Visibility.Collapsed;
        LogoutMenuItem.Visibility = isLoggedIn ? Visibility.Visible : Visibility.Collapsed;

        // Hide items for non-logged in users
        SignUpMenuItem.Visibility = isLoggedIn ? Visibility.Collapsed : Visibility.Visible;
        LoginMenuItem.Visibility = isLoggedIn ? Visibility.Collapsed : Visibility.Visible;
    }

    private void UserLoginStatusChanged(bool loggedIn = true)
    {
        isLoggedIn = loggedIn;
        UpdateMenuFlyoutVisibility();
    }

    private void MultilingualButton_Click(object sender, RoutedEventArgs e)
    {
        var clickedItem = sender as MenuFlyoutItem;
        var itemTag = clickedItem?.Tag?.ToString();
        if (itemTag != null)
        {
            switch (itemTag)
            {
                case "en-us":
                    break;
                case "vi-vn":
                    break;
                default:
                    break;
            }
        }
    }

    private async void NotificationToggleButton_Click(object sender, RoutedEventArgs e)
    {
        var element = (sender as ToggleButton)!.Tag;
        switch (element)
        {
            case "all":
                {   // Uncheck the UnreadNotificationToggleButton
                    UnreadNotificationToggleButton.IsChecked = false;
                    AllNotificationToggleButton.IsChecked = true;

                    // Reload the notification list
                    await ViewModel.LoadNotificationData();
                    break;
                }
            case "unread":
                {   // Uncheck the AllNotificationToggleButton
                    AllNotificationToggleButton.IsChecked = false;
                    UnreadNotificationToggleButton.IsChecked = true;

                    // Reload the notification list
                    await ViewModel.LoadNotificationData(isUnreadFilter: true);
                    break;
                }
        }
    }

    private void NotificationListView_ItemClick(object sender, ItemClickEventArgs e)
    {
        ViewModel.MarkAsReadSingleItem((Notification)e.ClickedItem);
    }
}
