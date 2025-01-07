using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Controls.Primitives;
using Windows.System;
using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.Helpers;
using BookingManagementSystem.ViewModels;
using BookingManagementSystem.Views.Host;
using BookingManagementSystem.Views.Account;
using BookingManagementSystem.Views.Client;
using BookingManagementSystem.ViewModels.Account;
using BookingManagementSystem.Core.Models;

namespace BookingManagementSystem.Views;

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
    } =
    [
        "Home - Hotels & Apartments",
        "Map Services",
        "Hosting",
        "Notifications",
        "FAQs",
        "Chat Services",
        "Settings"
    ];

    public ShellPage(ShellViewModel viewModel, LoginViewModel loginViewModel)
    {
        ViewModel = viewModel;
        LoginViewModel = loginViewModel;
        InitializeComponent();

        // Subscribe to events
        loginViewModel.UserLoggedIn += OnUserLoggedIn;
        loginViewModel.UserLoggedOut += OnUserLoggedOut;

        ViewModel.NavigationService.Frame = NavigationFrame;
        ViewModel.NavigationViewService.Initialize(NavigationViewControl);

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
    private void MenuAutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
    {
        switch (args.SelectedItem)
        {
            case "Home - Hotels & Apartments":
                NavigationFrame.Navigate(typeof(HomePage));
                break;
            case "Map Services":
                NavigationFrame.Navigate(typeof(MapPage));
                break;
            case "Hosting":
                NavigationFrame.Navigate(typeof(ListingPage));
                break;
            case "Notifications":
                NavigationFrame.Navigate(typeof(NotificationPage));
                break;
            case "FAQs":
                NavigationFrame.Navigate(typeof(FAQPage));
                break;
            case "Chat Services":
                NavigationFrame.Navigate(typeof(ChatPage));
                break;
            case "Settings":
                NavigationFrame.Navigate(typeof(SettingsPage));
                break;
            default:
                break;
        }
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
                    LoginViewModel.Logout();
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

    private async void OnUserLoggedIn(Core.Models.User user)
    {
        // Setup user data
        isLoggedIn = true;
        txtUsername.Text = user.FullName;
        UserProfilePicture.DisplayName = user.FullName;
        NavigationFrame.Navigate(typeof(LoginPage));
        UserLoginStatusChanged();

        // Load user notifications
        await ViewModel.RefreshNotificationsAsync();

        // Setup UI controls based on user role
        if (user.Role == Role.Admin)
        {
            Shell_Admin.Visibility = Visibility.Visible;
        }
    }

    private async void OnUserLoggedOut(Core.Models.User? user)
    {
        // Clear user data
        isLoggedIn = false;
        txtUsername.Text = "Sign In";
        UserProfilePicture.DisplayName = "";
        NavigationFrame.Navigate(typeof(HomePage));
        UserLoginStatusChanged(false);

        // Clear notification data
        await ViewModel.ResetUserNotificationsAsync();

        // Clear UI controls based on user role
        Shell_Admin.Visibility = Visibility.Collapsed;
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

    private void NotificationToggleButton_Click(object sender, RoutedEventArgs e)
    {
        var element = (sender as ToggleButton)!.Tag;
        switch (element)
        {
            case "all":
                {
                    AllNotificationToggleButton.IsChecked = true;
                    UnreadNotificationToggleButton.IsChecked = false;
                    ViewModel.IsSelectedUnreadFilter = false;
                    break;
                }
            case "unread":
                {
                    UnreadNotificationToggleButton.IsChecked = true;
                    AllNotificationToggleButton.IsChecked = false;
                    ViewModel.IsSelectedUnreadFilter = true;
                    break;
                }
        }
    }

    private async void NotificationListView_ItemClick(object sender, ItemClickEventArgs e)
    {
        await ViewModel.MarkAsRead((Notification)e.ClickedItem);
    }

    private async void ScrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
    {
        var scrollViewer = sender as ScrollViewer;
        if (scrollViewer == null) return;

        // Detect when scroll is near the end
        if (scrollViewer.VerticalOffset >= scrollViewer.ScrollableHeight)
        {
            await ViewModel.GetNextNotificationDataPage();
        }
    }
}
