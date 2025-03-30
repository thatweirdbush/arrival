using BookingManagementSystem.ViewModels;
using BookingManagementSystem.Core.Models;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace BookingManagementSystem.Views;

public sealed partial class MainPage : Page
{
    public MainViewModel ViewModel
    {
        get;
    }

    public MainPage()
    {
        ViewModel = App.GetService<MainViewModel>();
        InitializeComponent();

        // Prepare a random item when the window is loaded
        updateSmartphoneView();
    }

    private void updateSmartphoneView()
    {
        // Get next Smartphone from ViewModel
        Property property = ViewModel.GetNextItem();

        // Always show the Smartphone even if the InfoBar is closed
        infSmartphone.IsOpen = true;
        infSmartphone.Message = property.ToString();
    }

    private void btnClearDates_Click(object sender, RoutedEventArgs e)
    {
        CalendarView.SelectedDates.Clear();
    }
}
