using BookingManagementSystem.Core.Contracts.Repositories;
using BookingManagementSystem.Core.Models;
using BookingManagementSystem.ViewModels.Host.CreateListingSteps;

using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Windows.System;
using Windows.UI.Core;

namespace BookingManagementSystem.Views.Host.CreateListingSteps;

public sealed partial class FloorPlanPage : Page
{
    public FloorPlanViewModel ViewModel
    {
        get;
    }

    public FloorPlanPage()
    {
        ViewModel = App.GetService<FloorPlanViewModel>();
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        if (e.Parameter is Property property)
        {
            ViewModel.PropertyOnCreating = property;
            ViewModel.InitializePropertyFloorPlans();
        }
        base.OnNavigatedTo(e);
    }

    private static bool ParseDigitKeyDown(Microsoft.UI.Xaml.Input.KeyRoutedEventArgs e)
    {
        // Get current state of the Shift key
        var shiftState = Microsoft.UI.Input.InputKeyboardSource.GetKeyStateForCurrentThread(VirtualKey.Shift);
        var isShiftPressed = (shiftState & CoreVirtualKeyStates.Down) == CoreVirtualKeyStates.Down;

        // Check if the key pressed is not a number, backspace or navigation key
        if (!((char.IsDigit((char)e.Key) && !isShiftPressed) ||
            e.Key == VirtualKey.Back ||
            e.Key == VirtualKey.Delete ||
            e.Key == VirtualKey.Tab ||
            e.Key == VirtualKey.Left ||
            e.Key == VirtualKey.Right ||
            e.Key == VirtualKey.Up ||
            e.Key == VirtualKey.Down ||
            e.Key == VirtualKey.NumberPad0 ||
            e.Key == VirtualKey.NumberPad1 ||
            e.Key == VirtualKey.NumberPad2 ||
            e.Key == VirtualKey.NumberPad3 ||
            e.Key == VirtualKey.NumberPad4 ||
            e.Key == VirtualKey.NumberPad5 ||
            e.Key == VirtualKey.NumberPad6 ||
            e.Key == VirtualKey.NumberPad7 ||
            e.Key == VirtualKey.NumberPad8 ||
            e.Key == VirtualKey.NumberPad9))
        {
            e.Handled = true; // Block all non-numeric keys
            return false;
        }
        return true;
    }

    private void BedroomsNumberBox_PreviewKeyDown(object sender, Microsoft.UI.Xaml.Input.KeyRoutedEventArgs e)
    {
        ParseDigitKeyDown(e);
    }

    private void BathroomsNumberBox_PreviewKeyDown(object sender, Microsoft.UI.Xaml.Input.KeyRoutedEventArgs e)
    {
        ParseDigitKeyDown(e);
    }

    private void BedsNumberBox_PreviewKeyDown(object sender, Microsoft.UI.Xaml.Input.KeyRoutedEventArgs e)
    {
        ParseDigitKeyDown(e);
    }

    private void GuestsNumberBox_PreviewKeyDown(object sender, Microsoft.UI.Xaml.Input.KeyRoutedEventArgs e)
    {
        ParseDigitKeyDown(e);
    }

    private void BedroomsNumberBox_LostFocus(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        // Update floor plan if a digit key is pressed
        var BedroomPlan = ViewModel.PropertyOnCreating?.Amenities.FirstOrDefault(x => x.Name == "Bedroom");
        if (BedroomPlan != null && ViewModel.BedroomPlan != null)
        {
            BedroomPlan.Quantity = ViewModel.BedroomPlan.Quantity;
        }
    }

    private void BedsNumberBox_LostFocus(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        // Update floor plan if a digit key is pressed
        var BedPlan = ViewModel.PropertyOnCreating?.Amenities.FirstOrDefault(x => x.Name == "Bed");
        if (BedPlan != null && ViewModel.BedPlan != null)
        {
            BedPlan.Quantity = ViewModel.BedPlan.Quantity;
        }
    }

    private void BathroomsNumberBox_LostFocus(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        // Update floor plan if a digit key is pressed
        var BathroomPlan = ViewModel.PropertyOnCreating?.Amenities.FirstOrDefault(x => x.Name == "Bathroom");
        if (BathroomPlan != null && ViewModel.BathoomPlan != null)
        {
            BathroomPlan.Quantity = ViewModel.BathoomPlan.Quantity;
        }
    }
}
