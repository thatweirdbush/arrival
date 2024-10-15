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

        // Prepare a random Smartphone when the window is loaded
        updateSmartphoneView();
    }

    private async void updateSmartphoneView()
    {
        // Get next Smartphone from ViewModel
        Smartphone SelectedSmartphone = ViewModel.GetNextSmartphone();

        // Always show the Smartphone even if the InfoBar is closed
        infSmartphone.IsOpen = true;
        infSmartphone.Message = SelectedSmartphone.ToString();

        // Set blur effect animation when changing the Smartphone
        await AnimateImageTransitionAsync(imgSmartphone, SelectedSmartphone.ImagePath);
    }

    private async Task AnimateImageTransitionAsync(Image img, string newImagePath)
    {
        // Animation fade out
        var fadeOutAnimation = new DoubleAnimation
        {
            From = 1.0,
            To = 0.0,
            Duration = new Duration(TimeSpan.FromSeconds(0.3))
        };
        Storyboard fadeOutStoryboard = new Storyboard();
        fadeOutStoryboard.Children.Add(fadeOutAnimation);
        Storyboard.SetTarget(fadeOutAnimation, img);
        Storyboard.SetTargetProperty(fadeOutAnimation, "Opacity");

        fadeOutStoryboard.Begin();
        await Task.Delay(300);

        // Now change the image
        img.Source = new BitmapImage(new Uri(newImagePath));

        // Animation fade in
        var fadeInAnimation = new DoubleAnimation
        {
            From = 0.0,
            To = 1.0,
            Duration = new Duration(TimeSpan.FromSeconds(0.3))
        };
        Storyboard fadeInStoryboard = new Storyboard();
        fadeInStoryboard.Children.Add(fadeInAnimation);
        Storyboard.SetTarget(fadeInAnimation, img);
        Storyboard.SetTargetProperty(fadeInAnimation, "Opacity");

        fadeInStoryboard.Begin();
    }

    private void btnGetSmartphone_Click(object sender, RoutedEventArgs e)
    {
        updateSmartphoneView();
    }

    private void infSmartphone_Click(object sender, RoutedEventArgs e)
    {
        updateSmartphoneView();
    }
}
