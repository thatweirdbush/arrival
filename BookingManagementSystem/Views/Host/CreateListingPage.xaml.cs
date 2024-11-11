﻿using System.ComponentModel;
using BookingManagementSystem.Contracts.Services;
using BookingManagementSystem.ViewModels;
using BookingManagementSystem.Views.Host.CreateListingSteps;
using Microsoft.UI.Xaml.Controls;

namespace BookingManagementSystem.Views.Host;

public sealed partial class CreateListingPage : Page
{
    public CreateListingViewModel ViewModel
    {
        get;
    }

    public CreateListingPage()
    {
        ViewModel = App.GetService<CreateListingViewModel>();
        InitializeComponent();

        // Set up DataContext for binding from XAML to easily access ViewModel
        DataContext = ViewModel;

        // Subscribe to property change notifications
        ViewModel.PropertyChanged += ViewModel_PropertyChanged;

        // Set up initial content
        ContentFrame.Navigate(typeof(AboutYourPlacePage));
    }

    private void ViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ViewModel.CurrentStage))
        {
            ContentFrame.Navigate(Type.GetType($"BookingManagementSystem.Views.Host.CreateListingSteps.{ViewModel.CurrentStage}"));
        }
    }

    private void SaveAndExitButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        // Display dialog to confirm saving and exiting
        var dialog = new ContentDialog
        {
            XamlRoot = Content.XamlRoot,
            Title = "Save and Exit",
            Content = "Are you sure you want to save and exit?",
            PrimaryButtonText = "Save and Exit",
            CloseButtonText = "Cancel"
        };

        dialog.PrimaryButtonClick += (dialogSender, dialogArgs) =>
        {
            // Save listing
            //await ViewModel.SaveListingAsync();

            // Return to Listings page using BackTrack
            App.GetService<INavigationService>()?.Frame?.Navigate(typeof(ListingPage));
        };

        _ = dialog.ShowAsync();
    }

    private async void QuestionButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        // Navigate to Help page
        await Windows.System.Launcher.LaunchUriAsync(new Uri("https://example.com"));
    }
}