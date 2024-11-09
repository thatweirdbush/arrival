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

        // Set up DataContext for binding from XAML to easily access ViewModel
        DataContext = ViewModel;
    }
}
