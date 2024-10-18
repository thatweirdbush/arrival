using System.Security.Cryptography;
using System.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.DataProtection;
using Windows.Storage;
using BookingManagementSystem.ViewModels;
using BookingManagementSystem.Core.Models;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Media.Imaging;
using Windows.Globalization.DateTimeFormatting;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BookingManagementSystem.Views;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class HomePage : Page
{
    public HomeViewModel ViewModel
    {
        get;
    }

    public HomePage()
    {
        ViewModel = App.GetService<HomeViewModel>();
        InitializeComponent();
    }

    private void btnToggleSwitchWrapper_Click(object sender, RoutedEventArgs e)
    {
        // Toggle the switch
        ToggleSwitchDisplayTax.IsOn = !ToggleSwitchDisplayTax.IsOn;
    }

    private void btnCheckInCalendarWrapper_Click(object sender, RoutedEventArgs e)
    {
        // Show the calendar flyout
        CheckInCalendar.IsCalendarOpen = true;
    }

    private void btnCheckOutCalendarWrapper_Click(object sender, RoutedEventArgs e)
    {
        // Show the calendar flyout
        CheckOutCalendar.IsCalendarOpen = true;
    }

    private void btnSearchDestinationWrapper_Click(object sender, RoutedEventArgs e)
    {
        // Focus on TextBox
        TextBoxSearchDestination.Focus(FocusState.Programmatic);
    }

    private void btnFavourite_Click(object sender, RoutedEventArgs e)
    {
        // Toggle the favourite button
        // Change the image source to the filled heart icon
        if ((sender as FrameworkElement).DataContext is Smartphone smartphone)
        {
            smartphone.IsFavourite = !smartphone.IsFavourite;
        }
    }
}
