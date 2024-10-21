using BookingManagementSystem.ViewModels;

using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;

namespace BookingManagementSystem.Views;

public sealed partial class HostPage : Page
{
    public HostViewModel ViewModel
    {
        get;
    }

    public HostPage()
    {
        ViewModel = App.GetService<HostViewModel>();
        InitializeComponent();
    }

    int previousSelectedIndex = 0;

    private void SelectorBar2_SelectionChanged
                 (SelectorBar sender, SelectorBarSelectionChangedEventArgs args)
    {
        var selectedItem = sender.SelectedItem;
        var currentSelectedIndex = sender.Items.IndexOf(selectedItem);
        System.Type pageType;

        switch (currentSelectedIndex)
        {
            case 0:
                pageType = typeof(HomePage);
                break;
            case 1:
                pageType = typeof(LoginPage);
                break;
            case 2:
                pageType = typeof(MainPage);
                break;
            case 3:
                pageType = typeof(MapPage);
                break;
            case 4:
                return;
            default:
                pageType = typeof(HostPage);
                break;
        }

        var slideNavigationTransitionEffect =
                currentSelectedIndex - previousSelectedIndex > 0 ?
                    SlideNavigationTransitionEffect.FromRight :
                    SlideNavigationTransitionEffect.FromLeft;

        ContentFrame.Navigate(pageType, null, new SlideNavigationTransitionInfo()
        {
            Effect = slideNavigationTransitionEffect
        });

        previousSelectedIndex = currentSelectedIndex;
    }
}
