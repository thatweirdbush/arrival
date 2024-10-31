﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using BookingManagementSystem.ViewModels;
using BookingManagementSystem.Contracts.Services;
using CommunityToolkit.WinUI.UI.Animations;
using Windows.Devices.Geolocation;
using BookingManagementSystem.Core.Models;
using BookingManagementSystem.Views.Forms;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BookingManagementSystem.Views;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class RentalDetailPage : Page
{
    public RentalDetailViewModel ViewModel
    {
        get;
    }

    public RentalDetailPage()
    {
        ViewModel = App.GetService<RentalDetailViewModel>();
        this.InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);
        this.RegisterElementForConnectedAnimation("animationKeyContentGrid", itemHero);

        // Scroll to top when navigating to this page
        ContentScrollView.ScrollTo(0, 0);

        // Always show the Smartphone even if the InfoBar is closed
        infSmartphone.IsOpen = true;
        infSmartphone.Message = ViewModel.Item?.ToString() ?? "No item available";

        // Set up initial map location
        var centerPosition = new BasicGeoposition { Latitude = ViewModel.Item.Latitude, Longitude = ViewModel.Item.Longitude };
        var centerPoint = new Geopoint(centerPosition);

        MainMap.Center = centerPoint;

        var myLandmarks = new List<MapElement>();
        var position = new BasicGeoposition { Latitude = ViewModel.Item.Latitude, Longitude = ViewModel.Item.Longitude };
        var point = new Geopoint(position);

        var icon = new MapIcon
        {
            Location = point,
        };

        myLandmarks.Add(icon);

        var LandmarksLayer = new MapElementsLayer
        {
            MapElements = myLandmarks
        };

        MainMap.Layers.Add(LandmarksLayer);
    }

    protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
    {
        base.OnNavigatingFrom(e);
        if (e.NavigationMode == NavigationMode.Back)
        {
            var navigationService = App.GetService<INavigationService>();

            if (ViewModel.Item != null)
            {
                navigationService.SetListDataItemForNextConnectedAnimation(ViewModel.Item);
            }
        }
    }

    private void btnClearDates_Click(object sender, RoutedEventArgs e)
    {
        CalendarView.SelectedDates.Clear();
    }

    private void btnFavourite_Click(object sender, RoutedEventArgs e)
    {
        // Toggle the favourite button
        // Change the image source to the filled heart icon
        if (sender is FrameworkElement frameworkElement
            && frameworkElement.DataContext is Property property)
        {
            property.IsFavourite = !property.IsFavourite;
        }
    }

    private async void btnReport_Click(object sender, RoutedEventArgs e)
    {
        // Create an instance of BadReportDialog
        var reportDialog = new BadReportDialog();

        // XamlRoot must be set in the case of a ContentDialog running in a Desktop app
        reportDialog.XamlRoot = XamlRoot;
        var result = await reportDialog.ShowAsync();

        // Display the form as a dialog box
        if (result == ContentDialogResult.Primary)
        {
            // Get data from BadReportDialog and process the report
            var reportReason = reportDialog.ReportReason;
            var description = reportDialog.Description;
            var entityType = reportDialog.Type;

            // Create a new BadReport object
            var badReport = new BadReport
            {
                UserId = 1,  // Hardcoded user id for now
                ReportReason = reportReason,
                Description = description,
                EntityType = entityType,
                EntityId = ViewModel.Item?.Id ?? 0,
            };

            // Show confirmation after sending report
            _ = new ContentDialog
            {
                XamlRoot = XamlRoot,
                Title = "Report result",
                Content = $"Thank you for reporting this item.\n" +
                $"We will review this and inform you as soon as possible!\n\n" +
                $"Reason: {badReport.ReportReason} \n" +
                $"Description: {badReport.Description} \n" +
                $"Entity Type: {badReport.EntityType} \n" +
                $"Entity Id: {badReport.EntityId} \n" +
                $"Report Date: {badReport.ReportDate}",
                CloseButtonText = "Ok"
            }.ShowAsync();
        }
    }

    private void btnSubmitQustion_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(tbAskPropertyQuestion.Text))
        {
            _ = new ContentDialog
            {
                XamlRoot = XamlRoot,
                Title = "Field is required",
                Content = "Please enter a question before submitting!",
                CloseButtonText = "Ok"
            }.ShowAsync();
            return;
        }

        if (ViewModel.Item != null)
        {
            // Add the question to the QnA list
            ViewModel.QnAs.Insert(0, new QnA
            {
                Question = tbAskPropertyQuestion.Text,
                PropertyId = ViewModel.Item.Id,
            });

            // Show the successful dialog
            _ = new ContentDialog
            {
                XamlRoot = XamlRoot,
                Title = "Question submission result",
                Content = "Your question has been submitted successfully!",
                CloseButtonText = "Ok"
            }.ShowAsync();
        }

        // Clear the question text box
        tbAskPropertyQuestion.Text = "";
    }

    private void btnBookNow_Click(object sender, RoutedEventArgs e)
    {
        Frame.Navigate(typeof(PaymentPage));
    }
}
