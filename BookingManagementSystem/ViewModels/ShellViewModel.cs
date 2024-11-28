using CommunityToolkit.Mvvm.ComponentModel;

using Microsoft.UI.Xaml.Navigation;

using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.Views;
using BookingManagementSystem.Core.Contracts.Repositories;
using BookingManagementSystem.Core.Models;
using System.Collections.ObjectModel;

namespace BookingManagementSystem.ViewModels;

public partial class ShellViewModel : ObservableRecipient
{
    [ObservableProperty]
    private bool isBackEnabled;

    [ObservableProperty]
    private object? selected;
    public INavigationService NavigationService { get; }
    public INavigationViewService NavigationViewService { get; }
    public IRepository<Notification> NotificationRepository { get; }
    public ObservableCollection<Notification> Notifications { get; } = [];

    public ShellViewModel(INavigationService navigationService, INavigationViewService navigationViewService, IRepository<Notification> notificationRepository)
    {
        NavigationService = navigationService;
        NavigationService.Navigated += OnNavigated;
        NavigationViewService = navigationViewService;
        NotificationRepository = notificationRepository;
    }

    private async void OnNavigated(object sender, NavigationEventArgs e)
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

        // Get notification data list
        await LoadNotificationData();
    }

    public async Task LoadNotificationData(bool isUnreadFilter = false)
    {
        // Clear notification data list first
        Notifications.Clear();

        // Get notification data list
        var notifications = await NotificationRepository.GetAllAsync();

        if (isUnreadFilter)
        {
            notifications = notifications.Where(n => !n.IsRead).ToList();
        }
        foreach (var notification in notifications)
        {
            Notifications.Add(notification);
        }
    }
}
