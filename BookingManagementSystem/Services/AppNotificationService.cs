﻿using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.ViewModels;
using BookingManagementSystem.ViewModels.Client;

using System.Collections.Specialized;
using System.Web;

using Microsoft.Windows.AppNotifications;

namespace BookingManagementSystem.Services;

public class AppNotificationService : IAppNotificationService
{
    private readonly INavigationService _navigationService;

    public AppNotificationService(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    ~AppNotificationService()
    {
        Unregister();
    }

    public void Initialize()
    {
        AppNotificationManager.Default.NotificationInvoked += OnNotificationInvoked;
        AppNotificationManager.Default.Register();
    }

    public void OnNotificationInvoked(AppNotificationManager sender, AppNotificationActivatedEventArgs args)
    {
        var arguments = ParseArguments(args.Argument);

        switch (arguments["action"])
        {
            case "see-details":
                App.MainWindow.DispatcherQueue.TryEnqueue(() =>
                {
                    // Navigate to a specific page based on the notification arguments.
                    _navigationService.NavigateTo(typeof(NotificationViewModel).FullName!);

                    // Show a message dialog based on the notification arguments.
                    App.MainWindow.ShowMessageDialogAsync(
                        "You can always find more recent notifications in the Notification page",
                        "Quick Tips");

                    App.MainWindow.BringToFront();
                });
                break;

            case "dismiss":
                // Do nothing
                break;

            default:
                if (arguments["id"] is string Id && int.TryParse(Id, out var IntegerId))
                {
                    App.MainWindow.DispatcherQueue.TryEnqueue(() =>
                    {
                        // Navigate to a specific page based on the notification arguments.
                        _navigationService.NavigateTo(typeof(RentalDetailViewModel).FullName!, IntegerId);
                    });
                }
                break;
        }
    }

    public bool Show(string payload)
    {
        var appNotification = new AppNotification(payload);

        AppNotificationManager.Default.Show(appNotification);

        return appNotification.Id != 0;
    }

    public bool ShowNotification(string title, string message, string? imageUri = null, List<(string Content, string Arguments)>? buttons = null)
    {
        // Start XML payload
        var toastXml = $@"
        <toast>
            <visual>
                <binding template='ToastGeneric'>
                    <text>{title}</text>
                    <text>{message}</text>";

        // Add image to XML payload if any
        if (!string.IsNullOrWhiteSpace(imageUri))
        {
            toastXml += $@"
                    <image src='{imageUri}'/>";
        }

        toastXml += @"
                </binding>
            </visual>";

        // Add buttons to XML payload if any
        if (buttons != null && buttons.Count != 0)
        {
            toastXml += "<actions>";
            foreach (var (Content, Arguments) in buttons)
            {
                toastXml += $@"
                <action
                    content='{Content}'
                    arguments='{Arguments}'
                    activationType='foreground'/>";
            }
            toastXml += "</actions>";
        }
        // End XML payload
        toastXml += "</toast>";

        // Díplay notification
        var appNotification = new AppNotification(toastXml);
        AppNotificationManager.Default.Show(appNotification);

        return appNotification.Id != 0;
    }


    public NameValueCollection ParseArguments(string arguments)
    {
        return HttpUtility.ParseQueryString(arguments);
    }

    public void Unregister()
    {
        AppNotificationManager.Default.Unregister();
    }
}
