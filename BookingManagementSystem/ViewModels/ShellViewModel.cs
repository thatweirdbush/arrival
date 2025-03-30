using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.Core.Models;
using BookingManagementSystem.Views;
using BookingManagementSystem.ViewModels.Account;

using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using Microsoft.UI.Xaml.Navigation;

namespace BookingManagementSystem.ViewModels;

public partial class ShellViewModel : ObservableRecipient
{
    [ObservableProperty]
    private bool isBackEnabled;

    [ObservableProperty]
    private object? selected;

    [ObservableProperty]
    private int unreadNotificationCount;

    [ObservableProperty]
    private bool isNotificationListEmpty;

    [ObservableProperty]
    private bool isUnreadNotificationListEmpty;

    [ObservableProperty]
    private bool isSelectedUnreadFilter;

    [ObservableProperty]
    private bool isUserLoggedIn;

    [ObservableProperty]
    private bool isLoading;

    public INavigationService NavigationService { get; }
    public INavigationViewService NavigationViewService { get; }
    public INotificationService NotificationService { get; }
    public ObservableCollection<Notification> Notifications { get; set; } = new();

    private int _currentPage = 1;
    private const int PageSize = 5;

    public ShellViewModel(INavigationService navigationService, INavigationViewService navigationViewService, INotificationService notificationService)
    {
        NavigationService = navigationService;
        NavigationService.Navigated += OnNavigated;
        NavigationViewService = navigationViewService;
        NotificationService = notificationService;

        IsBackEnabled = NavigationService.CanGoBack;

        // Initialize filter
        IsSelectedUnreadFilter = false;

        // Subscribe to the OnNotificationListChanged event
        NotificationService.NotificationListChanged += OnNotificationListChanged;
    }

    private void OnNavigated(object sender, NavigationEventArgs e)
    {
        IsBackEnabled = NavigationService.CanGoBack;

        if (e.SourcePageType == typeof(SettingsPage))
        {
            Selected = NavigationViewService.SettingsItem;
            return;
        }

        var selectedItem = NavigationViewService.GetSelectedItem(e.SourcePageType);
        if (selectedItem != null)
        {
            Selected = selectedItem;
        }

        OnNotificationListChanged();
        CheckUserLoggedIn();
    }

    private void CheckUserLoggedIn()
    {
        IsUserLoggedIn = LoginViewModel.CurrentUser != null;
    }

    // Do nothing if the filter is not changed
    partial void OnIsSelectedUnreadFilterChanged(bool oldValue, bool newValue)
    {
        if (oldValue == newValue) return;
        _ = RefreshNotificationsAsync();
    }

    public void OnNotificationListChanged(int eventParameter = -1)
    {
        IsNotificationListEmpty = Notifications.Count == 0;

        if (eventParameter == -1)
        {
            IsUnreadNotificationListEmpty = NotificationService.UnreadNotificationCount == 0;
            UnreadNotificationCount = NotificationService.UnreadNotificationCount;
        }
        else
        {
            IsUnreadNotificationListEmpty = eventParameter == 0;
            UnreadNotificationCount = eventParameter;
        }
    }

    public async Task LoadNotificationsAsync()
    {
        await NotificationService.InitializeCacheAsync();
        await GetNextNotificationDataPage();
        OnNotificationListChanged();
    }

    public async Task GetNextNotificationDataPage()
    {
        if (IsLoading) return;

        try
        {
            // Begin loading
            IsLoading = true;

            // Load next page
            var pagedItems = await NotificationService.GetPagedNotificationsAsync(_currentPage, PageSize, unreadOnly: IsSelectedUnreadFilter);

            foreach (var notification in pagedItems)
            {
                Notifications.Add(notification);
            }

            _currentPage++;
        }
        finally
        {
            // End loading
            IsLoading = false;
        }
    }

    public async Task MarkAsRead(Notification notification)
    {
        await NotificationService.MarkAsReadAsync(notification);
        OnNotificationListChanged();
    }

    public async Task RefreshNotificationsAsync()
    {
        _currentPage = 1;

        Notifications.Clear();
        await GetNextNotificationDataPage();
        OnNotificationListChanged();
    }

    public async Task ResetUserNotificationsAsync()
    {
        await NotificationService.ResetUserNotificationsAsync();
        Notifications.Clear();
        OnNotificationListChanged();
    }
}
