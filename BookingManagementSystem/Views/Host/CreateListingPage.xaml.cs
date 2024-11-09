using System.ComponentModel;
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
}
