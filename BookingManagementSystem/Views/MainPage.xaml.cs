using Microsoft.UI.Xaml;
using System.Collections.ObjectModel;
using Microsoft.UI.Xaml.Controls;
using WinRT.Interop;
using Microsoft.UI.Windowing;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Media.Animation;
using BookingManagementSystem.ViewModels;
using Microsoft.UI.Xaml.Media.Imaging;
using BookingManagementSystem.Core.Models;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI;
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
