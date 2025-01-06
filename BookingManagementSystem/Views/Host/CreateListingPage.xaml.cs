using BookingManagementSystem.ViewModels;
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

        // Set up ViewModel's ContentFrame
        ViewModel.ContentFrame = ContentFrame;
    }

    private async void QuestionButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        // Navigate to Help page
        await Windows.System.Launcher.LaunchUriAsync(new Uri("https://example.com"));
    }
}
