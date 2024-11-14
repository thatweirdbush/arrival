using System.Globalization;
using BookingManagementSystem.ViewModels.Host.CreateListingSteps;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Windows.System;
using BookingManagementSystem.Helpers;
using Microsoft.UI.Xaml.Navigation;

namespace BookingManagementSystem.Views.Host.CreateListingSteps;

public sealed partial class SetPricePage : Page
{
    public SetPriceViewModel? ViewModel
    {
        get; set;
    }

    public SetPricePage()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        if (e.Parameter is SetPriceViewModel viewModel)
        {
            ViewModel = viewModel;
        }
        base.OnNavigatedTo(e);
    }

    private void LearnMoreHyperLink_Click(Microsoft.UI.Xaml.Documents.Hyperlink sender, Microsoft.UI.Xaml.Documents.HyperlinkClickEventArgs args)
    {
        FlyoutBase.ShowAttachedFlyout(LearnMoreTextBlock);
    }

    private void PriceTextBox_PreviewKeyDown(object sender, Microsoft.UI.Xaml.Input.KeyRoutedEventArgs e)
    {
        // Check if the key pressed is not a number, backspace or navigation key
        if (!(char.IsDigit((char)e.Key) ||
                  e.Key == VirtualKey.Back ||
                  e.Key == VirtualKey.Tab ||
                  e.Key == VirtualKey.Left ||
                  e.Key == VirtualKey.Right ||
                  e.Key == VirtualKey.NumberPad0 ||
                  e.Key == VirtualKey.NumberPad1 ||
                  e.Key == VirtualKey.NumberPad2 ||
                  e.Key == VirtualKey.NumberPad3 ||
                  e.Key == VirtualKey.NumberPad4 ||
                  e.Key == VirtualKey.NumberPad5 ||
                  e.Key == VirtualKey.NumberPad6 ||
                  e.Key == VirtualKey.NumberPad7 ||
                  e.Key == VirtualKey.NumberPad8 ||
                  e.Key == VirtualKey.NumberPad9 ||
                  e.Key == VirtualKey.Decimal))     // Include the dot (.) on the NumPad
        {
            e.Handled = true; // Block all non-numeric keys
        }
    }

    private void GuestFeeInfoExpander_Tapped(object sender, Microsoft.UI.Xaml.Input.TappedRoutedEventArgs e)
    {
        // Toggle off the IsExpanded property of the remaining Expander
        HostFeeInfoExpander.IsExpanded = false;
    }

    private void HostFeeInfoExpander_Tapped(object sender, Microsoft.UI.Xaml.Input.TappedRoutedEventArgs e)
    {
        // Toggle off the IsExpanded property of the remaining Expander
        GuestFeeInfoExpander.IsExpanded = false;
    }
}
