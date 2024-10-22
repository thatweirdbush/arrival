using System;
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

        // Scroll to top when navigating to this page
        ContentScrollView.ScrollTo(0, 0);

        this.RegisterElementForConnectedAnimation("animationKeyContentGrid", itemHero);

        // Always show the Smartphone even if the InfoBar is closed
        infSmartphone.IsOpen = true;
        infSmartphone.Message = ViewModel.Item?.ToString() ?? "No item available";

        // Set up initial map location
        BasicGeoposition centerPosition = new BasicGeoposition { Latitude = ViewModel.Item.Latitude, Longitude = ViewModel.Item.Longitude };
        Geopoint centerPoint = new Geopoint(centerPosition);

        MainMap.Center = centerPoint;

        var myLandmarks = new List<MapElement>();
        BasicGeoposition position = new BasicGeoposition { Latitude = ViewModel.Item.Latitude, Longitude = ViewModel.Item.Longitude };
        Geopoint point = new Geopoint(position);

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
        if ((sender as FrameworkElement).DataContext is Property property)
        {
            property.IsFavourite = !property.IsFavourite;
        }
    }
}
